using System;
using System.Collections;

namespace JobScheduler
{
    public delegate void JobExecutor(Job job);

    public enum JobStatus
    {
        Pending,
        Running,
        Completed,
        Failed
    }

    public class Job
    {
        public int Id { get; }
        public string Name { get; }
        public JobStatus Status { get; set; }
        public JobExecutor Executor { get; }

        public Job(int id, string name, JobExecutor executor)
        {
            Id = id;
            Name = name;
            Executor = executor;
            Status = JobStatus.Pending;
        }
    }

    public class JobEventArgs : EventArgs
    {
        public Job Job { get; }
        public string EventName { get; }
        public Exception Error { get; }

        public JobEventArgs(Job job, string eventName, Exception error = null)
        {
            Job = job;
            EventName = eventName;
            Error = error;
        }
    }

    public class JobQueue : IEnumerable
    {
        private Job[] _jobs;
        private int _count;

        public int Count => _count;

        public JobQueue(int initialCapacity = 4)
        {
            if (initialCapacity <= 0) initialCapacity = 4;
            _jobs = new Job[initialCapacity];
            _count = 0;
        }

        public void Enqueue(Job job)
        {
            if (_count == _jobs.Length)
            {
                Array.Resize(ref _jobs, _jobs.Length * 2);
            }
            _jobs[_count++] = job;
        }

        public Job GetJob(int index) => _jobs[index];

        public IEnumerator GetEnumerator()
        {
            return new JobQueueEnumerator(this);
        }
    }

    public class JobQueueEnumerator : IEnumerator
    {
        private readonly JobQueue _queue;
        private int _position = -1;

        public JobQueueEnumerator(JobQueue queue)
        {
            _queue = queue;
        }

        public object Current
        {
            get
            {
                if (_position < 0 || _position >= _queue.Count)
                    throw new InvalidOperationException("Enumerator is out of bounds.");
                return _queue.GetJob(_position);
            }
        }

        public bool MoveNext()
        {
            while (++_position < _queue.Count)
            {
                if (_queue.GetJob(_position).Status == JobStatus.Pending)
                {
                    return true;
                }
            }
            return false;
        }

        public void Reset()
        {
            _position = -1;
        }
    }

    public class Scheduler
    {
        private readonly JobQueue _queue;

        public event EventHandler<JobEventArgs> JobStateChanged;

        public Scheduler(JobQueue queue)
        {
            _queue = queue;
        }

        public void ExecuteAll()
        {
            foreach (Job job in _queue)
            {
                job.Status = JobStatus.Running;
                EmitEvent(job, "JobStarted");

                try
                {
                    job.Executor(job);
                    
                    job.Status = JobStatus.Completed;
                    EmitEvent(job, "JobCompleted");
                }
                catch (Exception ex)
                {
                    job.Status = JobStatus.Failed;
                    EmitEvent(job, "JobFailed", ex);
                }
                finally
                {
                    Console.WriteLine($"[Scheduler] Finished processing Job #{job.Id}: {job.Name}\n");
                }
            }
        }

        private void EmitEvent(Job job, string eventName, Exception ex = null)
        {
            JobStateChanged?.Invoke(this, new JobEventArgs(job, eventName, ex));
        }
    }

    public class MonitoringService
    {
        public void Handle(object sender, JobEventArgs e)
        {
            Console.WriteLine($"[Monitor] {e.EventName} | Job {e.Job.Id} ({e.Job.Name}) is now {e.Job.Status}");
        }
    }

    public class LoggerService
    {
        public void Handle(object sender, JobEventArgs e)
        {
            string time = DateTime.Now.ToString("HH:mm:ss");
            string log = $"[{time}] [Log] {e.EventName} - Job {e.Job.Id}";
            if (e.Error != null)
            {
                log += $" | Error: {e.Error.Message}";
            }
            Console.WriteLine(log);
        }
    }

    public class StatisticsService
    {
        private int _started = 0;
        private int _completed = 0;
        private int _failed = 0;

        public void Handle(object sender, JobEventArgs e)
        {
            switch (e.EventName)
            {
                case "JobStarted": _started++; break;
                case "JobCompleted": _completed++; break;
                case "JobFailed": _failed++; break;
            }
        }

        public void Print()
        {
            Console.WriteLine("=== Final Statistics ===");
            Console.WriteLine($"Started:   {_started}");
            Console.WriteLine($"Completed: {_completed}");
            Console.WriteLine($"Failed:    {_failed}");
            Console.WriteLine("========================");
        }
    }

    public static class Executors
    {
        public static void FastExecutor(Job job)
        {
            if (job.Name.IndexOf("fail-fast", StringComparison.OrdinalIgnoreCase) >= 0)
                throw new Exception("Fast failure triggered.");
        }

        public static void SafeExecutor(Job job)
        {
            try
            {
                if (job.Name.IndexOf("fail-safe", StringComparison.OrdinalIgnoreCase) >= 0)
                    throw new InvalidOperationException("Simulated inner crash.");
            }
            catch (Exception ex)
            {
                throw new Exception("SafeExecutor caught an error.", ex);
            }
        }

        public static void RetryExecutor(Job job)
        {
            int maxAttempts = 3;
            for (int attempt = 1; attempt <= maxAttempts; attempt++)
            {
                try
                {
                    if (job.Name.IndexOf("fail-retry", StringComparison.OrdinalIgnoreCase) >= 0)
                        throw new Exception($"Hard fail on attempt {attempt}.");

                    if (attempt < 3)
                        throw new Exception($"Transient network error on attempt {attempt}.");

                    return;
                }
                catch (Exception)
                {
                    if (attempt == maxAttempts) throw;
                }
            }
        }
    }

    class Program
    {
        static void Main()
        {
            JobQueue queue = new JobQueue();
            queue.Enqueue(new Job(1, "Data Sync", Executors.FastExecutor));
            queue.Enqueue(new Job(2, "Cache Clear (fail-fast)", Executors.FastExecutor));
            queue.Enqueue(new Job(3, "Send Emails (fail-safe)", Executors.SafeExecutor));
            queue.Enqueue(new Job(4, "API Call", Executors.RetryExecutor));

            Scheduler scheduler = new Scheduler(queue);

            MonitoringService monitor = new MonitoringService();
            LoggerService logger = new LoggerService();
            StatisticsService stats = new StatisticsService();

            scheduler.JobStateChanged += monitor.Handle;
            scheduler.JobStateChanged += logger.Handle;
            scheduler.JobStateChanged += stats.Handle;

            scheduler.ExecuteAll();
            stats.Print();
        }
    }
}