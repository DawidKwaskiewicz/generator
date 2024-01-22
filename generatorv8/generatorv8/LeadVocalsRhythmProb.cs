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
    public partial class LeadVocalsRhythmProb : Form
    {
        private readonly Main formMain;
        public readonly NumericUpDown[] numerics;
        public LeadVocalsRhythmProb(Main formMain)
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
        public void UpdateRhythmProb(int phraseIndex, int thoughtIndex)
        {
            for (int i = 0; i < formMain.LeadVocalsRhythmProb[phraseIndex][thoughtIndex].Count; i++)
                numerics[i].Value = formMain.LeadVocalsRhythmProb[phraseIndex][thoughtIndex][i];
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            //formMain.LeadVocalsRhythmProb[formMain.CurrentPhrase][formMain.LeadVocalsCurrentThought[formMain.CurrentPhrase]].Clear();
            //for (int i = 0; i < numerics.Length; i++)
            //    formMain.LeadVocalsRhythmProb[formMain.CurrentPhrase][formMain.LeadVocalsCurrentThought[formMain.CurrentPhrase]].Add((int)numerics[i].Value);

            //if (formMain.CurrentPhrase == formMain.PhrasesAllowed.Length)
            //{
            //    for (int i = 0; i < formMain.PhrasesAllowed.Length; i++)
            //    {
            //        formMain.LeadVocalsRhythmProb[i][formMain.LeadVocalsCurrentThought[formMain.CurrentPhrase]].Clear();
            //        for (int j = 0; j < numerics.Length; j++)
            //        {
            //            formMain.LeadVocalsRhythmProb[i][formMain.LeadVocalsCurrentThought[formMain.CurrentPhrase]].Add((int)numerics[j].Value);
            //        }
            //    }
            //}
            Toolbox.MainValueChangeProb(formMain.LeadVocalsRhythmProb, numerics, formMain.CurrentPhrase, formMain.LeadVocalsCurrentThought[formMain.CurrentPhrase], formMain.PhrasesAllowed.Length, formMain.LeadVocalsDistinctThoughtsCount[formMain.CurrentPhrase]);
            Hide();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Hide();
        }
        private void LeadVocalsRhythmProb_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }
    }
}
