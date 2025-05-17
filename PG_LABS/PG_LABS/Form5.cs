using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Tao.OpenGl;
using Tao.FreeGlut;
using Tao.Platform.Windows;


namespace PG_LABS
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
            AnT.InitializeContexts();
        }

        private anEngine ProgrammDrawingEngine;

        private int ActiveLayer = 0;
        private int LayersCount = 1;
        private int AllLayersCount = 1;


        private void Form5_Load(object sender, EventArgs e)
        {
           
            //Glut.glutInit();

            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH);

            Gl.glClearColor(255, 255, 255, 1);

            Gl.glViewport(0, 0, AnT.Width, AnT.Height);

            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();

            Glu.gluOrtho2D(0.0, AnT.Width, 0.0, AnT.Height);

            Gl.glMatrixMode(Gl.GL_MODELVIEW);

            ProgrammDrawingEngine = new anEngine(AnT.Width, AnT.Height, AnT.Width, AnT.Height);

            MessageBox.Show(
     $"{AnT.Width} {AnT.Height}",
     "Сообщение",
     MessageBoxButtons.YesNo,
     MessageBoxIcon.Information,
     MessageBoxDefaultButton.Button1,
     MessageBoxOptions.DefaultDesktopOnly);





            RenderTimer.Start();

            LayersControl.Items.Add("Главный слой", true);

        }

        private void RenderTimer_Tick(object sender, EventArgs e)
        {
            Drawing();
        }

        private void Drawing()
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            Gl.glLoadIdentity();

            Gl.glColor3f(0, 0, 0);

            ProgrammDrawingEngine.SwapImage();

            Gl.glEnd();
            Gl.glFlush();

            AnT.Invalidate();
        }

        private void AnT_MouseMove(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
                ProgrammDrawingEngine.Drawing(e.X, AnT.Height - e.Y);

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ProgrammDrawingEngine.SetStandartBrush(4);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            ProgrammDrawingEngine.SetSpecialBrush(0);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            ProgrammDrawingEngine.SetBrushFromFile("brush-1.bmp");
        }

        private void color1_MouseClick(object sender, MouseEventArgs e)
        {
            if (changeColor.ShowDialog() == DialogResult.OK)
            {
                color1.BackColor = changeColor.Color;
                ProgrammDrawingEngine.SetColor(color1.BackColor);
            }


        }

        private void linkLabel1_MouseClick(object sender, MouseEventArgs e)
        {
            (color1.BackColor, color2.BackColor) = (color2.BackColor, color1.BackColor);

            ProgrammDrawingEngine.SetColor(color1.BackColor);
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            LayersCount++;
            ProgrammDrawingEngine.AddLayer();

            int AddingLayerNom = LayersControl.Items.Add($"Слой {LayersCount}", false);

            LayersControl.SelectedIndex = AddingLayerNom;
            ActiveLayer = AddingLayerNom;
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Будет удален текущий слой, продолжить?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            
            if (res == DialogResult.Yes)
            {
                if (ActiveLayer == 0)
                {
                    MessageBox.Show("Вы не можете удалить нулевой слой", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    LayersCount--;

                    int LayerNomForDel = LayersControl.SelectedIndex;

                    LayersControl.Items.RemoveAt(LayerNomForDel);

                    LayersControl.SelectedIndex = 0;

                    ActiveLayer = 0;

                    LayersControl.SetItemCheckState(0, CheckState.Checked);

                    ProgrammDrawingEngine.RemoveLayer(LayerNomForDel);
                }
            }
        
        }

        private void LayersControl_SelectedValueChanged(object sender, EventArgs e)
        {
            if (LayersControl.SelectedIndex != ActiveLayer)
            {
                if (LayersControl.SelectedIndex != -1 && ActiveLayer < LayersControl.Items.Count)
                {
                    LayersControl.SetItemCheckState(ActiveLayer, CheckState.Unchecked);
                    ActiveLayer = LayersControl.SelectedIndex;

                    LayersControl.SetItemCheckState(ActiveLayer, CheckState.Checked);

                    ProgrammDrawingEngine.SetActiveLayerNom(ActiveLayer);
                }
            }



        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            ProgrammDrawingEngine.SetSpecialBrush(1);
        }

        private void карандашToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton1_Click(sender, e);
        }

        private void кистьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton2_Click(sender, e);
        }

        private void стеркаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton6_Click(sender, e);
        }

        private void новыйРисунокToolStripMenuItem_Click(object sender, EventArgs e)
        {

            DialogResult reslt = MessageBox.Show("В данный момент проект уже начат, сохранить изменения перед закрытием проекта?", "Внимание!", MessageBoxButtons.YesNoCancel);

            switch (reslt)
            {
                case DialogResult.No:
                    RecreateProject();
                    break;
                case DialogResult.Cancel:
                    return;
                case DialogResult.OK:

                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        Bitmap ToSave = ProgrammDrawingEngine.GetFinalImage();
                        ToSave.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                        RecreateProject();
                    }
                    else
                    {
                        return;
                    }

                    break;
            }

        }

        private void RecreateProject()
        {
            ProgrammDrawingEngine = new anEngine(AnT.Width, AnT.Height, AnT.Width, AnT.Height);
            LayersControl.Items.Clear();
            ActiveLayer = 0;
            LayersCount = 1;
            AllLayersCount = 1;
            LayersControl.Items.Add("Главный слой", true);
        }



        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Bitmap ToSave = ProgrammDrawingEngine.GetFinalImage();
                ToSave.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }

        private void выходToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void изфайлаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult reslt = MessageBox.Show("В данный момент проект уже начат, сохранить изменения перед закрытием проекта?", "Внимание!", MessageBoxButtons.YesNoCancel);
        
            switch (reslt)
            {
                case DialogResult.No:
                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        if (System.IO.File.Exists(openFileDialog1.FileName))
                        {
                            Bitmap ToLoad = new Bitmap(openFileDialog1.FileName);

                            if (ToLoad.Width > AnT.Width || ToLoad.Height > AnT.Height)
                            {
                                MessageBox.Show("Размер изображения превышает размеры области рисования", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                return;
                            }

                            RecreateProject();
                            ProgrammDrawingEngine.SetImageToMainLayer(ToLoad);

                        }
                    }
                    break;

                case DialogResult.Cancel:
                    return;

                case DialogResult.Yes:
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        Bitmap ToSave = ProgrammDrawingEngine.GetFinalImage();
                        ToSave.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);

                        if (openFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            if (System.IO.File.Exists(openFileDialog1.FileName))
                            {
                                Bitmap ToLoad = new Bitmap(openFileDialog1.FileName);

                                if (ToLoad.Width > AnT.Width || ToLoad.Height > AnT.Height)
                                {
                                    MessageBox.Show("Размер изображения превышает размеры области рисования", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                    return;
                                }

                                RecreateProject();
                                ProgrammDrawingEngine.SetImageToMainLayer(ToLoad);


                            }
                        }
                        else
                        {
                            return;
                        }
                    }
                    break;
            }
        }
    }
}
