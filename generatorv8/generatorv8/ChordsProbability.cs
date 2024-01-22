using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace generatorv8
{
    public partial class ChordsProbability : Form
    {
        private readonly Main formMain;
        public readonly Label[] labels;
        public readonly NumericUpDown[] numerics;
        public string[] labelTexts;
        public ChordsProbability(Main formMain)
        {
            InitializeComponent();
            this.formMain = formMain;
            labels = new Label[]
            {
                label1, label2, label3, label4, label5, label6, label7
            };
            numerics = new NumericUpDown[]
            {
                numericUpDown1, numericUpDown2, numericUpDown3, numericUpDown4, numericUpDown5, numericUpDown6, numericUpDown7
            };
            labelTexts = new string[]
            {
                "1st step (",
                "2nd step (",
                "3rd step (",
                "4th step (",
                "5th step (",
                "6th step (",
                "7th step (",
            };
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            formMain.ChordsProbabilities[formMain.CurrentPhrase].Clear();
            for (int i = 0; i < numerics.Length; i++)
                formMain.ChordsProbabilities[formMain.CurrentPhrase].Add((int)numerics[i].Value);

            if (formMain.CurrentPhrase == formMain.PhrasesAllowed.Length)
            {
                for (int i = 0; i < formMain.PhrasesAllowed.Length; i++)
                {
                    formMain.ChordsProbabilities[i].Clear();
                    for (int j = 0; j < numerics.Length; j++)
                    {
                        formMain.ChordsProbabilities[i].Add((int)numerics[j].Value);
                    }
                }
            }
            Hide();
        }

        private void ChordsProbability_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }
    }
}
