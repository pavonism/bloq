using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Windows.Forms;
using System.Resources;

#nullable enable

namespace lab4
{
    public partial class Form1 : Form
    {
        BlockSchema schema;
        private ResourceManager rm;

        int current_button;
        string? chosen_button = null;
        HookSource? currentHookSource = null;
        
        bool moveSelected = false;
        int lastMousePosX;
        int lastMousePosY;

        bool removeMode = false;
        bool linkMode = false;
        bool leftDown = false;

        public void LightOnButton(string button_name)
        {
            foreach (Button b in TableEditorButtons.Controls)
            {
                if(b.Name != button_name)
                b.BackColor = Color.Transparent;
                else 
                b.BackColor = Color.LightGoldenrodYellow;
            }

            chosen_button = button_name;
        }


        public Form1()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("pl-PL");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pl-PL");
            rm = new ResourceManager("lab4.Resources.global", System.Reflection.Assembly.GetExecutingAssembly());

            InitializeComponent();

            schema = new BlockSchema(800, 800);
            schema.AttachSchema(Canvas);
            schema.DrawCanvas();

            LightOnButton(button_operation_block.Name);
            current_button = 0;
            schema.ClearCanvas();

        }
        

        private void button_operation_block_MouseClick(object sender, MouseEventArgs e)
        {
            current_button = 0;
            LightOnButton(button_operation_block.Name);
            removeMode = false;
            linkMode = false;
            
        }

        private void button_decisive_block_MouseClick(object sender, MouseEventArgs e)
        {
            current_button = 1;
            LightOnButton(button_decisive_block.Name);
            removeMode = false;
            linkMode = false;

        }

        private void ButtonStartBlock_Click(object sender, EventArgs e)
        {

            current_button = 2;
            LightOnButton(ButtonStartBlock.Name);
            removeMode = false;
            linkMode = false;

        }

        private void ButtonStopBlock_Click(object sender, EventArgs e)
        {
            current_button = 3;
            LightOnButton(ButtonStopBlock.Name);
            removeMode = false;
            linkMode = false;

        }

        private void ButtonRemove_Click(object sender, EventArgs e)
        {
            LightOnButton(ButtonRemove.Name);
            linkMode = false;
            removeMode = true;
        }

        private void ButtonLink_Click(object sender, EventArgs e)
        {
            LightOnButton(ButtonLink.Name);
            linkMode = true;
            removeMode = false;
        }

        private void button_new_schema_Click(object sender, EventArgs e)
        {
            SizeDialog dialog = new SizeDialog();
            dialog.ShowDialog();

            if (dialog.changed)
            {
                schema.newCanvas(dialog.width, dialog.height);
                schema.AttachSchema(Canvas);
                schema.ClearSchema();

                TextBoxSelected.Text = null;
                TextBoxSelected.Hide();
                Refresh();
            }
        }

        private void Canvas_MouseClick(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Right && moveSelected == true) return;

            if (e.Button == MouseButtons.Right)
            {

                if (schema.trySelect(new Point(e.X, e.Y)))
                {
                    TextBoxSelected.Show();
                    TextBoxSelected.Text = schema.getSelectedBlock()!.Text;
                    if (schema.getSelectedBlock() is StartEndBlock)
                    {
                        TextBoxSelected.Enabled = false;
                    }
                    else
                        TextBoxSelected.Enabled = true;

                }
                else TextBoxSelected.Hide();

                schema.DrawCanvas();
                return;
            }

            if (linkMode == true && leftDown == true) return;


            if (e.Button == MouseButtons.Left && removeMode == true && moveSelected == false)
            {

                if (schema.tryRemove(new Point(e.X, e.Y)))
                {
                    updateTextBox();
                }

                schema.DrawCanvas();
                return;
            }



            if (e.Button == MouseButtons.Left && moveSelected == false)
            {
                Block? newBlock = null;

                switch (current_button)
                {
                    case 0:
                        {
                            newBlock = new OperationBlock(new Point(e.X, e.Y), rm.GetString("OPERATION_BLOCK_NAME"));
                            break;
                        }
                    case 1:
                        {
                            newBlock = new DecisiveBlock(new Point(e.X, e.Y), rm.GetString("DECISIVE_BLOCK_NAME"));
                            break;
                        }
                    case 2:
                        {
                            if (schema.alreadyHasStart())
                            {
                                MessageBox.Show(rm.GetString("START_EXIST_MSG"), "", MessageBoxButtons.OK);
                                return;
                            }
                            newBlock = new StartEndBlock(new Point(e.X, e.Y), rm.GetString("START_BLOCK_NAME"));
                            break;
                        }
                    case 3:
                        {
                            newBlock = new StartEndBlock(new Point(e.X, e.Y), rm.GetString("STOP_BLOCK_NAME"));
                            break;
                        }
                    default:
                        break;
                }

                schema.Add(newBlock);
            }

            Canvas.Refresh();

        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {

            if(linkMode == true && currentHookSource != null && leftDown == true)
            {
                currentHookSource.Destination.Location.X = e.X;
                currentHookSource.Destination.Location.Y = e.Y;

                schema.DrawCanvas();
                return;
            }

            if(moveSelected == true)
            {
                schema.moveBlock(e.X - lastMousePosX, e.Y - lastMousePosY);
                lastMousePosX = e.X;
                lastMousePosY = e.Y;

                schema.DrawCanvas();
                return;
            }

        }

        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left && linkMode == true && currentHookSource != null)
            {
                Point mousePosition = new Point(e.X, e.Y);
                schema.tryLink(mousePosition, currentHookSource);
                leftDown = false;
                return;
            }

            if (e.Button != MouseButtons.Left && e.Button == MouseButtons.Middle && schema.selected() == true)
            {
                schema.checkPosition();

                moveSelected = false;
            }
        }

        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left && linkMode == true)
            {
                Point mousePosition = new Point(e.X, e.Y);
                currentHookSource = schema.tryFindHook(mousePosition);
                leftDown = true;

                return;
            }

            if (e.Button == MouseButtons.Middle && schema.selected())
            {
                lastMousePosX = e.X;
                lastMousePosY = e.Y;
                moveSelected = true;
            }

        }

        private void TextBoxSelected_TextChanged(object sender, EventArgs e)
        {
            if(schema.selected())
            schema.getSelectedBlock()!.Text = TextBoxSelected.Text;
            schema.DrawCanvas();
        }

        private void button_save_schema_Click(object sender, EventArgs e)
        {
            Stream myStream;
            var bin_serializer = new BinaryFormatter();
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "Diagram files (*.diag)|*.diag";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = saveFileDialog1.OpenFile()) != null)
                {

#pragma warning disable SYSLIB0011 
                    try
                    {
                        bin_serializer.Serialize(myStream, schema);


                        MessageBox.Show(rm.GetString("SAVE_SUCCESS_MSG"), "", MessageBoxButtons.OK);
                    }
                    catch
                    {
                        MessageBox.Show(rm.GetString("SAVE_FAIL_MSG"), "", MessageBoxButtons.OK);
                    }
#pragma warning restore SYSLIB0011 

                    myStream.Close();
                }
            }

        }

        private void button_load_schema_Click(object sender, EventArgs e)
        {
            Stream myStream;
            var bin_serializer = new BinaryFormatter();
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Diagram files (*.diag)|*.diag";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = openFileDialog.OpenFile()) != null)
                {

#pragma warning disable SYSLIB0011 
                    try
                    {
                        schema = (BlockSchema)bin_serializer.Deserialize(myStream);
                        MessageBox.Show(rm.GetString("LOAD_SUCCESS_MSG"), "", MessageBoxButtons.OK);

                        schema.AttachSchema(Canvas);
                        updateTextBox();
                        schema.DrawCanvas();

                    }
                    catch
                    {
                        MessageBox.Show(rm.GetString("LOAD_FAIL_MSG"), "", MessageBoxButtons.OK);

                    }
#pragma warning restore SYSLIB0011 


                    myStream.Close();
                }
            }

        }

        private void button_polish_lang_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("pl-PL");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pl-PL");

            ReloadLayout();
        }

        private void button_eng_lang_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");

            ReloadLayout();
        }


        void updateTextBox()
        {
            if (schema.selected())
            {
                TextBoxSelected.Text = schema.getSelectedBlock()!.Text;
                TextBoxSelected.Show();
                TextBoxSelected.Enabled = true;

                if(schema.getSelectedBlock() is StartEndBlock)
                {
                    TextBoxSelected.Enabled = false;
                }

            }
            else
            {
                TextBoxSelected.Text = null;
                TextBoxSelected.Hide();
            }
        }

        void ReloadLayout()
        {
            int scrollX = CanvasPanel.HorizontalScroll.Value;
            int scrollY = CanvasPanel.VerticalScroll.Value;
            var size = this.Size;

            var windowState = this.WindowState;

            this.Controls.Clear();
            InitializeComponent();

            schema.AttachSchema(Canvas);
            LightOnButton(chosen_button!);

            CanvasPanel.HorizontalScroll.Value = scrollX;
            CanvasPanel.VerticalScroll.Value = scrollY;
            CanvasPanel.ScrollControlIntoView(CanvasPanel);

            this.Size = size;

            if(windowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                this.WindowState = FormWindowState.Maximized;
            }

            updateTextBox();
        }

    }
}
