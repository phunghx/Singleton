using System;

namespace Monostate
{
    public class GiangVien
    {
        private static string name;
        private static int age;
        private static int _id;
        public int ID { get => _id; }
        public string Name
        {
            get => name;
            set => name = value;
        }
        public int Age
        {
            set { age = value; }
            get { return age; }
        }
        public GiangVien()
        {
            _id++;
        }
        public override string ToString()
        {
            return $"{ID}-{Name}:{Age}";
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var gv = new GiangVien();
            gv.Name = "phung";
            gv.Age = 33;
            Console.WriteLine(gv);

            var gv2 = new GiangVien();
            Console.WriteLine(gv2);
        }
    }
}
