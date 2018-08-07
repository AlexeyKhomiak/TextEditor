using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextEditor
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        Form parent;
        public Form2(Form f)
        {
            InitializeComponent();
            parent = f;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 f = parent as Form1;
            f.ChangeFont();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 f = parent as Form1;
            f.ColorFont();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 f = parent as Form1;
            f.ColorBack();
        }
    }
}
