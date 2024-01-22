using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace generatorv8
{
    class LeadGuitar
    {
        public static string[] GetLeadGuitar(List<int> strongBeats, int meterup, int barsq, int bpm, int keyIndex, string key, string scale, Chord[] chords, int progressionLengthInBars, int chordLengthInStrongBeats, List<int> stepProb, List<int> intervalProb, List<int> rhythmProb, string lowestNote, string highestNote, bool forceTonicOn1, int rerolls, decimal[] breakProbs, char phrase)
        {
            Random rnd = new();
            decimal rhythmLength = barsq * meterup;
            decimal[] rhythmProbArray = Toolbox.GetProbDecimalArray(possibleValues, rhythmProb);
            List<decimal> valuesRhythm = GetRhythm(rhythmLength, rhythmProbArray, rnd, breakProbs, strongBeats, meterup);
            List<decimal> valuesRhythmCopy = new(valuesRhythm);
            string[] rhythm = Toolbox.DivideValuesAndGetNotes(valuesRhythm, meterup, strongBeats, 0);
            string[] rhythmDivided = string.Join('z', rhythm).Split('x');

            string[] notesInScale = Toolbox.GetNotesInScale(scale, Toolbox.NormalizeNote(key));
            string[] cBasedNotesInScale = Toolbox.GetCBasedNoteSequence(notesInScale);
            List<string> NotesInScaleInOctaves = new();
            for (int i = 0; i < allGuitarOctaves.Length; i++)
                for (int j = 0; j < cBasedNotesInScale.Length; j++)
                    NotesInScaleInOctaves.Add(cBasedNotesInScale[j] + allGuitarOctaves[i]);

            List<string> allAllowedNotes = Toolbox.RemoveNotesOutOfRange(NotesInScaleInOctaves, lowestNote, highestNote);
            if (allAllowedNotes.Count == 0) allAllowedNotes = Toolbox.RemoveNotesOutOfRange(NotesInScaleInOctaves, "e,", "e'''");
            string[] notesProbArray = Toolbox.GetProbArray(notesInScale, stepProb);

            string firstNote;
            if (forceTonicOn1) firstNote = chords[0].root;
            else firstNote = notesProbArray[rnd.Next(notesProbArray.Length)];
            List<string> tmpNotes = allAllowedNotes.FindAll(ele => ele.StartsWith(firstNote));
            firstNote = tmpNotes[rnd.Next(tmpNotes.Count)];
            rhythmDivided[0] += firstNote;
            int currentNoteIndex = allAllowedNotes.FindIndex(ele => ele == firstNote);


            decimal[] interProbArray = Toolbox.GetProbDecimalArray(possibleIntervals, intervalProb);
            int highestPossibleInterval;
            int lastPossibleIntervalInd;
            int interval;
            string note1;
            string note2;
            string[] tmpProb;
            int note1ind;
            int note2ind;
            string currentNote = "";
            for (int i = 1; i < rhythmDivided.Length - 1; i++)
            {
                string[] currentChordNotes = Toolbox.GetCurrentChordNotes(chords, new List<int>(strongBeats), meterup, valuesRhythmCopy.GetRange(0, i).Sum(), chordLengthInStrongBeats, progressionLengthInBars, notesInScale);
                highestPossibleInterval = Math.Max(allAllowedNotes.Count - 1 - currentNoteIndex, currentNoteIndex);
                lastPossibleIntervalInd = Array.FindLastIndex(interProbArray, ele => ele <= highestPossibleInterval);
                for (int j = 0; j < rerolls; j++)
                {
                    if (lastPossibleIntervalInd == -1)
                    {
                        lastPossibleIntervalInd = Array.FindLastIndex(possibleIntervals, ele => ele <= highestPossibleInterval);
                        interval = (int)possibleIntervals[rnd.Next(lastPossibleIntervalInd + 1)];
                    }
                    else interval = (int)interProbArray[rnd.Next(lastPossibleIntervalInd + 1)];

                    if (currentNoteIndex + interval > allAllowedNotes.Count - 1)
                        currentNote = allAllowedNotes[currentNoteIndex - interval];
                    else if (currentNoteIndex - interval < 0)
                        currentNote = allAllowedNotes[currentNoteIndex + interval];
                    else
                    {
                        note1 = allAllowedNotes[currentNoteIndex + interval];
                        note2 = allAllowedNotes[currentNoteIndex - interval];
                        note1ind = Array.FindIndex(notesInScale, ele => ele == Toolbox.EraseOctaves(note1));
                        note2ind = Array.FindIndex(notesInScale, ele => ele == Toolbox.EraseOctaves(note2));
                        tmpProb = Toolbox.GetProbArray(new string[] { note1, note2 }, new List<int> { intervalProb[note1ind], intervalProb[note2ind] });
                        if (tmpProb.Length == 0) currentNote = new string[] { note1, note2 }[rnd.Next(2)];
                        else currentNote = tmpProb[rnd.Next(tmpProb.Length)];
                    }
                    if (currentChordNotes.Contains(Toolbox.EraseOctaves(currentNote))) break;
                }
                rhythmDivided[i] += currentNote;
                currentNoteIndex = allAllowedNotes.FindIndex(ele => ele == currentNote);
            }
            string[] leadGuitar = string.Join("", rhythmDivided).Split('z');

            string[] variables = new string[leadGuitar.Length + 2];
            Array.Copy(leadGuitar, 0, variables, 1, leadGuitar.Length);
            //variables[0] = "leadGuitar" + phrase + " = { \\numericTimeSignature \\time " + meterup + "/4 \\tempo 4 = " + bpm + Toolbox.GetSongKey(keyIndex);
            variables[0] = "leadGuitar" + phrase + " = {";
            variables[^1] += "\\bar \"||\"}";

            string[] music =
            {
                "\\new StaffGroup \\with { instrumentName = \\markup { \\center-column { \"Lead\" \\line { \"Guitar\" } } } } <<",
                "\\new Staff \\with {midiInstrument = \"distorted guitar\" }",
                "{ \\clef \"G_8\" \\leadGuitar }",
                "\\new TabStaff { \\leadGuitar } >>",
            };
            //return new string[][] { variables, music };
            return variables;
        }
        private static List<decimal> GetRhythm(decimal rhythmLength, decimal[] rhythmProbArray, Random rnd, decimal[] probs, List<int> strongBeats, int meterup)
        {
            int minInd;
            bool overrideProb = false;
            decimal randomValue;
            List<decimal> rhythm = new();
            int counter = 0;
            while (rhythmLength > 0)
            {
                minInd = 0;
                if (!overrideProb)
                    for (int j = 0; j < rhythmProbArray.Length; j++)
                    {
                        if (rhythmProbArray[j] <= rhythmLength)
                        {
                            minInd = j;
                            break;
                        }
                        else if (j == rhythmProbArray.Length - 1) overrideProb = true;
                    }
                if (!overrideProb)
                    randomValue = rhythmProbArray[rnd.Next(minInd, rhythmProbArray.Length)];
                else
                {
                    for (int j = 0; j < possibleValues.Length; j++)
                    {
                        if (possibleValues[j] <= rhythmLength)
                        {
                            minInd = j;
                            break;
                        }
                    }
                    randomValue = possibleValues[rnd.Next(minInd, possibleValues.Length)];
                }
                decimal newValue = DivideNotes(randomValue, rhythm.Sum(), rnd, probs, strongBeats, meterup);
                rhythm.Add(newValue);
                counter++;
                rhythmLength -= newValue;
            }
            return rhythm;
        }
        private static decimal DivideNotes(decimal value, decimal sumPrevious, Random rnd, decimal[] probs, List<int> strongBeats, int meterup)
        {
            decimal probBar = probs[0];
            decimal probStrongBeat = probs[1];
            decimal probBeat = probs[2];
            int brokenInd;


            decimal oldBar = sumPrevious / meterup;
            decimal newBar = (sumPrevious + value) / meterup;
            if (Math.Ceiling(newBar) - Math.Floor(oldBar) >= 2)
            {
                if ((decimal)rnd.NextDouble() < probBar)
                {
                    if (Math.Ceiling(oldBar) == oldBar) brokenInd = (int)oldBar + 1;
                    else brokenInd = (int)Math.Ceiling(oldBar);
                    return brokenInd * meterup - sumPrevious;
                }
                else return value;
            }


            decimal oldStrongBeatHelp = sumPrevious % meterup;
            decimal newStrongBeatHelp = (sumPrevious + value) % meterup;
            int oldStrongBeat = strongBeats.Count - 1;
            int newStrongBeat = strongBeats.Count - 1;
            for (int k = 1; k < strongBeats.Count; k++)
            {
                if (strongBeats[k] > oldStrongBeatHelp)
                {
                    oldStrongBeat = k - 1;
                    break;
                }
            }
            for (int k = 1; k < strongBeats.Count; k++)
            {
                if (strongBeats[k] > newStrongBeatHelp)
                {
                    newStrongBeat = k - 1;
                    break;
                }
            }
            if (oldStrongBeat != newStrongBeat && strongBeats[newStrongBeat] != newStrongBeatHelp)
            {
                if ((decimal)rnd.NextDouble() < probStrongBeat)
                {
                    return strongBeats[newStrongBeat] - oldStrongBeatHelp;
                }
                else return value;
            }

            decimal oldBeat = sumPrevious;
            decimal newBeat = sumPrevious + value;

            if (Math.Ceiling(newBeat) - Math.Floor(oldBeat) >= 2)
            {
                if ((decimal)rnd.NextDouble() < probBeat)
                {
                    if (Math.Ceiling(oldBeat) == oldBeat) brokenInd = (int)oldBeat + 1;
                    else brokenInd = (int)Math.Ceiling(oldBeat);
                    return brokenInd - sumPrevious;
                }
                else return value;
            }
            return value;

        }
        private static readonly decimal[] possibleValues =
        {
            3, 2.75M, 2.5M, 2.25M, 2, 1.75M, 1.5M, 1.25M, 1, 0.75M, 0.5M, 0.25M
        };
        private static readonly string[] allGuitarOctaves =
        {
            ",", "", "'", "''", "'''"
        };
        private static readonly decimal[] possibleIntervals =
        {
            0, 1, 2, 3, 4, 5, 6, 7
        };
    }
}
