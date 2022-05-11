using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorApp.Test
{
    public static class Factory
    {
        public static MathFormulas CreateMathFormulas() => new MathFormulas();
    }
}
