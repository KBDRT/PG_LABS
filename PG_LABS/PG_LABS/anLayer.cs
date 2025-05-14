using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tao.OpenGl;

namespace PG_LABS
{
    public class anLayer
    {
        public int Width, Height;

        private int[,,] DrawPlace;

        public int[,,] GetDrawingPlace() => DrawPlace;

        private bool IsVisible;

        private Color ActiveColor;

        private int ListNom;

        public anLayer(int s_W, int s_H)
        {
            Width = s_W;
            Height = s_H;

            DrawPlace = new int[Width, Height, 4];

            for (int ax = 0; ax < Width; ax++)
            {
                for (int bx = 0; bx < Height; bx++)
                {
                    DrawPlace[ax, bx, 3] = 1;
                }
            }

            IsVisible = true;

            ActiveColor = Color.Black;
        }

        public void ClearList()
        {
            if (Gl.glIsList(ListNom) == Gl.GL_TRUE)
            {
                Gl.glDeleteLists(ListNom, 1);
            }
        }

        public void CreateNewList()
        {
            if (Gl.glIsList(ListNom) != Gl.GL_TRUE)
            {
                ListNom = Gl.glGenLists(1);
            }

            Gl.glNewList(ListNom, Gl.GL_COMPILE);

            RenderImage(false);

            Gl.glEndList();
        }



        public void SetVisibility(bool visibilityState) => IsVisible = visibilityState;

        public bool GetVisibility() => IsVisible;

        public void SetColor(Color NewColor) => ActiveColor = NewColor;


        public void Draw(anBrush BR, int x, int y)
        {
            int real_pos_draw_start_x = x - BR.myBrush.Width / 2;
            int real_pos_draw_start_y = y - BR.myBrush.Height / 2;

            if (real_pos_draw_start_x < 0)
                real_pos_draw_start_x = 0;

            if (real_pos_draw_start_y < 0)
                real_pos_draw_start_y = 0;

            int boundary_x = real_pos_draw_start_x + BR.myBrush.Width;
            int boundary_y = real_pos_draw_start_y + BR.myBrush.Height;


            if (boundary_x > Width)
                boundary_x = Width;

            if (boundary_y > Height)
                boundary_y = Height;

            int count_x = 0, count_y = 0;


            for (int ax = real_pos_draw_start_x; ax < boundary_x; ax++, count_x++)
            {
                count_y = 0;

                for (int bx = real_pos_draw_start_y; bx < boundary_y; bx++, count_y++)
                {
                    Color ret = BR.myBrush.GetPixel(count_x, count_y);

                    if (!(ret.R == 255 && ret.G == 0 && ret.B == 0))
                    {
                        DrawPlace[ax, bx, 0] = ActiveColor.R;
                        DrawPlace[ax, bx, 1] = ActiveColor.G;
                        DrawPlace[ax, bx, 2] = ActiveColor.B;
                        DrawPlace[ax, bx, 3] = BR.IsBrushErase() ? 1 : 0;
                    }
                }

            }
        }

        public void RenderImage(bool FromList)
        {

            if (FromList)
            {
                Gl.glCallList(ListNom);
                return;
            }

            int count = 0;


            for (int ax = 0; ax < Width; ax++)
            {
                for (int bx = 0; bx < Height; bx++)
                {
                    if (DrawPlace[ax, bx, 3] != 1)
                    {
                        count++;
                    }
                }
            }

            int[] arr_date_vertex = new int[count * 2];
            float[] arr_date_colors = new float[count * 3];

            int nom_elements = 0;


            for (int ax = 0; ax < Width; ax++)
            {
                for (int bx = 0; bx < Height; bx++)
                {
                    if (DrawPlace[ax, bx, 3] != 1)
                    {
                        arr_date_vertex[nom_elements * 2] = ax;
                        arr_date_vertex[nom_elements * 2 + 1] = bx;

                        for (int i = 0; i < 3; i++)
                        {
                            arr_date_colors[nom_elements * 3 + i] = (float)DrawPlace[ax, bx, i] / 255.0f;
                        }

                        nom_elements++;
                    }
                }
            }

            Gl.glEnable(Gl.GL_VERTEX_ARRAY);
            Gl.glEnable(Gl.GL_COLOR_ARRAY);


            Gl.glColorPointer(3, Gl.GL_FLOAT, 0, arr_date_colors);
            Gl.glVertexPointer(2, Gl.GL_INT, 0, arr_date_vertex);

            Gl.glDrawArrays(Gl.GL_POINTS, 0, count);

            Gl.glDisable(Gl.GL_VERTEX_ARRAY);
            Gl.glDisable(Gl.GL_COLOR_ARRAY);
        }

        public Color GetColor() => ActiveColor;
    }
}
