using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace generatorv8
{
    class BassGuitar
    {
        //public static string[][] GetBassGuitar(List<int> strongBeats, int meterup, int barsq, int bpm, int keyIndex, string scale, string key, Chord[] chords, int progressionLengthInBars, int chordLengthInStrongBeats, string[] drumVars, int drumLickLength, bool forceBassOn1, bool forceBassOnEveryStrongBeat)
        public static string[] GetBassGuitar(List<int> strongBeats, int meterup, int barsq, int bpm, int keyIndex, string scale, string key, Chord[] chords, int progressionLengthInBars, int chordLengthInStrongBeats, string[] drumsDown, int drumLickLength, bool forceBassOn1, bool forceBassOnEveryStrongBeat, char phrase)
        {
            Random rnd = new();
            if (forceBassOnEveryStrongBeat) forceBassOn1 = true;
            key = Toolbox.NormalizeNote(key);
            string[] roots = Toolbox.GetNotesInScale(scale, key);
            string bassRhythm = GetDrumsRhythm(drumsDown, drumLickLength);
            decimal ratio = (decimal)drumLickLength / progressionLengthInBars;
            if (ratio < 1)
            {
                string tmp = bassRhythm;
                ratio = Math.Round(1 / ratio);
                for (int i = 1; i < ratio; i++)
                    bassRhythm += tmp;
            }
            List<string> bassRhythmDivided = Toolbox.DivideRhythmToStrongBeats(strongBeats, meterup, bassRhythm);
            string[] progression = new string[chords.Length];
            for (int i = 0; i < chords.Length; i++)
            {
                progression[i] = possibleNotes[rnd.Next(possibleNotes.Length)];
                progression[i] = Toolbox.TransposeNote(progression[i], chords[i].root, roots);
            }

            int progressionCounter = 0;
            string[] rhythmSplit;
            bool done = false;
            bool rest = false;
            for (int i = 0; i < bassRhythmDivided.Count; i++) //i += chordLengthInStrongBeats)
            {
                int itmp = i % strongBeats.Count;
                if (itmp == 0 || itmp % chordLengthInStrongBeats == 0)
                {
                    if (i == 0 && !forceBassOn1)
                    {
                        if (bassRhythm.StartsWith('r'))
                        {
                            done = true;
                            rest = true;
                            rhythmSplit = bassRhythmDivided[i].Split(' ');
                            rhythmSplit[0] = "r" + rhythmSplit[0];
                            for (int j = 0; j < rhythmSplit.Length; j++)
                            {
                                if (!rhythmSplit[j].Contains("~") && j != rhythmSplit.Length - 1)
                                {
                                    rhythmSplit[j + 1] = progression[progressionCounter] + rhythmSplit[j + 1];
                                    break;
                                }
                            }
                            bassRhythmDivided[i] = string.Join(' ', rhythmSplit);
                        }
                    }
                    else if (i > 0 && !forceBassOnEveryStrongBeat)
                    {
                        if (bassRhythmDivided[i - 1].EndsWith("~") || bassRhythmDivided[i - 1].EndsWith("~|") || bassRhythmDivided[i - 1].EndsWith("~ |"))
                        {
                            done = true;
                            rhythmSplit = bassRhythmDivided[i].Split(' ');
                            for (int j = 0; j < rhythmSplit.Length; j++)
                            {
                                if (!rhythmSplit[j].Contains("~") && j != rhythmSplit.Length - 1)
                                {
                                    rhythmSplit[j + 1] = progression[progressionCounter] + rhythmSplit[j + 1];
                                    break;
                                }
                            }
                            bassRhythmDivided[i] = string.Join(' ', rhythmSplit);
                        }
                    }

                    if (!done) bassRhythmDivided[i] = progression[progressionCounter] + bassRhythmDivided[i];
                    progressionCounter++;
                    done = false;
                    if (progressionCounter == progression.Length) progressionCounter = 0;
                }
            }

            string bass = string.Join(" ", bassRhythmDivided);

            int bassLengthInBars;
            if (progressionLengthInBars > drumLickLength) bassLengthInBars = progressionLengthInBars;
            else bassLengthInBars = drumLickLength;
            int iterations = (int)Math.Ceiling((decimal)barsq / bassLengthInBars);
            int barsToDelete = iterations * bassLengthInBars - barsq;

            string[] variables = new string[iterations + 2];

            if (forceBassOn1 || !rest || iterations == 1)
            {
                for (int i = 1; i < iterations + 1; i++)
                    variables[i] = bass;
            }
            else
            {
                for (int i = 1; i < iterations + 1; i++)
                {
                    if (i != iterations)
                    {
                        variables[i] = i switch
                        {
                            1 => bass[..^2] + "~ |",
                            _ => bass[1..^2] + "~ |",
                        };
                    }
                    else
                    {
                        variables[i] = bass[1..];
                    }
                }
            }
            //variables[0] = "bassGuitar" + phrase + " = { \\numericTimeSignature \\time " + meterup + "/4 \\tempo 4 = " + bpm + Toolbox.GetSongKey(keyIndex);
            variables[0] = "bassGuitar" + phrase + " = {";
            variables[^1] += "\\bar \"||\"}";

            string[] lastProgression = variables[iterations].Split("|");
            variables[iterations] = "";
            for (int i = 0; i < bassLengthInBars - barsToDelete; i++)
                variables[iterations] += lastProgression[i] + "|";

            string[] music =
            {
                "\\new StaffGroup \\with { instrumentName = \\markup { \\center-column { \"Bass\" \\line { \"Guitar\" } } } } <<",
                "\\new Staff \\with {midiInstrument = \"electric bass (finger)\" }",
                "{ \\clef \"bass_8\" \\bassGuitar }",
                "\\new TabStaff \\with { stringTunings = #bass-tuning } { \\bassGuitar }",
                ">>",
            };

            //return new string[2][] { variables, music };
            return variables;
        }
        //private static string GetDrumsRhythm(string[] drumVars, int drumLickLength)
        //{
        //    int drumIndex = Array.FindIndex(drumVars, p => p.StartsWith("drumsDown = "));
        //    string[] drums = string.Join("", drumVars[(drumIndex + 1)..]).Split("|");
        //    List<string> drumsBars = new();
        //    int barsCounter = 0;
        //    for (int i = 0; i < drums.Length; i++)
        //        if (!string.IsNullOrWhiteSpace(drums[i]))
        //        {
        //            drumsBars.Add(drums[i]);
        //            barsCounter++;
        //            if (barsCounter == drumLickLength) break;
        //        }
        //    return string.Join("|", drumsBars).Replace("bd", "").Replace("sn", "") + "| ";
        //}
        private static string GetDrumsRhythm(string[] drumsDown, int drumLickLength)
        {
            //int drumIndex = Array.FindIndex(drumVars, p => p.StartsWith("drumsDown = "));
            string[] drums = string.Join("", drumsDown[1..]).Split("|");
            List<string> drumsBars = new();
            int barsCounter = 0;
            for (int i = 0; i < drums.Length; i++)
                if (!string.IsNullOrWhiteSpace(drums[i]))
                {
                    drumsBars.Add(drums[i]);
                    barsCounter++;
                    if (barsCounter == drumLickLength) break;
                }
            return string.Join("|", drumsBars).Replace("bd", "").Replace("sn", "") + "| ";
        }
        private static readonly string[] possibleNotes =
        {
            "e,,", "e,"
        };
    }
}
