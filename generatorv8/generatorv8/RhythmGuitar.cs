using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace generatorv8
{
    class RhythmGuitar
    {
        public static string[] GetRhythmGuitar(List<int> strongBeats, int meterup, int barsq, int bpm, int keyIndex, Chord[] chords, int progressionLengthInBars, int chordLengthInStrongBeats, List<int> rhythmProb, char phrase)
        {
            Random rnd = new();
            int progressionLength = chords.Length;
            List<decimal>[] strummingPattern = new List<decimal>[progressionLength];
            string[] chordStrumming = new string[progressionLength];
            decimal chordLeft;

            decimal[] rhythmProbArray = Toolbox.GetProbDecimalArray(possibleValues, rhythmProb);
            int highestAllowed;
            int iterations = (int)Math.Ceiling((decimal)barsq / progressionLengthInBars);
            int barsToDelete = iterations * progressionLengthInBars - barsq;
            List<int> barcheckInd = new();
            int count = 0;
            int strongBeatInd = 0;
            int randomInd = 0;
            int minInd = 0;
            bool overrideProb;

            for (int i = 0; ; i += chordLengthInStrongBeats)
            {
                if (i + chordLengthInStrongBeats >= strongBeats.Count)
                {
                    barcheckInd.Add(count);
                    break;
                }
                count++;
            }

            for (int i = 1; i < progressionLengthInBars; i++)
                barcheckInd.Add((barcheckInd[0] + 1) * (i + 1) - 1);


            for (int i = 0; i < progressionLength; i++)
            {
                overrideProb = false;
                if (barcheckInd.Contains(i)) chordLeft = meterup - strongBeats[strongBeatInd];
                else chordLeft = strongBeats[strongBeatInd + chordLengthInStrongBeats] - strongBeats[strongBeatInd];
                highestAllowed = possibleValues.Length - 1;
                strummingPattern[i] = new();

                while (chordLeft > 0)
                {
                    minInd = 0;
                    if (!overrideProb)
                        for (int j = 0; j < rhythmProbArray.Length; j++)
                        {
                            if (rhythmProbArray[j] <= chordLeft)
                            {
                                minInd = j;
                                break;
                            }
                            else if (j == rhythmProbArray.Length - 1) overrideProb = true;
                        }
                    if (!overrideProb)
                    {
                        randomInd = rnd.Next(minInd, rhythmProbArray.Length);
                        chordLeft -= rhythmProbArray[randomInd];
                        strummingPattern[i].Add(rhythmProbArray[randomInd]);
                    }
                    else
                    {
                        for (int j = 0; j < possibleValues.Length; j++)
                        {
                            if (possibleValues[j] <= chordLeft)
                            {
                                minInd = j;
                                break;
                            }
                        }
                        randomInd = rnd.Next(minInd, possibleValues.Length);
                        chordLeft -= possibleValues[randomInd];
                        strummingPattern[i].Add(possibleValues[randomInd]);
                    }
                }


                chordStrumming[i] = "";
                string[] tmp = Toolbox.DivideValuesAndGetNotes(strummingPattern[i], meterup, strongBeats, strongBeats[strongBeatInd]);
                for (int j = 0; j < tmp.Length; j++)
                    chordStrumming[i] += tmp[j];
                chordStrumming[i] = chords[i].notes + chordStrumming[i][1..];
                chordStrumming[i] = chordStrumming[i].Replace("x", "");
                if (barcheckInd.Contains(i)) chordStrumming[i] += "|";
                strongBeatInd += chordLengthInStrongBeats;
                if (barcheckInd.Contains(i)) strongBeatInd = 0;
            }

            string[] variables = new string[iterations + 2];
            for (int i = 1; i < iterations + 1; i++)
                for (int j = 0; j < progressionLength; j++)
                    variables[i] += chordStrumming[j];
            //variables[0] = "rhythmGuitar" + phrase + " = { \\numericTimeSignature \\time " + meterup + "/4 \\tempo 4 = " + bpm + Toolbox.GetSongKey(keyIndex);
            variables[0] = "rhythmGuitar" + phrase + " = {";
            variables[^1] += "\\bar \"||\"}";

            string[] lastProgression = variables[iterations].Split("|");
            variables[iterations] = "";
            for (int i = 0; i < progressionLengthInBars - barsToDelete; i++)
                variables[iterations] += lastProgression[i] + "|";

            string[] music =
            {
                "\\new StaffGroup \\with { instrumentName = \\markup { \\center-column { \"Rhythm\" \\line { \"Guitar\" } } } } <<",
                //"\\new ChordNames { \\set chordChanges = ##t \\rhythmGuitar }",
                "\\new Staff \\with {midiInstrument = \"acoustic guitar (steel)\" }",
                "{ \\clef \"G_8\" \\rhythmGuitar }",
                "\\new TabStaff { \\rhythmGuitar }",
                ">>",
            };

            //return new string[2][] { variables, music };
            return variables;
        }
        private static readonly decimal[] possibleValues =
        {
            3, 2.75M, 2.5M, 2.25M, 2, 1.75M, 1.5M, 1.25M, 1, 0.75M, 0.5M, 0.25M
        };
    }
}
