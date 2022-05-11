using Xunit;

namespace CalculatorApp.Test
{
    public class MathHelperTest
    {
        [Fact]
        public void IsEvenTest()
        {
            var calculator = new MathFormulas();

            int x = 1;
            int y = 2;

            var xResult = calculator.IsEven(x);
            var yResult = calculator.IsEven(y);

            Assert.False(xResult);
            Assert.True(yResult);
        }

        /// <summary>
        /// Parametriza la prueba unitaria
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="expectedValue"></param>
        [Theory]
        [InlineData(1,2,1)]
        [InlineData(1, 3, 2)]
        public void DiffTest(int x, int y, int expectedValue)
        {
            var calculator = Factory.CreateMathFormulas();

            var result = calculator.Diff(x, y);

            Assert.Equal(expectedValue, result);
        }

        [Theory]
        [InlineData(1,1,2)]
        public void AddTest(int x, int y, int expectedValue)
        {
            var calculator = Factory.CreateMathFormulas();
            var result = calculator.Add(x, y);
            Assert.Equal(expectedValue, result);
        }

        [Theory]
        [InlineData(new int[3] {1,2,3}, 6)]
        [InlineData(new int[3] { -4, -6, -10 }, -20)]
        public void SumTest(int[] values, int extectedValue)
        {
            var calculator = Factory.CreateMathFormulas();
            var result = calculator.Sum(values);
            Assert.Equal(extectedValue, result);
        }

        [Theory]
        [InlineData(new int[3] {2,2,2}, 2)]
        public void AverageTest(int[] values, double expectedValue)
        {
            var calculator = Factory.CreateMathFormulas();
            var result = calculator.Average(values);
            Assert.Equal(expectedValue, result);
        }

        [Theory]
        [MemberData(nameof(MathFormulas.Data), MemberType = typeof(MathFormulas))]
        public void Add_MemberData_Test(int x, int y, int expectedValue)
        {
            var calculator = Factory.CreateMathFormulas();
            var result = calculator.Add(x, y);
            Assert.Equal(expectedValue, result);
        }

        //Para saltarse la prueba:

        //[Theory(Skip = "La razon...")]
        [Theory]
        [ClassData(typeof(MathFormulas))]
        public void Add_ClassData_Test(int x, int y, int expectedValue)
        {
            var calculator = Factory.CreateMathFormulas();
            var result = calculator.Add(x,y);
            Assert.Equal((int)expectedValue, result);
        }
    }
}