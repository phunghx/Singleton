using System;
using System.Threading;
using System.Threading.Tasks;

namespace SingletonThread
{
    public class PerThreadSingleton
    {
        private static ThreadLocal<PerThreadSingleton> threadInstance
            = new ThreadLocal<PerThreadSingleton>(() => new PerThreadSingleton());
        public int Id { set; get; }
        private PerThreadSingleton()
        {
            Id = Thread.CurrentThread.ManagedThreadId;
        }
        public static PerThreadSingleton Instance => threadInstance.Value;
    }
    class Program
    {
        static void Main(string[] args)
        {
            var thread1 = Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"thread 1: {PerThreadSingleton.Instance.Id}");
            });
            var thread2 = Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"thread 2: {PerThreadSingleton.Instance.Id}");
                Console.WriteLine($"thread 2 call again: {PerThreadSingleton.Instance.Id}");
                Console.WriteLine($"thread 2 call again 2: {PerThreadSingleton.Instance.Id}");
            });
            Task.WaitAll(thread1, thread2);
        }
    }
}
