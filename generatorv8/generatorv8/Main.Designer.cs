
namespace generatorv8
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonPath = new System.Windows.Forms.Button();
            this.textBoxPath = new System.Windows.Forms.TextBox();
            this.numericMeter = new System.Windows.Forms.NumericUpDown();
            this.numericBars = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonRun = new System.Windows.Forms.Button();
            this.textBoxFileName = new System.Windows.Forms.TextBox();
            this.labelFileName = new System.Windows.Forms.Label();
            this.radioOverwrite = new System.Windows.Forms.RadioButton();
            this.radioIncrement = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonStrongBeats = new System.Windows.Forms.Button();
            this.checkForceKickOn1 = new System.Windows.Forms.CheckBox();
            this.numericBpm = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxKey = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxScales = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxProgressionLength = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBoxChordLength = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButtonPowerChords = new System.Windows.Forms.RadioButton();
            this.radioButtonFullChords = new System.Windows.Forms.RadioButton();
            this.checkForceTonicOn1 = new System.Windows.Forms.CheckBox();
            this.buttonChordsProbabilities = new System.Windows.Forms.Button();
            this.comboBoxDrumLickLength = new System.Windows.Forms.ComboBox();
            this.buttonRhythmGuitarValuesProb = new System.Windows.Forms.Button();
            this.checkForceBassOn1 = new System.Windows.Forms.CheckBox();
            this.checkForceBassOnEveryStrongBeat = new System.Windows.Forms.CheckBox();
            this.buttonMIDI = new System.Windows.Forms.Button();
            this.buttonLeadVocalsSettings = new System.Windows.Forms.Button();
            this.comboBoxPhrases = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxPattern = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.numericDistinctPhrases = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.buttonResetDefaults = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericMeter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericBars)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericBpm)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericDistinctPhrases)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonPath
            // 
            this.buttonPath.Location = new System.Drawing.Point(22, 11);
            this.buttonPath.Name = "buttonPath";
            this.buttonPath.Size = new System.Drawing.Size(111, 29);
            this.buttonPath.TabIndex = 0;
            this.buttonPath.Text = "Browse path";
            this.buttonPath.UseVisualStyleBackColor = true;
            this.buttonPath.Click += new System.EventHandler(this.ButtonPath_Click);
            // 
            // textBoxPath
            // 
            this.textBoxPath.Location = new System.Drawing.Point(152, 12);
            this.textBoxPath.Name = "textBoxPath";
            this.textBoxPath.ReadOnly = true;
            this.textBoxPath.Size = new System.Drawing.Size(457, 27);
            this.textBoxPath.TabIndex = 1;
            // 
            // numericMeter
            // 
            this.numericMeter.Location = new System.Drawing.Point(136, 185);
            this.numericMeter.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericMeter.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericMeter.Name = "numericMeter";
            this.numericMeter.Size = new System.Drawing.Size(118, 27);
            this.numericMeter.TabIndex = 3;
            this.numericMeter.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numericMeter.ValueChanged += new System.EventHandler(this.NumericMeter_ValueChanged);
            // 
            // numericBars
            // 
            this.numericBars.Increment = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numericBars.Location = new System.Drawing.Point(136, 219);
            this.numericBars.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericBars.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericBars.Name = "numericBars";
            this.numericBars.Size = new System.Drawing.Size(118, 27);
            this.numericBars.TabIndex = 4;
            this.numericBars.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numericBars.ValueChanged += new System.EventHandler(this.numericBars_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 187);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Meter (n/4)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 219);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "Number of bars";
            // 
            // buttonRun
            // 
            this.buttonRun.Location = new System.Drawing.Point(265, 355);
            this.buttonRun.Name = "buttonRun";
            this.buttonRun.Size = new System.Drawing.Size(344, 29);
            this.buttonRun.TabIndex = 8;
            this.buttonRun.Text = "Run";
            this.buttonRun.UseVisualStyleBackColor = true;
            this.buttonRun.Click += new System.EventHandler(this.ButtonRun_Click);
            // 
            // textBoxFileName
            // 
            this.textBoxFileName.Location = new System.Drawing.Point(152, 45);
            this.textBoxFileName.Name = "textBoxFileName";
            this.textBoxFileName.Size = new System.Drawing.Size(457, 27);
            this.textBoxFileName.TabIndex = 15;
            this.textBoxFileName.Text = "music";
            this.textBoxFileName.TextChanged += new System.EventHandler(this.TextBoxFileName_TextChanged);
            // 
            // labelFileName
            // 
            this.labelFileName.AutoSize = true;
            this.labelFileName.Location = new System.Drawing.Point(42, 48);
            this.labelFileName.Name = "labelFileName";
            this.labelFileName.Size = new System.Drawing.Size(73, 20);
            this.labelFileName.TabIndex = 16;
            this.labelFileName.Text = "File name";
            // 
            // radioOverwrite
            // 
            this.radioOverwrite.AutoSize = true;
            this.radioOverwrite.Checked = true;
            this.radioOverwrite.Location = new System.Drawing.Point(19, 23);
            this.radioOverwrite.Name = "radioOverwrite";
            this.radioOverwrite.Size = new System.Drawing.Size(119, 24);
            this.radioOverwrite.TabIndex = 17;
            this.radioOverwrite.TabStop = true;
            this.radioOverwrite.Text = "Overwrite file";
            this.radioOverwrite.UseVisualStyleBackColor = true;
            // 
            // radioIncrement
            // 
            this.radioIncrement.AutoSize = true;
            this.radioIncrement.Location = new System.Drawing.Point(19, 53);
            this.radioIncrement.Name = "radioIncrement";
            this.radioIncrement.Size = new System.Drawing.Size(162, 24);
            this.radioIncrement.TabIndex = 18;
            this.radioIncrement.Text = "Increment file name";
            this.radioIncrement.UseVisualStyleBackColor = true;
            this.radioIncrement.CheckedChanged += new System.EventHandler(this.RadioIncrement_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioIncrement);
            this.groupBox1.Controls.Add(this.radioOverwrite);
            this.groupBox1.Location = new System.Drawing.Point(12, 83);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(242, 91);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "If file already exists";
            // 
            // buttonStrongBeats
            // 
            this.buttonStrongBeats.Location = new System.Drawing.Point(265, 183);
            this.buttonStrongBeats.Name = "buttonStrongBeats";
            this.buttonStrongBeats.Size = new System.Drawing.Size(344, 29);
            this.buttonStrongBeats.TabIndex = 20;
            this.buttonStrongBeats.Text = "Set strong beats";
            this.buttonStrongBeats.UseVisualStyleBackColor = true;
            this.buttonStrongBeats.Click += new System.EventHandler(this.ButtonStrongBeats_Click);
            // 
            // checkForceKickOn1
            // 
            this.checkForceKickOn1.AutoSize = true;
            this.checkForceKickOn1.Checked = true;
            this.checkForceKickOn1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkForceKickOn1.Location = new System.Drawing.Point(647, 233);
            this.checkForceKickOn1.Name = "checkForceKickOn1";
            this.checkForceKickOn1.Size = new System.Drawing.Size(146, 24);
            this.checkForceKickOn1.TabIndex = 22;
            this.checkForceKickOn1.Text = "Force kick on one";
            this.checkForceKickOn1.UseVisualStyleBackColor = true;
            this.checkForceKickOn1.CheckedChanged += new System.EventHandler(this.checkForceKickOn1_CheckedChanged);
            // 
            // numericBpm
            // 
            this.numericBpm.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericBpm.Location = new System.Drawing.Point(136, 253);
            this.numericBpm.Maximum = new decimal(new int[] {
            400,
            0,
            0,
            0});
            this.numericBpm.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numericBpm.Name = "numericBpm";
            this.numericBpm.Size = new System.Drawing.Size(118, 27);
            this.numericBpm.TabIndex = 23;
            this.numericBpm.Value = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.numericBpm.ValueChanged += new System.EventHandler(this.numericBpm_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 249);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 20);
            this.label3.TabIndex = 24;
            this.label3.Text = "Bpm";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 359);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(126, 20);
            this.label4.TabIndex = 26;
            this.label4.Text = "Drum beat length";
            // 
            // comboBoxKey
            // 
            this.comboBoxKey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxKey.FormattingEnabled = true;
            this.comboBoxKey.Items.AddRange(new object[] {
            "Cb",
            "Gb",
            "Db",
            "Ab",
            "Eb",
            "Bb",
            "F",
            "C",
            "G",
            "D",
            "A",
            "E",
            "B",
            "F#",
            "C#"});
            this.comboBoxKey.Location = new System.Drawing.Point(136, 288);
            this.comboBoxKey.Name = "comboBoxKey";
            this.comboBoxKey.Size = new System.Drawing.Size(118, 28);
            this.comboBoxKey.TabIndex = 28;
            this.comboBoxKey.SelectedIndexChanged += new System.EventHandler(this.comboBoxKey_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 289);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 20);
            this.label5.TabIndex = 29;
            this.label5.Text = "Key";
            // 
            // comboBoxScales
            // 
            this.comboBoxScales.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxScales.FormattingEnabled = true;
            this.comboBoxScales.Items.AddRange(new object[] {
            "Ionian",
            "Dorian",
            "Phrygian",
            "Lydian",
            "Mixolydian",
            "Aeolian",
            "Locrian"});
            this.comboBoxScales.Location = new System.Drawing.Point(136, 322);
            this.comboBoxScales.Name = "comboBoxScales";
            this.comboBoxScales.Size = new System.Drawing.Size(118, 28);
            this.comboBoxScales.TabIndex = 30;
            this.comboBoxScales.SelectedIndexChanged += new System.EventHandler(this.comboBoxScales_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 325);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 20);
            this.label6.TabIndex = 31;
            this.label6.Text = "Scale";
            // 
            // comboBoxProgressionLength
            // 
            this.comboBoxProgressionLength.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProgressionLength.FormattingEnabled = true;
            this.comboBoxProgressionLength.Items.AddRange(new object[] {
            "1",
            "2",
            "4"});
            this.comboBoxProgressionLength.Location = new System.Drawing.Point(766, 27);
            this.comboBoxProgressionLength.Name = "comboBoxProgressionLength";
            this.comboBoxProgressionLength.Size = new System.Drawing.Size(64, 28);
            this.comboBoxProgressionLength.TabIndex = 32;
            this.comboBoxProgressionLength.SelectedIndexChanged += new System.EventHandler(this.comboBoxProgressionLength_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(620, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(140, 40);
            this.label7.TabIndex = 33;
            this.label7.Text = "Progression length in bars";
            // 
            // comboBoxChordLength
            // 
            this.comboBoxChordLength.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxChordLength.FormattingEnabled = true;
            this.comboBoxChordLength.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.comboBoxChordLength.Location = new System.Drawing.Point(766, 84);
            this.comboBoxChordLength.Name = "comboBoxChordLength";
            this.comboBoxChordLength.Size = new System.Drawing.Size(64, 28);
            this.comboBoxChordLength.TabIndex = 34;
            this.comboBoxChordLength.SelectedIndexChanged += new System.EventHandler(this.comboBoxChordLength_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(620, 76);
            this.label8.MaximumSize = new System.Drawing.Size(170, 100);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(140, 40);
            this.label8.TabIndex = 35;
            this.label8.Text = "Chord change every n strong beats";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioButtonPowerChords);
            this.groupBox2.Controls.Add(this.radioButtonFullChords);
            this.groupBox2.Location = new System.Drawing.Point(620, 136);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(210, 91);
            this.groupBox2.TabIndex = 37;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Chords";
            // 
            // radioButtonPowerChords
            // 
            this.radioButtonPowerChords.AutoSize = true;
            this.radioButtonPowerChords.Location = new System.Drawing.Point(15, 53);
            this.radioButtonPowerChords.Name = "radioButtonPowerChords";
            this.radioButtonPowerChords.Size = new System.Drawing.Size(118, 24);
            this.radioButtonPowerChords.TabIndex = 1;
            this.radioButtonPowerChords.Text = "Power chords";
            this.radioButtonPowerChords.UseVisualStyleBackColor = true;
            // 
            // radioButtonFullChords
            // 
            this.radioButtonFullChords.AutoSize = true;
            this.radioButtonFullChords.Checked = true;
            this.radioButtonFullChords.Location = new System.Drawing.Point(15, 23);
            this.radioButtonFullChords.Name = "radioButtonFullChords";
            this.radioButtonFullChords.Size = new System.Drawing.Size(101, 24);
            this.radioButtonFullChords.TabIndex = 0;
            this.radioButtonFullChords.TabStop = true;
            this.radioButtonFullChords.Text = "Full chords";
            this.radioButtonFullChords.UseVisualStyleBackColor = true;
            this.radioButtonFullChords.CheckedChanged += new System.EventHandler(this.radioButtonFullChords_CheckedChanged);
            // 
            // checkForceTonicOn1
            // 
            this.checkForceTonicOn1.AutoSize = true;
            this.checkForceTonicOn1.Checked = true;
            this.checkForceTonicOn1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkForceTonicOn1.Location = new System.Drawing.Point(647, 264);
            this.checkForceTonicOn1.Name = "checkForceTonicOn1";
            this.checkForceTonicOn1.Size = new System.Drawing.Size(154, 24);
            this.checkForceTonicOn1.TabIndex = 38;
            this.checkForceTonicOn1.Text = "Force tonic on one";
            this.checkForceTonicOn1.UseVisualStyleBackColor = true;
            this.checkForceTonicOn1.CheckedChanged += new System.EventHandler(this.checkForceTonicOn1_CheckedChanged);
            // 
            // buttonChordsProbabilities
            // 
            this.buttonChordsProbabilities.Location = new System.Drawing.Point(265, 217);
            this.buttonChordsProbabilities.Name = "buttonChordsProbabilities";
            this.buttonChordsProbabilities.Size = new System.Drawing.Size(344, 29);
            this.buttonChordsProbabilities.TabIndex = 39;
            this.buttonChordsProbabilities.Text = "Set chords probabilities";
            this.buttonChordsProbabilities.UseVisualStyleBackColor = true;
            this.buttonChordsProbabilities.Click += new System.EventHandler(this.buttonChordsProbabilities_Click);
            // 
            // comboBoxDrumLickLength
            // 
            this.comboBoxDrumLickLength.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDrumLickLength.FormattingEnabled = true;
            this.comboBoxDrumLickLength.Items.AddRange(new object[] {
            "1",
            "2",
            "4"});
            this.comboBoxDrumLickLength.Location = new System.Drawing.Point(136, 356);
            this.comboBoxDrumLickLength.Name = "comboBoxDrumLickLength";
            this.comboBoxDrumLickLength.Size = new System.Drawing.Size(118, 28);
            this.comboBoxDrumLickLength.TabIndex = 40;
            this.comboBoxDrumLickLength.SelectedIndexChanged += new System.EventHandler(this.comboBoxDrumLickLength_SelectedIndexChanged);
            // 
            // buttonRhythmGuitarValuesProb
            // 
            this.buttonRhythmGuitarValuesProb.Location = new System.Drawing.Point(265, 251);
            this.buttonRhythmGuitarValuesProb.Name = "buttonRhythmGuitarValuesProb";
            this.buttonRhythmGuitarValuesProb.Size = new System.Drawing.Size(344, 29);
            this.buttonRhythmGuitarValuesProb.TabIndex = 41;
            this.buttonRhythmGuitarValuesProb.Text = "Set rhythm guitar rhythmic values probabilities";
            this.buttonRhythmGuitarValuesProb.UseVisualStyleBackColor = true;
            this.buttonRhythmGuitarValuesProb.Click += new System.EventHandler(this.buttonRhythmGuitarValuesProb_Click);
            // 
            // checkForceBassOn1
            // 
            this.checkForceBassOn1.AutoSize = true;
            this.checkForceBassOn1.Checked = true;
            this.checkForceBassOn1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkForceBassOn1.Location = new System.Drawing.Point(647, 297);
            this.checkForceBassOn1.Name = "checkForceBassOn1";
            this.checkForceBassOn1.Size = new System.Drawing.Size(150, 24);
            this.checkForceBassOn1.TabIndex = 48;
            this.checkForceBassOn1.Text = "Force bass on one";
            this.checkForceBassOn1.UseVisualStyleBackColor = true;
            this.checkForceBassOn1.CheckedChanged += new System.EventHandler(this.checkForceBassOn1_CheckedChanged);
            // 
            // checkForceBassOnEveryStrongBeat
            // 
            this.checkForceBassOnEveryStrongBeat.Checked = true;
            this.checkForceBassOnEveryStrongBeat.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkForceBassOnEveryStrongBeat.Location = new System.Drawing.Point(647, 327);
            this.checkForceBassOnEveryStrongBeat.Name = "checkForceBassOnEveryStrongBeat";
            this.checkForceBassOnEveryStrongBeat.Size = new System.Drawing.Size(160, 50);
            this.checkForceBassOnEveryStrongBeat.TabIndex = 49;
            this.checkForceBassOnEveryStrongBeat.Text = "Force bass on every strong beat";
            this.checkForceBassOnEveryStrongBeat.UseVisualStyleBackColor = true;
            this.checkForceBassOnEveryStrongBeat.CheckedChanged += new System.EventHandler(this.checkForceBassOnEveryStrongBeat_CheckedChanged);
            // 
            // buttonMIDI
            // 
            this.buttonMIDI.Location = new System.Drawing.Point(265, 321);
            this.buttonMIDI.Name = "buttonMIDI";
            this.buttonMIDI.Size = new System.Drawing.Size(344, 29);
            this.buttonMIDI.TabIndex = 50;
            this.buttonMIDI.Text = "MIDI settings";
            this.buttonMIDI.UseVisualStyleBackColor = true;
            this.buttonMIDI.Click += new System.EventHandler(this.buttonMIDI_Click);
            // 
            // buttonLeadVocalsSettings
            // 
            this.buttonLeadVocalsSettings.Location = new System.Drawing.Point(265, 287);
            this.buttonLeadVocalsSettings.Name = "buttonLeadVocalsSettings";
            this.buttonLeadVocalsSettings.Size = new System.Drawing.Size(344, 29);
            this.buttonLeadVocalsSettings.TabIndex = 51;
            this.buttonLeadVocalsSettings.Text = "Lead vocals settings";
            this.buttonLeadVocalsSettings.UseVisualStyleBackColor = true;
            this.buttonLeadVocalsSettings.Click += new System.EventHandler(this.buttonLeadVocalsSettings_Click);
            // 
            // comboBoxPhrases
            // 
            this.comboBoxPhrases.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPhrases.FormattingEnabled = true;
            this.comboBoxPhrases.Items.AddRange(new object[] {
            "A",
            "B",
            "C",
            "D",
            "E",
            "F",
            "G",
            "H",
            "All"});
            this.comboBoxPhrases.Location = new System.Drawing.Point(376, 112);
            this.comboBoxPhrases.Name = "comboBoxPhrases";
            this.comboBoxPhrases.Size = new System.Drawing.Size(65, 28);
            this.comboBoxPhrases.TabIndex = 52;
            this.comboBoxPhrases.SelectedIndexChanged += new System.EventHandler(this.comboBoxPhrases_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(265, 115);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(105, 20);
            this.label9.TabIndex = 53;
            this.label9.Text = "Current phrase";
            // 
            // textBoxPattern
            // 
            this.textBoxPattern.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxPattern.Location = new System.Drawing.Point(376, 78);
            this.textBoxPattern.Name = "textBoxPattern";
            this.textBoxPattern.ShortcutsEnabled = false;
            this.textBoxPattern.Size = new System.Drawing.Size(233, 27);
            this.textBoxPattern.TabIndex = 54;
            this.textBoxPattern.Text = "A";
            this.textBoxPattern.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxPattern_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(265, 81);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(95, 20);
            this.label10.TabIndex = 55;
            this.label10.Text = "Song pattern";
            // 
            // numericDistinctPhrases
            // 
            this.numericDistinctPhrases.Location = new System.Drawing.Point(462, 152);
            this.numericDistinctPhrases.Maximum = new decimal(new int[] {
            26,
            0,
            0,
            0});
            this.numericDistinctPhrases.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericDistinctPhrases.Name = "numericDistinctPhrases";
            this.numericDistinctPhrases.Size = new System.Drawing.Size(65, 27);
            this.numericDistinctPhrases.TabIndex = 56;
            this.numericDistinctPhrases.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numericDistinctPhrases.ValueChanged += new System.EventHandler(this.numericDistinctPhrases_ValueChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(269, 154);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(187, 20);
            this.label11.TabIndex = 57;
            this.label11.Text = "Number of distinct phrases";
            // 
            // buttonResetDefaults
            // 
            this.buttonResetDefaults.Location = new System.Drawing.Point(462, 112);
            this.buttonResetDefaults.Name = "buttonResetDefaults";
            this.buttonResetDefaults.Size = new System.Drawing.Size(147, 29);
            this.buttonResetDefaults.TabIndex = 58;
            this.buttonResetDefaults.Text = "Reset defaults";
            this.buttonResetDefaults.UseVisualStyleBackColor = true;
            this.buttonResetDefaults.Click += new System.EventHandler(this.buttonResetDefaults_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 395);
            this.Controls.Add(this.buttonResetDefaults);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.numericDistinctPhrases);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.textBoxPattern);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.comboBoxPhrases);
            this.Controls.Add(this.buttonLeadVocalsSettings);
            this.Controls.Add(this.buttonMIDI);
            this.Controls.Add(this.checkForceBassOnEveryStrongBeat);
            this.Controls.Add(this.checkForceBassOn1);
            this.Controls.Add(this.buttonRhythmGuitarValuesProb);
            this.Controls.Add(this.comboBoxDrumLickLength);
            this.Controls.Add(this.buttonChordsProbabilities);
            this.Controls.Add(this.checkForceTonicOn1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.comboBoxChordLength);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.comboBoxProgressionLength);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.comboBoxScales);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBoxKey);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numericBpm);
            this.Controls.Add(this.checkForceKickOn1);
            this.Controls.Add(this.buttonStrongBeats);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.labelFileName);
            this.Controls.Add(this.textBoxFileName);
            this.Controls.Add(this.buttonRun);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericBars);
            this.Controls.Add(this.numericMeter);
            this.Controls.Add(this.textBoxPath);
            this.Controls.Add(this.buttonPath);
            this.Name = "Main";
            this.Text = "MusicGen";
            ((System.ComponentModel.ISupportInitialize)(this.numericMeter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericBars)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericBpm)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericDistinctPhrases)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonPath;
        private System.Windows.Forms.TextBox textBoxPath;
        private System.Windows.Forms.NumericUpDown numericMeter;
        private System.Windows.Forms.NumericUpDown numericBars;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonRun;
        private System.Windows.Forms.TextBox textBoxFileName;
        private System.Windows.Forms.Label labelFileName;
        private System.Windows.Forms.RadioButton radioOverwrite;
        private System.Windows.Forms.RadioButton radioIncrement;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonStrongBeats;
        private System.Windows.Forms.CheckBox checkForceKickOn1;
        private System.Windows.Forms.NumericUpDown numericBpm;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxKey;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxScales;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBoxProgressionLength;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBoxChordLength;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioButtonPowerChords;
        private System.Windows.Forms.RadioButton radioButtonFullChords;
        private System.Windows.Forms.CheckBox checkForceTonicOn1;
        private System.Windows.Forms.Button buttonChordsProbabilities;
        private System.Windows.Forms.ComboBox comboBoxDrumLickLength;
        private System.Windows.Forms.Button buttonRhythmGuitarValuesProb;
        private System.Windows.Forms.CheckBox checkForceBassOn1;
        private System.Windows.Forms.CheckBox checkForceBassOnEveryStrongBeat;
        private System.Windows.Forms.Button buttonMIDI;
        private System.Windows.Forms.Button buttonLeadVocalsSettings;
        private System.Windows.Forms.ComboBox comboBoxPhrases;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxPattern;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown numericDistinctPhrases;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button buttonResetDefaults;
    }
}

