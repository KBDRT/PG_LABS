using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG_LABS
{
    public class anBrush
    {
        public Bitmap myBrush;

        private bool IsErase = false;

        public anBrush()
        {
            DefaultBrush();
        }

        public bool IsBrushErase() => IsErase;



        public void DefaultBrush()
        {
            myBrush = new Bitmap(5, 5);

            for (int ax = 0; ax < 5; ax++)
                for (int bx = 0; bx < 5; bx++)
                    myBrush.SetPixel(ax, bx, Color.Red);

            myBrush.SetPixel(0, 2, Color.Black);
            myBrush.SetPixel(1, 2, Color.Black);

            for (int ax = 0; ax < 5; ax++)
                myBrush.SetPixel(2, ax, Color.Black);

            myBrush.SetPixel(3, 2, Color.Black);
            myBrush.SetPixel(4, 2, Color.Black);
        }


        public anBrush(int Value, bool Special)
        {
            if (!Special)
            {
                CreateStandartBrush(Value);
                IsErase = false;

            }
            else
            {
                switch (Value)
                {
                    case 1:
                        CreateStandartBrush(Value);
                        IsErase = true;
                        break;
                    default:
                        {
                            DefaultBrush();
                            IsErase = false;
                            break;
                        }
                }
            }
        }

        public void CreateStandartBrush(int Value)
        {
            myBrush = new Bitmap(Value, Value);

            for (int ax = 0; ax < Value; ax++)
                for (int bx = 0; bx < Value; bx++)
                    myBrush.SetPixel(0, 0, Color.Black);
        }


        public anBrush(string FromFile)
        {
            return;
            string path = Directory.GetCurrentDirectory();
            path += "" + FromFile;
            myBrush = new Bitmap(path);
        }


    }
}
