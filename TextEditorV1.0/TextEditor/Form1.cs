using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace TextEditor
{
    public partial class Form1 : Form
    {
        OpenFileDialog openFile = null;
        StreamReader reader = null;
        StreamWriter writer = null;
        SaveFileDialog saveFile = null;
        string saveText = null;
        string currentPathFile = null;
        Form options;
        public Form1()
        {
            InitializeComponent();
            this.Text="Новый документ";
        }
        private void закрытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox1.Text == saveText)
            {
                this.Close();
            }
            else if (textBox1.Text != saveText)
            {
                DialogResult dlgres = MessageBox.Show("Сохранить изменения?", "Внимание", MessageBoxButtons.YesNoCancel);
                if (dlgres == DialogResult.Yes)
                {
                    if (currentPathFile==null)
                    {
                        SaveAsFile();
                        if (currentPathFile!=null)
                        {
                            this.Close();
                        }
                    }
                    else
                    {
                        SaveFile();
                        this.Close();
                    }
                }
                else if (dlgres == DialogResult.No)
                {
                    this.Close();
                }
            }
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ProcessingNewFile();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            toolStrip1.ImageList = imageList1;
            toolStrip1.Items[0].ImageIndex = 0;
            toolStrip1.Items[1].ImageIndex = 1;
            toolStrip1.Items[2].ImageIndex = 2;
            toolStrip1.Items[3].ImageIndex = 3;
            toolStrip1.Items[4].ImageIndex = 4;
            toolStrip1.Items[5].ImageIndex = 5;
            toolStrip1.Items[6].ImageIndex = 6;
            toolStrip1.Items[7].ImageIndex = 7;
        }
        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProcessingOpenFile();
        }
        private void OpenFile()
        {
            openFile = new OpenFileDialog();
            openFile.InitialDirectory = Path.GetFullPath(".");
            openFile.Filter = "Text Files(*.txt)|*.txt||";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                currentPathFile = openFile.FileName;
                reader = File.OpenText(currentPathFile);
                textBox1.Text = reader.ReadToEnd();
                saveText = textBox1.Text;
                reader.Close();
                Text = currentPathFile;
            }
        }
        private void SaveFile()
        {
            saveFile = new SaveFileDialog();
            saveFile.InitialDirectory = Path.GetFullPath(".");
            if (currentPathFile!=null)
            {
                writer = new StreamWriter(currentPathFile);
                writer.Write(textBox1.Text);
                writer.Close();
                saveText = textBox1.Text;
            }
            else
            {
                SaveAsFile();
            }
        }
        private void SaveAsFile()
        {
            saveFile = new SaveFileDialog();
            saveFile.InitialDirectory = Path.GetFullPath(".");
            saveFile.Filter = "Text Files(*.txt)|*.txt||";
            saveFile.DefaultExt = "*.txt";
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                currentPathFile = saveFile.FileName;
                writer = new StreamWriter(currentPathFile);
                writer.Write(textBox1.Text);
                writer.Close();
                Text = currentPathFile;
                saveText = textBox1.Text;
            }
        }
        private void ProcessingOpenFile()
        {
            if (textBox1.Text == ""|| textBox1.Text == saveText)
            {
                OpenFile();
            }
            else if (textBox1.Text!= saveText)
            {
                DialogResult dlgres = MessageBox.Show("Сохранить изменения?", "Внимание", MessageBoxButtons.YesNoCancel);
                if (dlgres == DialogResult.Yes)
                {
                    SaveFile();
                    if (saveText == textBox1.Text)
                        OpenFile();
                }
                else if (dlgres == DialogResult.No)
                {
                    OpenFile();
                }
            }
        }
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            ProcessingOpenFile();
        }
        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile();
        }
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            SaveFile();
        }
        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAsFile();
        }
        private void новыйДокументToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProcessingNewFile();
        }
        private void ProcessingNewFile()
        {
            if (textBox1.Text == "" || textBox1.Text == saveText)
            {
                NewDocument();
            }
            else if (textBox1.Text != saveText)
            {
                DialogResult dlgres = MessageBox.Show("Сохранить изменения?", "Внимание", MessageBoxButtons.YesNoCancel);
                if (dlgres == DialogResult.Yes)
                {
                    SaveFile();
                    if (saveText == textBox1.Text)
                        NewDocument();
                }
                else if (dlgres == DialogResult.No)
                {
                    NewDocument();
                }
            }
        }
        private void NewDocument()
        {
            this.Text = "Новый документ";
            textBox1.Text=null;
            openFile = null;
            reader = null;
            writer = null;
            saveFile = null;
            saveText = null;
            currentPathFile = null;
        }
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (textBox1.SelectionLength > 0)
                textBox1.Copy();
        }
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            textBox1.AppendText(Clipboard.GetText());
        }
        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (textBox1.SelectedText != "")
                textBox1.Cut();
        }
        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            if (textBox1.CanUndo == true)
            {
                textBox1.Undo();
                textBox1.ClearUndo();
                toolStripButton7.Enabled = false;
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.CanUndo == true)
            {
                toolStripButton7.Enabled=true;
            }
        }
        private void отменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox1.CanUndo == true)
            {
                textBox1.Undo();
                textBox1.ClearUndo();
                toolStripButton7.Enabled = false;
            }
        }
        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox1.SelectionLength > 0)
                textBox1.Copy();
        }
        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.AppendText(Clipboard.GetText());
        }
        private void вырезатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox1.SelectedText != "")
                textBox1.Cut();
        }
        private void выделитьВсеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.SelectAll();
        }
        private void шрифтToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorFont();
        }
        private void фонToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorBack();
        }
        private void шрифтToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ChangeFont();
        }
        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            Form2 options = new Form2(this);
            options.ShowDialog();
        }
        public void ColorFont()
        {
            ColorDialog dlg = new ColorDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBox1.ForeColor = dlg.Color;
            }
        }
        public void ColorBack()
        {
            ColorDialog dlgColor = new ColorDialog();
            if (dlgColor.ShowDialog() == DialogResult.OK)
            {
                textBox1.BackColor = dlgColor.Color;
            }
        }
        public void ChangeFont()
        {
            FontDialog dlgFont = new FontDialog();
            if (dlgFont.ShowDialog() == DialogResult.OK)
            {
                textBox1.Font = dlgFont.Font;
            }
        }
        private void копироватьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (textBox1.SelectionLength > 0)
                textBox1.Copy();
        }
        private void вставитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            textBox1.AppendText(Clipboard.GetText());
        }
        private void вырезатьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (textBox1.SelectedText != "")
                textBox1.Cut();
        }
        private void отменитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            textBox1.Undo();
            textBox1.ClearUndo();
            toolStripButton7.Enabled = false;
        }
    }
}
