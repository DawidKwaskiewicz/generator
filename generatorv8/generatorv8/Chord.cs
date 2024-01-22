using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace generatorv8
{
    class Chord
    {
        public string notes;
        public string root;
        public Chord(string notes, string root)
        {
            this.notes = notes;
            this.root = root;
        }
        public static Chord[] MakeProgression(List<int> strongBeats, string key, string scale, int progressionLengthInBars, int chordLengthInStrongBeats, bool fullChords, bool forceTonicOn1, List<int> chordsProbabilities)
        {
            Random rnd = new();
            key = Toolbox.NormalizeNote(key);
            string[] roots = Toolbox.GetNotesInScale(scale, key);
            string[] rootsProb = Toolbox.GetProbArray(roots, chordsProbabilities);
            int chordsPossible = 0;
            for (int i = 0; i < chordsProbabilities.Count; i++)
                if (chordsProbabilities[i] != 0) chordsPossible++;
            string[] chordTypes = Toolbox.GetChordTypes(scale);
            int rootInd;
            Chord chord;
            int progressionLengthInChords = progressionLengthInBars * (int)Math.Ceiling((decimal)strongBeats.Count / chordLengthInStrongBeats);
            Chord[] chords = new Chord[progressionLengthInChords];
            bool threepeat = false;
            bool chordDomination = false;
            bool onlyOneChord = false;
            int[] chordCounter = new int[roots.Length];
            int randomInd = 0;

            for (int i = 0; i < progressionLengthInChords; i++)
            {
                if (forceTonicOn1 && i == 0) rootInd = 0;
                else
                {
                    do
                    {
                        randomInd = rnd.Next(rootsProb.Length);
                        rootInd = Array.FindIndex(roots, p => p == rootsProb[randomInd]);
                        if (i > 1) if (roots[rootInd] == chords[i - 1].root && roots[rootInd] == chords[i - 2].root) threepeat = true;
                            else threepeat = false;
                        if (chordCounter[rootInd] + 1 > progressionLengthInChords / 2) chordDomination = true;
                        else chordDomination = false;
                        if (i == progressionLengthInBars - 1 && chordCounter[rootInd] == progressionLengthInBars - 1) onlyOneChord = true;
                        else onlyOneChord = false;
                    } while ((chordsPossible > 3 && (threepeat || chordDomination)) || (chordsPossible > 1 && onlyOneChord));
                }
                chordCounter[rootInd]++;
                if (fullChords)
                    chord = chordTypes[rootInd] switch
                    {
                        "major" => basicMajorChords[rnd.Next(basicMajorChords.Length)],
                        "minor" => basicMinorChords[rnd.Next(basicMinorChords.Length)],
                        "diminished" => diminishedChords[rnd.Next(diminishedChords.Length)],
                        _ => basicPowerChords[rnd.Next(basicPowerChords.Length)],
                    };
                else
                    chord = chordTypes[rootInd] switch
                    {
                        "diminished" => diminishedPowerChords[rnd.Next(diminishedPowerChords.Length)],
                        _ => basicPowerChords[rnd.Next(basicPowerChords.Length)],
                    };
                chords[i] = TransposeChord(chord, roots[rootInd], roots);

            }
            return chords;
        }
        public static Chord TransposeChord(Chord chord, string root, string[] notesInScale)
        {
            int semitonesUp = Toolbox.FindSemitoneIndex(root) - Toolbox.FindSemitoneIndex(chord.root);
            if (semitonesUp < 0) semitonesUp += 12;
            string[] chordNotes = chord.notes.Split(' ');
            chordNotes[0] = chordNotes[0][1..];
            chordNotes[^1] = chordNotes[^1][..^1];
            int oldIndex;
            int newIndex;
            int octaves;
            for (int i = 0; i < chordNotes.Length; i++)
            {
                octaves = 0;
                while (chordNotes[i].EndsWith('\''))
                {
                    chordNotes[i] = chordNotes[i][..^1];
                    octaves++;
                }
                while (chordNotes[i].EndsWith(','))
                {
                    chordNotes[i] = chordNotes[i][..^1];
                    octaves--;
                }
                oldIndex = Toolbox.FindSemitoneIndex(chordNotes[i]);
                newIndex = oldIndex + semitonesUp;
                if (newIndex > 11)
                {
                    newIndex -= 12;
                    octaves++;
                }
                for (int j = 0; j < 3; j++)
                    if (Toolbox.semitonesSequence[newIndex][j] != null)
                        if (notesInScale.Contains(Toolbox.semitonesSequence[newIndex][j]))
                            chordNotes[i] = Toolbox.semitonesSequence[newIndex][j];
                if (octaves != 0) chordNotes[i] = Toolbox.NoteOctaveTranspose(chordNotes[i], octaves);
            }
            return new Chord("<" + String.Join(' ', chordNotes) + ">", root);
        }
        public static readonly Chord[] basicMajorChords =
        {
            new Chord("<c e g c' e'>", "c"),
            new Chord("<a, e a cis' e'>", "a"),
            new Chord("<g, b, d g b g'>", "g"),
            new Chord("<e, b, e gis b e'>", "e"),
            new Chord("<d a d' fis'>", "d")
        };
        public static readonly Chord[] basicMinorChords =
        {
            new Chord("<a, e a c' e'>", "a"),
            new Chord("<e, b, e g b e'>", "e"),
            new Chord("<d a d' f'>", "d")
        };
        public static readonly Chord[] diminishedChords =
        {
            new Chord("<f, b, aes>", "f"),
            new Chord("<f, b, f aes>", "f"),
            new Chord("<ges, c ees>", "c"),
            new Chord("<fis, dis a>", "dis"),
            new Chord("<g, bes, e>", "e"),
        };
        public static readonly Chord[] basicPowerChords =
        {
            new Chord("<e, b,>", "e"),
            new Chord("<e, b, e>", "e"),
            new Chord("<e, b, e b>", "e"),
            new Chord("<e, a,>", "a"),
            new Chord("<e, a, e>", "a"),
            new Chord("<e, a, e a>", "a")
        };
        public static readonly Chord[] diminishedPowerChords =
        {
            new Chord("<e, bes,>", "e"),
            new Chord("<e, bes, e>", "e"),
            new Chord("<e, bes, e bes>", "e"),
            new Chord("<ees, a,>", "a"),
            new Chord("<ees, a, ees>", "a"),
            new Chord("<ees, a, ees a>", "a")
        };
    }
}
