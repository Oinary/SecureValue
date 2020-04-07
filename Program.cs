using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new SecureValueTest();
        }
    }

    class SecureValueTest
    {
        public SecureValueTest()
        {
            SecFloat pos = new SecFloat();
            pos._Value = 4.545f;
            Console.WriteLine("Restored value is " + pos._Value);

            Console.WriteLine();

            SecInt HP = new SecInt();
            HP._Value = 114514;
            Console.WriteLine("Restored value is " + HP._Value);

        }
    }

    interface ISecureValueObject<T>
    {
        T _Value { get; set; }
    }

    public class SecInt : ISecureValueObject<int>
    {
        private int Value;
        public int _Value
        {
            get
            {
                return Value ^ Seed;
            }
            set
            {
                CalculatingSeed();
                Value = value ^ Seed;
                Console.WriteLine("The value that you set is " + value + "\n" + "Seed is " + Seed + "\n" + "Protected value is " + Value);
            }
        }
        private int Seed;
        private void CalculatingSeed()
        {
            var r = new Random();
            Seed = r.Next();
        }
    }

    public class SecFloat : ISecureValueObject<float>
    {
        private int SecValue;
        private int Seed;
        public unsafe float _Value
        {
            get
            {
                var result = SecValue ^ Seed;
                return BitConverter.ToSingle(BitConverter.GetBytes(result), 0);
            }
            set
            {
                CalculatingSeed();
                int v = *((int*)&value);
                SecValue = v ^ Seed;
                Console.WriteLine("The value that you set is " + value + "\n" + "Seed is " + Seed + "\n" + "Protected value is " + BitConverter.ToSingle(BitConverter.GetBytes(SecValue), 0));

            }
        }
        private void CalculatingSeed()
        {
            var r = new Random();
            Seed = r.Next();
        }
    }
}

