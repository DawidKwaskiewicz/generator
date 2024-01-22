using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace generatorv8
{
    public partial class Main : Form
    {
        public struct ParamsSet
        {
            public int Meterup { get; set; }
            public List<int> StrongBeats { get; set; }
            public List<int>[] ChordsProbabilities { get; set; }
            public List<int> RhythmGuitarValuesProb { get; set; }
            //public List<int> LeadGuitarNotesProb { get; set; }
            //public List<int> LeadGuitarRhythmProb { get; set; }
            //public List<int> LeadGuitarInterProb { get; set; }
            public bool TransposeLead { get; set; }
            public bool TransposeRhythm { get; set; }
            public bool TransposeBass { get; set; }
            //public string LowNote { get; set; }
            //public string HighNote { get; set; }
            //public string LowOctave { get; set; }
            //public string HighOctave { get; set; }
            //public int LowNoteIndex { get; set; }
            //public int HighNoteIndex { get; set; }
            //public int LowOctaveIndex { get; set; }
            //public int HighOctaveIndex { get; set; }
            //public decimal LeadProbBar { get; set; }
            //public decimal LeadProbStrongBeat { get; set; }
            //public decimal LeadProbWeakBeat { get; set; }
            //public bool LeadForceTonicOn1 { get; set; }
            //public int Rerolls { get; set; }
            public int Barsq { get; set; }
            public bool ForceKickOn1 { get; set; }
            public int Bpm { get; set; }
            public string Key { get; set; }
            public int KeyIndex { get; set; }
            public string Scales { get; set; }
            public int ScalesIndex { get; set; }
            public int BeatLength { get; set; }
            public int BeatLengthIndex { get; set; }
            public int ProgressionLengthInBars { get; set; }
            public int ProgressionLengthInBarsIndex { get; set; }
            public int ChordLengthInStrongBeats { get; set; }
            public int ChordLengthInStrongBeatsIndex { get; set; }
            public bool FullChords { get; set; }
            public bool ForceTonicOn1 { get; set; }
            public bool ForceBassOn1 { get; set; }
            public bool ForceBassOnEveryStrongBeat { get; set; }
            // Lead vocals
            //public int LeadVocalsCurrentThought { get; set; }
            //public int LeadVocalsDistinctThoughtsCount { get; set; }
            public int LeadVocalsCurrentThought { get; set; }
            public int LeadVocalsDistinctThoughtsCount { get; set; }
            public string LeadVocalsThoughtPattern { get; set; }
            public List<int> LeadVocalsNotesProb { get; set; }
            public List<int> LeadVocalsInterProb { get; set; }
            public List<int> LeadVocalsRhythmProb { get; set; }
            public int[] LeadVocalsDrawWeights { get; set; }
            public int LeadVocalsLowNoteIndex { get; set; }
            public int LeadVocalsLowOctaveIndex { get; set; }
            public int LeadVocalsHighNoteIndex { get; set; }
            public int LeadVocalsHighOctaveIndex { get; set; }
            public decimal LeadVocalsProbBar { get; set; }
            public decimal LeadVocalsProbStrongBeat { get; set; }
            public decimal LeadVocalsProbWeakBeat { get; set; }
            public int LeadVocalsThoughtLength { get; set; }
            public double LeadVocalsMelodyVariation { get; set; }
            public double LeadVocalsRhythmVariation { get; set; }
            public double[] LeadVocalsVariationSection { get; set; }
            public double LeadVocalsStartsOnChord { get; set; }
            public List<int> LeadVocalsStartsOnChordComp { get; set; }
            public double LeadVocalsEndsOnChord { get; set; }
            public List<int> LeadVocalsEndsOnChordComp { get; set; }
            public List<double> LeadVocalsCurrentChordBias { get; set; }
            //public string LeadVocalsThoughtPattern { get; set; }
            public bool LeadVocalsThoughtsRising { get; set; }
            public int LeadVocalsDelayInProgressions { get; set; }
            public int LeadVocalsThoughtDirection { get; set; }
            public double LeadVocalsDirectionStrength { get; set; }
            public double LeadVocalsRisingOverlap { get; set; }
            public int LeadVocalsWeightShape { get; set; }
            public double LeadVocalsBoomerangTurningPoint { get; set; }
            public decimal LeadVocalsGlissChance { get; set; }
            public decimal LeadVocalsLastSyllableChance { get; set; }
            public decimal LeadVocalsLastSyllableStart { get; set; }
            public decimal LeadVocalsEndRestLength { get; set; }
            public decimal LeadVocalsStartRestChance { get; set; }
            public decimal LeadVocalsMaxStartRestSize { get; set; }
            public decimal LeadVocalsRestChance { get; set; }
            public int LeadVocalsStartRestUnitIndex { get; set; }
        }
        public ParamsSet defaults = new();
        //private int[] _probhelp = new int[26]
        //{
        //        5, 1, 1, 8, 8, 1, 1, 8, 8, 1, 1, 5, 1, 1, 4, 1, 1, 3, 3, 1, 1, 2, 2, 1, 1, 1
        //};
        //public int[] Probhelp
        //{
        //    get { return _probhelp; }
        //    set { _probhelp = value; }
        //}
        private readonly string[] notesForCombo =
        {
            "C",
            "C#/Db",
            "D",
            "D#/Eb",
            "E",
            "F",
            "F#/Gb",
            "G",
            "G#/Ab",
            "A",
            "A#/Bb",
            "B"
        };
        private readonly string defaultName = "music";
        private static readonly int maxPhrases = 26;
        private static readonly int maxThoughts = 26;
        // Main controls
        public string CurrentPath { get; set; }
        public string CurrentFileName { get; set; }
        public int CurrentPhrase { get; set; }
        public string PhrasesAllowed { get; set; }
        public int[] Meterup { get; set; } = new int[maxPhrases];
        public int[] Barsq { get; set; } = new int[maxPhrases];
        public int[] Bpm { get; set; } = new int[maxPhrases];
        public string[] Key { get; set; } = new string[maxPhrases];
        public int[] KeyIndices { get; set; } = new int[maxPhrases];
        public string[] Scales { get; set; } = new string[maxPhrases];
        public int[] ScalesIndices { get; set; } = new int[maxPhrases];
        public int[] BeatLength { get; set; } = new int[maxPhrases];
        public int[] BeatLengthIndices { get; set; } = new int[maxPhrases];
        public int[] ProgressionLengthInBars { get; set; } = new int[maxPhrases];
        public int[] ProgressionLengthInBarsIndices { get; set; } = new int[maxPhrases];
        public int[] ChordLengthInStrongBeats { get; set; } = new int[maxPhrases];
        public int[] ChordLengthInStrongBeatsIndices { get; set; } = new int[maxPhrases];
        public bool[] FullChords { get; set; } = new bool[maxPhrases];
        public bool[] ForceKickOn1 { get; set; } = new bool[maxPhrases];
        public bool[] ForceTonicOn1 { get; set; } = new bool[maxPhrases];
        public bool[] ForceBassOn1 { get; set; } = new bool[maxPhrases];
        public bool[] ForceBassOnEveryStrongBeat { get; set; } = new bool[maxPhrases];
        // Strong beats
        public List<int>[] StrongBeats { get; set; } = new List<int>[maxPhrases];
        // Chords probabilities
        public List<int>[] ChordsProbabilities { get; set; } = new List<int>[maxPhrases];
        // Rhythm guitar rhythmic values probabilities
        public List<int>[] RhythmGuitarValuesProb { get; set; } = new List<int>[maxPhrases];
        //// LEAD GUITAR SETTINGS
        //public string[] LowNote { get; set; } = new string[maxPhrases];
        //public string[] HighNote { get; set; } = new string[maxPhrases];
        //public string[] LowOctave { get; set; } = new string[maxPhrases];
        //public string[] HighOctave { get; set; } = new string[maxPhrases];
        //public int[] LowNoteIndex { get; set; } = new int[maxPhrases];
        //public int[] HighNoteIndex { get; set; } = new int[maxPhrases];
        //public int[] LowOctaveIndex { get; set; } = new int[maxPhrases];
        //public int[] HighOctaveIndex { get; set; } = new int[maxPhrases];
        //public decimal[] LeadProbBar { get; set; } = new decimal[maxPhrases];
        //public decimal[] LeadProbStrongBeat { get; set; } = new decimal[maxPhrases];
        //public decimal[] LeadProbWeakBeat { get; set; } = new decimal[maxPhrases];
        //public bool[] LeadForceTonicOn1 { get; set; } = new bool[maxPhrases];
        //public int[] Rerolls { get; set; } = new int[maxPhrases];
        //// Lead guitar notes probabilities
        //public List<int>[] LeadGuitarNotesProb { get; set; } = new List<int>[maxPhrases];
        //// Lead guitar rhythmic values probabilities
        //public List<int>[] LeadGuitarRhythmProb { get; set; } = new List<int>[maxPhrases];
        //// Lead guitar intervals probabilities
        //public List<int>[] LeadGuitarInterProb { get; set; } = new List<int>[maxPhrases];

        // LEAD VOCALS SETTINGS
        public string[] LeadVocalsThoughtPattern { get; set; } = new string[maxPhrases];
        public int[] LeadVocalsCurrentThought { get; set; } = new int[maxPhrases];
        public int[] LeadVocalsDistinctThoughtsCount { get; set; } = new int[maxPhrases];
        public List<int>[][] LeadVocalsNotesProb { get; set; } = new List<int>[maxPhrases][];
        public List<int>[][] LeadVocalsInterProb { get; set; } = new List<int>[maxPhrases][];
        public List<int>[][] LeadVocalsRhythmProb { get; set; } = new List<int>[maxPhrases][];
        public int[][][] LeadVocalsDrawWeights { get; set; } = new int[maxPhrases][][];
        // public string[][] LeadVocalsLowNote { get; set; } = new string[maxPhrases][];
        // public string[][] LeadVocalsLowOctave { get; set; } = new string[maxPhrases][];
        public int[][] LeadVocalsLowNoteIndex { get; set; } = new int[maxPhrases][];
        public int[][] LeadVocalsLowOctaveIndex { get; set; } = new int[maxPhrases][];
        // public string[][] LeadVocalsHighNote { get; set; } = new string[maxPhrases][];
        // public string[][] LeadVocalsHighOctave { get; set; } = new string[maxPhrases][];
        public int[][] LeadVocalsHighNoteIndex { get; set; } = new int[maxPhrases][];
        public int[][] LeadVocalsHighOctaveIndex { get; set; } = new int[maxPhrases][];
        public decimal[][] LeadVocalsProbBar { get; set; } = new decimal[maxPhrases][];
        public decimal[][] LeadVocalsProbStrongBeat { get; set; } = new decimal[maxPhrases][];
        public decimal[][] LeadVocalsProbWeakBeat { get; set; } = new decimal[maxPhrases][];
        public int[][] LeadVocalsThoughtLength { get; set; } = new int[maxPhrases][]; // in quarter notes?
        public double[][] LeadVocalsMelodyVariation { get; set; } = new double[maxPhrases][];
        public double[][] LeadVocalsRhythmVariation { get; set; } = new double[maxPhrases][];
        public double[][][] LeadVocalsVariationSection { get; set; } = new double[maxPhrases][][];
        public double[][] LeadVocalsStartsOnChord { get; set; } = new double[maxPhrases][];
        public List<int>[][] LeadVocalsStartsOnChordComp { get; set; } = new List<int>[maxPhrases][];
        public double[][] LeadVocalsEndsOnChord { get; set; } = new double[maxPhrases][];
        public List<int>[][] LeadVocalsEndsOnChordComp { get; set; } = new List<int>[maxPhrases][];
        public List<double>[][] LeadVocalsCurrentChordBias { get; set; } = new List<double>[maxPhrases][];
        //public string[] LeadVocalsThoughtPattern { get; set; } = new string[maxPhrases];
        //public bool[][] LeadVocalsThoughtsRising { get; set; } = new bool[maxPhrases][];
        public bool[] LeadVocalsThoughtsRising { get; set; } = new bool[maxPhrases];
        public int[] LeadVocalsDelayInProgressions { get; set; } = new int[maxPhrases];
        public int[][] LeadVocalsThoughtDirection { get; set; } = new int[maxPhrases][];
        public double[][] LeadVocalsDirectionStrength { get; set; } = new double[maxPhrases][];
        //public double[][] LeadVocalsRisingOverlap { get; set; } = new double[maxPhrases][];
        public double[] LeadVocalsRisingOverlap { get; set; } = new double[maxPhrases];
        public int[][] LeadVocalsWeightShape { get; set; } = new int[maxPhrases][];
        public double[][] LeadVocalsBoomerangTurningPoint { get; set; } = new double[maxPhrases][];
        public decimal[][] LeadVocalsGlissChance { get; set; } = new decimal[maxPhrases][];
        public decimal[][] LeadVocalsLastSyllableChance { get; set; } = new decimal[maxPhrases][];
        public decimal[][] LeadVocalsLastSyllableStart { get; set; } = new decimal[maxPhrases][];
        public decimal[][] LeadVocalsEndRestLength { get; set; } = new decimal[maxPhrases][];
        public decimal[][] LeadVocalsStartRestChance { get; set; } = new decimal[maxPhrases][];
        public decimal[][] LeadVocalsMaxStartRestSize { get; set; } = new decimal[maxPhrases][];
        public decimal[][] LeadVocalsRestChance { get; set; } = new decimal[maxPhrases][];
        public int[][] LeadVocalsStartRestUnitIndex { get; set; } = new int[maxPhrases][];
        // MIDI settings
        public bool[] TransposeLead { get; set; } = new bool[maxPhrases];
        public bool[] TransposeRhythm { get; set; } = new bool[maxPhrases];
        public bool[] TransposeBass { get; set; } = new bool[maxPhrases];
        //public int[] KeyIndex { get; set; } = new int[maxPhrases];
        private readonly string allPhrases = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public readonly string allThoughts = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private readonly StrongBeats formStrongBeats;
        private readonly ChordsProbability formChordsProbability;
        private readonly RhythmGuitarValuesProb formRhythmGuitarValuesProb;
        //private LeadGuitarNotesProb formLeadGuitarNotesProb;
        //private LeadGuitarRhythmProb formLeadGuitarRhythmProb;
        //private LeadGuitarInterProb formLeadGuitarInterProb;
        private readonly MidiSettings formMidiSettings;
        // private readonly LeadGuitarSettings formLeadGuitarSettings;
        private readonly LeadVocalsSettings formLeadVocalsSettings;
        private bool supressKeyChange = false;
        private readonly string[] lilyStart1 =
            {
                "\\version \"2.22.0\"",
                "",
                "#(set-default-paper-size \"a3\")",
                "",
            };
        private readonly string[] lilyStart2 =
            {
                "\\score {",
                //"\\layout { indent = 0\\in }",
                "\\layout { }",
                //"\\midi { }",
                "<<",
            };
        private readonly string[] lilyEnd =
            {
                ">>",
                "}",
            };
        private string[] GetLilyMidi()
        {
            string lead = "e";
            string rhythm = "e";
            string bass = "e";
            string phraseSequence = textBoxPattern.Text;
            if (TransposeLead[CurrentPhrase]) lead += "'";
            if (TransposeRhythm[CurrentPhrase]) rhythm += "'";
            if (TransposeBass[CurrentPhrase]) bass += "'";
            return new string[]
            {
                "\\score {",
            "\\midi { }",
            "<<",
            "\\new Staff \\with {midiInstrument = \"distorted guitar\" }",
            //"{ \\transpose e " + lead + " { " + Toolbox.GetTrackSongSequence(phraseSequence, "leadGuitar", Meterup, Bpm, KeyIndices) + "} }",
            "{ \\transpose e " + lead + " { " + Toolbox.GetTrackSongSequence(phraseSequence, "leadVocals", Meterup, Bpm, KeyIndices) + "} }",
            "\\new Staff \\with {midiInstrument = \"acoustic guitar (steel)\" }",
            "{ \\transpose e " + rhythm + " { " + Toolbox.GetTrackSongSequence(phraseSequence, "rhythmGuitar", Meterup, Bpm, KeyIndices) + "} }",
            "\\new Staff \\with {midiInstrument = \"electric bass (finger)\" }",
            "{ \\transpose e " + bass + " { " + Toolbox.GetTrackSongSequence(phraseSequence, "bassGuitar", Meterup, Bpm, KeyIndices) + "} }",
            "\\new DrumStaff \\with { instrumentName = \"Drums\" } <<",
            //"  \\drummode { \\numericTimeSignature \\time 4/4 \\tempo 4 = 120",
            "  \\drummode {",
            "    << { " + Toolbox.GetTrackSongSequenceNoKey(phraseSequence, "drumsUp", Meterup, Bpm) + "}",
            @"      \\",
            "       { " + Toolbox.GetTrackSongSequenceNoKey(phraseSequence, "drumsDown", Meterup, Bpm) + "} >>",
            "  }",
            ">>",
            ">>",
            "}",
            };
        }
        public Main()
        {
            InitializeComponent();
            CurrentPath = Directory.GetCurrentDirectory();
            textBoxFileName.Text = defaultName;
            CurrentFileName = textBoxFileName.Text;
            PhrasesAllowed = allPhrases[0..(int)numericDistinctPhrases.Value];
            comboBoxPhrases.Items.Clear();
            for (int i = 0; i < numericDistinctPhrases.Value; i++)
            {
                comboBoxPhrases.Items.Add(PhrasesAllowed[i]);
            }
            comboBoxPhrases.Items.Add("All");
            InitializeDefaults();

            formStrongBeats = new(this);
            formStrongBeats.Owner = this;
            formChordsProbability = new(this);
            formChordsProbability.Owner = this;
            formRhythmGuitarValuesProb = new(this);
            formRhythmGuitarValuesProb.Owner = this;
            formMidiSettings = new(this);
            formMidiSettings.Owner = this;
            //formLeadGuitarSettings = new(this);
            //formLeadGuitarSettings.Owner = this;
            formLeadVocalsSettings = new(this);
            formLeadVocalsSettings.Owner = this;

            for (int i = 0; i < maxPhrases; i++)
            {
                InitStrongBeats(i);
                ChordsProbabilities[i] = new();
                RhythmGuitarValuesProb[i] = new();
                //LeadGuitarNotesProb[i] = new();
                //LeadGuitarRhythmProb[i] = new();
                //LeadGuitarInterProb[i] = new();
                LeadVocalsNotesProb[i] = new List<int>[maxThoughts];
                LeadVocalsRhythmProb[i] = new List<int>[maxThoughts];
                LeadVocalsInterProb[i] = new List<int>[maxThoughts];
                LeadVocalsStartsOnChordComp[i] = new List<int>[maxThoughts];
                LeadVocalsEndsOnChordComp[i] = new List<int>[maxThoughts];
                LeadVocalsCurrentChordBias[i] = new List<double>[maxThoughts];
                LeadVocalsDrawWeights[i] = new int[maxThoughts][];
                LeadVocalsLowOctaveIndex[i] = new int[maxThoughts];
                LeadVocalsLowNoteIndex[i] = new int[maxThoughts];
                LeadVocalsHighOctaveIndex[i] = new int[maxThoughts];
                LeadVocalsHighNoteIndex[i] = new int[maxThoughts];
                LeadVocalsProbBar[i] = new decimal[maxThoughts];
                LeadVocalsProbStrongBeat[i] = new decimal[maxThoughts];
                LeadVocalsProbWeakBeat[i] = new decimal[maxThoughts];
                LeadVocalsThoughtLength[i] = new int[maxThoughts];
                LeadVocalsMelodyVariation[i] = new double[maxThoughts];
                LeadVocalsRhythmVariation[i] = new double[maxThoughts];
                LeadVocalsVariationSection[i] = new double[maxThoughts][];
                LeadVocalsStartsOnChord[i] = new double[maxThoughts];
                LeadVocalsEndsOnChord[i] = new double[maxThoughts];
                //LeadVocalsThoughtsRising[i] = new bool[maxThoughts];
                LeadVocalsThoughtDirection[i] = new int[maxThoughts];
                LeadVocalsDirectionStrength[i] = new double[maxThoughts];
                //LeadVocalsRisingOverlap[i] = new double[maxThoughts];
                LeadVocalsWeightShape[i] = new int[maxThoughts];
                LeadVocalsBoomerangTurningPoint[i] = new double[maxThoughts];
                LeadVocalsGlissChance[i] = new decimal[maxThoughts];
                LeadVocalsLastSyllableChance[i] = new decimal[maxThoughts];
                LeadVocalsLastSyllableStart[i] = new decimal[maxThoughts];
                LeadVocalsEndRestLength[i] = new decimal[maxThoughts];
                LeadVocalsStartRestChance[i] = new decimal[maxThoughts];
                LeadVocalsMaxStartRestSize[i] = new decimal[maxThoughts];
                LeadVocalsRestChance[i] = new decimal[maxThoughts];
                LeadVocalsStartRestUnitIndex[i] = new int[maxThoughts];
                for (int j = 0; j < maxThoughts; j++)
                {
                    LeadVocalsNotesProb[i][j] = new();
                    LeadVocalsRhythmProb[i][j] = new();
                    LeadVocalsInterProb[i][j] = new();
                    LeadVocalsStartsOnChordComp[i][j] = new();
                    LeadVocalsEndsOnChordComp[i][j] = new();
                    LeadVocalsCurrentChordBias[i][j] = new();
                    LeadVocalsDrawWeights[i][j] = new int[3];
                    LeadVocalsVariationSection[i][j] = new double[2];
                }
                SetDefaults(i);
            }

            //comboBoxPhrases.SelectedIndex = 0;
            //CurrentPhrase = 0;
            //comboBoxKey.SelectedIndex = 7;
            //comboBoxScales.SelectedIndex = 0;
            //comboBoxProgressionLength.SelectedIndex = 1;
            //comboBoxChordLength.SelectedIndex = 0;
            //comboBoxDrumLickLength.SelectedIndex = 1;
            //comboBoxPhrases.SelectedIndex = 0;
            comboBoxPhrases.SelectedIndex = 0;
            CurrentPhrase = 0;
            comboBoxKey.SelectedIndex = defaults.KeyIndex;
            comboBoxScales.SelectedIndex = defaults.ScalesIndex;
            comboBoxProgressionLength.SelectedIndex = defaults.ProgressionLengthInBarsIndex;
            comboBoxChordLength.SelectedIndex = defaults.ChordLengthInStrongBeatsIndex;
            comboBoxDrumLickLength.SelectedIndex = defaults.BeatLength;

            UpdateRhythmGuitarValuesProb(CurrentPhrase);
            SetPath();
        }
        private void SetPath()
        {
            textBoxPath.Text = CurrentPath + "\\" + CurrentFileName + ".ly";
        }
        private static string IncrementFileName(string[] files, string fileName)
        {
            if (files.Length > 0)
            {
                string fileName1 = fileName;
                int fileName2 = 0;
                List<int> files2 = new();
                bool dupe = false;
                bool digitsInFileName = false;
                for (int i = fileName.Length - 1; i > -1; i--)
                {
                    if (!char.IsDigit(fileName[i]))
                    {
                        if (i != fileName.Length - 1)
                        {
                            fileName1 = fileName[..(i + 1)];
                            fileName2 = Int32.Parse(fileName[(i + 1)..]);
                            digitsInFileName = true;
                        }
                        else digitsInFileName = false;
                        break;
                    }
                }
                for (int i = 0; i < files.Length; i++)
                {
                    files[i] = files[i][2..^3];
                    if (files[i] == fileName) dupe = true;
                    if (files[i].StartsWith(fileName1))
                    {
                        for (int j = files[i].Length - 1; j > -1; j--)
                        {
                            if (!char.IsDigit(files[i][j]))
                            {
                                if (files[i][..(j + 1)] == fileName1)
                                    if (j != files[i].Length - 1)
                                        if (Int32.Parse(files[i][(j + 1)..]) >= fileName2)
                                            files2.Add(Int32.Parse(files[i][(j + 1)..]));
                                break;
                            }
                        }
                    }
                }
                if (!dupe) return fileName;
                if (files2.Count == 0 && !digitsInFileName)
                    return fileName1 + "1";
                if (!digitsInFileName) fileName2 = files2.Min();
                for (int i = 1; i < files2.Count + 1; i++)
                {
                    if (!files2.Contains(fileName2 + i))
                        return fileName1 + (fileName2 + i).ToString();
                }
            }
            return fileName;
        }
        private void UpdatePath()
        {
            if (string.IsNullOrWhiteSpace(textBoxFileName.Text) && radioIncrement.Checked)
                CurrentFileName = IncrementFileName(Directory.GetFiles(".", "*.ly"), defaultName);
            else if (string.IsNullOrWhiteSpace(textBoxFileName.Text) && !radioIncrement.Checked) CurrentFileName = defaultName;
            else if (radioIncrement.Checked) CurrentFileName = IncrementFileName(Directory.GetFiles(".", "*.ly"), textBoxFileName.Text);
            else CurrentFileName = textBoxFileName.Text;
            SetPath();
        }
        private void InitStrongBeats(int phraseIndex)
        {
            int metersum = 0;
            List<int> strongBeats = new();
            if (Meterup[phraseIndex] % 3 == 0)
                for (; metersum < Meterup[phraseIndex]; metersum += 3)
                    strongBeats.Add(metersum);
            else
            {
                while (metersum < Meterup[phraseIndex])
                {
                    if (Meterup[phraseIndex] - metersum == 3)
                    {
                        strongBeats.Add(metersum);
                        break;
                    }
                    else
                    {
                        strongBeats.Add(metersum);
                        metersum += 2;
                    }
                }
            }
            StrongBeats[phraseIndex] = strongBeats;
            if (phraseIndex == PhrasesAllowed.Length - 1)
                for (int i = 0; i < PhrasesAllowed.Length; i++)
                    StrongBeats[i] = strongBeats;
        }
        private void InitChordsProbabilities()
        {
            int scale = -1;
            //switch ((string)comboBoxScales.SelectedItem)
            //{
            //    case "Ionian": ChordsProbabilities[CurrentPhrase].AddRange(new int[] { 10, 2, 3, 6, 8, 5, 1 }); break;
            //    case "Dorian": ChordsProbabilities[CurrentPhrase].AddRange(new int[] { 10, 2, 7, 7, 5, 1, 7 }); break;
            //    case "Phrygian": ChordsProbabilities[CurrentPhrase].AddRange(new int[] { 10, 2, 5, 6, 6, 5, 5 }); break;
            //    case "Lydian": ChordsProbabilities[CurrentPhrase].AddRange(new int[] { 10, 2, 3, 4, 8, 5, 1 }); break;
            //    case "Mixolydian": ChordsProbabilities[CurrentPhrase].AddRange(new int[] { 10, 2, 1, 6, 8, 5, 3 }); break;
            //    case "Aeolian": ChordsProbabilities[CurrentPhrase].AddRange(new int[] { 10, 1, 7, 7, 7, 5, 7 }); break;
            //    case "Locrian": ChordsProbabilities[CurrentPhrase].AddRange(new int[] { 10, 2, 4, 7, 7, 4, 4 }); break;
            //}
            switch ((string)comboBoxScales.SelectedItem)
            {
                case "Ionian": scale = 0; break;
                case "Dorian": scale = 1; break;
                case "Phrygian": scale = 2; break;
                case "Lydian": scale = 3; break;
                case "Mixolydian": scale = 4; break;
                case "Aeolian": scale = 5; break;
                case "Locrian": scale = 6; break;
            }
            for (int i = 0; i < defaults.ChordsProbabilities[0].Count; i++)
            {
                ChordsProbabilities[CurrentPhrase].Add(defaults.ChordsProbabilities[scale][i]);
            }
        }
        private void InitializeDefaults()
        {
            defaults.StrongBeats = new()
            {
                0,
                2
            };
            //defaults.ChordsProbabilities = new List<int>[7]
            //{
            //    new() { 10, 2, 3, 6, 8, 5, 1 },     // Ionian
            //    new() { 10, 2, 7, 7, 5, 1, 7 },     // Dorian
            //    new() { 10, 2, 5, 6, 6, 5, 5 },     // Phrygian
            //    new() { 10, 2, 3, 4, 8, 5, 1 },     // Lydian
            //    new() { 10, 2, 1, 6, 8, 5, 3 },     // Mixolydian
            //    new() { 10, 1, 7, 7, 7, 5, 7 },     // Aeolian
            //    new() { 10, 2, 4, 7, 7, 4, 4 }      // Locrian
            //};
            defaults.ChordsProbabilities = new List<int>[7]
            {
                new() { 3, 2, 3, 6, 8, 5, 1 },     // Ionian
                new() { 3, 2, 7, 7, 5, 1, 7 },     // Dorian
                new() { 3, 2, 5, 6, 6, 5, 5 },     // Phrygian
                new() { 3, 2, 3, 4, 8, 5, 1 },     // Lydian
                new() { 3, 2, 1, 6, 8, 5, 3 },     // Mixolydian
                new() { 3, 1, 7, 7, 7, 5, 7 },     // Aeolian
                new() { 3, 2, 4, 7, 7, 4, 4 }      // Locrian
            };
            defaults.RhythmGuitarValuesProb = new()
            {
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                1,
                2,
                2
            };
            //defaults.LeadGuitarNotesProb = new()
            //{
            //    10, 2, 2, 6, 7, 2, 2
            //};
            //defaults.LeadGuitarRhythmProb = new()
            //{
            //    0, 0, 0, 0, 1, 0, 3, 1, 6, 7, 8, 1
            //};
            //defaults.LeadGuitarInterProb = new()
            //{
            //    4, 20, 15, 4, 2, 1, 1, 0
            //};
            defaults.Meterup = 4;
            //defaults.LowOctaveIndex = 1;
            //defaults.LowNoteIndex = 4;
            //defaults.HighOctaveIndex = 3;
            //defaults.HighNoteIndex = 4;
            //defaults.LowOctave = "Small";
            //defaults.LowNote = "E";
            //defaults.HighOctave = "2 Line";
            //defaults.HighNote = "E";
            defaults.TransposeLead = true;
            defaults.TransposeRhythm = false;
            defaults.TransposeBass = true;
            //defaults.LeadProbBar = 0.9M;
            //defaults.LeadProbStrongBeat = 0.7M;
            //defaults.LeadProbWeakBeat = 0.4M;
            //defaults.Rerolls = 5;
            //defaults.LeadForceTonicOn1 = true;
            defaults.Barsq = 16;
            defaults.ForceKickOn1 = true;
            defaults.Bpm = 90;
            defaults.Key = "D";
            defaults.KeyIndex = 7;
            defaults.Scales = "Dorian";
            defaults.ScalesIndex = 1;
            defaults.BeatLength = 2;
            defaults.BeatLengthIndex = 1;
            defaults.ProgressionLengthInBars = 2;
            defaults.ProgressionLengthInBarsIndex = 1;
            defaults.ChordLengthInStrongBeats = 1;
            defaults.ChordLengthInStrongBeatsIndex = 0;
            defaults.FullChords = true;
            defaults.ForceTonicOn1 = true;
            defaults.ForceBassOn1 = true;
            defaults.ForceBassOnEveryStrongBeat = true;
            // Lead vocals
            //public int[] LeadVocalsCurrentThought { get; set; }
            //public int[] LeadVocalsDistinctThoughtsCount { get; set; }
            //public string[] LeadVocalsThoughtPattern { get; set; }
            defaults.LeadVocalsThoughtPattern = "A";
            defaults.LeadVocalsCurrentThought = 0;
            defaults.LeadVocalsDistinctThoughtsCount = 4;
            defaults.LeadVocalsDelayInProgressions = 0;
            //defaults.LeadVocalsNotesProb = new()
            //{
            //    10,
            //    2,
            //    2,
            //    6,
            //    7,
            //    2,
            //    2
            //};
            defaults.LeadVocalsNotesProb = new()
            {
                7,
                2,
                7,
                7,
                7,
                2,
                7
            };
            defaults.LeadVocalsInterProb = new()
            {
                4,
                20,
                15,
                4,
                1,
                1,
                0,
                0
            };
            //defaults.LeadVocalsRhythmProb = new()
            //{
            //    0,
            //    0,
            //    0,
            //    0,
            //    1,
            //    0,
            //    3,
            //    1,
            //    6,
            //    7,
            //    8,
            //    1
            //};
            defaults.LeadVocalsRhythmProb = new()
            {
                0,
                0,
                0,
                0,
                1,
                0,
                2,
                1,
                6,
                7,
                8,
                2
            };
            defaults.LeadVocalsDrawWeights = new int[] { 3, 5, 10 }; // pitch, step, interval
            defaults.LeadVocalsLowOctaveIndex = 3; // C small (tenor)
            defaults.LeadVocalsLowNoteIndex = 0;
            defaults.LeadVocalsHighOctaveIndex = 4; // A 1 line (tenor)
            defaults.LeadVocalsHighNoteIndex = 9;
            defaults.LeadVocalsProbBar = 0.8M;
            defaults.LeadVocalsProbStrongBeat = 0.8M;
            defaults.LeadVocalsProbWeakBeat = 0.8M;
            defaults.LeadVocalsThoughtLength = 2;
            defaults.LeadVocalsMelodyVariation = 0.5;
            defaults.LeadVocalsRhythmVariation = 0.5;
            defaults.LeadVocalsVariationSection = new double[] { 0.5, 1 };
            //defaults.LeadVocalsStartsOnChord = 0;
            //defaults.LeadVocalsStartsOnChordComp = new() { 0, 0, 0 };
            //defaults.LeadVocalsEndsOnChord = 0;
            //defaults.LeadVocalsEndsOnChordComp = new() { 0, 0, 0 };
            defaults.LeadVocalsStartsOnChord = -0.01;
            defaults.LeadVocalsStartsOnChordComp = new() { -1, 0, 0 };
            defaults.LeadVocalsEndsOnChord = -0.01;
            defaults.LeadVocalsEndsOnChordComp = new() { -1, 0, 0 };
            defaults.LeadVocalsCurrentChordBias = new() { 4.4, 4.4, 4.4 };
            defaults.LeadVocalsThoughtsRising = true;
            defaults.LeadVocalsThoughtDirection = 5;
            defaults.LeadVocalsDirectionStrength = 1;
            defaults.LeadVocalsRisingOverlap = 0.75;
            defaults.LeadVocalsWeightShape = 0;
            defaults.LeadVocalsBoomerangTurningPoint = 0.75;
            defaults.LeadVocalsGlissChance = 0.5M;
            defaults.LeadVocalsLastSyllableChance = 0.75M;
            defaults.LeadVocalsLastSyllableStart = 0.75M;
            defaults.LeadVocalsEndRestLength = 1;
            defaults.LeadVocalsStartRestChance = 0.2M;
            defaults.LeadVocalsMaxStartRestSize = 3;
            defaults.LeadVocalsRestChance = 0.1M;
            defaults.LeadVocalsStartRestUnitIndex = 1;
        }
        private void SetDefaults(int phraseIndex)
        {
            StrongBeats[phraseIndex].Clear();
            for (int i = 0; i < defaults.StrongBeats.Count; i++)
                StrongBeats[phraseIndex].Add(defaults.StrongBeats[i]);

            ChordsProbabilities[phraseIndex].Clear();
            for (int i = 0; i < defaults.ChordsProbabilities[0].Count; i++)
                ChordsProbabilities[phraseIndex].Add(defaults.ChordsProbabilities[0][i]);

            RhythmGuitarValuesProb[phraseIndex].Clear();
            for (int i = 0; i < defaults.RhythmGuitarValuesProb.Count; i++)
                RhythmGuitarValuesProb[phraseIndex].Add(defaults.RhythmGuitarValuesProb[i]);

            //LeadGuitarNotesProb[phraseIndex].Clear();
            //for (int i = 0; i < defaults.LeadGuitarNotesProb.Count; i++)
            //    LeadGuitarNotesProb[phraseIndex].Add(defaults.LeadGuitarNotesProb[i]);

            //LeadGuitarRhythmProb[phraseIndex].Clear();
            //for (int i = 0; i < defaults.LeadGuitarRhythmProb.Count; i++)
            //    LeadGuitarRhythmProb[phraseIndex].Add(defaults.LeadGuitarRhythmProb[i]);

            //LeadGuitarInterProb[phraseIndex].Clear();
            //for (int i = 0; i < defaults.LeadGuitarInterProb.Count; i++)
            //    LeadGuitarInterProb[phraseIndex].Add(defaults.LeadGuitarInterProb[i]);

            Meterup[phraseIndex] = defaults.Meterup;
            //LowOctaveIndex[phraseIndex] = defaults.LowOctaveIndex;
            //LowNoteIndex[phraseIndex] = defaults.LowNoteIndex;
            //HighOctaveIndex[phraseIndex] = defaults.HighOctaveIndex;
            //HighNoteIndex[phraseIndex] = defaults.HighNoteIndex;
            //LowOctave[phraseIndex] = defaults.LowOctave;
            //LowNote[phraseIndex] = defaults.LowNote;
            //HighOctave[phraseIndex] = defaults.HighOctave;
            //HighNote[phraseIndex] = defaults.HighNote;
            TransposeLead[phraseIndex] = defaults.TransposeLead;
            TransposeRhythm[phraseIndex] = defaults.TransposeRhythm;
            TransposeBass[phraseIndex] = defaults.TransposeBass;
            //LeadProbBar[phraseIndex] = defaults.LeadProbBar;
            //LeadProbStrongBeat[phraseIndex] = defaults.LeadProbStrongBeat;
            //LeadProbWeakBeat[phraseIndex] = defaults.LeadProbWeakBeat;
            //Rerolls[phraseIndex] = defaults.Rerolls;
            //LeadForceTonicOn1[phraseIndex] = defaults.LeadForceTonicOn1;
            Barsq[phraseIndex] = defaults.Barsq;
            ForceKickOn1[phraseIndex] = defaults.ForceKickOn1;
            Bpm[phraseIndex] = defaults.Bpm;
            Key[phraseIndex] = defaults.Key;
            KeyIndices[phraseIndex] = defaults.KeyIndex;
            Scales[phraseIndex] = defaults.Scales;
            ScalesIndices[phraseIndex] = defaults.ScalesIndex;
            BeatLength[phraseIndex] = defaults.BeatLength;
            BeatLengthIndices[phraseIndex] = defaults.BeatLengthIndex;
            ProgressionLengthInBars[phraseIndex] = defaults.ProgressionLengthInBars;
            ProgressionLengthInBarsIndices[phraseIndex] = defaults.ProgressionLengthInBarsIndex;
            ChordLengthInStrongBeats[phraseIndex] = defaults.ChordLengthInStrongBeats;
            ChordLengthInStrongBeatsIndices[phraseIndex] = defaults.ChordLengthInStrongBeatsIndex;
            FullChords[phraseIndex] = defaults.FullChords;
            ForceTonicOn1[phraseIndex] = defaults.ForceTonicOn1;
            ForceBassOn1[phraseIndex] = defaults.ForceBassOn1;
            ForceBassOnEveryStrongBeat[phraseIndex] = defaults.ForceBassOnEveryStrongBeat;

            // Lead vocals

            LeadVocalsDelayInProgressions[phraseIndex] = defaults.LeadVocalsDelayInProgressions;
            LeadVocalsThoughtPattern[phraseIndex] = defaults.LeadVocalsThoughtPattern;
            LeadVocalsCurrentThought[phraseIndex] = defaults.LeadVocalsCurrentThought;
            LeadVocalsDistinctThoughtsCount[phraseIndex] = defaults.LeadVocalsDistinctThoughtsCount;
            LeadVocalsThoughtsRising[phraseIndex] = defaults.LeadVocalsThoughtsRising;
            LeadVocalsRisingOverlap[phraseIndex] = defaults.LeadVocalsRisingOverlap;
            for (int i = 0; i < allThoughts.Length; i++)
            {
                //LeadVocalsNotesProb[phraseIndex][i] = defaults.LeadVocalsNotesProb;
                //LeadVocalsInterProb[phraseIndex][i] = defaults.LeadVocalsInterProb;
                //LeadVocalsRhythmProb[phraseIndex][i] = defaults.LeadVocalsRhythmProb;
                //LeadVocalsDrawWeights[phraseIndex][i] = defaults.LeadVocalsDrawWeights;
                LeadVocalsLowOctaveIndex[phraseIndex][i] = defaults.LeadVocalsLowOctaveIndex;
                LeadVocalsLowNoteIndex[phraseIndex][i] = defaults.LeadVocalsLowNoteIndex;
                LeadVocalsHighOctaveIndex[phraseIndex][i] = defaults.LeadVocalsHighOctaveIndex;
                LeadVocalsHighNoteIndex[phraseIndex][i] = defaults.LeadVocalsHighNoteIndex;
                LeadVocalsProbBar[phraseIndex][i] = defaults.LeadVocalsProbBar;
                LeadVocalsProbStrongBeat[phraseIndex][i] = defaults.LeadVocalsProbStrongBeat;
                LeadVocalsProbWeakBeat[phraseIndex][i] = defaults.LeadVocalsProbWeakBeat;
                LeadVocalsThoughtLength[phraseIndex][i] = defaults.LeadVocalsThoughtLength;
                LeadVocalsMelodyVariation[phraseIndex][i] = defaults.LeadVocalsMelodyVariation;
                LeadVocalsRhythmVariation[phraseIndex][i] = defaults.LeadVocalsRhythmVariation;
                //LeadVocalsVariationSection[phraseIndex][i] = defaults.LeadVocalsVariationSection;
                LeadVocalsStartsOnChord[phraseIndex][i] = defaults.LeadVocalsStartsOnChord;
                //LeadVocalsStartsOnChordComp[phraseIndex][i] = defaults.LeadVocalsStartsOnChordComp;
                LeadVocalsEndsOnChord[phraseIndex][i] = defaults.LeadVocalsEndsOnChord;
                //LeadVocalsEndsOnChordComp[phraseIndex][i] = defaults.LeadVocalsEndsOnChordComp;
                //LeadVocalsCurrentChordBias[phraseIndex][i] = defaults.LeadVocalsCurrentChordBias;
                LeadVocalsThoughtDirection[phraseIndex][i] = defaults.LeadVocalsThoughtDirection;
                LeadVocalsDirectionStrength[phraseIndex][i] = defaults.LeadVocalsDirectionStrength;
                LeadVocalsWeightShape[phraseIndex][i] = defaults.LeadVocalsWeightShape;
                LeadVocalsBoomerangTurningPoint[phraseIndex][i] = defaults.LeadVocalsBoomerangTurningPoint;
                LeadVocalsGlissChance[phraseIndex][i] = defaults.LeadVocalsGlissChance;
                LeadVocalsLastSyllableChance[phraseIndex][i] = defaults.LeadVocalsLastSyllableChance;
                LeadVocalsLastSyllableStart[phraseIndex][i] = defaults.LeadVocalsLastSyllableStart;
                LeadVocalsEndRestLength[phraseIndex][i] = defaults.LeadVocalsEndRestLength;
                LeadVocalsStartRestChance[phraseIndex][i] = defaults.LeadVocalsStartRestChance;
                LeadVocalsMaxStartRestSize[phraseIndex][i] = defaults.LeadVocalsMaxStartRestSize;
                LeadVocalsRestChance[phraseIndex][i] = defaults.LeadVocalsRestChance;
                LeadVocalsStartRestUnitIndex[phraseIndex][i] = defaults.LeadVocalsStartRestUnitIndex;
                // Collections
                for (int j = 0; j < defaults.LeadVocalsNotesProb.Count; j++)
                    LeadVocalsNotesProb[phraseIndex][i].Add(defaults.LeadVocalsNotesProb[j]);
                for (int j = 0; j < defaults.LeadVocalsInterProb.Count; j++)
                    LeadVocalsInterProb[phraseIndex][i].Add(defaults.LeadVocalsInterProb[j]);
                for (int j = 0; j < defaults.LeadVocalsRhythmProb.Count; j++)
                    LeadVocalsRhythmProb[phraseIndex][i].Add(defaults.LeadVocalsRhythmProb[j]);
                for (int j = 0; j < defaults.LeadVocalsDrawWeights.Length; j++)
                    LeadVocalsDrawWeights[phraseIndex][i][j] = defaults.LeadVocalsDrawWeights[j];
                for (int j = 0; j < defaults.LeadVocalsVariationSection.Length; j++)
                    LeadVocalsVariationSection[phraseIndex][i][j] = defaults.LeadVocalsVariationSection[j];
                for (int j = 0; j < defaults.LeadVocalsStartsOnChordComp.Count; j++)
                    LeadVocalsStartsOnChordComp[phraseIndex][i].Add(defaults.LeadVocalsStartsOnChordComp[j]);
                for (int j = 0; j < defaults.LeadVocalsEndsOnChordComp.Count; j++)
                    LeadVocalsEndsOnChordComp[phraseIndex][i].Add(defaults.LeadVocalsEndsOnChordComp[j]);
                for (int j = 0; j < defaults.LeadVocalsCurrentChordBias.Count; j++)
                    LeadVocalsCurrentChordBias[phraseIndex][i].Add(defaults.LeadVocalsCurrentChordBias[j]);

            }
        }
        public void SetDefaultsLeadVocals(int thoughtIndex)
        {
            LeadVocalsDelayInProgressions[CurrentPhrase] = defaults.LeadVocalsDelayInProgressions;
            LeadVocalsThoughtsRising[CurrentPhrase] = defaults.LeadVocalsThoughtsRising;
            LeadVocalsRisingOverlap[CurrentPhrase] = defaults.LeadVocalsRisingOverlap;
            LeadVocalsNotesProb[CurrentPhrase][thoughtIndex] = defaults.LeadVocalsNotesProb;
            LeadVocalsInterProb[CurrentPhrase][thoughtIndex] = defaults.LeadVocalsInterProb;
            LeadVocalsRhythmProb[CurrentPhrase][thoughtIndex] = defaults.LeadVocalsRhythmProb;
            LeadVocalsDrawWeights[CurrentPhrase][thoughtIndex] = defaults.LeadVocalsDrawWeights;
            LeadVocalsLowOctaveIndex[CurrentPhrase][thoughtIndex] = defaults.LeadVocalsLowOctaveIndex;
            LeadVocalsLowNoteIndex[CurrentPhrase][thoughtIndex] = defaults.LeadVocalsLowNoteIndex;
            LeadVocalsHighOctaveIndex[CurrentPhrase][thoughtIndex] = defaults.LeadVocalsHighOctaveIndex;
            LeadVocalsHighNoteIndex[CurrentPhrase][thoughtIndex] = defaults.LeadVocalsHighNoteIndex;
            LeadVocalsProbBar[CurrentPhrase][thoughtIndex] = defaults.LeadVocalsProbBar;
            LeadVocalsProbStrongBeat[CurrentPhrase][thoughtIndex] = defaults.LeadVocalsProbStrongBeat;
            LeadVocalsProbWeakBeat[CurrentPhrase][thoughtIndex] = defaults.LeadVocalsProbWeakBeat;
            LeadVocalsThoughtLength[CurrentPhrase][thoughtIndex] = defaults.LeadVocalsThoughtLength;
            LeadVocalsMelodyVariation[CurrentPhrase][thoughtIndex] = defaults.LeadVocalsMelodyVariation;
            LeadVocalsRhythmVariation[CurrentPhrase][thoughtIndex] = defaults.LeadVocalsRhythmVariation;
            LeadVocalsVariationSection[CurrentPhrase][thoughtIndex] = defaults.LeadVocalsVariationSection;
            LeadVocalsStartsOnChord[CurrentPhrase][thoughtIndex] = defaults.LeadVocalsStartsOnChord;
            LeadVocalsStartsOnChordComp[CurrentPhrase][thoughtIndex] = defaults.LeadVocalsStartsOnChordComp;
            LeadVocalsEndsOnChord[CurrentPhrase][thoughtIndex] = defaults.LeadVocalsEndsOnChord;
            LeadVocalsEndsOnChordComp[CurrentPhrase][thoughtIndex] = defaults.LeadVocalsEndsOnChordComp;
            LeadVocalsCurrentChordBias[CurrentPhrase][thoughtIndex] = defaults.LeadVocalsCurrentChordBias;
            LeadVocalsThoughtDirection[CurrentPhrase][thoughtIndex] = defaults.LeadVocalsThoughtDirection;
            LeadVocalsDirectionStrength[CurrentPhrase][thoughtIndex] = defaults.LeadVocalsDirectionStrength;
            LeadVocalsWeightShape[CurrentPhrase][thoughtIndex] = defaults.LeadVocalsWeightShape;
            LeadVocalsBoomerangTurningPoint[CurrentPhrase][thoughtIndex] = defaults.LeadVocalsBoomerangTurningPoint;
            LeadVocalsGlissChance[CurrentPhrase][thoughtIndex] = defaults.LeadVocalsGlissChance;
            LeadVocalsLastSyllableChance[CurrentPhrase][thoughtIndex] = defaults.LeadVocalsLastSyllableChance;
            LeadVocalsLastSyllableStart[CurrentPhrase][thoughtIndex] = defaults.LeadVocalsLastSyllableStart;
            LeadVocalsEndRestLength[CurrentPhrase][thoughtIndex] = defaults.LeadVocalsEndRestLength;
            LeadVocalsStartRestChance[CurrentPhrase][thoughtIndex] = defaults.LeadVocalsStartRestChance;
            LeadVocalsMaxStartRestSize[CurrentPhrase][thoughtIndex] = defaults.LeadVocalsMaxStartRestSize;
            LeadVocalsRestChance[CurrentPhrase][thoughtIndex] = defaults.LeadVocalsRestChance;
            LeadVocalsStartRestUnitIndex[CurrentPhrase][thoughtIndex] = defaults.LeadVocalsStartRestUnitIndex;
        }
        private void ButtonPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new();
            if (fbd.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                CurrentPath = fbd.SelectedPath;
                SetPath();
            }
        }
        //private void ButtonProb_Click(object sender, EventArgs e)
        //{
        //    Prob formProb = new();
        //    formProb.Owner = this;
        //    formProb.Show();
        //}
        private void TextBoxFileName_TextChanged(object sender, EventArgs e)
        {
            UpdatePath();
        }

        private void RadioIncrement_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePath();
        }

        private void ButtonStrongBeats_Click(object sender, EventArgs e)
        {
            //formStrongBeats = new(this);
            //generatorv8.StrongBeats.UpdateChecks(StrongBeats[CurrentPhrase], Meterup[CurrentPhrase]);
            //formStrongBeats.Owner = this;
            formStrongBeats.Show();
        }

        private void NumericMeter_ValueChanged(object sender, EventArgs e)
        {
            if (numericMeter.Focused) generatorv8.StrongBeats.EnableCheck(Meterup[CurrentPhrase] - 1);
            else
            {
                for (int i = 2; i < Meterup[CurrentPhrase] - 2; i++)
                {
                    generatorv8.StrongBeats.EnableCheck(i);
                }
            }
            Meterup[CurrentPhrase] = (int)numericMeter.Value;
            if (numericMeter.Focused) InitStrongBeats(CurrentPhrase);
            //InitStrongBeats(CurrentPhrase);
            generatorv8.StrongBeats.UpdateChecks(StrongBeats[CurrentPhrase], Meterup[CurrentPhrase]);
            comboBoxDrumLickLength.SelectedIndex = numericMeter.Value switch
            {
                2 => 2,
                < 7 => 1,
                _ => 0,
            };
            if (CurrentPhrase == PhrasesAllowed.Length)
                for (int i = 0; i < PhrasesAllowed.Length; i++)
                    Meterup[i] = (int)numericMeter.Value;
        }

        //private void buttonDrums_Click(object sender, EventArgs e)
        //{
        //    GetMusic(Drums.GetDrums(StrongBeats, Meterup, (int)numericBars.Value, checkForceKickOn1.Checked, (int)comboBoxDrumLickLength.SelectedItem, (int)numericBpm.Value));
        //}

        //private void buttonRhythmGuitar_Click(object sender, EventArgs e)
        //{
        //    //GetMusic(RhythmGuitar.GetRhythmGuitar(StrongBeats, Meterup, (int)numericBars.Value, (int)numericBpm.Value, (string)comboBoxKey.SelectedItem, comboBoxKey.SelectedIndex, (string)comboBoxScales.SelectedItem, Int32.Parse((string)comboBoxProgressionLength.SelectedItem), Int32.Parse((string)comboBoxChordLength.SelectedItem), radioButtonFullChords.Checked, checkForceTonicOn1.Checked, ChordsProbabilities));
        //    Chord[] chords = Chord.MakeProgression(StrongBeats, comboBoxKey.Text, comboBoxScales.Text, Int32.Parse((string)comboBoxProgressionLength.SelectedItem), Int32.Parse((string)comboBoxChordLength.SelectedItem), radioButtonFullChords.Checked, checkForceTonicOn1.Checked, ChordsProbabilities);
        //    GetMusic(RhythmGuitar.GetRhythmGuitar(StrongBeats, Meterup, (int)numericBars.Value, (int)numericBpm.Value, comboBoxKey.SelectedIndex, chords, Int32.Parse((string)comboBoxProgressionLength.SelectedItem), Int32.Parse((string)comboBoxChordLength.SelectedItem)));
        //}
        private void ButtonRun_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxPattern.Text))
            {
                string message = "Song pattern cannot be empty!";
                string caption = "Error";
                MessageBoxButtons patternEmptyMessageBox = MessageBoxButtons.OK;
                DialogResult result;
                result = MessageBox.Show(message, caption, patternEmptyMessageBox);
                return;
            }
            //int barsq = (int)numericBars.Value;
            //bool forceKickOn1 = checkForceKickOn1.Checked;
            //int beatLength = Int32.Parse((string)comboBoxDrumLickLength.SelectedItem);
            //int bpm = (int)numericBpm.Value;
            //string key = comboBoxKey.Text;
            //string scale = comboBoxScales.Text;
            //int progressionLengthInBars = Int32.Parse((string)comboBoxProgressionLength.SelectedItem);
            //int chordLengthInStrongBeats = Int32.Parse((string)comboBoxChordLength.SelectedItem);
            //bool fullChords = radioButtonFullChords.Checked;
            //bool forceTonicOn1 = checkForceTonicOn1.Checked;
            //int keyIndex = comboBoxKey.SelectedIndex;
            //string lowestNote;
            //if (LowNote.Length > 2) lowestNote = LowNote[CurrentPhrase][..2];
            //else lowestNote = LowNote[CurrentPhrase];
            //string highestNote;
            //if (HighNote.Length > 2) highestNote = HighNote[CurrentPhrase][..2];
            //else highestNote = HighNote[CurrentPhrase];
            //lowestNote = Toolbox.NoteOctaveTranspose(Toolbox.NormalizeNote(lowestNote), LowOctaveIndex[CurrentPhrase] - 1);
            //highestNote = Toolbox.NoteOctaveTranspose(Toolbox.NormalizeNote(highestNote), HighOctaveIndex[CurrentPhrase] - 1);
            //bool forceBassOn1 = checkForceBassOn1.Checked;
            //bool forceBassOnEveryStrongBeat = checkForceBassOnEveryStrongBeat.Checked;
            //decimal[] LeadBreakProbs = new decimal[] { LeadProbBar[CurrentPhrase], LeadProbStrongBeat[CurrentPhrase], LeadProbWeakBeat[CurrentPhrase] };

            //string[][] drums = Drums.GetDrums(StrongBeats[CurrentPhrase], Meterup[CurrentPhrase], barsq, forceKickOn1, beatLength, bpm);
            //Chord[] chords = Chord.MakeProgression(StrongBeats[CurrentPhrase], key, scale, progressionLengthInBars, chordLengthInStrongBeats, fullChords, forceTonicOn1, ChordsProbabilities[CurrentPhrase]);
            //string[][] bass = BassGuitar.GetBassGuitar(StrongBeats[CurrentPhrase], Meterup[CurrentPhrase], barsq, bpm, keyIndex, scale, key, chords, progressionLengthInBars, chordLengthInStrongBeats, drums[0], beatLength, forceBassOn1, forceBassOnEveryStrongBeat);
            //string[][] rhythm = RhythmGuitar.GetRhythmGuitar(StrongBeats[CurrentPhrase], Meterup[CurrentPhrase], barsq, bpm, keyIndex, chords, progressionLengthInBars, chordLengthInStrongBeats, RhythmGuitarValuesProb[CurrentPhrase]);
            //string[][] lead = LeadGuitar.GetLeadGuitar(StrongBeats[CurrentPhrase], Meterup[CurrentPhrase], barsq, bpm, keyIndex, key, scale, chords, progressionLengthInBars, chordLengthInStrongBeats, LeadGuitarNotesProb[CurrentPhrase], LeadGuitarInterProb[CurrentPhrase], LeadGuitarRhythmProb[CurrentPhrase], lowestNote, highestNote, LeadForceTonicOn1[CurrentPhrase], Rerolls[CurrentPhrase], LeadBreakProbs);
            //string[] variables = Toolbox.JoinMultipleArrays(new string[][] { lead[0], rhythm[0], bass[0], drums[0] });
            //string[] music = Toolbox.JoinMultipleArrays(new string[][] { lead[1], rhythm[1], bass[1], drums[1] });
            //GetMusic(new string[][] { variables, music });
            int phrasesCount = PhrasesAllowed.Length;
            bool[] phraseExists = new bool[phrasesCount];
            bool[][] thoughtExists = new bool[phrasesCount][];
            string phrasePattern = textBoxPattern.Text;
            for (int i = 0; i < phrasesCount; i++)
            {
                thoughtExists[i] = new bool[LeadVocalsDistinctThoughtsCount[i]];
                //phraseExists[i] = phrasePattern.Contains(PhrasesAllowed[i]);
                if (phrasePattern.Contains(PhrasesAllowed[i]))
                {
                    phraseExists[i] = true;
                    for (int j = 0; j < LeadVocalsDistinctThoughtsCount[i]; j++)
                        if (LeadVocalsThoughtPattern[j].Contains(allThoughts[LeadVocalsDistinctThoughtsCount[j]]))
                            thoughtExists[i][j] = true;
                }
            }

            string[] lowestNote = new string[phrasesCount];
            string[] highestNote = new string[phrasesCount];
            //for (int i = 0; i < phrasesCount; i++)
            //{
            //    if (phraseExists[i])
            //    {

            //        if (LowNote[i].Length > 2) lowestNote[i] = LowNote[i][..2];
            //        else lowestNote[i] = LowNote[i];
            //        if (HighNote[i].Length > 2) highestNote[i] = HighNote[i][..2];
            //        else highestNote[i] = HighNote[i];
            //        lowestNote[i] = Toolbox.NoteOctaveTranspose(Toolbox.NormalizeNote(lowestNote[i]), LowOctaveIndex[i] - 1);
            //        highestNote[i] = Toolbox.NoteOctaveTranspose(Toolbox.NormalizeNote(highestNote[i]), HighOctaveIndex[i] - 1);
            //    }
            //}

            //decimal[][] LeadBreakProbs = new decimal[][] { LeadProbBar, LeadProbStrongBeat, LeadProbWeakBeat };
            //decimal[][] LeadBreakProbs = new decimal[phrasesCount][];
            //for (int i = 0; i < phrasesCount; i++)
            //    LeadBreakProbs[i] = new decimal[] { LeadProbBar[i], LeadProbStrongBeat[i], LeadProbWeakBeat[i] };

            string[][][] drums = new string[phrasesCount][][];
            Chord[][] chords = new Chord[phrasesCount][];
            string[][] bass = new string[phrasesCount][];
            string[][] rhythm = new string[phrasesCount][];
            // string[][] lead = new string[phrasesCount][];
            string[][] leadVocals = new string[phrasesCount][];
            for (int i = 0; i < phrasesCount; i++)
            {
                if (phraseExists[i])
                {
                    drums[i] = Drums.GetDrums(StrongBeats[i], Meterup[i], Barsq[i], ForceKickOn1[i], BeatLength[i], Bpm[i],
                        PhrasesAllowed[i]);
                    chords[i] = Chord.MakeProgression(StrongBeats[i], Key[i], Scales[i], ProgressionLengthInBars[i],
                        ChordLengthInStrongBeats[i], FullChords[i], ForceTonicOn1[i], ChordsProbabilities[i]);
                    bass[i] = BassGuitar.GetBassGuitar(StrongBeats[i], Meterup[i], Barsq[i], Bpm[i], KeyIndices[i],
                        Scales[i], Key[i], chords[i], ProgressionLengthInBars[i], ChordLengthInStrongBeats[i], drums[i][1],
                        BeatLength[i], ForceBassOn1[i], ForceBassOnEveryStrongBeat[i], PhrasesAllowed[i]);
                    rhythm[i] = RhythmGuitar.GetRhythmGuitar(StrongBeats[i], Meterup[i], Barsq[i], Bpm[i], KeyIndices[i],
                        chords[i], ProgressionLengthInBars[i], ChordLengthInStrongBeats[i], RhythmGuitarValuesProb[i],
                        PhrasesAllowed[i]);
                    /* lead[i] = LeadGuitar.GetLeadGuitar(StrongBeats[i], Meterup[i], Barsq[i], Bpm[i], KeyIndices[i], Key[i],
                        Scales[i], chords[i], ProgressionLengthInBars[i], ChordLengthInStrongBeats[i], LeadGuitarNotesProb[i],
                        LeadGuitarInterProb[i], LeadGuitarRhythmProb[i], lowestNote[i], highestNote[i], LeadForceTonicOn1[i],
                        Rerolls[i], LeadBreakProbs[i], PhrasesAllowed[i]); */
                    leadVocals[i] = LeadVocals.GetLeadVocals(StrongBeats[i], Meterup[i], Barsq[i], Key[i], Scales[i], chords[i],
                        ProgressionLengthInBars[i], ChordLengthInStrongBeats[i], LeadVocalsNotesProb[i], LeadVocalsInterProb[i],
                        LeadVocalsRhythmProb[i], LeadVocalsDrawWeights[i], Toolbox.GetLilyNoteArray(LeadVocalsLowNoteIndex[i],
                        LeadVocalsLowOctaveIndex[i]), Toolbox.GetLilyNoteArray(LeadVocalsHighNoteIndex[i], LeadVocalsHighOctaveIndex[i]),
                        /*new decimal[][] { LeadVocalsProbBar[i], LeadVocalsProbStrongBeat[i], LeadVocalsProbWeakBeat[i] },*/
                        Toolbox.GetBreakProbsArray(LeadVocalsProbBar[i], LeadVocalsProbStrongBeat[i], LeadVocalsProbWeakBeat[i]), allPhrases[i],
                        LeadVocalsThoughtLength[i], LeadVocalsMelodyVariation[i], LeadVocalsRhythmVariation[i], LeadVocalsVariationSection[i],
                        LeadVocalsStartsOnChord[i], LeadVocalsStartsOnChordComp[i], LeadVocalsEndsOnChord[i], LeadVocalsEndsOnChordComp[i],
                        LeadVocalsCurrentChordBias[i], LeadVocalsThoughtPattern[i], LeadVocalsThoughtsRising, LeadVocalsDelayInProgressions[i],
                        LeadVocalsThoughtDirection[i], LeadVocalsDirectionStrength[i], LeadVocalsRisingOverlap, LeadVocalsWeightShape[i],
                        LeadVocalsBoomerangTurningPoint[i], LeadVocalsGlissChance[i], LeadVocalsLastSyllableChance[i], LeadVocalsLastSyllableStart[i],
                        LeadVocalsEndRestLength[i], LeadVocalsStartRestChance[i], LeadVocalsMaxStartRestSize[i], LeadVocalsRestChance[i],
                        LeadVocalsStartRestUnitIndex[i]);
                }
                // DO ZOBACZENIA JUTRO ^^
            }


            string dirly = textBoxPath.Text;
            File.WriteAllLines(dirly, lilyStart1);
            for (int i = 0; i < phrasesCount; i++)
            {
                if (phraseExists[i])
                {
                    File.AppendAllLines(dirly, drums[i][0]);
                    File.AppendAllLines(dirly, drums[i][1]);
                    File.AppendAllLines(dirly, bass[i]);
                    File.AppendAllLines(dirly, rhythm[i]);
                    //File.AppendAllLines(dirly, lead[i]);
                    File.AppendAllLines(dirly, leadVocals[i]);
                }
            }
            File.AppendAllLines(dirly, lilyStart2);
            //string leadGuitarPattern = Toolbox.GetTrackSongSequence(phrasePattern, "leadGuitar", Meterup, Bpm, KeyIndices);
            //File.AppendAllLines(dirly, new string[]
            //{
            //    "\\new StaffGroup \\with { instrumentName = \\markup { \\center-column { \"Lead\" \\line { \"Guitar\" } } } } <<",
            //    "\\new Staff \\with {midiInstrument = \"distorted guitar\" }",
            //    "{ \\clef \"G_8\" " + leadGuitarPattern + " }",
            //    "\\new TabStaff { " + leadGuitarPattern + " } >>"
            //});
            string leadVocalsPattern = Toolbox.GetTrackSongSequence(phrasePattern, "leadVocals", Meterup, Bpm, KeyIndices);
            File.AppendAllLines(dirly, new string[]
            {
                //"\\new StaffGroup \\with { instrumentName = \\markup { \\center-column { \"Lead\" \\line { \"Guitar\" } } } } <<",
                "\\new Staff \\with {midiInstrument = \"distorted guitar\" instrumentName = \\markup { \\center-column { \"Lead\" \\line { \"Vocals\" } } } }",
                "{ \\clef \"G_8\" " + leadVocalsPattern + " }"//,
                //"\\new TabStaff { " + leadVocalsPattern + " } >>"
            });
            string rhythmGuitarPattern = Toolbox.GetTrackSongSequence(phrasePattern, "rhythmGuitar", Meterup, Bpm, KeyIndices);
            File.AppendAllLines(dirly, new string[]
            {
                "\\new StaffGroup \\with { instrumentName = \\markup { \\center-column { \"Rhythm\" \\line { \"Guitar\" } } } } <<",
                "\\new Staff \\with {midiInstrument = \"acoustic guitar (steel)\" }",
                "{ \\clef \"G_8\" " + rhythmGuitarPattern + " }",
                "\\new TabStaff { " + rhythmGuitarPattern + " }",
                ">>",
            });
            string bassGuitarPattern = Toolbox.GetTrackSongSequence(phrasePattern, "bassGuitar", Meterup, Bpm, KeyIndices);
            File.AppendAllLines(dirly, new string[]
            {
                "\\new StaffGroup \\with { instrumentName = \\markup { \\center-column { \"Bass\" \\line { \"Guitar\" } } } } <<",
                "\\new Staff \\with {midiInstrument = \"electric bass (finger)\" }",
                "{ \\clef \"bass_8\" " + bassGuitarPattern + " }",
                "\\new TabStaff \\with { stringTunings = #bass-tuning } { " + bassGuitarPattern + " }",
                ">>",
            });
            File.AppendAllLines(dirly, new string[]
            {
                "\\new DrumStaff \\with { instrumentName = \"Drums\" } <<",
                @"  \drummode {",
                "    << { " + Toolbox.GetTrackSongSequence(phrasePattern, "drumsUp", Meterup, Bpm, KeyIndices) + " }",
                @"      \\",
                "       { " + Toolbox.GetTrackSongSequence(phrasePattern, "drumsDown", Meterup, Bpm, KeyIndices) + " } >>",
                "  }",
                ">>"
            });
            File.AppendAllLines(dirly, lilyEnd);
            File.AppendAllLines(dirly, GetLilyMidi());
            RunLilyPond(dirly);
        }
        private void GetMusic(string[] music)
        {
            string dirly = textBoxPath.Text;
            File.WriteAllLines(dirly, lilyStart1);
            File.AppendAllLines(dirly, lilyStart2);
            File.AppendAllLines(dirly, music);
            File.AppendAllLines(dirly, lilyEnd);
            RunLilyPond(dirly);
        }
        private void GetMusic(string[][] variablesAndMusic)
        {
            string dirly = textBoxPath.Text;
            string[] variables = variablesAndMusic[0];
            string[] music = variablesAndMusic[1];
            File.WriteAllLines(dirly, lilyStart1);
            File.AppendAllLines(dirly, variables);
            File.AppendAllLines(dirly, lilyStart2);
            File.AppendAllLines(dirly, music);
            File.AppendAllLines(dirly, lilyEnd);
            File.AppendAllLines(dirly, GetLilyMidi());
            RunLilyPond(dirly);
        }
        private void RunLilyPond(string dirly)
        {
            string dirpdf = textBoxPath.Text[..^3] + ".pdf";
            string dircat = string.Join("\\", dirly.Split("\\")[..^1]);
            Process process = new();
            ProcessStartInfo startInfo = new()
            {
                UseShellExecute = true,
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = "cmd.exe",
                //Arguments = "/C cd C:\\Program Files (x86)\\LilyPond\\usr\\bin && lilypond --output=C:\\Users\\Dewey\\Desktop\\Dewey\\Studia\\7\\pracainz\\repos\\generatorv8 " + dirly
                Arguments = "/C cd C:\\Program Files (x86)\\LilyPond\\usr\\bin && lilypond --output=" + dircat + " " + dirly
            };
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
            Process.Start(new ProcessStartInfo(dirpdf) { UseShellExecute = true });
        }

        private void comboBoxScales_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ChordsProbabilities != null)
            if (comboBoxScales.Focused)
            {
                ChordsProbabilities[CurrentPhrase].Clear();
                InitChordsProbabilities();
            }
            string scale = comboBoxScales.SelectedItem.ToString();
            comboBoxKey.Items.Clear();
            string[] allKeys =
            {
                "Fb", "Cb", "Gb", "Db", "Ab", "Eb", "Bb",
                "F", "C", "G", "D", "A", "E", "B",
                "F#", "C#", "G#", "D#", "A#", "E#", "B#"
            };
            int whiteIndex;
            string scaleKey = "";
            switch (scale)
            {
                case "Ionian": scaleKey = "C"; break;
                case "Dorian": scaleKey = "D"; break;
                case "Phrygian": scaleKey = "E"; break;
                case "Lydian": scaleKey = "F"; break;
                case "Mixolydian": scaleKey = "G"; break;
                case "Aeolian": scaleKey = "A"; break;
                case "Locrian": scaleKey = "B"; break;
            }
            whiteIndex = Array.FindIndex(allKeys, key => key == scaleKey);
            comboBoxKey.Items.AddRange(allKeys[(whiteIndex - 7)..(whiteIndex + 8)]);
            if (!supressKeyChange) comboBoxKey.SelectedIndex = 7;
            if (formChordsProbability != null) UpdateChords(CurrentPhrase);
            // UpdateChords();
            Scales[CurrentPhrase] = comboBoxScales.Text;
            ScalesIndices[CurrentPhrase] = comboBoxScales.SelectedIndex;
            if (CurrentPhrase == PhrasesAllowed.Length)
            {
                for (int i = 0; i < PhrasesAllowed.Length; i++)
                {
                    Scales[i] = comboBoxScales.Text;
                    ScalesIndices[i] = comboBoxScales.SelectedIndex;
                }
            }
        }
        private void UpdateChords(int phraseIndex)
        {
            // string key = Toolbox.NormalizeNote(comboBoxKey.Text);
            // string scale = comboBoxScales.Text;
            string key = Toolbox.NormalizeNote(Key[phraseIndex]);
            string scale = Scales[phraseIndex];
            string[] roots = Toolbox.GetNotesInScale(scale, key);
            string[] chordTypes = Toolbox.GetChordTypes(scale);
            for (int i = 0; i < formChordsProbability.labels.Length; i++)
            {
                if (chordTypes[i] == "major") roots[i] = Toolbox.UnnormalizeNote(roots[i], true);
                else roots[i] = Toolbox.UnnormalizeNote(roots[i], false);
                formChordsProbability.labels[i].Text = formChordsProbability.labelTexts[i] + roots[i] + " " + chordTypes[i] + ")";
            }
            for (int i = 0; i < formChordsProbability.numerics.Length; i++)
            {
                formChordsProbability.numerics[i].Value = ChordsProbabilities[phraseIndex][i];
            }
        }
        private void UpdateRhythmGuitarValuesProb(int phraseIndex)
        {
            for (int i = 0; i < RhythmGuitarValuesProb[phraseIndex].Count; i++)
                formRhythmGuitarValuesProb.numerics[i].Value = RhythmGuitarValuesProb[phraseIndex][i];
        }
        //private void UpdateNotes()
        //{
        //    string key = Toolbox.NormalizeNote(comboBoxKey.Text);
        //    string scale = comboBoxScales.Text;
        //    string[] roots = Toolbox.GetNotesInScale(scale, key);
        //    for (int i = 0; i < formLeadGuitarNotesProb.labels.Length; i++)
        //    {
        //        roots[i] = Toolbox.UnnormalizeNote(roots[i], true);
        //        formLeadGuitarNotesProb.labels[i].Text = formLeadGuitarNotesProb.labelTexts[i] + roots[i] + ")";
        //    }
        //    for (int i = 0; i < formLeadGuitarNotesProb.numerics.Length; i++)
        //    {
        //        formLeadGuitarNotesProb.numerics[i].Value = LeadGuitarNotesProb[i];
        //    }
        //}
        private void comboBoxKey_SelectedIndexChanged(object sender, EventArgs e)
        {
            Key[CurrentPhrase] = comboBoxKey.Text;
            KeyIndices[CurrentPhrase] = comboBoxKey.SelectedIndex;
            if (CurrentPhrase == PhrasesAllowed.Length)
            {
                for (int i = 0; i < PhrasesAllowed.Length; i++)
                {
                    Key[i] = comboBoxKey.Text;
                    KeyIndices[i] = comboBoxKey.SelectedIndex;
                }
            }
            UpdateChords(CurrentPhrase);
            //formLeadGuitarSettings.UpdateNotes(CurrentPhrase);
            formLeadVocalsSettings.formNotesProb.UpdateNotes(CurrentPhrase, LeadVocalsCurrentThought[CurrentPhrase]);
        }

        private void buttonChordsProbabilities_Click(object sender, EventArgs e)
        {
            //formChordsProbability = new(this);
            //formChordsProbability.Owner = this;
            formChordsProbability.Show();
            UpdateChords(CurrentPhrase);
        }

        private void buttonRhythmGuitarValuesProb_Click(object sender, EventArgs e)
        {
            //formRhythmGuitarValuesProb = new(this, RhythmGuitarValuesProb[CurrentPhrase]);
            //formRhythmGuitarValuesProb.Owner = this;
            formRhythmGuitarValuesProb.Show();
        }

        //private void buttonLeadGuitarNotesProb_Click(object sender, EventArgs e)
        //{
        //    formLeadGuitarNotesProb = new(this);
        //    formLeadGuitarNotesProb.Owner = this;
        //    formLeadGuitarNotesProb.Show();
        //    UpdateNotes();
        //}

        //private void buttonLeadGuitarRhythmProb_Click(object sender, EventArgs e)
        //{
        //    formLeadGuitarRhythmProb = new(this, LeadGuitarRhythmProb);
        //    formLeadGuitarRhythmProb.Owner = this;
        //    formLeadGuitarRhythmProb.Show();
        //}

        //private void buttonLeadGuitarIntervalProb_Click(object sender, EventArgs e)
        //{
        //    formLeadGuitarInterProb = new(this, LeadGuitarInterProb);
        //    formLeadGuitarInterProb.Owner = this;
        //    formLeadGuitarInterProb.Show();
        //}

        //private void comboBoxLowOctave_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    comboBoxLowNote.Items.Clear();
        //    switch (comboBoxLowOctave.SelectedIndex)
        //    {
        //        case 0:
        //            comboBoxLowNote.Items.AddRange(notesForCombo[4..]);
        //            comboBoxLowNote.SelectedIndex = 0;
        //            break;
        //        case 4:
        //            comboBoxLowNote.Items.AddRange(notesForCombo[..5]);
        //            comboBoxLowNote.SelectedIndex = 4;
        //            break;
        //        default:
        //            comboBoxLowNote.Items.AddRange(notesForCombo);
        //            comboBoxLowNote.SelectedIndex = 4;
        //            break;
        //    }
        //}

        //private void comboBoxHighOctave_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    comboBoxHighNote.Items.Clear();
        //    switch (comboBoxHighOctave.SelectedIndex)
        //    {
        //        case 0:
        //            comboBoxHighNote.Items.AddRange(notesForCombo[4..]);
        //            comboBoxHighNote.SelectedIndex = 0;
        //            break;
        //        case 4:
        //            comboBoxHighNote.Items.AddRange(notesForCombo[..5]);
        //            comboBoxHighNote.SelectedIndex = 4;
        //            break;
        //        default:
        //            comboBoxHighNote.Items.AddRange(notesForCombo);
        //            comboBoxHighNote.SelectedIndex = 4;
        //            break;
        //    }
        //}

        private void buttonMIDI_Click(object sender, EventArgs e)
        {
            //formMidiSettings = new(this, TransposeLead[CurrentPhrase], TransposeRhythm[CurrentPhrase], TransposeBass[CurrentPhrase]);
            //formMidiSettings.Owner = this;
            formMidiSettings.Show();
        }

        private void buttonLeadVocalsSettings_Click(object sender, EventArgs e)
        {
            //formLeadGuitarSettings = new(this, comboBoxKey.Text, comboBoxScales.Text, LowOctaveIndex[CurrentPhrase], LowNoteIndex[CurrentPhrase], HighOctaveIndex[CurrentPhrase], HighNoteIndex[CurrentPhrase]);
            //formLeadGuitarSettings.Owner = this;
            //formLeadGuitarSettings.Show();
            formLeadVocalsSettings.Show();
        }

        private void textBoxPattern_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !PhrasesAllowed.Contains(char.ToUpper(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void numericDistinctPhrases_ValueChanged(object sender, EventArgs e)
        {
            int selectedIndex = comboBoxPhrases.SelectedIndex;
            if (comboBoxPhrases.SelectedIndex == comboBoxPhrases.Items.Count - 1) selectedIndex = -1;
            PhrasesAllowed = allPhrases[0..(int)numericDistinctPhrases.Value];
            comboBoxPhrases.Items.Clear();
            for (int i = 0; i < numericDistinctPhrases.Value; i++)
            {
                comboBoxPhrases.Items.Add(PhrasesAllowed[i]);
            }
            comboBoxPhrases.Items.Add("All");
            if (selectedIndex == -1) comboBoxPhrases.SelectedIndex = PhrasesAllowed.Length;
            else if (selectedIndex > -1 && selectedIndex < PhrasesAllowed.Length) comboBoxPhrases.SelectedIndex = selectedIndex;
            else comboBoxPhrases.SelectedIndex = 0;

            string text = textBoxPattern.Text;
            byte[] asciiBytes = Encoding.ASCII.GetBytes(text);
            int maxValue = 64 + PhrasesAllowed.Length;
            for (int i = asciiBytes.Length - 1; i > -1; i--)
            {
                if (asciiBytes[i] > maxValue) text = text.Remove(i, 1);
            }
            textBoxPattern.Text = text;
        }

        private void buttonResetDefaults_Click(object sender, EventArgs e)
        {
            if (comboBoxPhrases.SelectedIndex != comboBoxPhrases.Items.Count - 1)
            {
                SetDefaults(CurrentPhrase);
                //UpdateControls(CurrentPhrase);
            }
            else
            {
                for (int i = 0; i < comboBoxPhrases.Items.Count; i++)
                {
                    SetDefaults(i);
                    //UpdateControls(i);
                }
            }
            UpdateControls(CurrentPhrase, LeadVocalsCurrentThought[CurrentPhrase]);
        }

        private void comboBoxPhrases_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentPhrase = comboBoxPhrases.SelectedIndex;
            UpdateControls(CurrentPhrase, LeadVocalsCurrentThought[CurrentPhrase]);
            //if (comboBoxPhrases.SelectedIndex != comboBoxPhrases.Items.Count - 1)
            //{
            //    UpdateControls(CurrentPhrase);
            //}
            //else
            //{
            //    for (int i = 0; i < PhrasesAllowed.Length; i++)
            //    {
            //        UpdateControls(i);
            //    }
            //}
        }
        private void UpdateControls(int phraseIndex, int leadVocalsThoughtIndex)
        {
            // Main
            numericMeter.Value = Meterup[phraseIndex];
            numericBars.Value = Barsq[phraseIndex];
            numericBpm.Value = Bpm[phraseIndex];
            supressKeyChange = true;
            comboBoxScales.SelectedIndex = ScalesIndices[phraseIndex];
            supressKeyChange = false;
            comboBoxKey.SelectedIndex = KeyIndices[phraseIndex];
            comboBoxDrumLickLength.SelectedIndex = BeatLengthIndices[phraseIndex];
            comboBoxProgressionLength.SelectedIndex = ProgressionLengthInBarsIndices[phraseIndex];
            comboBoxChordLength.SelectedIndex = ChordLengthInStrongBeatsIndices[phraseIndex];
            radioButtonFullChords.Checked = FullChords[phraseIndex];
            radioButtonPowerChords.Checked = !FullChords[phraseIndex];
            checkForceKickOn1.Checked = ForceKickOn1[phraseIndex];
            checkForceTonicOn1.Checked = ForceTonicOn1[phraseIndex];
            checkForceBassOn1.Checked = ForceBassOn1[phraseIndex];
            checkForceBassOnEveryStrongBeat.Checked = ForceBassOnEveryStrongBeat[phraseIndex];
            // Strong beats
            generatorv8.StrongBeats.UpdateChecks(StrongBeats[phraseIndex], Meterup[phraseIndex]);
            // Chords probabilities
            UpdateChords(phraseIndex);
            // Rhythm guitar rhythmic values probabilities
            UpdateRhythmGuitarValuesProb(phraseIndex);
            //// Lead guitar settings
            //formLeadGuitarSettings.UpdateLeadGuitarSettings(phraseIndex);
            //// Lead guitar notes probabilities
            //formLeadGuitarSettings.UpdateNotes(phraseIndex);
            //// Lead guitar rhythmic values probabilities
            //formLeadGuitarSettings.formLeadGuitarRhythmProb.UpdateLeadGuitarRhythmProb(phraseIndex);
            //// Lead guitar intervals probabilities
            //formLeadGuitarSettings.formLeadGuitarInterProb.UpdateLeadGuitarInterProb(phraseIndex);
            // Lead vocals settings
            formLeadVocalsSettings.UpdateControls(phraseIndex, leadVocalsThoughtIndex);
            // MIDI settings
            formMidiSettings.UpdateMidiSettings(phraseIndex);
        }

        private void numericBars_ValueChanged(object sender, EventArgs e)
        {
            Barsq[CurrentPhrase] = (int)numericBars.Value;
            if (CurrentPhrase == PhrasesAllowed.Length)
                for (int i = 0; i < PhrasesAllowed.Length; i++)
                    Barsq[i] = (int)numericBars.Value;
        }

        private void numericBpm_ValueChanged(object sender, EventArgs e)
        {
            Bpm[CurrentPhrase] = (int)numericBpm.Value;
            if (CurrentPhrase == PhrasesAllowed.Length)
                for (int i = 0; i < PhrasesAllowed.Length; i++)
                    Bpm[i] = (int)numericBpm.Value;
        }

        private void comboBoxDrumLickLength_SelectedIndexChanged(object sender, EventArgs e)
        {
            BeatLength[CurrentPhrase] = Int32.Parse(comboBoxDrumLickLength.Text);
            BeatLengthIndices[CurrentPhrase] = comboBoxDrumLickLength.SelectedIndex;
            if (CurrentPhrase == PhrasesAllowed.Length)
            {
                for (int i = 0; i < PhrasesAllowed.Length; i++)
                {
                    BeatLength[i] = Int32.Parse(comboBoxDrumLickLength.Text);
                    BeatLengthIndices[i] = comboBoxDrumLickLength.SelectedIndex;
                }
            }
        }

        private void comboBoxProgressionLength_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProgressionLengthInBars[CurrentPhrase] = Int32.Parse(comboBoxProgressionLength.Text);
            ProgressionLengthInBarsIndices[CurrentPhrase] = comboBoxProgressionLength.SelectedIndex;
            if (CurrentPhrase == PhrasesAllowed.Length)
            {
                for (int i = 0; i < PhrasesAllowed.Length; i++)
                {
                    ProgressionLengthInBars[i] = Int32.Parse(comboBoxProgressionLength.Text);
                    ProgressionLengthInBarsIndices[i] = comboBoxProgressionLength.SelectedIndex;
                }
            }
        }

        private void comboBoxChordLength_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChordLengthInStrongBeats[CurrentPhrase] = Int32.Parse(comboBoxChordLength.Text);
            ChordLengthInStrongBeatsIndices[CurrentPhrase] = comboBoxChordLength.SelectedIndex;
            if (CurrentPhrase == PhrasesAllowed.Length)
            {
                for (int i = 0; i < PhrasesAllowed.Length; i++)
                {
                    ChordLengthInStrongBeats[i] = Int32.Parse(comboBoxChordLength.Text);
                    ChordLengthInStrongBeatsIndices[i] = comboBoxChordLength.SelectedIndex;
                }
            }
        }

        private void radioButtonFullChords_CheckedChanged(object sender, EventArgs e)
        {
            FullChords[CurrentPhrase] = radioButtonFullChords.Checked;
            if (CurrentPhrase == PhrasesAllowed.Length)
                for (int i = 0; i < PhrasesAllowed.Length; i++)
                    FullChords[i] = radioButtonFullChords.Checked;
        }

        private void checkForceKickOn1_CheckedChanged(object sender, EventArgs e)
        {
            ForceKickOn1[CurrentPhrase] = checkForceKickOn1.Checked;
            if (CurrentPhrase == PhrasesAllowed.Length)
                for (int i = 0; i < PhrasesAllowed.Length; i++)
                    ForceKickOn1[i] = checkForceKickOn1.Checked;
        }

        private void checkForceTonicOn1_CheckedChanged(object sender, EventArgs e)
        {
            ForceTonicOn1[CurrentPhrase] = checkForceTonicOn1.Checked;
            if (CurrentPhrase == PhrasesAllowed.Length)
                for (int i = 0; i < PhrasesAllowed.Length; i++)
                    ForceTonicOn1[i] = checkForceTonicOn1.Checked;
        }

        private void checkForceBassOn1_CheckedChanged(object sender, EventArgs e)
        {
            ForceBassOn1[CurrentPhrase] = checkForceBassOn1.Checked;
            if (CurrentPhrase == PhrasesAllowed.Length)
                for (int i = 0; i < PhrasesAllowed.Length; i++)
                    ForceBassOn1[i] = checkForceBassOn1.Checked;
        }

        private void checkForceBassOnEveryStrongBeat_CheckedChanged(object sender, EventArgs e)
        {
            ForceBassOnEveryStrongBeat[CurrentPhrase] = checkForceBassOnEveryStrongBeat.Checked;
            if (CurrentPhrase == PhrasesAllowed.Length)
                for (int i = 0; i < PhrasesAllowed.Length; i++)
                    ForceBassOnEveryStrongBeat[i] = checkForceBassOnEveryStrongBeat.Checked;
        }
    }
}
