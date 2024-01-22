using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace generatorv8
{
    static class Toolbox
    {
        public static string[] JoinMultipleArrays(string[][] arrays)
        {
            int arrayLen = 0;
            int currentIndex = 0;
            for (int i = 0; i < arrays.Length; i++)
                arrayLen += arrays[i].Length;
            string[] bigArray = new string[arrayLen];
            for (int i = 0; i < arrays.Length; i++)
            {
                arrays[i].CopyTo(bigArray, currentIndex);
                currentIndex += arrays[i].Length;
            }
            return bigArray;
        }
        public static string NormalizeNote(string note)
        {
            if (note.EndsWith('b')) note = note[..^1] + "es";
            else if (note.EndsWith('#')) note = note[..^1] + "is";
            note = note.ToLowerInvariant();
            return note;
        }
        public static string UnnormalizeNote(string note, bool capitalize)
        {
            if (capitalize)
            {
                if (note.Length == 1) note = note.ToUpperInvariant();
                else note = note[0].ToString().ToUpperInvariant() + note[1..];
            }
            if (note.EndsWith("es")) note = note[..^2] + "b";
            else if (note.EndsWith("is")) note = note[..^2] + "#";
            return note;
        }
        public static string[] GetNotesInScale(string scale, string key)
        {
            int[] intervals = { 2, 2, 2, 2, 2, 2 };
            switch (scale)
            {
                case "Ionian": intervals[2] = 1; break;
                case "Dorian": intervals[1] = 1; intervals[5] = 1; break;
                case "Phrygian": intervals[0] = 1; intervals[4] = 1; break;
                case "Lydian": intervals[3] = 1; break;
                case "Mixolydian": intervals[5] = 1; intervals[2] = 1; break;
                case "Aeolian": intervals[4] = 1; intervals[1] = 1; break;
                case "Locrian": intervals[3] = 1; intervals[0] = 1; break;
            }
            string[] notes = new string[7];
            notes[0] = key;
            int stepInd = -5;
            int semitoneInd = -5;
            bool foundnote;
            int stepsLen = stepsSequence.Length;
            int semitonesLen = semitonesSequence.Length;
            for (int i = 0; i < stepsLen; i++) if (stepsSequence[i].Contains(key)) stepInd = i;
            for (int i = 0; i < semitonesLen; i++) if (semitonesSequence[i].Contains(key)) semitoneInd = i;
            for (int i = 1; i < 7; i++)
            {
                foundnote = false;
                stepInd++;
                if (stepInd >= stepsLen) stepInd -= stepsLen;
                if (intervals[i - 1] == 2) semitoneInd += 2;
                else semitoneInd++;
                if (semitoneInd >= semitonesLen) semitoneInd -= semitonesLen;
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        if (semitonesSequence[semitoneInd][j] != null)
                        {
                            if (semitonesSequence[semitoneInd][j] == stepsSequence[stepInd][k])
                            {
                                notes[i] = semitonesSequence[semitoneInd][j];
                                foundnote = true;
                                break;
                            }
                        }
                    }
                    if (foundnote) break;
                }
            }
            return notes;
        }
        public static string[] GetChordTypes(string scale)
        {
            return scale switch
            {
                "Ionian" => ionianChordTypes,
                "Dorian" => ionianChordTypes[1..].Concat(ionianChordTypes[..1]).ToArray(),
                "Phrygian" => ionianChordTypes[2..].Concat(ionianChordTypes[..2]).ToArray(),
                "Lydian" => ionianChordTypes[3..].Concat(ionianChordTypes[..3]).ToArray(),
                "Mixolydian" => ionianChordTypes[4..].Concat(ionianChordTypes[..4]).ToArray(),
                "Aeolian" => ionianChordTypes[5..].Concat(ionianChordTypes[..5]).ToArray(),
                "Locrian" => ionianChordTypes[6..].Concat(ionianChordTypes[..6]).ToArray(),
                _ => Array.Empty<string>(),
            };
        }
        public static int FindSemitoneIndex(string note)
        {
            for (int i = 0; i < semitonesSequence.Length; i++)
                for (int j = 0; j < 3; j++)
                    if (semitonesSequence[i][j] != null)
                        if (semitonesSequence[i][j] == note)
                            return i;
            return -1;
        }
        public static string NoteOctaveTranspose(string note, int octaves)
        {
            for (; octaves > 0; octaves--)
            {
                if (note.EndsWith(',')) note = note[..^1];
                else note += "'";
            }
            for (; octaves < 0; octaves++)
            {
                if (note.EndsWith('\'')) note = note[..^1];
                else note += ",";
            }
            return note;
        }
        public static string GetSongKey(int keyIndex)
        {
            return keyIndex switch
            {
                0 => @" \key ces \major",
                1 => @" \key ges \major",
                2 => @" \key des \major",
                3 => @" \key aes \major",
                4 => @" \key ees \major",
                5 => @" \key bes \major",
                6 => @" \key f \major",
                7 => @" \key c \major",
                8 => @" \key g \major",
                9 => @" \key d \major",
                10 => @" \key a \major",
                11 => @" \key e \major",
                12 => @" \key b \major",
                13 => @" \key fis \major",
                14 => @" \key cis \major",
                _ => "error in GetSongKey",
            };
        }
        public static string[] GetProbArray(string[] values, List<int> probabilities)
        {
            string[] probArray = new string[probabilities.Sum()];
            int probIndex = 0;
            for (int i = 0; i < probabilities.Count; i++)
            {
                for (int j = probabilities[i]; j > 0; j--)
                {
                    probArray[probIndex] = values[i];
                    probIndex++;
                }
            }
            return probArray;
        }
        public static decimal[] GetProbDecimalArray(decimal[] values, List<int> probabilities)
        {
            decimal[] probArray = new decimal[probabilities.Sum()];
            int probIndex = 0;
            for (int i = 0; i < probabilities.Count; i++)
            {
                for (int j = probabilities[i]; j > 0; j--)
                {
                    probArray[probIndex] = values[i];
                    probIndex++;
                }
            }
            return probArray;
        }
        //public static string[] DivideValuesAndGetNotes(List<decimal> values, int meterup, List<int> strongBeatsInt, decimal currentBeat)
        //{
        //    decimal noteStart = currentBeat;
        //    decimal noteEnd;
        //    List<decimal> valuesCopy = new(values);
        //    List<List<decimal>> newNotes = new();
        //    newNotes.Add(new List<decimal>());
        //    List<List<bool>> ligatures = new();
        //    ligatures.Add(new List<bool>());
        //    List<decimal> strongBeats = strongBeatsInt.ConvertAll(x => (decimal)x);
        //    int strongBeatIndex = 0;
        //    for (int i = 0; i < strongBeats.Count; i++)
        //    {
        //        if (noteStart >= strongBeats[i] && i == strongBeats.Count - 1)
        //            strongBeatIndex = i;
        //        else if (noteStart < strongBeats[i])
        //            strongBeatIndex = i - 1;
        //    }
        //    decimal nextStrongBeat;
        //    bool newBar = false;
        //    int brokenBeat;
        //    bool added;
        //    int currentBar = 0;
        //    int newBarCount = 0;
        //    for (int i = 0; i < values.Count; i++)
        //    {
        //        if (newBar)
        //        {
        //            currentBar++;
        //            currentBeat = 0;
        //            newNotes.Add(new List<decimal>());
        //            ligatures.Add(new List<bool>());
        //            strongBeatIndex = 0;
        //            newBarCount++;
        //            newBar = false;
        //        }
        //        if (strongBeatIndex == strongBeats.Count - 1) nextStrongBeat = meterup;
        //        else nextStrongBeat = strongBeats[strongBeatIndex + 1];

        //        added = false;
        //        noteEnd = values.GetRange(0, i + 1).Sum() + currentBeat - newBarCount * meterup;
        //        if (i > 0) noteStart = values.GetRange(0, i).Sum() + currentBeat - newBarCount * meterup;

        //        if (meterup == 4 && noteStart == 0)
        //        {
        //            if (noteEnd == 4)
        //            {
        //                newNotes[currentBar].Add(4);
        //                ligatures[currentBar].Add(false);
        //                newBar = true;
        //                added = true;
        //            }
        //            else if (noteEnd > 4)
        //            {
        //                newNotes[currentBar].Add(4);
        //                ligatures[currentBar].Add(true);
        //                newBar = true;
        //                values[i] = 4;
        //                values.Insert(i + 1, noteEnd - 4);
        //                added = true;
        //                noteEnd = 4;
        //            }
        //        }

        //        if (noteStart == strongBeats[strongBeatIndex] && !added)
        //        {
        //            if (noteEnd > nextStrongBeat)
        //            {
        //                newNotes[currentBar].Add(nextStrongBeat - noteStart);
        //                ligatures[currentBar].Add(true);
        //                values[i] = nextStrongBeat - noteStart;
        //                values.Insert(i + 1, noteEnd - nextStrongBeat);
        //                noteEnd = nextStrongBeat;
        //            }
        //            else if (noteEnd <= nextStrongBeat)
        //            {
        //                newNotes[currentBar].Add(noteEnd - noteStart);
        //                ligatures[currentBar].Add(false);
        //            }
        //        }

        //        else
        //        {
        //            if (Math.Ceiling(noteEnd) - Math.Floor(noteStart) >= 2)
        //            {
        //                if (Math.Ceiling(noteStart) == noteStart) brokenBeat = (int)noteStart + 1;
        //                else brokenBeat = (int)Math.Ceiling(noteStart);
        //                newNotes[currentBar].Add(brokenBeat - noteStart);
        //                ligatures[currentBar].Add(true);
        //                values[i] = brokenBeat - noteStart;
        //                values.Insert(i + 1, noteEnd - brokenBeat);
        //                noteEnd = brokenBeat;
        //            }
        //            else
        //            {
        //                newNotes[currentBar].Add(noteEnd - noteStart);
        //                ligatures[currentBar].Add(false);
        //            }
        //        }
        //        if (noteEnd >= nextStrongBeat) strongBeatIndex++;
        //        if (strongBeatIndex == strongBeats.Count) newBar = true;
        //    }
        //    return DividedValuesToNotes(newNotes, ligatures);
        //}
        public static string[] DivideValuesAndGetNotes(List<decimal> values, int meterup, List<int> strongBeatsInt, decimal currentBeat)
        {
            decimal noteStart = currentBeat;
            decimal noteEnd;
            List<decimal> valuesCopy = new(values);
            List<List<decimal>> newNotes = new();
            newNotes.Add(new List<decimal>());
            List<List<bool>> ligatures = new();
            ligatures.Add(new List<bool>());
            List<decimal> strongBeats = strongBeatsInt.ConvertAll(x => (decimal)x);
            int strongBeatIndex = 0;
            for (int i = 0; i < strongBeats.Count; i++)
            {
                if (noteStart >= strongBeats[i] && i == strongBeats.Count - 1)
                    strongBeatIndex = i;
                else if (noteStart < strongBeats[i])
                    strongBeatIndex = i - 1;
            }
            decimal nextStrongBeat;
            bool newBar = false;
            int brokenBeat;
            bool added;
            int currentBar = 0;
            int newBarCount = 0;
            for (int i = 0; i < valuesCopy.Count; i++)
            {
                if (newBar)
                {
                    currentBar++;
                    currentBeat = 0;
                    newNotes.Add(new List<decimal>());
                    ligatures.Add(new List<bool>());
                    strongBeatIndex = 0;
                    newBarCount++;
                    newBar = false;
                }
                if (strongBeatIndex == strongBeats.Count - 1) nextStrongBeat = meterup;
                else nextStrongBeat = strongBeats[strongBeatIndex + 1];

                added = false;
                noteEnd = valuesCopy.GetRange(0, i + 1).Sum() + currentBeat - newBarCount * meterup;
                if (i > 0) noteStart = valuesCopy.GetRange(0, i).Sum() + currentBeat - newBarCount * meterup;

                if (meterup == 4 && noteStart == 0)
                {
                    if (noteEnd == 4)
                    {
                        newNotes[currentBar].Add(4);
                        ligatures[currentBar].Add(false);
                        newBar = true;
                        added = true;
                    }
                    else if (noteEnd > 4)
                    {
                        newNotes[currentBar].Add(4);
                        ligatures[currentBar].Add(true);
                        newBar = true;
                        valuesCopy[i] = 4;
                        valuesCopy.Insert(i + 1, noteEnd - 4);
                        added = true;
                        noteEnd = 4;
                    }
                }

                if (noteStart == strongBeats[strongBeatIndex] && !added)
                {
                    if (noteEnd > nextStrongBeat)
                    {
                        newNotes[currentBar].Add(nextStrongBeat - noteStart);
                        ligatures[currentBar].Add(true);
                        valuesCopy[i] = nextStrongBeat - noteStart;
                        valuesCopy.Insert(i + 1, noteEnd - nextStrongBeat);
                        noteEnd = nextStrongBeat;
                    }
                    else if (noteEnd <= nextStrongBeat)
                    {
                        newNotes[currentBar].Add(noteEnd - noteStart);
                        ligatures[currentBar].Add(false);
                    }
                }

                else
                {
                    if (Math.Ceiling(noteEnd) - Math.Floor(noteStart) >= 2)
                    {
                        if (Math.Ceiling(noteStart) == noteStart) brokenBeat = (int)noteStart + 1;
                        else brokenBeat = (int)Math.Ceiling(noteStart);
                        newNotes[currentBar].Add(brokenBeat - noteStart);
                        ligatures[currentBar].Add(true);
                        valuesCopy[i] = brokenBeat - noteStart;
                        valuesCopy.Insert(i + 1, noteEnd - brokenBeat);
                        noteEnd = brokenBeat;
                    }
                    else
                    {
                        newNotes[currentBar].Add(noteEnd - noteStart);
                        ligatures[currentBar].Add(false);
                    }
                }
                if (noteEnd >= nextStrongBeat) strongBeatIndex++;
                if (strongBeatIndex == strongBeats.Count) newBar = true;
            }
            return DividedValuesToNotes(newNotes, ligatures);
        }
        public static string[] DividedValuesToNotes(List<List<decimal>> values, List<List<bool>> ligatures)
        {
            bool x = true;
            string[] notes = new string[values.Count];
            for (int i = 0; i < values.Count; i++)
            {
                notes[i] = "";
                for (int j = 0; j < values[i].Count; j++)
                {
                    notes[i] += TranslateValuesToNotes(values[i][j], x);
                    if (ligatures[i][j])
                    {
                        notes[i] += "~";
                        x = false;
                    }
                    else x = true;
                    notes[i] += " ";
                }
                if (values.Count > 1) notes[i] += "|";
            }
            return notes;
        }
        public static string TranslateValuesToNotes(decimal value, bool x)
        {
            string note = "";
            int lowestInd = 0;
            bool firstTime = true;
            while (value > 0)
            {
                for (int i = lowestInd; i < possibleNoteValuesIncludingDotted.Length; i++)
                {
                    if (value >= possibleNoteValuesIncludingDotted[i])
                    {
                        lowestInd = i;
                        break;
                    }
                }
                if (firstTime)
                {
                    if (x) note = noteNamesIncludingDottedx[lowestInd];
                    else note = noteNamesIncludingDotted[lowestInd];
                    firstTime = false;
                }
                else note += "~ " + noteNamesIncludingDotted[lowestInd];
                value -= possibleNoteValuesIncludingDotted[lowestInd];
            }
            return note;
        }
        public static string TransposeNote(string noteToTranspose, string newRoot, string[] notesInScale)
        {
            int octaves = 0;
            while (noteToTranspose.EndsWith('\''))
            {
                noteToTranspose = noteToTranspose[..^1];
                octaves++;
            }
            while (noteToTranspose.EndsWith(','))
            {
                noteToTranspose = noteToTranspose[..^1];
                octaves--;
            }
            int semitonesUp = FindSemitoneIndex(newRoot) - FindSemitoneIndex(noteToTranspose);
            if (semitonesUp < 0) semitonesUp += 12;
            int oldIndex;
            int newIndex;
            oldIndex = Toolbox.FindSemitoneIndex(noteToTranspose);
            newIndex = oldIndex + semitonesUp;
            if (newIndex > 11)
            {
                newIndex -= 12;
                octaves++;
            }
            for (int j = 0; j < 3; j++)
                if (semitonesSequence[newIndex][j] != null)
                    if (notesInScale.Contains(semitonesSequence[newIndex][j]))
                        noteToTranspose = semitonesSequence[newIndex][j];
            if (octaves != 0) noteToTranspose = NoteOctaveTranspose(noteToTranspose, octaves);
            return noteToTranspose;
        }
        public static List<string> DivideRhythmToStrongBeats(List<int> strongBeats, int meterup, string rhythm)
        {
            List<string> rhythmValuesList = new();
            List<string> dividedRhythm = new();
            string[] rhythmValuesArr = rhythm.Replace("|", "").Split(" ");
            foreach (string ele in rhythmValuesArr)
                if (!string.IsNullOrWhiteSpace(ele)) rhythmValuesList.Add(ele);
            List<bool> rests = new();

            for (int i = 0; i < rhythmValuesList.Count; i++)
                if (rhythmValuesList[i].Contains('r'))
                {
                    rests.Add(true);
                    rhythmValuesList[i] = rhythmValuesList[i].Replace("r", "");
                }
                else rests.Add(false);

            int nextStrongBeatInd = 1;
            decimal nextStrongBeat;
            if (strongBeats.Count > nextStrongBeatInd) nextStrongBeat = strongBeats[1];
            else nextStrongBeat = meterup;
            decimal counter = 0;
            int firstIndToAdd = 0;
            for (int i = 0; i < rhythmValuesList.Count; i++)
            {
                counter += TranslateNoteToValue(rhythmValuesList[i]);
                if (rests.Count > i + 2) if (rests[i + 1]) rhythmValuesList[i] += "~";
                if (counter == nextStrongBeat)
                {
                    dividedRhythm.Add(string.Join(" ", rhythmValuesList.GetRange(firstIndToAdd, i + 1 - firstIndToAdd)));
                    firstIndToAdd = i + 1;
                    if (nextStrongBeat == meterup)
                    {
                        counter = 0;
                        nextStrongBeatInd = 1;
                    }
                    else nextStrongBeatInd++;
                    if (strongBeats.Count > nextStrongBeatInd) nextStrongBeat = strongBeats[nextStrongBeatInd];
                    else nextStrongBeat = meterup;
                }
            }

            for (int i = 1; i < dividedRhythm.Count / strongBeats.Count + 1; i++)
                dividedRhythm[i * strongBeats.Count - 1] += " |";

            return dividedRhythm;
        }
        public static decimal TranslateNoteToValue(string note)
        {
            return possibleNoteValuesIncludingDotted[Array.FindIndex(noteNamesIncludingDotted, p => p == note)];
        }
        public static int GetNotesDifference(string highNote, string lowNote)
        {
            int highNoteOctaves = 0;
            int lowNoteOctaves = 0;
            while (highNote.EndsWith(','))
            {
                highNote = highNote[..^1];
                highNoteOctaves--;
            }
            while (highNote.EndsWith('\''))
            {
                highNote = highNote[..^1];
                highNoteOctaves++;
            }
            while (lowNote.EndsWith(','))
            {
                lowNote = lowNote[..^1];
                lowNoteOctaves--;
            }
            while (lowNote.EndsWith('\''))
            {
                lowNote = lowNote[..^1];
                lowNoteOctaves++;
            }
            return FindSemitoneIndex(highNote) + 12 * highNoteOctaves - FindSemitoneIndex(lowNote) - 12 * lowNoteOctaves;
        }
        public static List<string> RemoveNotesOutOfRange(List<string> notes, string lowestAllowed, string highestAllowed)
        {
            int lowestInd = 0;
            int highestInd = 0;
            for (int i = 0; i < notes.Count; i++)
            {
                if (GetNotesDifference(lowestAllowed, notes[i]) <= 0)
                {
                    lowestInd = i;
                    break;
                }
            }
            for (int i = notes.Count - 1; i > -1; i--)
            {
                if (GetNotesDifference(highestAllowed, notes[i]) >= 0)
                {
                    highestInd = i;
                    break;
                }
            }
            if (highestInd <= lowestInd) return new List<string>();
            else return notes.GetRange(lowestInd, highestInd - lowestInd + 1);
        }
        public static string EraseOctaves(string note)
        {
            while (note.EndsWith(','))
                note = note[..^1];
            while (note.EndsWith('\''))
                note = note[..^1];
            return note;
        }
        public static string[] GetCurrentChordNotes(Chord[] chords, List<int> strongBeats, int meterup, decimal valuesSum, int chordLengthInStrongBeats, int progressionLengthInBars, string[] notesInScale)
        {
            //bool shorterChord = false;
            //int barStart = (int)Math.Floor(valuesSum / meterup);
            //if (strongBeats.Count % chordLengthInStrongBeats != 0) shorterChord = true;
            //int strongBeatsInitialCount = strongBeats.Count;
            valuesSum %= meterup * progressionLengthInBars;
            int barNumber = (int)Math.Floor(valuesSum / meterup);
            valuesSum -= barNumber * meterup;
            int chordsInBars = (int)Math.Ceiling((decimal)strongBeats.Count / chordLengthInStrongBeats);
            int initialChordIndex = barNumber * chordsInBars;
            //for (int i = 1; i < progressionLengthInBars; i++)
            //    for (int j = 0; j < strongBeatsInitialCount; j++)
            //        strongBeats.Add(strongBeats[j] + meterup * i);
            int strongBeatInd = strongBeats.Count - 1;
            int chordIndexInStrongBeats = chordLengthInStrongBeats;
            int chordIndex = chords.Length - 1;
            for (int i = 1; i < strongBeats.Count; i++)
            {
                if (strongBeats[i] > valuesSum)
                {
                    strongBeatInd = i - 1;
                    break;
                }
            }
            for (int i = 1; i < strongBeats.Count; i++)
            {
                if (strongBeatInd < chordIndexInStrongBeats)
                {
                    chordIndex = initialChordIndex + i - 1;
                    break;
                }
                chordIndexInStrongBeats += chordLengthInStrongBeats;
            }

            //bool shorterChord = false;
            //if (strongBeats.Count % chordLengthInStrongBeats != 0) shorterChord = true;
            //int strongBeatsInitialCount = strongBeats.Count;
            //valuesSum %= meterup * progressionLengthInBars;
            //for (int i = 1; i < progressionLengthInBars; i++)
            //    for (int j = 0; j < strongBeatsInitialCount; j++)
            //        strongBeats.Add(strongBeats[j] + meterup * i);
            //int strongBeatInd = strongBeats.Count - 1;
            //int chordIndexInStrongBeats = chordLengthInStrongBeats;
            //int chordIndex = chords.Length - 1;
            //for (int i = 1; i < strongBeats.Count; i++)
            //{
            //    if (strongBeats[i] > valuesSum)
            //    {
            //        strongBeatInd = i - 1;
            //        break;
            //    }
            //}
            //for (int i = 1; i < strongBeats.Count; i++)
            //{
            //    if (shorterChord && chordIndexInStrongBeats % meterup == 0)
            //    {
            //        chordIndex = i;
            //        break;
            //    }
            //    if (strongBeatInd < chordIndexInStrongBeats)
            //    {
            //        chordIndex = i - 1;
            //        break;
            //    }
            //    chordIndexInStrongBeats += chordLengthInStrongBeats;
            //}

            string root = chords[chordIndex].root;
            int noteIndexInScale = Array.FindIndex(notesInScale, p => p == root);
            string[] currentChordNotes = new string[3];
            currentChordNotes[0] = root;
            for (int i = 0; i < 2; i++)
            {
                if (noteIndexInScale > notesInScale.Length - 3)
                    noteIndexInScale -= notesInScale.Length;
                noteIndexInScale += 2;
                currentChordNotes[i + 1] = notesInScale[noteIndexInScale];
            }
            return currentChordNotes;
        }
        public static string[] GetCBasedNoteSequence(string[] orderedSequence)
        {
            int lowestSemitoneIndex = 0;
            for (int i = 1; i < orderedSequence.Length; i++)
            {
                if (FindSemitoneIndex(orderedSequence[i - 1]) > FindSemitoneIndex(orderedSequence[i]))
                {
                    lowestSemitoneIndex = i;
                    break;
                }
            }
            return orderedSequence[lowestSemitoneIndex..].Concat(orderedSequence[..lowestSemitoneIndex]).ToArray();
        }
        public static string GetTrackSongSequence(string phraseSequence, string trackName, int[] meterup, int[] bpm, int[] keyIndices)
        {
            string trackSongSequence = "";
            byte[] phraseIndices = Encoding.ASCII.GetBytes(phraseSequence);
            for (int i = 0; i < phraseSequence.Length; i++)
            {
                phraseIndices[i] -= 65;
                if (i == 0) trackSongSequence += "\\numericTimeSignature \\time " + meterup[phraseIndices[i]] + "/4 \\tempo 4 = "
                        + bpm[phraseIndices[i]] + GetSongKey(keyIndices[phraseIndices[i]]) + " \\" + trackName + phraseSequence[i] + " ";
                else
                {
                    if (meterup[phraseIndices[i]] != meterup[phraseIndices[i - 1]])
                        trackSongSequence += "\\numericTimeSignature \\time " + meterup[phraseIndices[i]] + "/4 ";
                    if (bpm[phraseIndices[i]] != bpm[phraseIndices[i - 1]])
                        trackSongSequence += "\\tempo 4 = " + bpm[phraseIndices[i]] + " ";
                    if (keyIndices[phraseIndices[i]] != keyIndices[phraseIndices[i - 1]])
                        trackSongSequence += GetSongKey(keyIndices[phraseIndices[i]])[1..] + " ";
                    trackSongSequence += "\\" + trackName + phraseSequence[i] + " ";
                }
            }
            return trackSongSequence;
        }
        public static string GetTrackSongSequenceNoKey(string phraseSequence, string trackName, int[] meterup, int[] bpm)
        {
            string trackSongSequence = "";
            byte[] phraseIndices = Encoding.ASCII.GetBytes(phraseSequence);
            for (int i = 0; i < phraseSequence.Length; i++)
            {
                phraseIndices[i] -= 65;
                if (i == 0) trackSongSequence += "\\numericTimeSignature \\time " + meterup[phraseIndices[i]] + "/4 \\tempo 4 = "
                        + bpm[phraseIndices[i]] + " \\" + trackName + phraseSequence[i] + " ";
                else
                {
                    if (meterup[phraseIndices[i]] != meterup[phraseIndices[i - 1]])
                        trackSongSequence += "\\numericTimeSignature \\time " + meterup[phraseIndices[i]] + "/4 ";
                    if (bpm[phraseIndices[i]] != bpm[phraseIndices[i - 1]])
                        trackSongSequence += "\\tempo 4 = " + bpm[phraseIndices[i]] + " ";
                    trackSongSequence += "\\" + trackName + phraseSequence[i] + " ";
                }
            }
            return trackSongSequence;
        }
        public static List<double> GetGaussArray(int count, double invertedSigma, double mi)
        {
            List<double> gaussArray = new();
            //double sigma = 1.0 / invertedSigma;
            double sigma = count / 5.0 / invertedSigma;
            for (int i = 0; i < count; i++)
            {
                double var1 = (i - mi) / sigma;
                // gaussArray.Add(1.0 / (sigma * Math.Sqrt(2.0 * Math.PI)) * Math.Exp(-var1 * var1 / 2));
                gaussArray.Add(Math.Exp(-var1 * var1 / 2));
            }
            return NormalizeProbArray(gaussArray);
        }
        public static List<double> GetTriangleArray(int count, double invertedSigma, double mi)
        {
            List<double> triangleArray = new();
            double sigma = count / 5.0 / invertedSigma;
            double a = 1.0 / 3.0 / sigma;
            int i = 0;
            double limit = Math.Min(mi, count);
            for (; i <= limit; i++)
                triangleArray.Add(Math.Max(a * i + 1 - mi * a, 0.0));
            for (; i < count; i++)
                triangleArray.Add(Math.Max(-a * i + 1 + mi * a, 0.0));
            return NormalizeProbArray(triangleArray);
        }
        public static List<double> GetSineArray(int count, double invertedSigma, double mi)
        {
            List<double> sineArray = new();
            double sigma = count / 5.0 / invertedSigma;
            double a = Math.PI / 6.0 / sigma;
            for (int i = 0; i < count; i++)
                sineArray.Add(Math.Max(Math.Sin(a * i - a * mi + Math.PI / 2), 0));
            return NormalizeProbArray(sineArray);
        }
        public static List<double> NormalizeProbArray(List<int> probArray)
        {
            double probSum = probArray.Sum();
            List<double> normProbArray = new();
            for (int i = 0; i < probArray.Count; i++)
                normProbArray.Add(probArray[i] / probSum);
            return normProbArray;
        }
        public static List<double> NormalizeProbArray(List<double> probArray)
        {
            double probSum = probArray.Sum();
            // List<double> normProbArray = new();
            for (int i = 0; i < probArray.Count; i++)
                probArray[i] /= probSum;
            return probArray;
        }
        public static int WeightedRandom(List<double> probArray, Random rnd)
        {
            double probSum = probArray.Sum();
            System.Diagnostics.Debug.Assert(probSum > 0);
            double rndDbl = rnd.NextDouble();
            double rndValue = rndDbl * probSum;
            for (int i = 0; i < probArray.Count; i++)
                if ((rndValue -= probArray[i]) <= 0)
                    return i;
            return -1;
        }
        public static int GetTwoSideNoteProb(List<double> note1Prob, List<double> note2Prob, Random rnd, int firstNoteIndex, int secondNoteIndex)
        {
            System.Diagnostics.Debug.Assert(note1Prob.Count == note2Prob.Count);
            note1Prob = NormalizeProbArray(note1Prob);
            note2Prob = NormalizeProbArray(note2Prob);
            List<double> probComplete = new();
            for (int i = 0; i < note1Prob.Count; i++)
                probComplete.Add(note1Prob[i] * note2Prob[i]);
            if (probComplete.Sum() > 0) return WeightedRandom(probComplete, rnd);
            else return (int)Math.Round((firstNoteIndex + secondNoteIndex) / 2.0);
        }
        public static void MainValueChangeProb(List<int>[][] valuesStored, System.Windows.Forms.NumericUpDown[] numerics, int currentPhrase, int currentThought, int phrasesCount, int thoughtsCount)
        {
            //valuesStored[CurrentPhrase][LeadVocalsCurrentThought[CurrentPhrase]][index] = newValue;
            valuesStored[currentPhrase][currentThought].Clear();
            for (int i = 0; i < numerics.Length; i++)
                valuesStored[currentPhrase][currentThought].Add((int)numerics[i].Value);
            if (currentPhrase == phrasesCount)
                if (currentThought == thoughtsCount)
                    for (int i = 0; i < phrasesCount; i++)
                        for (int j = 0; j < thoughtsCount; j++)
                        {
                            valuesStored[i][j].Clear();
                            for (int k = 0; k < numerics.Length; k++)
                                valuesStored[i][j].Add((int)numerics[k].Value);
                        }
                else for (int i = 0; i < phrasesCount; i++)
                    {
                        valuesStored[i][currentThought].Clear();
                        for (int k = 0; k < numerics.Length; k++)
                            valuesStored[i][currentThought].Add((int)numerics[k].Value);
                    }
            else if (currentThought == thoughtsCount)
                for (int j = 0; j < thoughtsCount; j++)
                {
                    valuesStored[currentPhrase][j].Clear();
                    for (int k = 0; k < numerics.Length; k++)
                        valuesStored[currentPhrase][j].Add((int)numerics[k].Value);
                }
        }
        public static string GetLilyNote(int noteIndex, int octaveIndex)
        {
            string lilyNote;
            if (ambitusNoteTextbox[noteIndex].Length > 2) lilyNote = ambitusNoteTextbox[noteIndex][..2];
            else lilyNote = ambitusNoteTextbox[noteIndex];
            //return NoteOctaveTranspose(NormalizeNote(lilyNote), octaveIndex - 1);
            return NoteOctaveTranspose(NormalizeNote(lilyNote), octaveIndex - 2);
        }
        public static string[] GetLilyNoteArray(int[] noteIndices, int[] octaveIndices)
        {
            System.Diagnostics.Debug.Assert(noteIndices.Length == octaveIndices.Length);
            int arrayLen = noteIndices.Length;
            string[] lilyNoteArray = new string[arrayLen];
            for (int i = 0; i < arrayLen; i++)
                lilyNoteArray[i] = GetLilyNote(noteIndices[i], octaveIndices[i]);
            return lilyNoteArray;
        }
        //public static ICollection<T> CollectionDeepCopy<T>(ICollection<T> collection)
        //{
        //    dynamic deepCopy = Activator.CreateInstance(collection.GetType());
        //    for (int i = 0; i < collection.Count(); i++)
        //        deepCopy[i] = collection[i];
        //    return deepCopy;
        //}
        //public static List<T> ListDeepCopy<T>(List<T> list)
        //{
        //    dynamic deepCopy = Activator.CreateInstance(list.GetType());
        //    for (int i = 0; i < list.Count; i++)
        //        deepCopy[i] = list[i];
        //    return deepCopy;
        //}
        //public static T[] ArrayDeepCopy<T>(T[] array)
        //{
        //    dynamic deepCopy = Activator.CreateInstance(array.GetType());
        //    for (int i = 0; i < array.Length; i++)
        //        deepCopy[i] = array[i];
        //    return deepCopy;
        //}
        public static decimal[][] GetBreakProbsArray(decimal[] probBar, decimal[] probStrongBeat, decimal[] probWeakBeat)
        {
            System.Diagnostics.Debug.Assert(probBar.Length == probStrongBeat.Length && probBar.Length == probWeakBeat.Length);
            decimal[][] probs = new decimal[probBar.Length][];
            for (int i = 0; i < probs.Length; i++)
            {
                probs[i] = new decimal[3];
                probs[i][0] = probBar[i];
                probs[i][1] = probStrongBeat[i];
                probs[i][2] = probWeakBeat[i];
            }
            return probs;
        }
        public static readonly decimal[] possibleNoteValues =
        {
            16, 8, 4, 2, 1, 0.5M, 0.25M, 0.125M, 0.0625M, 0.03125M
        };
        public static readonly decimal[] possibleNoteValuesIncludingDotted =
        {
            20, 16, 12, 8, 6, 4, 3, 2, 1.5M, 1, 0.75M, 0.5M, 0.375M, 0.25M, 0.1875M, 0.125M, 0.09375M, 0.0625M, 0.03125M
        };
        public static readonly string[] noteNamesxSpaces =
        {
            "x\\longa ", "x\\breve ", "x1 ", "x2 ", "x4 ", "x8 ", "x16 ", "x32 ", "x64 ", "x128 "
        };
        public static readonly string[] noteNamesSpaces =
        {
            "\\longa ", "\\breve ", "1 ", "2 ", "4 ", "8 ", "16 ", "32 ", "64 ", "128 "
        };
        public static readonly string[] noteNamesx =
        {
            "x\\longa", "x\\breve", "x1", "x2", "x4", "x8", "x16", "x32", "x64", "x128"
        };
        public static readonly string[] noteNames =
        {
            "\\longa", "\\breve", "1", "2", "4", "8", "16", "32", "64", "128"
        };
        public static readonly string[] noteNamesIncludingDottedx =
        {
            "x\\longa.", "x\\longa", "x\\breve.", "x\\breve", "x1.", "x1", "x2.", "x2",
            "x4.", "x4", "x8.", "x8", "x16.", "x16", "x32.", "x32", "x64.", "x64", "x128"
        };
        public static readonly string[] noteNamesIncludingDotted =
        {
            "\\longa.", "\\longa", "\\breve.", "\\breve", "1.", "1", "2.", "2",
            "4.", "4", "8.", "8", "16.", "16", "32.", "32", "64.", "64", "128"
        };
        public static readonly string[] ionianChordTypes =
        {
            "major", "minor", "minor", "major", "major", "minor", "diminished"
        };
        public static readonly string[][] stepsSequence = new string[7][]
        {
            new string[]{"ces", "c", "cis"},
            new string[]{"des", "d", "dis"},
            new string[]{"ees", "e", "eis"},
            new string[]{"fes", "f", "fis"},
            new string[]{"ges", "g", "gis"},
            new string[]{"aes", "a", "ais"},
            new string[]{"bes", "b", "bis"},
        };
        readonly public static string[][] semitonesSequence = new string[12][]
        {
            new string[]{"bis", "c", null},
            new string[]{"cis", null, "des"},
            new string[]{null, "d", null},
            new string[]{"dis", null, "ees"},
            new string[]{null, "e", "fes"},
            new string[]{"eis", "f", null},
            new string[]{"fis", null, "ges"},
            new string[]{null, "g", null},
            new string[]{"gis", null, "aes"},
            new string[]{null, "a", null},
            new string[]{"ais", null, "bes"},
            new string[]{null, "b", "ces"},
        };
        readonly public static string[] ambitusNoteTextbox = new string[]
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
        readonly public static decimal[] startRestUnits = new decimal[]
        {
            0.25M,
            0.5M,
            1.0M,
            2.0M
        };
    }
}
