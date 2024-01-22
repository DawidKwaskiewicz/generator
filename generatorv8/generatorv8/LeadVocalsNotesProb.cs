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
    public partial class LeadVocalsNotesProb : Form
    {
        private readonly Main formMain;
        public readonly Label[] labels;
        public readonly NumericUpDown[] numerics;
        public string[] labelTexts;
        public LeadVocalsNotesProb(Main formMain)
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
        public void UpdateNotes(int phraseIndex, int thoughtIndex)
        {
            string key = Toolbox.NormalizeNote(formMain.Key[phraseIndex]);
            string scale = formMain.Scales[phraseIndex];
            string[] roots = Toolbox.GetNotesInScale(scale, key);
            for (int i = 0; i < labels.Length; i++)
            {
                roots[i] = Toolbox.UnnormalizeNote(roots[i], true);
                labels[i].Text = labelTexts[i] + roots[i] + ")";
            }
            for (int i = 0; i < numerics.Length; i++)
            {
                numerics[i].Value = formMain.LeadVocalsNotesProb[phraseIndex][thoughtIndex][i];
            }
        }
        private void buttonApply_Click(object sender, EventArgs e)
        {
            //formMain.LeadVocalsNotesProb[formMain.CurrentPhrase][formMain.LeadVocalsCurrentThought[formMain.CurrentPhrase]].Clear();
            //for (int i = 0; i < numerics.Length; i++)
            //    formMain.LeadVocalsNotesProb[formMain.CurrentPhrase][formMain.LeadVocalsCurrentThought[formMain.CurrentPhrase]].Add((int)numerics[i].Value);

            //if (formMain.CurrentPhrase == formMain.PhrasesAllowed.Length)
            //{
            //    for (int i = 0; i < formMain.PhrasesAllowed.Length; i++)
            //    {
            //        formMain.LeadVocalsNotesProb[i][formMain.LeadVocalsCurrentThought[formMain.CurrentPhrase]].Clear();
            //        for (int j = 0; j < numerics.Length; j++)
            //        {
            //            formMain.LeadVocalsNotesProb[i][formMain.LeadVocalsCurrentThought[formMain.CurrentPhrase]].Add((int)numerics[j].Value);
            //        }
            //    }
            //}

            Toolbox.MainValueChangeProb(formMain.LeadVocalsNotesProb, numerics, formMain.CurrentPhrase, formMain.LeadVocalsCurrentThought[formMain.CurrentPhrase], formMain.PhrasesAllowed.Length, formMain.LeadVocalsDistinctThoughtsCount[formMain.CurrentPhrase]);
            Hide();
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void LeadVocalsNotesProb_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }
    }
}
