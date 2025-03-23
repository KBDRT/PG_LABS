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
    public partial class Form2: Form
    {
        public Form2()
        {
            InitializeComponent();
            anT.InitializeContexts();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

            //Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_SINGLE);
            Gl.glClearColor(255, 255, 255, 1);
            Gl.glViewport(0, 0, anT.Width, anT.Height);
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();

            if ((float)anT.Width <= (float)anT.Height)
            {
                Glu.gluOrtho2D(0.0, 30 * (float)anT.Height / (float)anT.Width, 0.0, 30.0);
            }
            else
            {
                Glu.gluOrtho2D(0.0, 30 * (float)anT.Width / (float)anT.Height, 0.0, 30.0);
            }

            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();
            Gl.glEnable(Gl.GL_DEPTH_TEST);
            Gl.glEnable(Gl.GL_COLOR_MATERIAL);


        }

        private void button1_Click(object sender, EventArgs e)
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            Gl.glLoadIdentity();
            Gl.glColor3f(1.0f, 0, 0); // цвет

            Gl.glBegin(Gl.GL_LINE_STRIP);
            Gl.glVertex2d(0, 0);

            if ((float)anT.Width <= (float)anT.Height)
            {
                Gl.glVertex2d(30.0f * (float)anT.Height / (float)anT.Width, 30);
            }
            else
            {
                Gl.glVertex2d(30.0f * (float)anT.Width / (float)anT.Height, 30);
            }


            Gl.glEnd();
            Gl.glFlush();
            anT.Invalidate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            Gl.glLoadIdentity();
            Gl.glColor3f(1.0f, 0, 0); // цвет

            Gl.glBegin(Gl.GL_LINE_LOOP);
            Gl.glVertex2d(8, 7);
            Gl.glVertex2d(15, 27);
            Gl.glVertex2d(17, 27);
            Gl.glVertex2d(23, 7);
            Gl.glVertex2d(21, 7);
            Gl.glVertex2d(19, 14);
            Gl.glVertex2d(12.5, 14);
            Gl.glVertex2d(10, 7);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_LINE_LOOP);
            Gl.glVertex2d(18.5, 16);
            Gl.glVertex2d(16, 25);
            Gl.glVertex2d(13.2, 16);
            Gl.glEnd();


            Gl.glFlush();
            anT.Invalidate();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            Gl.glLoadIdentity();
            Gl.glColor3f(0.0f, 0.5f, 0.1f); // цвет
            Gl.glTranslated(3, 0, 0); // размещение


            Gl.glBegin(Gl.GL_LINE_LOOP);
            Gl.glVertex2d(3, 7);
            Gl.glVertex2d(3, 27);
            Gl.glVertex2d(7, 27);
            Gl.glVertex2d(7, 16.5);
            Gl.glVertex2d(17, 16.5);
            Gl.glVertex2d(17, 7);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_LINE_LOOP);
            Gl.glVertex2d(7, 10);
            Gl.glVertex2d(7, 14);
            Gl.glVertex2d(13, 14);
            Gl.glVertex2d(13, 10);


            Gl.glEnd();


            Gl.glBegin(Gl.GL_LINE_LOOP);
            Gl.glVertex2d(20, 7);
            Gl.glVertex2d(20, 27);
            Gl.glVertex2d(23.5, 27);
            Gl.glVertex2d(23.5, 7);
            Gl.glEnd();


            Gl.glFlush();
            anT.Invalidate();
        }
    }
}
