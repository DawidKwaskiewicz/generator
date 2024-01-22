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
    public partial class LeadVocalsInterProb : Form
    {
        private readonly Main formMain;
        public readonly NumericUpDown[] numerics;
        public LeadVocalsInterProb(Main formMain)
        {
            InitializeComponent();
            this.formMain = formMain;
            numerics = new NumericUpDown[]
            {
                numericUpDown1, numericUpDown2, numericUpDown3, numericUpDown4, numericUpDown5, numericUpDown6, numericUpDown7, numericUpDown8
            };
        }
        public void UpdateInterProb(int phraseIndex, int currentThought)
        {
            for (int i = 0; i < formMain.LeadVocalsInterProb[phraseIndex][currentThought].Count; i++)
            {
                numerics[i].Value = formMain.LeadVocalsInterProb[phraseIndex][currentThought][i];
            }
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            //formMain.LeadVocalsInterProb[formMain.CurrentPhrase][formMain.LeadVocalsCurrentThought[formMain.CurrentPhrase]].Clear();
            //for (int i = 0; i < numerics.Length; i++)
            //    formMain.LeadVocalsInterProb[formMain.CurrentPhrase][formMain.LeadVocalsCurrentThought[formMain.CurrentPhrase]].Add((int)numerics[i].Value);

            //if (formMain.CurrentPhrase == formMain.PhrasesAllowed.Length)
            //{
            //    for (int i = 0; i < formMain.PhrasesAllowed.Length; i++)
            //    {
            //        formMain.LeadVocalsInterProb[i][formMain.LeadVocalsCurrentThought[formMain.CurrentPhrase]].Clear();
            //        for (int j = 0; j < numerics.Length; j++)
            //        {
            //            formMain.LeadVocalsInterProb[i][formMain.LeadVocalsCurrentThought[formMain.CurrentPhrase]].Add((int)numerics[j].Value);
            //        }
            //    }
            //}

            Toolbox.MainValueChangeProb(formMain.LeadVocalsInterProb, numerics, formMain.CurrentPhrase, formMain.LeadVocalsCurrentThought[formMain.CurrentPhrase], formMain.PhrasesAllowed.Length, formMain.LeadVocalsDistinctThoughtsCount[formMain.CurrentPhrase]);
            Hide();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void LeadVocalsInterProb_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }
    }
}
