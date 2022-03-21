using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MoreLinq;


namespace Singleton
{
    public interface IDatabase
    {
        int GetPopulation(string name);
    }
    public class SingletonFileDatabase : IDatabase
    {
        private Dictionary<string, int> capitals;
        private static int instanceCount;
        public static int Count => instanceCount;
        private SingletonFileDatabase()
        {
            capitals = File.ReadAllLines(
                Path.Combine(
                    new FileInfo(typeof(IDatabase).Assembly.Location).DirectoryName, "capitals.txt")
                )
                .Batch(2)
                .ToDictionary(
                   list => list.ElementAt(0).Trim(),
                   list => int.Parse(list.ElementAt(1)));

        }

        public int GetPopulation(string name)
        {
            return this.capitals[name];
        }
        private static Lazy<SingletonFileDatabase> instance = new Lazy<SingletonFileDatabase>(() =>
            {
                //instanceCount++;
                return new SingletonFileDatabase();
            }
            );
        public static IDatabase Instance {
            get {
                instanceCount++;
                return instance.Value; }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var db = SingletonFileDatabase.Instance;
            var city = "Osaka";
            Console.WriteLine($"{city}:{db.GetPopulation(city)}");

            var db2 = SingletonFileDatabase.Instance;
            city = "Mumbai";
            Console.WriteLine($"{city}:{db2.GetPopulation(city)}");
            city = "Mumbai";
            var db3 = SingletonFileDatabase.Instance;
            Console.WriteLine($"{city}:{db3.GetPopulation(city)}");


            Console.WriteLine($"Count: {SingletonFileDatabase.Count}");

        }
    }
}
