using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam11_Simpson
{
    public class Integral
    {
        public double a;
        public double b;
        public int n; 

        public Integral(double A, double B, int N)
        {
            this.a = A;
            this.b = B;
            this.n = N;
        }

        public double Func(double x)
        {
            return 1 / Math.Sqrt(Math.Pow(1 + x * x, 3));
        }

        public double Simpson_Method(Func<double, double> f)
        {
            double h = (b - a) / n;
            double sum1 = 0, sum2 = 0;
            Parallel.For(1, this.n, ((i) =>
            {
                var xk = a + i * h;
                if (i <= n - 1)
                {
                    sum1 += f(xk);
                }

                var xk_1 = a + (i - 1) * h;
                sum2 += f((xk + xk_1) / 2);
            }));
            var result = h / 3d * (1d / 2d * f(a) + sum1 + 2 * sum2 + 1d / 2d * f(b));
            return result;
        }
    }
}
