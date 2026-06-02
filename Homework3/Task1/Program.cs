namespace Project1;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.Write("Enter a bracket sequence: ");

            var input = Console.ReadLine();

            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Invalid Input");
                continue;
            }

            bool result = BracketValidator.isValid(input);

            if (result)
            {
                Console.WriteLine("Valid bracket sequence");
            }
            else
            {
                Console.WriteLine("Invalid bracket sequence");
            }
        }
    }

    static class BracketValidator
    {
        public static bool isValid(string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;

            int len = input.Length;

            if (len % 2 != 0)
                return false;

            char[] stack = new char[len];
            int top = 0;

            for (int i = 0; i < len; i++)
            {
                char c = input[i];

                if (c == '(' || c == '{' || c == '[')
                {
                    stack[top++] = c;
                    continue;
                }

                if (top == 0)
                    return false;

                if (c != ')' && c != ']' && c != '}')
                    return false;

                char open = stack[--top];

                if (!isPair(open, c))
                    return false;
            }

            return top == 0;
        }

        private static bool isPair(char open, char close)
        {
            return
                (open == '(' && close == ')') ||
                (open == '{' && close == '}') ||
                (open == '[' && close == ']');
        }
    }
}