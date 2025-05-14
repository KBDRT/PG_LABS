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
using Tao.Platform;

namespace PG_LABS
{
    public partial class Form1: Form
    {
        public Form1()
        {
            InitializeComponent();
            anT.InitializeContexts();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            Gl.glLoadIdentity();
            Gl.glColor3f(1.0f, 0, 0); // цвет

            Gl.glPushMatrix();
            Gl.glTranslated(0, 0, -6); // размещение
            Gl.glRotated(45, 1, 1, 0); // поворот

            Glut.glutWireSphere(2, 32, 32);


            //Gl.glColor3f(0.0f, 5.0f, 0); // цвет
            //Gl.glTranslated(10, 0, -6); 
            //Glut.glutWireCube(2);


            Gl.glPopMatrix();
            Gl.glFlush();
            anT.Invalidate();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH);
            Gl.glClearColor(255, 255, 255, 1);
            Gl.glViewport(0, 0, anT.Width, anT.Height);
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Glu.gluPerspective(45, (float)anT.Width / (float)anT.Height, 0.1, 200);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();
            Gl.glEnable(Gl.GL_DEPTH_TEST);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            new Form2().ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new Form3().ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new Form4().ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            new Form5().ShowDialog();
        }

    }
}
