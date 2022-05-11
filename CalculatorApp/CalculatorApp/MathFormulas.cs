using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorApp
{
    public class MathFormulas : IEnumerable<object[]>
    {
        public bool IsEven(int number) => number % 2 == 0;

        public int Diff(int x, int y) => y - x;

        public int Add(int x, int y) => x + y;

        public int Sum(params int[] values)
        {
            int sum = 0;

            foreach (int i in values)
            {
                sum += i;
            }

            return sum;
        }

        public double Average(params int[] values)
        {
            double sum = 0;
            foreach (var item in values)
            {
                sum += item;
            }

            return sum / values.Length;
        }

        public static IEnumerable<object[]> Data => new List<object[]>
        {
            new object[] { 1, 2, 3 },
            new object[] { 4, 5, 9 },
            new object[] { 7, 8, 15 },
            new object[] { 9, 10, 19 },
            new object[] { 11, 12, 23 }
        };


        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { 1, 2, 3 };
            yield return new object[] { -4, -6, -10 };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
