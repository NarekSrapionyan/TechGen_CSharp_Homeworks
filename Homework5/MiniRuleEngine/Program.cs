using System;

namespace MiniRuleEngine
{
    public delegate void RuleCheck(IEntity entity);

    public interface IEntity
    {
        int Id { get; }
        string EntityType { get; }
    }

    public class UserEntity : IEntity
    {
        public int Id { get; }
        public string EntityType => "User";
        
        public int Age { get; }
        public string Email { get; }

        public UserEntity(int id, int age, string email)
        {
            Id = id;
            Age = age;
            Email = email;
        }
    }

    public class OrderEntity : IEntity
    {
        public int Id { get; }
        public string EntityType => "Order";
        
        public decimal TotalAmount { get; }

        public OrderEntity(int id, decimal totalAmount)
        {
            Id = id;
            TotalAmount = totalAmount;
        }
    }

    public class Rule
    {
        public string Name { get; }
        public string TargetEntityType { get; }
        public RuleCheck Check { get; }

        public Rule(string name, string targetEntityType, RuleCheck check)
        {
            Name = name;
            TargetEntityType = targetEntityType;
            Check = check;
        }

        public bool AppliesTo(IEntity entity)
        {
            return TargetEntityType == entity.EntityType;
        }
    }

    public class RuleViolationException : Exception
    {
        public string RuleName { get; }

        public RuleViolationException(string ruleName, string message) : base(message)
        {
            RuleName = ruleName;
        }
    }

    public class EntityValidationException : Exception
    {
        public IEntity Entity { get; }
        public RuleViolationException[] Violations { get; }

        public EntityValidationException(IEntity entity, RuleViolationException[] violations) 
            : base($"{entity.EntityType} #{entity.Id} has {violations.Length} validation error(s).")
        {
            Entity = entity;
            Violations = violations;
        }
    }

    public class RuleEngine
    {
        private Rule[] _rules;
        private int _count;

        public RuleEngine(int initialCapacity = 4)
        {
            if (initialCapacity <= 0) initialCapacity = 4;
            _rules = new Rule[initialCapacity];
            _count = 0;
        }

        public void AddRule(Rule rule)
        {
            if (_count == _rules.Length)
            {
                Array.Resize(ref _rules, _rules.Length * 2);
            }
            _rules[_count++] = rule;
        }

        public void ValidateFailFast(IEntity entity)
        {
            for (int i = 0; i < _count; i++)
            {
                Rule rule = _rules[i];
                if (!rule.AppliesTo(entity)) continue;

                try
                {
                    rule.Check(entity);
                }
                catch (RuleViolationException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    throw new RuleViolationException(rule.Name, $"Unexpected rule error: {ex.Message}");
                }
            }
        }

        public void ValidateCollectAll(IEntity entity)
        {
            RuleViolationException[] violations = new RuleViolationException[4];
            int violationCount = 0;

            for (int i = 0; i < _count; i++)
            {
                Rule rule = _rules[i];
                if (!rule.AppliesTo(entity)) continue;

                try
                {
                    rule.Check(entity);
                }
                catch (RuleViolationException rve)
                {
                    AddViolation(ref violations, ref violationCount, rve);
                }
                catch (Exception ex)
                {
                    AddViolation(ref violations, ref violationCount, new RuleViolationException(rule.Name, $"Unexpected rule error: {ex.Message}"));
                }
            }

            if (violationCount > 0)
            {
                Array.Resize(ref violations, violationCount);
                throw new EntityValidationException(entity, violations);
            }
        }

        private void AddViolation(ref RuleViolationException[] array, ref int count, RuleViolationException ex)
        {
            if (count == array.Length)
            {
                Array.Resize(ref array, array.Length * 2);
            }
            array[count++] = ex;
        }
    }

    public static class EntityValidationExtensions
    {
        public static void ValidateFailFast(this IEntity entity, RuleEngine engine)
        {
            engine.ValidateFailFast(entity);
        }

        public static void ValidateCollectAll(this IEntity entity, RuleEngine engine)
        {
            engine.ValidateCollectAll(entity);
        }
    }

    class Program
    {
        static void Main()
        {
            RuleEngine engine = new RuleEngine();
            RegisterRules(engine);

            IEntity validUser = new UserEntity(1, 25, "test@example.com");
            IEntity invalidUser = new UserEntity(2, 16, "invalid_email");
            
            IEntity validOrder = new OrderEntity(100, 50.0m);
            IEntity invalidOrder = new OrderEntity(101, -10.0m);

            IEntity[] entitiesToTest = { validUser, invalidUser, validOrder, invalidOrder };

            Console.WriteLine("=== Fail-Fast Mode ===");
            foreach (var entity in entitiesToTest)
            {
                try
                {
                    entity.ValidateFailFast(engine);
                    Console.WriteLine($"{entity.EntityType} #{entity.Id} passed Fail-Fast.");
                }
                catch (RuleViolationException ex)
                {
                    Console.WriteLine($"{entity.EntityType} #{entity.Id} FAILED fast on rule [{ex.RuleName}]: {ex.Message}");
                }
            }
            Console.WriteLine();

            Console.WriteLine("=== Collect-All Mode ===");
            foreach (var entity in entitiesToTest)
            {
                try
                {
                    entity.ValidateCollectAll(engine);
                    Console.WriteLine($"{entity.EntityType} #{entity.Id} passed Collect-All.");
                }
                catch (EntityValidationException ex)
                {
                    Console.WriteLine(ex.Message);
                    foreach (var v in ex.Violations)
                    {
                        Console.WriteLine($"  -> [{v.RuleName}]: {v.Message}");
                    }
                }
            }
        }

        static void RegisterRules(RuleEngine engine)
        {
            engine.AddRule(new Rule("CheckAdult", "User", entity =>
            {
                var user = (UserEntity)entity;
                if (user.Age < 18) throw new RuleViolationException("CheckAdult", "User must be 18 or older.");
            }));

            engine.AddRule(new Rule("CheckEmail", "User", entity =>
            {
                var user = (UserEntity)entity;
                if (!user.Email.Contains("@")) throw new RuleViolationException("CheckEmail", "Invalid email format.");
            }));

            engine.AddRule(new Rule("CheckPositiveAmount", "Order", entity =>
            {
                var order = (OrderEntity)entity;
                if (order.TotalAmount <= 0) throw new RuleViolationException("CheckPositiveAmount", "Order amount must be greater than zero.");
            }));
            
            engine.AddRule(new Rule("CrashTest", "Order", entity =>
            {
                var order = (OrderEntity)entity;
                if (order.TotalAmount < 0) throw new NullReferenceException("Something went terribly wrong while fetching DB rates!");
            }));
        }
    }
}