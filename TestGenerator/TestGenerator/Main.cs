using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.ListBox;

namespace TestGenerator
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();

            HideFields();
        }

        private void btnEditName_Click(object sender, EventArgs e)
        {
            ProjectName pn = new ProjectName();

            if (pn.ShowDialog() == DialogResult.OK)
            {
                lblProjectName.Text = Program.project.Name;

                if (lblProjectName.Text != "")
                    ShowFields();
                else
                    HideFields();
            }
        }

        private void RefreshModels()
        {
            chkListModels.Items.Clear();

            lblRows.Text = Program.project.ClassTo.Count + " Classes";

            foreach (ClassTo c in Program.project.ClassTo)
                chkListModels.Items.Add(c.Name);
        }


        private void HideFields()
        {
            lblModels.Visible = false;
            chkListModels.Visible = false;
            btnCreate.Visible = false;
            btnDelete.Visible = false;
            btnEdit.Visible = false;
            btnRead.Visible = false;
            txtViewModel.Visible = false;
            lblRows.Visible = false;
        }

        private void ShowFields()
        {
            lblModels.Visible = true;
            chkListModels.Visible = true;
            btnCreate.Visible = true;
            btnDelete.Visible = true;
            btnEdit.Visible = true;
            btnRead.Visible = true;
            txtViewModel.Visible = true;
            lblRows.Visible = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < chkListModels.CheckedItems.Count; i++)
                Program.project.ClassTo.Remove(Program.project.ClassTo.Find(item => item.Name == chkListModels.CheckedItems[i].ToString()));

            RefreshModels();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            foreach (ClassTo c in Program.project.ClassTo)
            {
                TestGenerator.Generate(c);
            }
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string file in dialog.FileNames)
                {
                    string result = "";
                    string[] classes;

                    System.IO.StreamReader sr = new System.IO.StreamReader(file);
                    result = sr.ReadToEnd();
                    sr.Close();

                    classes = result.Split(new string[] { "class" }, StringSplitOptions.None);

                    for (int i = 1; i < classes.Length; i++)
                    {
                        ClassTo m = new ClassTo(classes[i]);

                        Program.project.ClassTo.Add(m);
                    }

                    RefreshModels();
                }
            }
        }

        private void chkListModels_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (0 < chkListModels.SelectedItems.Count)
            {
                string name = chkListModels.SelectedItems[0].ToString();

                ClassTo c = Program.project.ClassTo.Find(item => item.Name == name);

                txtViewModel.Text = "class " + c.Name + "\r\n";

                txtViewModel.Text += "{\r\n";

                foreach (Method m in c.Methods)
                    txtViewModel.Text += "\t" + m.Type + " " + m.Name + "\r\n";

                txtViewModel.Text += "}";
            }
        }
    }
}
