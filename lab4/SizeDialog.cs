using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab4
{
    public partial class SizeDialog : Form
    {
        public Bitmap drawContext;
        public bool changed = false;
        public int width;
        public int height;

        public SizeDialog()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            width = ((int)numericUpDown1.Value);
            height = ((int)numericUpDown2.Value);
            changed = true;
            Close();
        }
    }
}
