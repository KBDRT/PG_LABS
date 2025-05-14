using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG_LABS
{
    public class anEngine
    {
        private int picture_size_x, picture_size_y;

        private int scroll_x, scroll_y;


        private int screen_width, screen_height;


        private int ActiveLayerNom;

        private ArrayList Layers = new ArrayList();

        private anBrush standartBrush;

        private Color LastColorInUse;


        public anEngine(int size_x, int size_y, int screen_w, int screen_h)
        {
            picture_size_x = size_x;
            picture_size_y = size_y;

            screen_width = screen_w;
            screen_height = screen_h;

            scroll_x = scroll_y = 0;

            Layers.Add(new anLayer(picture_size_x, picture_size_y));

            ActiveLayerNom = 0;
            standartBrush = new anBrush(3, false);
        }


        public void SetStandartBrush(int SizeB) => standartBrush = new anBrush(SizeB, false);

        public void SetSpecialBrush(int Nom) => standartBrush = new anBrush(Nom, true);

        public void SetBrushFromFile(string FileName) => standartBrush = new anBrush(FileName);

        public void AddLayer()
        {
            SetActiveLayerNom(Layers.Add(new anLayer(picture_size_x, picture_size_y)));
        }

        public void RemoveLayer(int Nom)
        {
            if (Nom < Layers.Count && Nom >= 0)
            {
                SetActiveLayerNom(0);

                ((anLayer)Layers[Nom]).ClearList();

                Layers.RemoveAt(Nom);
            }
        }



        public void SetColor(Color NewColor)
        {
            ((anLayer)Layers[ActiveLayerNom]).SetColor(NewColor);
        }


        public void SetActiveLayerNom(int nom)
        {
            ((anLayer)Layers[ActiveLayerNom]).CreateNewList();
            ((anLayer)Layers[nom]).SetColor(((anLayer)Layers[ActiveLayerNom]).GetColor());
            ActiveLayerNom = nom;
        }

        public void SetVisibilityLayerNom(int nom, bool visible)
        {

        }

        public void Drawing(int x, int y)
        {
            ((anLayer)Layers[ActiveLayerNom]).Draw(standartBrush, x, y);
        }

        public void SwapImage()
        {
            for (int i = 0; i < Layers.Count; i++)
                ((anLayer)Layers[i]).RenderImage(i != ActiveLayerNom);
        }

        public Bitmap GetFinalImage()
        {
            Bitmap resultBitmap = new Bitmap(picture_size_x, picture_size_y);

            for (int ax = 0; ax < Layers.Count; ax++)
            {
                int[,,] tmp_layer_data = ((anLayer)Layers[ax]).GetDrawingPlace();

                for (int a = 0; a < picture_size_x; a++)
                {
                    for (int b = 0; b < picture_size_y; b++)
                    {
                        if (tmp_layer_data[a, b, 3] != 1)
                        {
                            resultBitmap.SetPixel(a, b, Color.FromArgb(tmp_layer_data[a, b, 0], tmp_layer_data[a, b, 1], tmp_layer_data[a, b, 2]));
                        }
                        else
                        {
                            if (ax == 0)
                            {
                                resultBitmap.SetPixel(a, b, Color.FromArgb(255, 255, 255));
                            }


                        }
                    }
                }
            }

            resultBitmap.RotateFlip(RotateFlipType.Rotate180FlipX);
            return resultBitmap;
        }


        public void SetImageToMainLayer(Bitmap Layer)
        {
            Layer.RotateFlip(RotateFlipType.Rotate180FlipX);

            for (int ax = 0; ax < Layer.Width; ax++)
            {
                for (int bx = 0; bx < Layer.Height; bx++)
                {
                    SetColor(Layer.GetPixel(ax, bx));
                    Drawing(ax, bx);
                }
            }

        }


    }
}
