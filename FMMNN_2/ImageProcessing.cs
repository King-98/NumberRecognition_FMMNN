using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMMNN_2
{
    class ImageProcessing
    {
        public int[,] GrayArray(Bitmap bitmap)
        {
            /*
                비트맵을 그레이어레이로
             */
            Color gray;
            int brightness;
            int[,] grayarray = new int[bitmap.Width, bitmap.Height];

            for (int y = 0; y < bitmap.Height; y++)
                for (int x = 0; x < bitmap.Width; x++)
                {
                    gray = bitmap.GetPixel(x, y);
                    brightness = (int)(0.299 * gray.R + 0.587 * gray.G + 0.114 * gray.B);
                    grayarray[x, y] = brightness;
                }

            return grayarray;
        }

        public int[,] maxmin(int[,] grayarray)
        {
            int avg = 0;
            int max = 0, min = 256;

            for (int y = 0; y < grayarray.GetLength(1); y++)
                for (int x = 0; x < grayarray.GetLength(0); x++)
                {
                    if (max < grayarray[x, y])
                        max = grayarray[x, y];
                    if (min > grayarray[x, y])
                        min = grayarray[x, y];
                }

            avg = (max + min) / 2;

            for (int y = 0; y < grayarray.GetLength(1); y++)
                for (int x = 0; x < grayarray.GetLength(0); x++)
                {
                    if (avg > grayarray[x, y])
                        grayarray[x, y] = 0;
                    else
                        grayarray[x, y] = 255;
                }

            return grayarray;
        }

        public int[,] roiarea(int[,] grayarray)
        {
            int[] start = new int[2];
            int[] end = new int[2];
            int x2 = 0, y2 = 0;
            int x, y;

            start[0] = grayarray.GetLength(0);//왼쪽
            start[1] = grayarray.GetLength(1);//아래
            end[0] = 0;//오른쪽
            end[1] = 0;//위쪽

            for (y = 0; y < grayarray.GetLength(1); y++)
            {
                for (x = 0; x < grayarray.GetLength(0); x++)
                {
                    if (grayarray[x, y] == 0)
                    {
                        if (start[0] > x)
                            start[0] = x;//왼
                        if (start[1] > y)
                            start[1] = y;//밑
                        if (end[0] < x)
                            end[0] = x;//오른
                        if (end[1] < y)
                            end[1] = y;//위
                    }
                }
            }
            end[0]++; end[1]++; // 좌표고를때 1낮은 수를 검사하므로 초기화할때 바꿈
            int[,] newarray = new int[end[0] - start[0], end[1] - start[1]];

            //원본파일 복사
            for (y = start[1]; y < end[1]; y++)
            {
                x2 = 0;
                for (x = start[0]; x < end[0]; x++)
                {
                    newarray[x2, y2] = grayarray[x, y];
                    x2++;
                }
                y2++;
            }

            return newarray;
        }
    }
}
