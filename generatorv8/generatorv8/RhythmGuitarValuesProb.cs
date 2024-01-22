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
    public partial class RhythmGuitarValuesProb : Form
    {
        private readonly Main formMain;
        public readonly NumericUpDown[] numerics;
        public RhythmGuitarValuesProb(Main formMain)
        {
            InitializeComponent();
            this.formMain = formMain;
            numerics = new NumericUpDown[]
            {
                numericUpDown1, numericUpDown2, numericUpDown3, numericUpDown4,
                numericUpDown5, numericUpDown6, numericUpDown7, numericUpDown8,
                numericUpDown9, numericUpDown10, numericUpDown11, numericUpDown12
            };
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            formMain.RhythmGuitarValuesProb[formMain.CurrentPhrase].Clear();
            for (int i = 0; i < numerics.Length; i++)
                formMain.RhythmGuitarValuesProb[formMain.CurrentPhrase].Add((int)numerics[i].Value);

            if (formMain.CurrentPhrase == formMain.PhrasesAllowed.Length)
            {
                for (int i = 0; i < formMain.PhrasesAllowed.Length; i++)
                {
                    formMain.RhythmGuitarValuesProb[i].Clear();
                    for (int j = 0; j < numerics.Length; j++)
                    {
                        formMain.RhythmGuitarValuesProb[i].Add((int)numerics[j].Value);
                    }
                }
            }
            Hide();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void RhythmGuitarValuesProb_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }
    }
}
