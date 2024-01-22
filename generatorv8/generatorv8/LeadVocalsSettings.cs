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
    public partial class LeadVocalsSettings : Form
    {
        //public string PhrasesAllowed { get; set; }
        //public int CurrentThought { get; set; }
        //public List<int>[] StepProb { get; set; }
        //public List<int>[] IntervalProb { get; set; }
        //public List<int>[] RhythmProb { get; set; }
        //public double[][] DrawWeights { get; set; }
        //string[] LowestNote { get; set; }
        //string[] HighestNote { get; set; }
        //decimal[][] BreakProbs { get; set; }
        //int[] ThoughtLength { get; set; }
        //double[] MelodyVariation { get; set; }
        //double[] RhythmVariation { get; set; }
        //double[][] VarSection { get; set; }
        //double[] StartsOnChord { get; set; }
        //List<int>[] StartsOnChordComp { get; set; }
        //double[] EndsOnChord { get; set; }
        //List<int>[] EndsOnChordComp { get; set; }
        //List<double>[] CurrentChordBias { get; set; }
        //bool[] ThoughtsRising { get; set; }
        //int DelayInProgressions { get; set; }
        //int[] ThoughtDirection { get; set; }
        //double[] DirStrength { get; set; }
        //double[] RisingOverlap { get; set; }
        //string[] PitchWeightShape { get; set; }
        //double[] BoomerangTurningPoint { get; set; }
        //decimal[] GlissChance { get; set; }
        //decimal[] LastSyllableChance { get; set; }
        //decimal[] LastSyllableStart { get; set; }
        //decimal[] EndRestLength { get; set; }
        //decimal[] StartRestChance { get; set; }
        //decimal[] MaxStartRestSize { get; set; }
        //decimal[] RestChance { get; set; }
        //decimal[] StartRestUnit { get; set; }
        //private readonly string allPhrases = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private readonly Main formMain;
        public LeadVocalsNotesProb formNotesProb;
        public LeadVocalsRhythmProb formRhythmProb;
        public LeadVocalsInterProb formInterProb;
        //private bool startsForceAnyEnabled;
        //private bool startsForceSpecEnabled;
        //private bool endsForceAnyEnabled;
        //private bool endsForceSpecEnabled;
        private Main.ParamsSet defaults;
        private bool updatingControls = false;
        public LeadVocalsSettings(Main formMain)
        {
            InitializeComponent();
            this.formMain = formMain;
            formNotesProb = new(formMain);
            formNotesProb.Owner = this;
            formRhythmProb = new(formMain);
            formRhythmProb.Owner = this;
            formInterProb = new(formMain);
            formInterProb.Owner = this;
            //startsForceAnyEnabled = checkStartsForceAnyComponent.Enabled;
            //startsForceSpecEnabled = checkStartsForceSpecificComponent.Enabled;
            //endsForceAnyEnabled = checkEndsForceAnyComponent.Enabled;
            //endsForceSpecEnabled = checkEndsForceSpecificComponent.Enabled;
            //PhrasesAllowed = allPhrases[0..(int)numericDistinctPhrases.Value];
            defaults = formMain.defaults;
        }
        private void textBoxPattern_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!char.IsControl(e.KeyChar) && !PhrasesAllowed.Contains(char.ToUpper(e.KeyChar)))
            if (!char.IsControl(e.KeyChar) && !formMain.allThoughts[..formMain.LeadVocalsDistinctThoughtsCount[formMain.CurrentPhrase]].Contains(char.ToUpper(e.KeyChar)))
            {
                e.Handled = true;
            }
        }
        private void textBoxPattern_TextChanged(object sender, EventArgs e)
        {

            formMain.LeadVocalsThoughtPattern[formMain.CurrentPhrase] = textBoxPattern.Text;
            if (formMain.CurrentPhrase == formMain.PhrasesAllowed.Length)
                for (int i = 0; i < formMain.PhrasesAllowed.Length; i++)
                    formMain.LeadVocalsThoughtPattern[i] = textBoxPattern.Text;
        }

        private void buttonRhythmProb_Click(object sender, EventArgs e)
        {
            formRhythmProb.Show();
        }

        private void buttonNotesProb_Click(object sender, EventArgs e)
        {
            formNotesProb.Show();
            // UpdateNotes(formMain.CurrentPhrase);
        }
        //public void UpdateNotes(int phraseIndex)
        //{
        //    // string key = Toolbox.NormalizeNote(keyMain);
        //    // string scale = scaleMain;
        //    string key = Toolbox.NormalizeNote(formMain.Key[phraseIndex]);
        //    string scale = formMain.Scales[phraseIndex];
        //    string[] roots = Toolbox.GetNotesInScale(scale, key);
        //    for (int i = 0; i < formNotesProb.labels.Length; i++)
        //    {
        //        roots[i] = Toolbox.UnnormalizeNote(roots[i], true);
        //        formNotesProb.labels[i].Text = formNotesProb.labelTexts[i] + roots[i] + ")";
        //    }
        //    for (int i = 0; i < formNotesProb.numerics.Length; i++)
        //    {
        //        formNotesProb.numerics[i].Value = formMain.LeadGuitarNotesProb[phraseIndex][i];
        //    }
        //}

        private void buttonInterProb_Click(object sender, EventArgs e)
        {
            formInterProb.Show();
        }

        private void comboBoxCurrentThought_SelectedIndexChanged(object sender, EventArgs e)
        {
            formMain.LeadVocalsCurrentThought[formMain.CurrentPhrase] = comboBoxCurrentThought.SelectedIndex;
            UpdateControls(formMain.CurrentPhrase, formMain.LeadVocalsCurrentThought[formMain.CurrentPhrase]);
        }
        public void UpdateControls(int currentPhrase, int currentThought)
        {
            updatingControls = true;
            // global vars
            textBoxPattern.Text = formMain.LeadVocalsThoughtPattern[currentPhrase];
            comboBoxCurrentThought.SelectedIndex = currentThought;
            numericDistinctPhrases.Value = formMain.LeadVocalsDistinctThoughtsCount[currentPhrase];
            numericDelayInProgressions.Value = formMain.LeadVocalsDelayInProgressions[currentPhrase];
            // prob forms
            formRhythmProb.UpdateRhythmProb(currentPhrase, currentThought);
            formNotesProb.UpdateNotes(currentPhrase, currentThought);
            formInterProb.UpdateInterProb(currentPhrase, currentThought);
            // % chance to force new note on
            numericBarProb.Value = formMain.LeadVocalsProbBar[currentPhrase][currentThought] * 100;
            numericStrongBeatProb.Value = formMain.LeadVocalsProbStrongBeat[currentPhrase][currentThought] * 100;
            numericWeakBeatProb.Value = formMain.LeadVocalsProbWeakBeat[currentPhrase][currentThought] * 100;
            // Current chord bias
            //numericChordBiasPrime.Value = (decimal)(formMain.LeadVocalsCurrentChordBias[currentPhrase][currentThought][0] * 100);
            //numericChordBiasThird.Value = (decimal)(formMain.LeadVocalsCurrentChordBias[currentPhrase][currentThought][1] * 100);
            //numericChordBiasFifth.Value = (decimal)(formMain.LeadVocalsCurrentChordBias[currentPhrase][currentThought][2] * 100);
            numericChordBiasPrime.Value = (decimal)formMain.LeadVocalsCurrentChordBias[currentPhrase][currentThought][0];
            numericChordBiasThird.Value = (decimal)formMain.LeadVocalsCurrentChordBias[currentPhrase][currentThought][1];
            numericChordBiasFifth.Value = (decimal)formMain.LeadVocalsCurrentChordBias[currentPhrase][currentThought][2];
            // Thought direction
            comboBoxThoughtDirection.SelectedIndex = formMain.LeadVocalsThoughtDirection[currentPhrase][currentThought];
            numericDirectionStrength.Value = (decimal)(formMain.LeadVocalsDirectionStrength[currentPhrase][currentThought]);
            numericBoomerangTurningPoint.Value = (decimal)(formMain.LeadVocalsBoomerangTurningPoint[currentPhrase][currentThought] * 100);
            // Thought starts on current chord component
            numericStartsForceAnyComponent.Value = (decimal)(formMain.LeadVocalsStartsOnChord[currentPhrase][currentThought] * 100);
            numericStartsForcePrime.Value = formMain.LeadVocalsStartsOnChordComp[currentPhrase][currentThought][0];
            numericStartsForceThird.Value = formMain.LeadVocalsStartsOnChordComp[currentPhrase][currentThought][1];
            numericStartsForceFifth.Value = formMain.LeadVocalsStartsOnChordComp[currentPhrase][currentThought][2];
            if (formMain.LeadVocalsStartsOnChord[currentPhrase][currentThought] < 0) checkStartsForceAnyComponent.Checked = false;
            else checkStartsForceAnyComponent.Checked = true;
            if (formMain.LeadVocalsStartsOnChordComp[currentPhrase][currentThought][0] < 0) checkStartsForceSpecificComponent.Checked = false;
            else checkStartsForceSpecificComponent.Checked = true;
            //if (formMain.LeadVocalsStartsOnChord[currentPhrase][currentThought] < 0)
            //    checkStartsForceAnyComponent.Checked = false;
            //else
            //{
            //    checkStartsForceAnyComponent.Checked = true;
            //    //numericStartsForceAnyComponent.Value = (decimal)(formMain.LeadVocalsStartsOnChord[currentPhrase][currentThought] * 100);
            //    if (formMain.LeadVocalsStartsOnChordComp[currentPhrase][currentThought][0] < 0)
            //        checkStartsForceSpecificComponent.Checked = false;
            //    else
            //    {
            //        checkStartsForceSpecificComponent.Checked = true;
            //        //numericStartsForcePrime.Value = formMain.LeadVocalsStartsOnChordComp[currentPhrase][currentThought][0];
            //        //numericStartsForceThird.Value = formMain.LeadVocalsStartsOnChordComp[currentPhrase][currentThought][1];
            //        //numericStartsForceFifth.Value = formMain.LeadVocalsStartsOnChordComp[currentPhrase][currentThought][2];
            //    }
            //}
            // Thought ends on current chord component
            numericEndsForceAnyComponent.Value = (decimal)(formMain.LeadVocalsEndsOnChord[currentPhrase][currentThought] * 100);
            numericEndsForcePrime.Value = formMain.LeadVocalsEndsOnChordComp[currentPhrase][currentThought][0];
            numericEndsForceThird.Value = formMain.LeadVocalsEndsOnChordComp[currentPhrase][currentThought][1];
            numericEndsForceFifth.Value = formMain.LeadVocalsEndsOnChordComp[currentPhrase][currentThought][2];
            if (formMain.LeadVocalsEndsOnChord[currentPhrase][currentThought] < 0) checkEndsForceAnyComponent.Checked = false;
            else checkEndsForceAnyComponent.Checked = true;
            if (formMain.LeadVocalsEndsOnChordComp[currentPhrase][currentThought][0] < 0) checkEndsForceSpecificComponent.Checked = false;
            else checkEndsForceSpecificComponent.Checked = true;
            //if (formMain.LeadVocalsEndsOnChord[currentPhrase][currentThought] < 0)
            //    checkEndsForceAnyComponent.Checked = false;
            //else
            //{
            //    checkEndsForceAnyComponent.Checked = true;
            //    numericEndsForceAnyComponent.Value = (decimal)(formMain.LeadVocalsEndsOnChord[currentPhrase][currentThought] * 100);
            //    if (formMain.LeadVocalsEndsOnChordComp[currentPhrase][currentThought][0] < 0)
            //        checkEndsForceSpecificComponent.Checked = false;
            //    else
            //    {
            //        checkEndsForceSpecificComponent.Checked = true;
            //        numericEndsForcePrime.Value = formMain.LeadVocalsEndsOnChordComp[currentPhrase][currentThought][0];
            //        numericEndsForceThird.Value = formMain.LeadVocalsEndsOnChordComp[currentPhrase][currentThought][1];
            //        numericEndsForceFifth.Value = formMain.LeadVocalsEndsOnChordComp[currentPhrase][currentThought][2];
            //    }
            //}
            // Thoughts rising
            if (formMain.LeadVocalsThoughtsRising[currentPhrase])
            {
                checkThoughtsRising.Checked = true;
                numericThoughtOverlap.Value = (decimal)(formMain.LeadVocalsRisingOverlap[currentPhrase] * 100);
            }
            else checkThoughtsRising.Checked = false;
            // Miscellaneous
            numericThoughtLengthInProgressions.Value = formMain.LeadVocalsThoughtLength[currentPhrase][currentThought];
            numericEndRestLengthInQuarterNotes.Value = formMain.LeadVocalsEndRestLength[currentPhrase][currentThought];
            numericRestChance.Value = formMain.LeadVocalsRestChance[currentPhrase][currentThought] * 100;
            numericGlissChance.Value = formMain.LeadVocalsGlissChance[currentPhrase][currentThought] * 100;
            comboBoxPitchWeightShape.SelectedIndex = formMain.LeadVocalsWeightShape[currentPhrase][currentThought];
            // Ambitus
            comboBoxLowOctave.SelectedIndex = formMain.LeadVocalsLowOctaveIndex[currentPhrase][currentThought];
            comboBoxLowNote.SelectedIndex = formMain.LeadVocalsLowNoteIndex[currentPhrase][currentThought];
            comboBoxHighOctave.SelectedIndex = formMain.LeadVocalsHighOctaveIndex[currentPhrase][currentThought];
            comboBoxHighNote.SelectedIndex = formMain.LeadVocalsHighNoteIndex[currentPhrase][currentThought];
            // Weights
            numericWeightPitch.Value = formMain.LeadVocalsDrawWeights[currentPhrase][currentThought][0];
            numericWeightStep.Value = formMain.LeadVocalsDrawWeights[currentPhrase][currentThought][1];
            numericWeightInterval.Value = formMain.LeadVocalsDrawWeights[currentPhrase][currentThought][2];
            // Last syllable
            numericLastSyllableStart.Value = formMain.LeadVocalsLastSyllableStart[currentPhrase][currentThought] * 100;
            numericLastSyllableChance.Value = formMain.LeadVocalsLastSyllableChance[currentPhrase][currentThought] * 100;
            // Start rest
            comboBoxStartRestUnit.SelectedIndex = formMain.LeadVocalsStartRestUnitIndex[currentPhrase][currentThought];
            numericStartRestChance.Value = formMain.LeadVocalsStartRestChance[currentPhrase][currentThought] * 100;
            numericStartRestMaxSize.Value = formMain.LeadVocalsMaxStartRestSize[currentPhrase][currentThought];
            // Variation
            numericMelodyVariation.Value = (decimal)formMain.LeadVocalsMelodyVariation[currentPhrase][currentThought] * 100;
            numericRhythmVariation.Value = (decimal)formMain.LeadVocalsRhythmVariation[currentPhrase][currentThought] * 100;
            numericVariationSectionStart.Value = (decimal)formMain.LeadVocalsVariationSection[currentPhrase][currentThought][0] * 100;
            numericVariationSectionEnd.Value = (decimal)formMain.LeadVocalsVariationSection[currentPhrase][currentThought][1] * 100;
            updatingControls = false;
        }
        //private void SetDefaults(int thoughtIndex)
        //{
        //    int phraseIndex = formMain.CurrentPhrase;
        //    //LeadVocalsDelayInProgressions[phraseIndex] = defaults.LeadVocalsDelayInProgressions;
        //    //LeadVocalsThoughtPattern[phraseIndex] = defaults.LeadVocalsThoughtPattern;
        //    LeadVocalsNotesProb[phraseIndex][thoughtIndex] = defaults.LeadVocalsNotesProb;
        //    LeadVocalsInterProb[phraseIndex][thoughtIndex] = defaults.LeadVocalsInterProb;
        //    LeadVocalsRhythmProb[phraseIndex][thoughtIndex] = defaults.LeadVocalsRhythmProb;
        //    LeadVocalsDrawWeights[phraseIndex][thoughtIndex] = defaults.LeadVocalsDrawWeights;
        //    LeadVocalsLowOctaveIndex[phraseIndex][thoughtIndex] = defaults.LeadVocalsLowOctaveIndex;
        //    LeadVocalsLowNoteIndex[phraseIndex][thoughtIndex] = defaults.LeadVocalsLowNoteIndex;
        //    LeadVocalsHighOctaveIndex[phraseIndex][thoughtIndex] = defaults.LeadVocalsHighOctaveIndex;
        //    LeadVocalsHighNoteIndex[phraseIndex][thoughtIndex] = defaults.LeadVocalsHighNoteIndex;
        //    LeadVocalsProbBar[phraseIndex][thoughtIndex] = defaults.LeadVocalsProbBar;
        //    LeadVocalsProbStrongBeat[phraseIndex][thoughtIndex] = defaults.LeadVocalsProbStrongBeat;
        //    LeadVocalsProbWeakBeat[phraseIndex][thoughtIndex] = defaults.LeadVocalsProbWeakBeat;
        //    LeadVocalsThoughtLength[phraseIndex][thoughtIndex] = defaults.LeadVocalsThoughtLength;
        //    LeadVocalsMelodyVariation[phraseIndex][thoughtIndex] = defaults.LeadVocalsMelodyVariation;
        //    LeadVocalsRhythmVariation[phraseIndex][thoughtIndex] = defaults.LeadVocalsRhythmVariation;
        //    LeadVocalsVariationSection[phraseIndex][thoughtIndex] = defaults.LeadVocalsVariationSection;
        //    LeadVocalsStartsOnChord[phraseIndex][thoughtIndex] = defaults.LeadVocalsStartsOnChord;
        //    LeadVocalsStartsOnChordComp[phraseIndex][thoughtIndex] = defaults.LeadVocalsStartsOnChordComp;
        //    LeadVocalsEndsOnChord[phraseIndex][thoughtIndex] = defaults.LeadVocalsEndsOnChord;
        //    LeadVocalsEndsOnChordComp[phraseIndex][thoughtIndex] = defaults.LeadVocalsEndsOnChordComp;
        //    LeadVocalsCurrentChordBias[phraseIndex][thoughtIndex] = defaults.LeadVocalsCurrentChordBias;
        //    LeadVocalsThoughtsRising[phraseIndex][thoughtIndex] = defaults.LeadVocalsThoughtsRising;
        //    LeadVocalsThoughtDirection[phraseIndex][thoughtIndex] = defaults.LeadVocalsThoughtDirection;
        //    LeadVocalsDirectionStrength[phraseIndex][thoughtIndex] = defaults.LeadVocalsDirectionStrength;
        //    LeadVocalsRisingOverlap[phraseIndex][thoughtIndex] = defaults.LeadVocalsRisingOverlap;
        //    LeadVocalsWeightShape[phraseIndex][thoughtIndex] = defaults.LeadVocalsWeightShape;
        //    LeadVocalsBoomerangTurningPoint[phraseIndex][thoughtIndex] = defaults.LeadVocalsBoomerangTurningPoint;
        //    LeadVocalsGlissChance[phraseIndex][thoughtIndex] = defaults.LeadVocalsGlissChance;
        //    LeadVocalsLastSyllableChance[phraseIndex][thoughtIndex] = defaults.LeadVocalsLastSyllableChance;
        //    LeadVocalsLastSyllableStart[phraseIndex][thoughtIndex] = defaults.LeadVocalsLastSyllableStart;
        //    LeadVocalsEndRestLength[phraseIndex][thoughtIndex] = defaults.LeadVocalsEndRestLength;
        //    LeadVocalsStartRestChance[phraseIndex][thoughtIndex] = defaults.LeadVocalsStartRestChance;
        //    LeadVocalsMaxStartRestSize[phraseIndex][thoughtIndex] = defaults.LeadVocalsMaxStartRestSize;
        //    LeadVocalsRestChance[phraseIndex][thoughtIndex] = defaults.LeadVocalsRestChance;
        //    LeadVocalsStartRestUnitIndex[phraseIndex][thoughtIndex] = defaults.LeadVocalsStartRestUnitIndex;
        //}

        private void buttonResetDefaults_Click(object sender, EventArgs e)
        {
            if (comboBoxCurrentThought.SelectedIndex != comboBoxCurrentThought.Items.Count - 1) formMain.SetDefaultsLeadVocals(formMain.LeadVocalsCurrentThought[formMain.CurrentPhrase]);
            else for (int i = 0; i < comboBoxCurrentThought.Items.Count; i++) formMain.SetDefaultsLeadVocals(i);
            UpdateControls(formMain.CurrentPhrase, formMain.LeadVocalsCurrentThought[formMain.CurrentPhrase]);
        }

        private void numericDistinctPhrases_ValueChanged(object sender, EventArgs e)
        {
            int selectedIndex = comboBoxCurrentThought.SelectedIndex;
            if (comboBoxCurrentThought.SelectedIndex == comboBoxCurrentThought.Items.Count - 1) selectedIndex = -1;
            formMain.LeadVocalsDistinctThoughtsCount[formMain.CurrentPhrase] = (int)numericDistinctPhrases.Value;
            //string thoughtsAllowed = allPhrases[0..(int)numericDistinctPhrases.Value];
            string thoughtsAllowed = formMain.allThoughts[0..formMain.LeadVocalsDistinctThoughtsCount[formMain.CurrentPhrase]];
            comboBoxCurrentThought.Items.Clear();
            for (int i = 0; i < numericDistinctPhrases.Value; i++)
            {
                comboBoxCurrentThought.Items.Add(thoughtsAllowed[i]);
            }
            comboBoxCurrentThought.Items.Add("All");
            if (selectedIndex == -1) comboBoxCurrentThought.SelectedIndex = thoughtsAllowed.Length;
            else if (selectedIndex > -1 && selectedIndex < thoughtsAllowed.Length) comboBoxCurrentThought.SelectedIndex = selectedIndex;
            else comboBoxCurrentThought.SelectedIndex = 0;

            string text = textBoxPattern.Text;
            byte[] asciiBytes = Encoding.ASCII.GetBytes(text);
            int maxValue = 64 + thoughtsAllowed.Length;
            for (int i = asciiBytes.Length - 1; i > -1; i--)
            {
                if (asciiBytes[i] > maxValue) text = text.Remove(i, 1);
            }
            textBoxPattern.Text = text;
        }

        private void numericDelayInProgressions_ValueChanged(object sender, EventArgs e)
        {
            formMain.LeadVocalsDelayInProgressions[formMain.CurrentPhrase] = (int)numericDelayInProgressions.Value;
            if (formMain.CurrentPhrase == formMain.PhrasesAllowed.Length)
                for (int i = 0; i < formMain.PhrasesAllowed.Length; i++)
                    formMain.LeadVocalsDelayInProgressions[i] = (int)numericDelayInProgressions.Value;
        }
        // % CHANCE TO FORCE NEW NOTE ON
        private void numericBarProb_ValueChanged(object sender, EventArgs e)
        {
            //formMain.LeadVocalsProbBar[formMain.CurrentPhrase][formMain.LeadVocalsCurrentThought[formMain.CurrentPhrase]] = (int)numericBarProb.Value;
            //if (formMain.CurrentPhrase == formMain.PhrasesAllowed.Length)
            //    if (formMain.LeadVocalsCurrentThought[formMain.CurrentPhrase] == formMain.LeadVocalsDistinctThoughtsCount[formMain.CurrentPhrase])
            //        for (int i = 0; i < formMain.PhrasesAllowed.Length; i++)
            //            for (int j = 0; j < formMain.LeadVocalsDistinctThoughtsCount[formMain.CurrentPhrase]; j++)
            //                formMain.LeadVocalsProbBar[i][j] = (int)numericBarProb.Value;
            //    else for (int i = 0; i < formMain.PhrasesAllowed.Length; i++)
            //        formMain.LeadVocalsProbBar[i][formMain.LeadVocalsCurrentThought[formMain.CurrentPhrase]] = (int)numericBarProb.Value;
            //else if (formMain.LeadVocalsCurrentThought[formMain.CurrentPhrase] == formMain.LeadVocalsDistinctThoughtsCount[formMain.CurrentPhrase])
            //    for (int j = 0; j < formMain.LeadVocalsDistinctThoughtsCount[formMain.CurrentPhrase]; j++)
            //        formMain.LeadVocalsProbBar[formMain.CurrentPhrase][j] = (int)numericBarProb.Value;
            MainValueChange(formMain.LeadVocalsProbBar, numericBarProb.Value * 0.01M);
        }
        private void MainValueChange(decimal[][] valuesStored, decimal newValue)
        {
            //decimal newValue = numeric.Value * multiplier;
            valuesStored[formMain.CurrentPhrase][formMain.LeadVocalsCurrentThought[formMain.CurrentPhrase]] = newValue;
            if (formMain.CurrentPhrase == formMain.PhrasesAllowed.Length)
                if (formMain.LeadVocalsCurrentThought[formMain.CurrentPhrase] == formMain.LeadVocalsDistinctThoughtsCount[formMain.CurrentPhrase])
                    for (int i = 0; i < formMain.PhrasesAllowed.Length; i++)
                        for (int j = 0; j < formMain.LeadVocalsDistinctThoughtsCount[formMain.CurrentPhrase]; j++)
                            valuesStored[i][j] = newValue;
                else for (int i = 0; i < formMain.PhrasesAllowed.Length; i++)
                        valuesStored[i][formMain.LeadVocalsCurrentThought[formMain.CurrentPhrase]] = newValue;
            else if (formMain.LeadVocalsCurrentThought[formMain.CurrentPhrase] == formMain.LeadVocalsDistinctThoughtsCount[formMain.CurrentPhrase])
                for (int j = 0; j < formMain.LeadVocalsDistinctThoughtsCount[formMain.CurrentPhrase]; j++)
                    valuesStored[formMain.CurrentPhrase][j] = newValue;
        }
        private void MainValueChange(List<double>[][] valuesStored, double newValue, int index)
        {
            valuesStored[formMain.CurrentPhrase][formMain.LeadVocalsCurrentThought[formMain.CurrentPhrase]][index] = newValue;
            if (formMain.CurrentPhrase == formMain.PhrasesAllowed.Length)
                if (formMain.LeadVocalsCurrentThought[formMain.CurrentPhrase] == formMain.LeadVocalsDistinctThoughtsCount[formMain.CurrentPhrase])
                    for (int i = 0; i < formMain.PhrasesAllowed.Length; i++)
                        for (int j = 0; j < formMain.LeadVocalsDistinctThoughtsCount[formMain.CurrentPhrase]; j++)
                            valuesStored[i][j][index] = newValue;
                else for (int i = 0; i < formMain.PhrasesAllowed.Length; i++)
                        valuesStored[i][formMain.LeadVocalsCurrentThought[formMain.CurrentPhrase]][index] = newValue;
            else if (formMain.LeadVocalsCurrentThought[formMain.CurrentPhrase] == formMain.LeadVocalsDistinctThoughtsCount[formMain.CurrentPhrase])
                for (int j = 0; j < formMain.LeadVocalsDistinctThoughtsCount[formMain.CurrentPhrase]; j++)
                    valuesStored[formMain.CurrentPhrase][j][index] = newValue;
        }
        private void MainValueChange(List<int>[][] valuesStored, int newValue, int index)
        {
            valuesStored[formMain.CurrentPhrase][formMain.LeadVocalsCurrentThought[formMain.CurrentPhrase]][index] = newValue;
            if (formMain.CurrentPhrase == formMain.PhrasesAllowed.Length)
                if (formMain.LeadVocalsCurrentThought[formMain.CurrentPhrase] == formMain.LeadVocalsDistinctThoughtsCount[formMain.CurrentPhrase])
                    for (int i = 0; i < formMain.PhrasesAllowed.Length; i++)
                        for (int j = 0; j < formMain.LeadVocalsDistinctThoughtsCount[formMain.CurrentPhrase]; j++)
                            valuesStored[i][j][index] = newValue;
                else for (int i = 0; i < formMain.PhrasesAllowed.Length; i++)
                        valuesStored[i][formMain.LeadVocalsCurrentThought[formMain.CurrentPhrase]][index] = newValue;
            else if (formMain.LeadVocalsCurrentThought[formMain.CurrentPhrase] == formMain.LeadVocalsDistinctThoughtsCount[formMain.CurrentPhrase])
                for (int j = 0; j < formMain.LeadVocalsDistinctThoughtsCount[formMain.CurrentPhrase]; j++)
                    valuesStored[formMain.CurrentPhrase][j][index] = newValue;
        }
        private void MainValueChange(int[][] valuesStored, int comboBoxIndex)
        {
            valuesStored[formMain.CurrentPhrase][formMain.LeadVocalsCurrentThought[formMain.CurrentPhrase]] = comboBoxIndex;
            if (formMain.CurrentPhrase == formMain.PhrasesAllowed.Length)
                if (formMain.LeadVocalsCurrentThought[formMain.CurrentPhrase] == formMain.LeadVocalsDistinctThoughtsCount[formMain.CurrentPhrase])
                    for (int i = 0; i < formMain.PhrasesAllowed.Length; i++)
                        for (int j = 0; j < formMain.LeadVocalsDistinctThoughtsCount[formMain.CurrentPhrase]; j++)
                            valuesStored[i][j] = comboBoxIndex;
                else for (int i = 0; i < formMain.PhrasesAllowed.Length; i++)
                        valuesStored[i][formMain.LeadVocalsCurrentThought[formMain.CurrentPhrase]] = comboBoxIndex;
            else if (formMain.LeadVocalsCurrentThought[formMain.CurrentPhrase] == formMain.LeadVocalsDistinctThoughtsCount[formMain.CurrentPhrase])
                for (int j = 0; j < formMain.LeadVocalsDistinctThoughtsCount[formMain.CurrentPhrase]; j++)
                    valuesStored[formMain.CurrentPhrase][j] = comboBoxIndex;
        }
        private void MainValueChange(double[][] valuesStored, double newValue)
        {
            valuesStored[formMain.CurrentPhrase][formMain.LeadVocalsCurrentThought[formMain.CurrentPhrase]] = newValue;
            if (formMain.CurrentPhrase == formMain.PhrasesAllowed.Length)
                if (formMain.LeadVocalsCurrentThought[formMain.CurrentPhrase] == formMain.LeadVocalsDistinctThoughtsCount[formMain.CurrentPhrase])
                    for (int i = 0; i < formMain.PhrasesAllowed.Length; i++)
                        for (int j = 0; j < formMain.LeadVocalsDistinctThoughtsCount[formMain.CurrentPhrase]; j++)
                            valuesStored[i][j] = newValue;
                else for (int i = 0; i < formMain.PhrasesAllowed.Length; i++)
                        valuesStored[i][formMain.LeadVocalsCurrentThought[formMain.CurrentPhrase]] = newValue;
            else if (formMain.LeadVocalsCurrentThought[formMain.CurrentPhrase] == formMain.LeadVocalsDistinctThoughtsCount[formMain.CurrentPhrase])
                for (int j = 0; j < formMain.LeadVocalsDistinctThoughtsCount[formMain.CurrentPhrase]; j++)
                    valuesStored[formMain.CurrentPhrase][j] = newValue;
        }
        private void MainValueChange(bool[][] valuesStored, bool newValue)
        {
            valuesStored[formMain.CurrentPhrase][formMain.LeadVocalsCurrentThought[formMain.CurrentPhrase]] = newValue;
            if (formMain.CurrentPhrase == formMain.PhrasesAllowed.Length)
                if (formMain.LeadVocalsCurrentThought[formMain.CurrentPhrase] == formMain.LeadVocalsDistinctThoughtsCount[formMain.CurrentPhrase])
                    for (int i = 0; i < formMain.PhrasesAllowed.Length; i++)
                        for (int j = 0; j < formMain.LeadVocalsDistinctThoughtsCount[formMain.CurrentPhrase]; j++)
                            valuesStored[i][j] = newValue;
                else for (int i = 0; i < formMain.PhrasesAllowed.Length; i++)
                        valuesStored[i][formMain.LeadVocalsCurrentThought[formMain.CurrentPhrase]] = newValue;
            else if (formMain.LeadVocalsCurrentThought[formMain.CurrentPhrase] == formMain.LeadVocalsDistinctThoughtsCount[formMain.CurrentPhrase])
                for (int j = 0; j < formMain.LeadVocalsDistinctThoughtsCount[formMain.CurrentPhrase]; j++)
                    valuesStored[formMain.CurrentPhrase][j] = newValue;
        }
        private void MainValueChange(int[][][] valuesStored, int newValue, int index)
        {
            valuesStored[formMain.CurrentPhrase][formMain.LeadVocalsCurrentThought[formMain.CurrentPhrase]][index] = newValue;
            if (formMain.CurrentPhrase == formMain.PhrasesAllowed.Length)
                if (formMain.LeadVocalsCurrentThought[formMain.CurrentPhrase] == formMain.LeadVocalsDistinctThoughtsCount[formMain.CurrentPhrase])
                    for (int i = 0; i < formMain.PhrasesAllowed.Length; i++)
                        for (int j = 0; j < formMain.LeadVocalsDistinctThoughtsCount[formMain.CurrentPhrase]; j++)
                            valuesStored[i][j][index] = newValue;
                else for (int i = 0; i < formMain.PhrasesAllowed.Length; i++)
                        valuesStored[i][formMain.LeadVocalsCurrentThought[formMain.CurrentPhrase]][index] = newValue;
            else if (formMain.LeadVocalsCurrentThought[formMain.CurrentPhrase] == formMain.LeadVocalsDistinctThoughtsCount[formMain.CurrentPhrase])
                for (int j = 0; j < formMain.LeadVocalsDistinctThoughtsCount[formMain.CurrentPhrase]; j++)
                    valuesStored[formMain.CurrentPhrase][j][index] = newValue;
        }
        private void MainValueChange(double[][][] valuesStored, double newValue, int index)
        {
            valuesStored[formMain.CurrentPhrase][formMain.LeadVocalsCurrentThought[formMain.CurrentPhrase]][index] = newValue;
            if (formMain.CurrentPhrase == formMain.PhrasesAllowed.Length)
                if (formMain.LeadVocalsCurrentThought[formMain.CurrentPhrase] == formMain.LeadVocalsDistinctThoughtsCount[formMain.CurrentPhrase])
                    for (int i = 0; i < formMain.PhrasesAllowed.Length; i++)
                        for (int j = 0; j < formMain.LeadVocalsDistinctThoughtsCount[formMain.CurrentPhrase]; j++)
                            valuesStored[i][j][index] = newValue;
                else for (int i = 0; i < formMain.PhrasesAllowed.Length; i++)
                        valuesStored[i][formMain.LeadVocalsCurrentThought[formMain.CurrentPhrase]][index] = newValue;
            else if (formMain.LeadVocalsCurrentThought[formMain.CurrentPhrase] == formMain.LeadVocalsDistinctThoughtsCount[formMain.CurrentPhrase])
                for (int j = 0; j < formMain.LeadVocalsDistinctThoughtsCount[formMain.CurrentPhrase]; j++)
                    valuesStored[formMain.CurrentPhrase][j][index] = newValue;
        }
        private void numericStrongBeatProb_ValueChanged(object sender, EventArgs e)
        {
            MainValueChange(formMain.LeadVocalsProbStrongBeat, numericStrongBeatProb.Value * 0.01M);
        }

        private void numericWeakBeatProb_ValueChanged(object sender, EventArgs e)
        {
            MainValueChange(formMain.LeadVocalsProbWeakBeat, numericWeakBeatProb.Value * 0.01M);
        }

        // CURRENT CHORD BIAS
        private void numericChordBiasPrime_ValueChanged(object sender, EventArgs e)
        {
            MainValueChange(formMain.LeadVocalsCurrentChordBias, (double)numericChordBiasPrime.Value, 0);
        }

        private void numericChordBiasThird_ValueChanged(object sender, EventArgs e)
        {
            MainValueChange(formMain.LeadVocalsCurrentChordBias, (double)numericChordBiasThird.Value, 1);
        }

        private void numericChordBiasFifth_ValueChanged(object sender, EventArgs e)
        {
            MainValueChange(formMain.LeadVocalsCurrentChordBias, (double)numericChordBiasFifth.Value, 2);
        }
        // THOUGHT DIRECTION
        private void comboBoxThoughtDirection_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Upwards, Downwards, DUD, UDU, Neither, Random
            MainValueChange(formMain.LeadVocalsThoughtDirection, comboBoxThoughtDirection.SelectedIndex);
            if (comboBoxThoughtDirection.SelectedIndex == 0 || comboBoxThoughtDirection.SelectedIndex == 1 || comboBoxThoughtDirection.SelectedIndex == 4)
            {
                numericBoomerangTurningPoint.Value = 0;
                numericBoomerangTurningPoint.Enabled = false;
            }
            else
            {
                if (!numericBoomerangTurningPoint.Enabled) numericBoomerangTurningPoint.Value = (decimal)defaults.LeadVocalsBoomerangTurningPoint * 100;
                numericBoomerangTurningPoint.Enabled = true;
            }
        }

        private void numericDirectionStrength_ValueChanged(object sender, EventArgs e)
        {
            MainValueChange(formMain.LeadVocalsDirectionStrength, (double)numericDirectionStrength.Value);
        }

        private void numericBoomerangTurningPoint_ValueChanged(object sender, EventArgs e)
        {
            MainValueChange(formMain.LeadVocalsBoomerangTurningPoint, (double)(numericBoomerangTurningPoint.Value * 0.01M));
        }
        // THOUGHT STARTS ON CURRENT CHORD COMPONENT
        private void checkStartsForceAnyComponent_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = checkStartsForceAnyComponent.Checked;
            numericStartsForceAnyComponent.Enabled = isChecked;
            checkStartsForceSpecificComponent.Enabled = isChecked;
            //numericStartsForcePrime.Enabled = isChecked;
            //numericStartsForceThird.Enabled = isChecked;
            //numericStartsForceFifth.Enabled = isChecked;
            //if (isChecked && !startsForceAnyEnabled)
            //{
            //    numericStartsForceAnyComponent.Value = (decimal)defaults.LeadVocalsStartsOnChord;
            //}
            if (updatingControls) return;
            //if (isChecked) numericStartsForceAnyComponent.Value = (decimal)(defaults.LeadVocalsStartsOnChord * 100);
            if (isChecked) numericStartsForceAnyComponent.Value = 0;
            else
            {
                //numericStartsForceAnyComponent.Value = 0;
                //MainValueChange(formMain.LeadVocalsStartsOnChord, -0.01);
                numericStartsForceAnyComponent.Value = -1;
                checkStartsForceSpecificComponent.Checked = false;
                numericStartsForcePrime.Value = -1;
                numericStartsForceThird.Value = 0;
                numericStartsForceFifth.Value = 0;
                //numericStartsForcePrime.Enabled = false;
                //numericStartsForceThird.Enabled = false;
                //numericStartsForceFifth.Enabled = false;
            }
        }

        private void numericStartsForceAnyComponent_ValueChanged(object sender, EventArgs e)
        {
            MainValueChange(formMain.LeadVocalsStartsOnChord, (double)(numericStartsForceAnyComponent.Value * 0.01M));
        }

        private void checkStartsForceSpecificComponent_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = checkStartsForceSpecificComponent.Checked;
            numericStartsForcePrime.Enabled = isChecked && checkStartsForceAnyComponent.Checked;
            numericStartsForceThird.Enabled = isChecked && checkStartsForceAnyComponent.Checked;
            numericStartsForceFifth.Enabled = isChecked && checkStartsForceAnyComponent.Checked;
            if (updatingControls) return;
            if (isChecked)
            {
                //numericStartsForcePrime.Value = defaults.LeadVocalsStartsOnChordComp[0];
                //numericStartsForceThird.Value = defaults.LeadVocalsStartsOnChordComp[1];
                //numericStartsForceFifth.Value = defaults.LeadVocalsStartsOnChordComp[2];
                numericStartsForcePrime.Value = 1;
                numericStartsForceThird.Value = 1;
                numericStartsForceFifth.Value = 1;
            }
            else
            {
                //numericStartsForcePrime.Value = 0;
                numericStartsForcePrime.Value = -1;
                numericStartsForceThird.Value = 0;
                numericStartsForceFifth.Value = 0;
                //MainValueChange(formMain.LeadVocalsStartsOnChordComp, -1, 0);
            }
        }

        private void numericStartsForcePrime_ValueChanged(object sender, EventArgs e)
        {
            MainValueChange(formMain.LeadVocalsStartsOnChordComp, (int)numericStartsForcePrime.Value, 0);
        }

        private void numericStartsForceThird_ValueChanged(object sender, EventArgs e)
        {
            MainValueChange(formMain.LeadVocalsStartsOnChordComp, (int)numericStartsForceThird.Value, 1);
        }

        private void numericStartsForceFifth_ValueChanged(object sender, EventArgs e)
        {
            MainValueChange(formMain.LeadVocalsStartsOnChordComp, (int)numericStartsForceFifth.Value, 2);
        }
        // THOUGHT ENDS ON CURRENT CHORD COMPONENT

        private void checkEndsForceAnyComponent_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = checkEndsForceAnyComponent.Checked;
            numericEndsForceAnyComponent.Enabled = isChecked;
            checkEndsForceSpecificComponent.Enabled = isChecked;
            if (updatingControls) return;
            if (isChecked) numericEndsForceAnyComponent.Value = 0;
            else
            {
                numericEndsForceAnyComponent.Value = -1;
                checkEndsForceSpecificComponent.Checked = false;
                numericEndsForcePrime.Value = -1;
                numericEndsForceThird.Value = 0;
                numericEndsForceFifth.Value = 0;
            }
        }

        private void numericEndsForceAnyComponent_ValueChanged(object sender, EventArgs e)
        {
            MainValueChange(formMain.LeadVocalsEndsOnChord, (double)(numericEndsForceAnyComponent.Value * 0.01M));
        }

        private void checkEndsForceSpecificComponent_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = checkEndsForceSpecificComponent.Checked;
            numericEndsForcePrime.Enabled = isChecked && checkEndsForceAnyComponent.Checked;
            numericEndsForceThird.Enabled = isChecked && checkEndsForceAnyComponent.Checked;
            numericEndsForceFifth.Enabled = isChecked && checkEndsForceAnyComponent.Checked;
            if (updatingControls) return;
            if (isChecked)
            {
                numericEndsForcePrime.Value = 1;
                numericEndsForceThird.Value = 1;
                numericEndsForceFifth.Value = 1;
            }
            else
            {
                numericEndsForcePrime.Value = -1;
                numericEndsForceThird.Value = 0;
                numericEndsForceFifth.Value = 0;
            }
        }

        private void numericEndsForcePrime_ValueChanged(object sender, EventArgs e)
        {
            MainValueChange(formMain.LeadVocalsEndsOnChordComp, (int)numericEndsForcePrime.Value, 0);
        }

        private void numericEndsForceThird_ValueChanged(object sender, EventArgs e)
        {
            MainValueChange(formMain.LeadVocalsEndsOnChordComp, (int)numericEndsForceThird.Value, 1);
        }

        private void numericEndsForceFifth_ValueChanged(object sender, EventArgs e)
        {
            MainValueChange(formMain.LeadVocalsEndsOnChordComp, (int)numericEndsForceFifth.Value, 2);
        }
        //private void checkEndsForceAnyComponent_CheckedChanged(object sender, EventArgs e)
        //{
        //    bool isChecked = checkEndsForceAnyComponent.Checked;
        //    numericEndsForceAnyComponent.Enabled = isChecked;
        //    checkEndsForceSpecificComponent.Enabled = isChecked;
        //    if (isChecked) numericEndsForceAnyComponent.Value = (decimal)defaults.LeadVocalsEndsOnChord;
        //    else
        //    {
        //        numericEndsForceAnyComponent.Value = 0;
        //        MainValueChange(formMain.LeadVocalsEndsOnChord, -1);
        //        checkEndsForceSpecificComponent.Checked = false;
        //        numericEndsForcePrime.Value = 0;
        //        numericEndsForceThird.Value = 0;
        //        numericEndsForceFifth.Value = 0;
        //        numericEndsForcePrime.Enabled = false;
        //        numericEndsForceThird.Enabled = false;
        //        numericEndsForceFifth.Enabled = false;
        //    }
        //}

        //private void numericEndsForceAnyComponent_ValueChanged(object sender, EventArgs e)
        //{
        //    MainValueChange(formMain.LeadVocalsEndsOnChord, (double)(numericEndsForceAnyComponent.Value * 0.01M));
        //}

        //private void checkEndsForceSpecificComponent_CheckedChanged(object sender, EventArgs e)
        //{
        //    bool isChecked = checkEndsForceSpecificComponent.Checked;
        //    numericEndsForcePrime.Enabled = isChecked;
        //    numericEndsForceThird.Enabled = isChecked;
        //    numericEndsForceFifth.Enabled = isChecked;
        //    if (isChecked)
        //    {
        //        numericEndsForcePrime.Value = defaults.LeadVocalsEndsOnChordComp[0];
        //        numericEndsForceThird.Value = defaults.LeadVocalsEndsOnChordComp[1];
        //        numericEndsForceFifth.Value = defaults.LeadVocalsEndsOnChordComp[2];
        //    }
        //    else
        //    {
        //        numericEndsForcePrime.Value = 0;
        //        numericEndsForceThird.Value = 0;
        //        numericEndsForceFifth.Value = 0;
        //        MainValueChange(formMain.LeadVocalsEndsOnChordComp, -1, 0);
        //    }
        //}

        //private void numericEndsForcePrime_ValueChanged(object sender, EventArgs e)
        //{
        //    MainValueChange(formMain.LeadVocalsEndsOnChordComp, (int)numericEndsForcePrime.Value, 0);
        //}

        //private void numericEndsForceThird_ValueChanged(object sender, EventArgs e)
        //{
        //    MainValueChange(formMain.LeadVocalsEndsOnChordComp, (int)numericEndsForceThird.Value, 1);
        //}

        //private void numericEndsForceFifth_ValueChanged(object sender, EventArgs e)
        //{
        //    MainValueChange(formMain.LeadVocalsEndsOnChordComp, (int)numericEndsForceFifth.Value, 2);
        //}
        // THOUGHTS RISING
        private void checkThoughtsRising_CheckedChanged(object sender, EventArgs e)
        {
            //MainValueChange(formMain.LeadVocalsThoughtsRising, checkThoughtsRising.Checked);
            formMain.LeadVocalsThoughtsRising[formMain.CurrentPhrase] = checkThoughtsRising.Checked;
            if (formMain.CurrentPhrase == formMain.PhrasesAllowed.Length)
                for (int i = 0; i < formMain.PhrasesAllowed.Length; i++)
                    formMain.LeadVocalsThoughtsRising[i] = checkThoughtsRising.Checked;
            if (checkThoughtsRising.Checked)
            {
                numericThoughtOverlap.Enabled = true;
                numericThoughtOverlap.Value = (decimal)(defaults.LeadVocalsRisingOverlap * 100);
            }
            else
            {
                numericThoughtOverlap.Enabled = false;
                numericThoughtOverlap.Value = 0;
            }
        }

        private void numericThoughtOverlap_ValueChanged(object sender, EventArgs e)
        {
            //MainValueChange(formMain.LeadVocalsRisingOverlap, (double)(numericThoughtOverlap.Value * 0.01M));
            formMain.LeadVocalsRisingOverlap[formMain.CurrentPhrase] = (double)(numericThoughtOverlap.Value * 0.01M);
            if (formMain.CurrentPhrase == formMain.PhrasesAllowed.Length)
                for (int i = 0; i < formMain.PhrasesAllowed.Length; i++)
                    formMain.LeadVocalsRisingOverlap[i] = (double)(numericThoughtOverlap.Value * 0.01M);
        }
        // MISCELLANEOUS
        private void numericThoughtLengthInProgressions_ValueChanged(object sender, EventArgs e)
        {
            MainValueChange(formMain.LeadVocalsThoughtLength, (int)numericThoughtLengthInProgressions.Value);
        }

        private void numericEndRestLengthInQuarterNotes_ValueChanged(object sender, EventArgs e)
        {
            MainValueChange(formMain.LeadVocalsEndRestLength, numericEndRestLengthInQuarterNotes.Value);
        }

        private void numericRestChance_ValueChanged(object sender, EventArgs e)
        {
            MainValueChange(formMain.LeadVocalsRestChance, numericRestChance.Value * 0.01M);
        }

        private void numericGlissChance_ValueChanged(object sender, EventArgs e)
        {
            MainValueChange(formMain.LeadVocalsGlissChance, numericGlissChance.Value * 0.01M);
        }

        private void comboBoxPitchWeightShape_SelectedIndexChanged(object sender, EventArgs e)
        {
            MainValueChange(formMain.LeadVocalsWeightShape, comboBoxPitchWeightShape.SelectedIndex);
        }
        // AMBITUS
        private void comboBoxLowOctave_SelectedIndexChanged(object sender, EventArgs e)
        {
            MainValueChange(formMain.LeadVocalsLowOctaveIndex, comboBoxLowOctave.SelectedIndex);
        }

        private void comboBoxLowNote_SelectedIndexChanged(object sender, EventArgs e)
        {
            MainValueChange(formMain.LeadVocalsLowNoteIndex, comboBoxLowNote.SelectedIndex);
        }

        private void comboBoxHighOctave_SelectedIndexChanged(object sender, EventArgs e)
        {
            MainValueChange(formMain.LeadVocalsHighOctaveIndex, comboBoxHighOctave.SelectedIndex);
        }

        private void comboBoxHighNote_SelectedIndexChanged(object sender, EventArgs e)
        {
            MainValueChange(formMain.LeadVocalsHighNoteIndex, comboBoxHighNote.SelectedIndex);
        }
        // WEIGHTS
        private void numericWeightPitch_ValueChanged(object sender, EventArgs e)
        {
            MainValueChange(formMain.LeadVocalsDrawWeights, (int)numericWeightPitch.Value, 0);
        }

        private void numericWeightStep_ValueChanged(object sender, EventArgs e)
        {
            MainValueChange(formMain.LeadVocalsDrawWeights, (int)numericWeightStep.Value, 1);
        }

        private void numericWeightInterval_ValueChanged(object sender, EventArgs e)
        {
            MainValueChange(formMain.LeadVocalsDrawWeights, (int)numericWeightInterval.Value, 2);
        }
        // LAST SYLLABLE
        private void numericLastSyllableStart_ValueChanged(object sender, EventArgs e)
        {
            MainValueChange(formMain.LeadVocalsLastSyllableStart, numericLastSyllableStart.Value * 0.01M);
        }

        private void numericLastSyllableChance_ValueChanged(object sender, EventArgs e)
        {
            MainValueChange(formMain.LeadVocalsLastSyllableChance, numericLastSyllableChance.Value * 0.01M);
        }
        // START REST
        private void comboBoxStartRestUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            MainValueChange(formMain.LeadVocalsStartRestUnitIndex, comboBoxStartRestUnit.SelectedIndex);
        }

        private void numericStartRestChance_ValueChanged(object sender, EventArgs e)
        {
            MainValueChange(formMain.LeadVocalsStartRestChance, numericStartRestChance.Value * 0.01M);
        }

        private void numericStartRestMaxSize_ValueChanged(object sender, EventArgs e)
        {
            MainValueChange(formMain.LeadVocalsMaxStartRestSize, numericStartRestMaxSize.Value);
        }
        // VARIATION
        private void numericMelodyVariation_ValueChanged(object sender, EventArgs e)
        {
            MainValueChange(formMain.LeadVocalsMelodyVariation, (double)(numericMelodyVariation.Value * 0.01M));
        }

        private void numericRhythmVariation_ValueChanged(object sender, EventArgs e)
        {
            MainValueChange(formMain.LeadVocalsRhythmVariation, (double)(numericRhythmVariation.Value * 0.01M));
        }

        private void numericVariationSectionStart_ValueChanged(object sender, EventArgs e)
        {
            MainValueChange(formMain.LeadVocalsVariationSection, (double)(numericVariationSectionStart.Value * 0.01M), 0);
        }

        private void numericVariationSectionEnd_ValueChanged(object sender, EventArgs e)
        {
            MainValueChange(formMain.LeadVocalsVariationSection, (double)(numericVariationSectionEnd.Value * 0.01M), 1);
        }

        private void LeadVocalsSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

    }
}
