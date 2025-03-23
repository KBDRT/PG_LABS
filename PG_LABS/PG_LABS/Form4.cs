using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tao.FreeGlut;
using Tao.OpenGl;

namespace PG_LABS
{
    public partial class Form4: Form
    {
        private double ScreenW, ScreenH;
        private float devX, devY;
        private float[,] GrapValuesArray;
        private int elements_count = 0;
        private bool not_calculate = true;
        private int pointPosition = 0;
        private float lineX, lineY;
        private float Mcoord_X = 0, Mcoord_Y = 0;

        public Form4()
        {
            InitializeComponent();
            anT.InitializeContexts();
        }


        private void anT_MouseMove(object sender, MouseEventArgs e)
        {
            Mcoord_X = e.X;
            Mcoord_Y = e.Y;

            lineX = devX * e.X;
            lineY = (float)(ScreenH - devY * e.Y);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (pointPosition == elements_count - 1)
                pointPosition = 0;

            Draw();

            pointPosition++;
        }

        private void Draw()
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            Gl.glLoadIdentity();
            Gl.glColor3f(0, 0, 0);
            Gl.glPushMatrix();

            Gl.glTranslated(15, 15, 0);
            Gl.glBegin(Gl.GL_POINTS);

            for (int ax = -15; ax < 15; ax++)
            {
                for (int bx = -15; bx < 15; bx++)
                {
                    Gl.glVertex2d(ax, bx);
                }
            }

            Gl.glEnd();
            Gl.glBegin(Gl.GL_LINES);

            Gl.glVertex2d(0, -15);
            Gl.glVertex2d(0, 15);
            Gl.glVertex2d(-15, 0);
            Gl.glVertex2d(15, 0);

            Gl.glVertex2d(0, 15);
            Gl.glVertex2d(0.1, 14.5);
            Gl.glVertex2d(0, 15);
            Gl.glVertex2d(-0.1, 14.5);

            Gl.glVertex2d(15, 0);
            Gl.glVertex2d(14.5, 0.1);
            Gl.glVertex2d(15, 0);
            Gl.glVertex2d(14.5, -0.1);
            Gl.glEnd();


            PrintText2D(15.5f, 0, "x");
            PrintText2D(0.5f, 14.5f, "y");

            DrawDiagram();
            Gl.glPopMatrix();


            float x_temp = devX * Mcoord_X;
            float y_temp = (float)ScreenH - devY * Mcoord_Y;

            //x_temp = 0;
            //y_temp = 0;

            PrintText2D(x_temp  + 0.2f, y_temp + 0.4f, $"[ x: {x_temp - 15} ; y: {y_temp - 15} ]");


            Gl.glColor3f(255, 0, 0);
            Gl.glBegin(Gl.GL_LINES);

            Gl.glVertex2d(lineX, 15);
            Gl.glVertex2d(lineX, lineY);
            Gl.glVertex2d(15, lineY);
            Gl.glVertex2d(lineX, lineY);
            Gl.glEnd();

            Gl.glFlush();

            anT.Invalidate();
        }


        private void PrintText2D(float x, float y, string text)
        {
            Gl.glRasterPos2f(x, y);
            foreach (char char_for_draw in text)
            {
                Glut.glutBitmapCharacter(Glut.GLUT_BITMAP_9_BY_15, char_for_draw);
            }
        }


        private void functionCalculation()
        {
            float x = 0, y = 0;

            GrapValuesArray = new float[300, 2];
            elements_count = 0;

            for (x = -15; x < 15; x += 0.1f)
            {
                y = (float)Math.Sin(x) * 3 + 1;
                GrapValuesArray[elements_count, 0] = x;
                GrapValuesArray[elements_count, 1] = y;
                elements_count++;
            }

            not_calculate = false;

        }


        private void DrawDiagram()
        {
            if (not_calculate)
            {
                functionCalculation();
            }

            Gl.glBegin(Gl.GL_LINE_STRIP);
            Gl.glVertex2d(GrapValuesArray[0, 0], GrapValuesArray[0, 1]);

            for (int ax = 1; ax < elements_count; ax += 2)
            {
                Gl.glVertex2d(GrapValuesArray[ax, 0], GrapValuesArray[ax, 1]);
            }

            Gl.glEnd();
            Gl.glPointSize(5);
            Gl.glColor3f(255, 0, 0);

            Gl.glBegin(Gl.GL_POINTS);
            Gl.glVertex2d(GrapValuesArray[pointPosition, 0], GrapValuesArray[pointPosition, 1]);
            Gl.glEnd();
            Gl.glPointSize(1);


        }

        private void Form4_Load(object sender, EventArgs e)
        {
            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_SINGLE);
            Gl.glClearColor(255, 255, 255, 1);
            Gl.glViewport(0, 0, anT.Width, anT.Height);
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();

            if ((float)anT.Width <= (float)anT.Height)
            {
                ScreenW = 30.0;
                ScreenH = 30 * (float)anT.Height / (float)anT.Width;
                Glu.gluOrtho2D(0.0,  ScreenW , 0.0, ScreenH);
            }
            else
            {
                ScreenW = 30 * (float)anT.Width / (float)anT.Height;
                ScreenH = 30.0;
                Glu.gluOrtho2D(0.0, ScreenW, 0.0, ScreenH);
            }


            devX = (float)ScreenW / (float)anT.Width;
            devY = (float)ScreenH / (float)anT.Height;

            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            timer1.Start();
        }
    }
}
