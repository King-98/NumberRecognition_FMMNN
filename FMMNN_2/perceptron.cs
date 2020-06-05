using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMMNN_2
{
    class perceptron
    {
        public double numFMMNN(double[] x)
        {
            // 소속도를 받아서 비교분류
            int i;
            const int INPUT = 4; // bias포함
            const int OUTPUT = 10; // bias포함
            double[][] w = new double[][] {
                new double[] {0.29,0.94,1,0}, // 0
                new double[] {0,0.3,1,0.5}, // 1
                new double[] {0.49,0.92,0.77,1}, // 2
                new double[] {0.53,0.82,0,1}, // 3
                new double[] {0.9,1,0,0.09}, // 4
                new double[] {0.75,0,1,0.8}, // 5
                new double[] {1,0,0.75,0.80}, // 6
                new double[] {0.89,1,0,0.28}, // 7
                new double[] {0.52, 1, 0, 0.42}, // 8
                new double[] {0.62, 0.75, 0, 1 } // 9
            };

            double[] tmp = new double[INPUT];
            double[] result = new double[OUTPUT];
            double max = 0.0;
            int number = -1;

            for (i = 0; i < OUTPUT; i++)
            {
                result[i] = min_max(x, w[i]);
                max = Math.Max(result[i], max); // 젤높은Max

                if (max == result[i])
                    number = i;
            }

            Console.WriteLine("---------출력층---------");
            for (i = 0; i < OUTPUT; i++)
                Console.WriteLine("result[" + i + "] : " + result[i]);

            Console.WriteLine("----------결과----------");
            return number;
        }

        public double min_max(double[] x, double[] w)
        {
            // 가중치랑 비교 min 한것과 더해서 리턴
            int i;
            const int INPUT = 4; // bias포함
            double[] tmp = new double[INPUT];
            double result = 0.0;
            double bias = 0.1;

            for (i = 0; i < INPUT; i++)
                tmp[i] = Math.Min(x[i], w[i]);
            for (i = 0; i < INPUT; i++)
                tmp[i] = Math.Max(tmp[i], bias);
            for (i = 0; i < INPUT; i++)
                result += tmp[i];

            return result;
        }

        public double maxminStretch(double x, double max, double min)
        {
            return (x - min) * (1 / (max - min));
        }
    }
}
