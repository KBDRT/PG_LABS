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
    public partial class Form3: Form
    {
        private double a, b, c;
        public Form3()
        {
            InitializeComponent();
            anT.InitializeContexts();
        }


        private void Draw()
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            Gl.glLoadIdentity();
            Gl.glBegin(Gl.GL_TRIANGLES);

            Gl.glColor3d(a, b, c); 
            Gl.glVertex2d(5.0, 5.0);

            Gl.glColor3d(c, a, b);
            Gl.glVertex2d(25.0, 5.0);

            Gl.glColor3d(b, c, a);
            Gl.glVertex2d(5.0, 25.0);

            Gl.glEnd();
            Gl.glFlush();
            anT.Invalidate();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            b = (double)trackBar2.Value / 1000;
            label5.Text = b.ToString();
            Draw();
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            c = (double)trackBar3.Value / 1000;
            label6.Text = c.ToString();
            Draw();

        }


        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            a = (double)trackBar1.Value / 1000;
            label4.Text = a.ToString();
            Draw();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
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

            Draw();
        }
    }
}
