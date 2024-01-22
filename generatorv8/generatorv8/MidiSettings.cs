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
    public partial class MidiSettings : Form
    {
        private readonly Main formMain;
        public MidiSettings(Main formMain)
        {
            InitializeComponent();
            this.formMain = formMain;
        }
        public void UpdateMidiSettings(int phraseIndex)
        {
            checkTransposeLead.Checked = formMain.TransposeLead[phraseIndex];
            checkTransposeRhythm.Checked = formMain.TransposeRhythm[phraseIndex];
            checkTransposeBass.Checked = formMain.TransposeBass[phraseIndex];
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            formMain.TransposeLead[formMain.CurrentPhrase] = checkTransposeLead.Checked;
            formMain.TransposeRhythm[formMain.CurrentPhrase] = checkTransposeRhythm.Checked;
            formMain.TransposeBass[formMain.CurrentPhrase] = checkTransposeBass.Checked;

            if (formMain.CurrentPhrase == formMain.PhrasesAllowed.Length)
            {
                for (int i = 0; i < formMain.PhrasesAllowed.Length; i++)
                {
                    formMain.TransposeLead[i] = checkTransposeLead.Checked;
                    formMain.TransposeRhythm[i] = checkTransposeRhythm.Checked;
                    formMain.TransposeBass[i] = checkTransposeBass.Checked;
                }
            }
            Hide();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void MidiSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }
    }
}
