using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMMNN_2
{
    static class num
    {
        public const int CLUSTER = 4;
        public const double LEARNINGRATE = 0.05;
    }

    class Run
    {
        public void numFMMNN(Bitmap bitmap)
        {
            double[] input = membership(bitmap);

            perceptron per = new perceptron();
            Console.WriteLine(per.numFMMNN(input));
        }

        public double[] membership(Bitmap bitmap)
        {
            // 4방향의 소속도를 출력
            ImageProcessing ip = new ImageProcessing();
            perceptron per = new perceptron();

            // gray->binary->roi
            int[,] grayarray = ip.roiarea(ip.maxmin(ip.GrayArray(bitmap)));
            double[] distance = new double[num.CLUSTER];
            double sum = 0.0;
            double[] Csum = new double[num.CLUSTER] { 0, 0, 0, 0 };
            double min = 1.0;
            double max = 0.0;

            // 총길이로 나오기때문에 -1해줘야함
            int[][] Cw = new int[4][] {
                new int[] { 0, 0 },
                new int[] { grayarray.GetLength(0)-1, 0 },
                new int[] { 0, grayarray.GetLength(1)-1 },
                new int[] { grayarray.GetLength(0)-1, grayarray.GetLength(1)-1 }
            };

            for (int y = 0; y < grayarray.GetLength(1); y++)
                for (int x = 0; x < grayarray.GetLength(0); x++)
                {
                    if (grayarray[x, y] == 0)
                    {
                        for (int i = 0; i < num.CLUSTER; i++)
                        {
                            distance[i] = Math.Pow((x - Cw[i][0]), 2) + Math.Pow(y - Cw[i][1], 2);
                            distance[i] = Math.Sqrt(distance[i]);
                            sum += distance[i];
                        }
                        for (int i = 0; i < num.CLUSTER; i++)
                        {
                            distance[i] = 1 - distance[i] / sum;
                            if (min > distance[i])
                                min = distance[i];
                            if (max < distance[i])
                                max = distance[i];
                        }
                        sum = 0.0;
                    }
                }

            for (int y = 0; y < grayarray.GetLength(1); y++)
                for (int x = 0; x < grayarray.GetLength(0); x++)
                {
                    if (grayarray[x, y] == 0)
                    {
                        for (int i = 0; i < num.CLUSTER; i++)
                        {
                            distance[i] = Math.Pow((x - Cw[i][0]), 2) + Math.Pow(y - Cw[i][1], 2);
                            distance[i] = Math.Sqrt(distance[i]);
                            sum += distance[i];
                        }
                        for (int i = 0; i < num.CLUSTER; i++)
                        {
                            distance[i] = 1 - distance[i] / sum;
                            distance[i] = per.maxminStretch(distance[i], max, min);
                            Csum[i] += distance[i];
                            // Console.WriteLine(distance[i]);
                        }
                        sum = 0.0;
                        // Console.WriteLine();
                    }
                }

            min = 99999999.0;
            max = 0.0;

            for (int i = 0; i < num.CLUSTER; i++)
            {
                if (min > Csum[i])
                    min = Csum[i];
                if (max < Csum[i])
                    max = Csum[i];
            }
            Console.WriteLine("----------Clustering 출력값----------");
            for (int i = 0; i < num.CLUSTER; i++)
            {
                Csum[i] = per.maxminStretch(Csum[i], max, min);
                Console.WriteLine((i + 1) + "번째 클러스터 : " + Csum[i]);
            }

            return Csum;
        }

        public Bitmap Convert(int[,] grayarray)
        {
            // 배열을 받아서 비트맵으로
            Bitmap bitmap = new Bitmap(grayarray.GetLength(0), grayarray.GetLength(1));
            Color color;

            for (int y = 0; y < grayarray.GetLength(1); y++)
            {
                for (int x = 0; x < grayarray.GetLength(0); x++)
                {
                    color = Color.FromArgb(grayarray[x, y], grayarray[x, y], grayarray[x, y]);
                    bitmap.SetPixel(x, y, color);
                }
            }

            return bitmap;
        }
    }
}
