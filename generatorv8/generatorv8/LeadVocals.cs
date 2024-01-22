using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace generatorv8
{
    class LeadVocals
    {
        // public static string[] GetLeadVocals(List<int> strongBeats, int meterup, int barsq, string key, string scale, Chord[] chords, int progressionLengthInBars, int chordLengthInStrongBeats, List<int> stepProb, List<int> intervalProb, List<int> rhythmProb, double[] weights, string lowestNote, string highestNote, decimal[] breakProbs, char phrase, int thoughtLength, double melodyVariation, double rhythmVariation, double[] varSection, double startsOnChord, List<int> startsOnChordComp, double endsOnChord, List<int> endsOnChordComp, List<double> currentChordBias, /* double[] endRestSize, */ string thoughtPattern, bool thoughtsRising, int delayInProgressions, int thoughtDirection, double dirStrength, double risingOverlap, string weightShape, double boomerangTurningPoint, decimal glissChance, decimal lastSyllableChance, decimal lastSyllableStart, decimal endRestLength, decimal startRestChance, decimal maxStartRestSize, decimal restChance, decimal startRestUnit)
        public static string[] GetLeadVocals(List<int> strongBeats, int meterup, int barsq, string key, string scale, Chord[] chords, int progressionLengthInBars, int chordLengthInStrongBeats, List<int>[] stepProb, List<int>[] intervalProb, List<int>[] rhythmProb, int[][] weights, string[] lowestNote, string[] highestNote, decimal[][] breakProbs, char phrase, int[] thoughtLength, double[] melodyVariation, double[] rhythmVariation, double[][] varSection, double[] startsOnChord, List<int>[] startsOnChordComp, double[] endsOnChord, List<int>[] endsOnChordComp, List<double>[] currentChordBias, /* double[][] endRestSize, */ string thoughtPattern, bool[] thoughtsRising, int delayInProgressions, int[] thoughtDirection, double[] dirStrength, double[] risingOverlap, int[] weightShapeIndices, double[] boomerangTurningPoint, decimal[] glissChance, decimal[] lastSyllableChance, decimal[] lastSyllableStart, decimal[] endRestLength, decimal[] startRestChance, decimal[] maxStartRestSize, decimal[] restChance, int[] startRestUnitIndices)
        {
            Random rnd = new();
            // decimal totalLength = barsq * meterup;
            //decimal[] rhythmProbArray = Toolbox.GetProbDecimalArray(possibleValues, rhythmProb);
            // najpierw w ogóle to wyliczenie ile potrzeba osobnych thoughts itp (pamiętać że najpierw jest jeszcze opcjonalny delay in progressions, co ma służyć troche jako taki wstępn instrumentalny, zazwyczaj tak jest w piosenkach)

            int barsToDelete;
            (thoughtPattern, barsToDelete) = GetExpandedThoughtPattern(thoughtPattern, barsq, progressionLengthInBars, delayInProgressions, thoughtLength);

            string distinctThoughts = new(thoughtPattern.Distinct().ToArray());
            int thoughtsCount = distinctThoughts.Length;
            
            int[] totalVariations = new int[thoughtsCount];
            byte[] distinctThoughtsAscii = Encoding.ASCII.GetBytes(distinctThoughts);
            for (int i = 0; i < thoughtsCount; i++)
            {
                int index = distinctThoughtsAscii[i] - 65;
                if (rhythmVariation[index] != 0 || melodyVariation[index] != 0)
                    totalVariations[i] = thoughtPattern.Count(ele => ele == distinctThoughts[i]);
            }
            

            //decimal probBar = breakProbs[0];
            //decimal probStrongBeat = breakProbs[1];
            // decimal probBeat = breakProbs[2];

            // PAUZA STARTOWA

            //decimal startRestChance = 0.5M;
            //decimal maxRestSize = meterup / 2;
            //decimal restUnit = 0.5M;
            //List<decimal> startRestSizeProbArray = GetStartRestSizeProbList(maxStartRestSize, startRestUnit);
            //decimal restChance = 0.1M;

            List<string> variables = new();
            string[] leadVocalsEnd;

            // for dla każdego thought

            for (int i = 0; i < thoughtsCount; i++)
            {
                decimal[] rhythmProbArray = Toolbox.GetProbDecimalArray(possibleValues, rhythmProb[i]);

                //thoughtPattern = GetExpandedThoughtPattern(thoughtPattern, barsq, progressionLengthInBars, delayInProgressions, thoughtLength);

                /*
                int[] totalVariations = new int[thoughtsCount];
                if (rhythmVariation != 0 || melodyVariation != 0)
                    for (int i = 0; i < thoughtsCount; i++)
                        totalVariations[i] = thoughtPattern.Count(ele => ele == thoughtPattern[i]);
                */
                /*
                decimal probBar = breakProbs[i][0];
                decimal probStrongBeat = breakProbs[i][1];
                // decimal probBeat = breakProbs[2];
                */

                // PAUZA STARTOWA

                List<decimal> startRestSizeProbArray = GetStartRestSizeProbList(maxStartRestSize[i], Toolbox.startRestUnits[startRestUnitIndices[i]]);

                // najpierw rytm
                // generalnie krótkie wartości, chyba same ósemki, ósemki z kropką, ćwierćnuty i szczypta pozostałych? jakaś mała szansa na pauzę w każdej nucie?
                // możliwe że kropki i pełne będzie trzeba jakoś sztucznie przemieszać ze sobą czy coś, bo jeśli będą wypadały wartości na pełne beaty cały czas to będzie słaby phrasing
                // nie musi się chyba zaczynać na raz idealnie, można coś popróbować żeby się losowało czy jest pauza ósemkowa - półnutowa czy coś z dość małymi szansami, może dać kontrolke na to?
                // w sumie lepiej żeby się zaczynało troche przed chyba niż troche po - coś jak przedtakt jakiś, tym bardziej jest potrzeba dostępu do poprzednich fraz
                // na koniec thought dłuższa nuta plus pauza
                // można spróbować legato jakieś na koniec - ta sama sylaba ale zmiana wysokości
                // ogólnie może troche legato i glissów powciskać
                // jakaś mała szansa generalnie na pauze przy każdej nucie?

                (List<decimal> rhythmValues, List<bool> rests, int lastSyllableIndex) = GetRhythm(thoughtLength[i] * meterup * progressionLengthInBars, rhythmProbArray, rnd, breakProbs[i], strongBeats, meterup, startRestChance[i], startRestSizeProbArray, restChance[i], lastSyllableChance[i], lastSyllableStart[i], endRestLength[i]);
                List<decimal> rhythmValuesCopy = new(rhythmValues);
                //string[] rhythm = Toolbox.DivideValuesAndGetNotes(rhythmValues, meterup, strongBeats, 0);
                //string[] rhythmDivided = string.Join('z', rhythm).Split('x');
                //string[] rhythmDivided = string.Join('z', rhythm).Split('x')[1..];

                // potem melodia
                // pierwsza nuta - chyba dobrze żeby najbardziej prawdopodobne w 1/3 ambitusu jakoś żeby było miejsce budować do góry?
                // thoughtDirection - może przyjmować wartości downwards (jak zwrotka safe and sound, will to death, ibyd, make a sound), upwards , boomerang up (sunday bloody sunday początek, to end the rapture), boomerang down, neither (los bez żadnego biasu), random (losowa opcja z wszystkich podanych)
                // losowana siła tego kierunku? chyba lepiej do wyboru
                // losowany ostatni dźwięk, żeby wiedzieć do którego dążyć? - generalnie w jakiś sposób bias w stronę środka, ale do tego uwzględnić thoughtDirection
                // uwzględnić też ofc endsOnChord[] (cztery wartości: 0 - szansa na wymuszenie któregoś ze składników akordu, 1 - szansa na pryme, 2 - tercje, 3 - kwinte) i to chyba w pierszej kolejności
                // for dla każdego pozostałego dźwięku
                // losowany kierunek
                // interwały - praktycznie tylko sekundy, troche mniej tercji, potem już minimalne szanse chyba
                // stopnie - pentatonic bias chyba
                // liczone prawdopodobieństwo każdego dźwięku biorąc pod uwagę interwał i stopień - pomnożyć obie szanse jako ułamki? delikatny bias do składników akordu do tego?
                // na podstawie tego prawdopodobieństwa dźwięk
                // sprawdzić czy nie wykracza poza ambitus
                // if przedostatni dźwięk - dodatkowo zmniejszone prawdopodobieństwo na powtózenie ostatniego w thought?
                // List<int> strongBeats, int meterup, int barsq, string key, string scale, Chord[] chords, int progressionLengthInBars, int chordLengthInStrongBeats, List<int> stepProb, List<int> intervalProb, List<int> rhythmProb, string lowestNote, string highestNote, decimal[] breakProbs, char phrase, int thoughtLength, double melodyVariation, double rhythmVariation, double[] varSection, double startsOnChord, int[] startsOnChordComp, double endsOnChord, int[] endsOnChordComp, double[] endRestSize, string thoughtPattern, bool thoughtsRising, int delayInProgressions, int thoughtDirection, decimal glissChance, decimal lastSyllableChance, decimal lastSyllableStart, decimal minEndRest
                // string key, string scale, List<int> stepProb, List<int> intervalProb, List<int> rhythmProb, string lowestNote, string highestNote, double startsOnChord, int[] startsOnChordComp, double endsOnChord, int[] endsOnChordComp, bool thoughtsRising, int thoughtDirection, decimal glissChance

                // OGÓLNE ZMIENNE DOTYCZĄCE MELODII

                string[] notesInScale = Toolbox.GetNotesInScale(scale, Toolbox.NormalizeNote(key));
                string[] cBasedNotesInScale = Toolbox.GetCBasedNoteSequence(notesInScale);
                //string[] octaveSpan = GetOctaveSpan(lowestNote, highestNote);

                List<string> NotesInScaleInOctaves = new();
                for (int j = 0; j < allVoiceOctaves.Length; j++)
                    for (int k = 0; k < cBasedNotesInScale.Length; k++)
                        NotesInScaleInOctaves.Add(cBasedNotesInScale[k] + allVoiceOctaves[j]);
                List<string> allAllowedNotes = Toolbox.RemoveNotesOutOfRange(NotesInScaleInOctaves, lowestNote[i], highestNote[i]);
                if (allAllowedNotes.Count == 0) allAllowedNotes = Toolbox.RemoveNotesOutOfRange(NotesInScaleInOctaves, "f,,", "g''''");
                string[] notesProbArray = Toolbox.GetProbArray(notesInScale, stepProb[i]);
                int firstRootIndex = allAllowedNotes.FindIndex(ele => ele.StartsWith(notesInScale[0]));
                int thoughtDirectionCount = 5;
                if (thoughtDirection[i] >= thoughtDirectionCount) thoughtDirection[i] = rnd.Next(thoughtDirectionCount);
                List<int> thoughtDirectionDownStart = new() { 0, 2 };
                List<int> thoughtDirectionUpStart = new() { 1, 3 };
                List<int> thoughtDirectionGenericStart = new() { 4 };
                List<int> thoughtDirectionDownEnd = new() { 1, 2 };
                List<int> thoughtDirectionUpEnd = new() { 0, 3 };
                List<int> thoughtDirectionGenericEnd = new() { 4 };
                // genericDirectionEdgeMultiplier chyba jest od tego żeby jak jest opcja direction "neither", która nie narzuca określonego kierunku frazie,
                // to został poszerzony dzwon prawdopodobieństwa od pitch, bo wtedy pitch cały czas jako wartość oczekiwaną ma środek. w ten sposób jest delikatny
                // bias ku środkowi cały czas ale tylko delikatny.
                //double genericDirectionEdgeMultiplier = 3;
                double genericDirectionEdgeMultiplier = 0.3;
                List<int> nonRestIndices = new();
                List<int> restIndices = new();
                //for (int j = 0; j < rhythmDivided.Length - 1; j++)
                for (int j = 0; j < rhythmValuesCopy.Count; j++)
                    if (rests[j] == false) nonRestIndices.Add(j);
                    //else rhythmDivided[j] += "r";
                    else restIndices.Add(j);
                if (lastSyllableIndex == -1) lastSyllableIndex = nonRestIndices[^1];


                // PIERWSZA I OSTATNIA NUTA

                int[] noteIndices = new int[nonRestIndices.Count];

                // string firstNote;
                int firstNonRestIndex = rests.IndexOf(false);
                string[] firstNoteChordNotes = Toolbox.GetCurrentChordNotes(chords, strongBeats, meterup, rhythmValuesCopy.GetRange(0, firstNonRestIndex).Sum(), chordLengthInStrongBeats, progressionLengthInBars, notesInScale);
                string[] lastNoteChordNotes = Toolbox.GetCurrentChordNotes(chords, strongBeats, meterup, rhythmValuesCopy.GetRange(0, lastSyllableIndex).Sum(), chordLengthInStrongBeats, progressionLengthInBars, notesInScale);
                //List<double> stepProbEdge = Toolbox.NormalizeProbArray(ExpandStepProbArray(stepProb[i], allAllowedNotes, firstRootIndex));
                List<double> firstNoteStepProb = Toolbox.NormalizeProbArray(ExpandStepProbArray(ApplyCurrentChordBias(stepProb[i], notesInScale, firstNoteChordNotes, currentChordBias[i]), allAllowedNotes, firstRootIndex));
                List<double> lastNoteStepProb = Toolbox.NormalizeProbArray(ExpandStepProbArray(ApplyCurrentChordBias(stepProb[i], notesInScale, lastNoteChordNotes, currentChordBias[i]), allAllowedNotes, firstRootIndex));
                (List<double> firstNoteChordProb, bool forcedFirstNote) = GetForceChordNoteProbArray(allAllowedNotes, startsOnChord[i], startsOnChordComp[i], firstNoteChordNotes, rnd);
                (List<double> lastNoteChordProb, bool forcedLastNote) = GetForceChordNoteProbArray(allAllowedNotes, endsOnChord[i], endsOnChordComp[i], lastNoteChordNotes, rnd);
                bool rollLastNote = !(thoughtDirection[i] == 4 && !forcedLastNote);
                // double[] edgeNoteWeights = { weights[i][0], weights[i][1], 1 };
                int[] edgeNoteWeights = { weights[i][0], weights[i][1], 1 };

                List<double> firstNotePitchProb;
                if (thoughtDirectionDownStart.Any(ele => ele == thoughtDirection[i])) firstNotePitchProb = GetPitchProbArrayShape(weightShapeIndices[i], allAllowedNotes.Count, dirStrength[i], 0);
                else if (thoughtDirectionUpStart.Any(ele => ele == thoughtDirection[i])) firstNotePitchProb = GetPitchProbArrayShape(weightShapeIndices[i], allAllowedNotes.Count, dirStrength[i], allAllowedNotes.Count - 1);
                else firstNotePitchProb = GetPitchProbArrayShape(weightShapeIndices[i], allAllowedNotes.Count, dirStrength[i] * genericDirectionEdgeMultiplier, (allAllowedNotes.Count - 1) / 2.0);
                List<double> firstNoteProb = GetEachNoteProbValues(firstNotePitchProb, firstNoteStepProb, firstNoteChordProb, edgeNoteWeights);
                int firstNoteIndex = Toolbox.WeightedRandom(firstNoteProb, rnd);

                List<double> lastNotePitchProb, lastNoteProb;
                int lastNoteIndex = -1;
                if (rollLastNote)
                {
                    if (thoughtDirectionDownEnd.Any(ele => ele == thoughtDirection[i])) lastNotePitchProb = GetPitchProbArrayShape(weightShapeIndices[i], allAllowedNotes.Count, dirStrength[i], 0);
                    else if (thoughtDirectionUpEnd.Any(ele => ele == thoughtDirection[i])) lastNotePitchProb = GetPitchProbArrayShape(weightShapeIndices[i], allAllowedNotes.Count, dirStrength[i], allAllowedNotes.Count - 1);
                    else lastNotePitchProb = GetPitchProbArrayShape(weightShapeIndices[i], allAllowedNotes.Count, dirStrength[i] * genericDirectionEdgeMultiplier, (allAllowedNotes.Count - 1) / 2.0);
                    lastNoteProb = GetEachNoteProbValues(lastNotePitchProb, lastNoteStepProb, lastNoteChordProb, edgeNoteWeights);
                    lastNoteIndex = Toolbox.WeightedRandom(lastNoteProb, rnd);
                }



                // else firstNote = notesProbArray[rnd.Next(notesProbArray.Length)];
                // List<string> tmpNotes = allAllowedNotes.FindAll(ele => ele.StartsWith(firstNote));
                // firstNote = tmpNotes[rnd.Next(tmpNotes.Count)];
                //if (nonRestIndices.Count > 0) rhythmDivided[nonRestIndices[0]] += allAllowedNotes[firstNoteIndex];
                if (nonRestIndices.Count > 0)
                {
                    noteIndices[0] = firstNoteIndex;
                    noteIndices[^1] = lastNoteIndex;
                }
                int currentNoteIndex = firstNoteIndex;

                //List<decimal> startNoteProbArray;
                //switch(thoughtDirection)
                //{
                //    case 0: startNoteProbArray = Toolbox.GetProbDecimalArray()
                //}

                //List<int> stepProbCopy = new();
                //for (int j = 0; j < stepProb.Count; j++)
                //    stepProbCopy.Add(stepProb[j]);

                for (int j = 1; j < nonRestIndices.Count; j++)
                {
                    string[] currentChordNotes = Toolbox.GetCurrentChordNotes(chords, strongBeats, meterup, rhythmValuesCopy.GetRange(0, nonRestIndices[j]).Sum(), chordLengthInStrongBeats, progressionLengthInBars, notesInScale);
                    List<double> stepProbCurrent = ApplyCurrentChordBias(stepProb[i], notesInScale, currentChordNotes, currentChordBias[i]);
                    List<double> stepProbNorm = Toolbox.NormalizeProbArray(ExpandStepProbArray(stepProbCurrent, allAllowedNotes, firstRootIndex));
                    List<double> intervalProbNorm = Toolbox.NormalizeProbArray(ExpandIntervalProbArray(intervalProb[i], allAllowedNotes, currentNoteIndex));

                    double lastNotePositionInQuarterNotes = (double)rhythmValuesCopy.GetRange(0, nonRestIndices[^1]).Sum();
                    List<double> pitchProb = GetPitchProbArray(allAllowedNotes.Count, firstNoteIndex, lastNoteIndex, thoughtDirection[i], dirStrength[i], (double)rhythmValuesCopy.GetRange(0, nonRestIndices[j]).Sum() / lastNotePositionInQuarterNotes, weightShapeIndices[i], thoughtsRising[i], thoughtsCount, i, risingOverlap[i], boomerangTurningPoint[i], genericDirectionEdgeMultiplier);
                    // int noteIndex = Toolbox.WeightedRandom(GetEachNoteProbValues(pitchProb, stepProbNorm, intervalProbNorm, weights), rnd);
                    // int noteIndex;
                    // currentNoteIndex = noteIndex;
                    // currentNoteIndex = Toolbox.WeightedRandom(GetEachNoteProbValues(pitchProb, stepProbNorm, intervalProbNorm, weights), rnd);
                    List<double> note1Prob = GetEachNoteProbValues(pitchProb, stepProbNorm, intervalProbNorm, weights[i]);

                    // if (j == nonRestIndices.Count - 2 && forcedLastNote && !thoughtDirectionGenericEnd.Contains(thoughtDirection))
                    //if (j == nonRestIndices.Count - 2 && forcedLastNote)
                    if (j == nonRestIndices.Count - 2 && rollLastNote)
                    {
                        lastNoteChordNotes = Toolbox.GetCurrentChordNotes(chords, strongBeats, meterup, rhythmValuesCopy.GetRange(0, nonRestIndices[j]).Sum(), chordLengthInStrongBeats, progressionLengthInBars, notesInScale);
                        List<double> stepProbLast = ApplyCurrentChordBias(stepProb[i], notesInScale, lastNoteChordNotes, currentChordBias[i]);
                        stepProbLast = Toolbox.NormalizeProbArray(ExpandStepProbArray(stepProbLast, allAllowedNotes, firstRootIndex));
                        List<double> intervalProbLast = Toolbox.NormalizeProbArray(ExpandIntervalProbArray(intervalProb[i], allAllowedNotes, lastNoteIndex));
                        // List<double> pitchProbLast = GetPitchProbArray(allAllowedNotes.Count, firstNoteIndex, lastNoteIndex, thoughtDirection, dirStrength, (double)rhythmValues.GetRange(0, j).Sum(), weightShape, thoughtsRising, thoughtsCount, i, risingOverlap, boomerangTurningPoint);
                        //List<double> pitchProbLast = pitchProb;
                        List<double> pitchProbLast = GetPitchProbArray(allAllowedNotes.Count, firstNoteIndex, lastNoteIndex, thoughtDirection[i], dirStrength[i], (double)rhythmValuesCopy.GetRange(0, nonRestIndices[^1]).Sum() / lastNotePositionInQuarterNotes, weightShapeIndices[i], thoughtsRising[i], thoughtsCount, i, risingOverlap[i], boomerangTurningPoint[i], genericDirectionEdgeMultiplier);
                        //int currentNoteIndex2 = Toolbox.WeightedRandom(GetEachNoteProbValues(pitchProb, stepProbNorm, intervalProbNorm, weights), rnd);
                        List<double> note2Prob = GetEachNoteProbValues(pitchProbLast, stepProbLast, intervalProbLast, weights[i]);
                        currentNoteIndex = Toolbox.GetTwoSideNoteProb(note1Prob, note2Prob, rnd, currentNoteIndex, lastNoteIndex);
                        //rhythmDivided[nonRestIndices[j]] += allAllowedNotes[currentNoteIndex];
                        noteIndices[j] = currentNoteIndex;

                        //if (forcedLastNote) break;
                        //else continue;
                        break;
                    }
                    else
                    {
                        currentNoteIndex = Toolbox.WeightedRandom(note1Prob, rnd);
                        //rhythmDivided[nonRestIndices[j]] += allAllowedNotes[currentNoteIndex];
                        noteIndices[j] = currentNoteIndex;
                    }
                }
                string[][] variationsMusic = new string[totalVariations[i]][];
                //variationsMusic[0] = GetMusic(rhythm, nonRestIndices, restIndices, allAllowedNotes, noteIndices);
                //if (glissChance[i] <= 0) variationsMusic[0] = GetMusic(rhythmDivided, nonRestIndices, restIndices, allAllowedNotes, noteIndices);
                //else variationsMusic[0] = GetMusic(GlissThought(rhythmDivided, nonRestIndices, restIndices, allAllowedNotes, noteIndices));
                int glissMinInterval = 3; // różnica w stopniach chyba, 0 - pryma, 1 - sekunda itd
                double glissNoteLengthInQuarterNotes = 0.25; // 0.25 - szesnastka, 0.5 - ósemka itd
                int[] glissMaxNoteCount = new int[8] { 0, 0, 1, 1, 1, 2, 2, 3 }; // maksymalna liczba nut w glissie dla poszczególnych interwałów od prymy do oktawy
                (List<decimal> rhythmValuesGliss, List<int> nonRestIndicesGliss, List<int> restIndicesGliss, List<string> allAllowedNotesGliss, int[] noteIndicesGliss) = GlissThought(rhythmValuesCopy, rests, allAllowedNotes, noteIndices, glissChance[i], glissMinInterval, glissNoteLengthInQuarterNotes, glissMaxNoteCount, rnd, chords, strongBeats, meterup, chordLengthInStrongBeats, progressionLengthInBars, notesInScale, stepProb[i], currentChordBias[i], firstRootIndex, intervalProb[i], dirStrength[i], weights[i], weightShapeIndices[i]);
                string[] rhythm = Toolbox.DivideValuesAndGetNotes(rhythmValuesGliss, meterup, strongBeats, 0);
                string[] rhythmDividedGliss = string.Join('z', rhythm).Split('x');
                variationsMusic[0] = GetMusic(rhythmDividedGliss, nonRestIndicesGliss, restIndicesGliss, allAllowedNotesGliss, noteIndicesGliss);
                // !!!!!!!!!!! WARIACJE DO ZROBIENIA
                for (int j = 0; j < totalVariations[i] - 1; j++)
                {
                    // potem wariacje?
                    // jeśli 0 wariacji w rhythmVariation i melodyVariation to skip
                    // wyliczenie ile różnych wariacji dla frazy
                    // wyliczenie sekcji która ma być wariowana
                    // for dla każdej wariacji po kolei
                    // wariacja rytmu - rhythmVariation chyba od 0 do 1, 0 to brak wariacji, 1 to gwarantowane przelosowanie (może nastąpić zmiana liczby nut) wartości rytmicznych wybranego fragmentu?
                    // wariacja melodii - podobnie, 1 to losowanie od nowa każdej wysokości w fragmencie
                    // jeśli melodyVariation == 0, a zmieniła się ilość wartości rytmicznych to trzeba jakoś wybrać te które zostały dodane i wylosować im nowe wysokości, dobrze by było jakoś mało inwazyjnie dla całej linii melodycznej

                    int varStartInd = -1, varEndInd = rhythmValuesCopy.Count - 1, varStreakStart, rhythmIndexBalance = 0, startIndTmp, varStreakCount = 0, varStreakEnd = 0;
                    decimal varStreakLength = 0;
                    int endRestFactor = endRestLength[i] > 0 ? 1 : 0;
                    List<decimal> rhythmVarValues = new();
                    List<bool> rhythmVarRests = new();
                    int varLastSyllableIndex = lastSyllableIndex;
                    List<int> varMelodyIndices = new();
                    List<int> varMelodyIndicesNonRests = new();
                    List<int> newNotesIndices = new();
                    List<int> newNotesIndicesNonRests = new();
                    List<int> erasedNotesIndices = new();
                    List<int> erasedNotesIndicesNonRests = new();
                    List<int> residualNotesIndices = new();
                    //List<int> noteIndicesAfterRhythm = new();
                    int[] noteIndicesAfterRhythm;
                    List<int> nonRestIndicesVar = new();
                    List<int> restIndicesVar = new();
                    //decimal rhythmValuesCopySum = rhythmValuesCopy.Sum();
                    decimal rhythmValuesCopySum = rhythmValuesCopy.GetRange(0, rhythmValuesCopy.Count - endRestFactor).Sum();
                    double varStart = varSection[i][0] * (double)rhythmValuesCopySum;
                    double varEnd = varSection[i][1] * (double)rhythmValuesCopySum;
                    for (int k = 1; k < rhythmValuesCopy.Count + 1; k++)
                        //if ((double)rhythmValuesCopy.GetRange(0, k).Sum() > varSection[i][0])
                        if ((double)rhythmValuesCopy.GetRange(0, k).Sum() > varStart)
                        {
                            varStartInd = k - 1;
                            break;
                        }
                    for (int k = varStartInd + 1; k < rhythmValuesCopy.Count + 1; k++)
                        //if ((double)rhythmValuesCopy.GetRange(0, k).Sum() > varSection[i][1])
                        if ((double)rhythmValuesCopy.GetRange(0, k).Sum() > varEnd)
                        {
                            varEndInd = k - 1;
                            break;
                        }
                    varEndInd = Math.Min(varEndInd, lastSyllableIndex);
                    if (rhythmVariation[i] != 0)
                    {
                        //varStreakStart = varEndInd;
                        varStreakStart = varStartInd;
                        for (int k = 0; k < rhythmValuesCopy.Count; k++)
                        {
                            rhythmVarValues.Add(rhythmValuesCopy[k]);
                            rhythmVarRests.Add(rests[k]);
                        }
                        List<decimal> rhythmValuesTmp;
                        int lastSyllableIndexTmp;
                        List<bool> restsValuesTmp;
                        //int nonRestCounter = 0;
                        //for (int k = varStartInd; k < varEndInd - endRestFactor; k++)
                        for (int k = varStartInd; k <= varEndInd; k++)
                        //for (int k = varEndInd; k >= varStartInd; k--)
                        {
                            //if (rnd.NextDouble() < rhythmVariation[i] && k != varStartInd)
                            double rndDbl = rnd.NextDouble();
                            if (rndDbl < rhythmVariation[i])
                            {
                                if (varStreakCount == 0) varStreakStart = k;
                                varStreakLength += rhythmValuesCopy[k];
                                //varStreakStart = k;
                                varStreakCount++;
                                varStreakEnd = k;
                            }
                            if (rndDbl >= rhythmVariation[i] || k == varEndInd)
                            {
                                if (varStreakLength > 0)
                                {
                                    if (varStreakEnd == varEndInd)
                                    {
                                        varStreakCount = rhythmValuesCopy.Count - varStreakStart;
                                        varStreakEnd = rhythmValuesCopy.Count - 1;
                                    }
                                    startIndTmp = varStreakStart + rhythmIndexBalance;
                                    //indCountTmp = varEndInd - varStartInd + 1 + rhythmIndexBalance;
                                    //indCountTmp = varEndInd - varStartInd + 1;
                                    //(rhythmValuesTmp, restsValuesTmp, lastSyllableIndexTmp) = GetRhythmVariation(rhythmProbArray, rnd, breakProbs[i], strongBeats, meterup, startRestChance[i], startRestSizeProbArray, restChance[i], lastSyllableChance[i], lastSyllableStart[i], endRestLength[i], rhythmValuesCopy, varStartInd, varEndInd, lastSyllableIndex, endRestFactor);
                                    (rhythmValuesTmp, restsValuesTmp, lastSyllableIndexTmp) = GetRhythmVariation(rhythmProbArray, rnd, breakProbs[i], strongBeats, meterup, startRestChance[i], startRestSizeProbArray, restChance[i], lastSyllableChance[i], lastSyllableStart[i], endRestLength[i], rhythmValuesCopy, varStreakStart, varStreakEnd, varLastSyllableIndex, endRestFactor);
                                    //rhythmVarValues.RemoveRange(startIndTmp, indCountTmp);
                                    //rhythmVarRests.RemoveRange(startIndTmp, indCountTmp);
                                    rhythmVarValues.RemoveRange(startIndTmp, varStreakCount);
                                    rhythmVarRests.RemoveRange(startIndTmp, varStreakCount);
                                    for (int l = varStreakStart; l < varStreakEnd + 1; l++)
                                        //erasedNotesIndices.Add(l);
                                        //if (!restIndices.Contains(l)) erasedNotesIndices.Add(l);
                                        if (!rests[l]) erasedNotesIndices.Add(l);
                                    //rhythmVarValues.AddRange(rhythmValuesTmp, varStartInd)
                                    rhythmVarValues.InsertRange(startIndTmp, rhythmValuesTmp);
                                    rhythmVarRests.InsertRange(startIndTmp, restsValuesTmp);
                                    for (int l = startIndTmp; l < startIndTmp + rhythmValuesTmp.Count; l++)
                                        //newNotesIndices.Add(l);
                                        if (!rhythmVarRests[l]) newNotesIndices.Add(l);
                                    //for (int l = rhythmValuesTmp.Count - 1; l >= 0; l--)
                                    //{
                                    //    rhythmVarValues.Insert(varStartInd, rhythmValuesTmp[l]);
                                    //    rhythmVarRests.Insert(varStartInd, restsValuesTmp[l]);
                                    //}
                                    if (lastSyllableIndexTmp != -1) varLastSyllableIndex = lastSyllableIndexTmp + rhythmVarValues.Count - rhythmValuesTmp.Count;
                                    //varStreakLength = 0;
                                    rhythmIndexBalance -= varStreakEnd - varStreakStart + 1;
                                    rhythmIndexBalance += rhythmValuesTmp.Count;
                                    varStreakLength = 0;
                                }
                                //varStreakStart = k;
                                varStreakCount = 0;
                            }
                        }
                        for (int k = 0; k < rhythmVarRests.Count; k++)
                        {
                            if (rhythmVarRests[k]) restIndicesVar.Add(k);
                            else nonRestIndicesVar.Add(k);
                            //if (!newNotesIndices.Contains(k) && !rhythmVarRests[k]) residualNotesIndices.Add(k);
                        }
                        noteIndicesAfterRhythm = new int[nonRestIndicesVar.Count];
                        //for (int k = 0; k < noteIndices.Length; k++)
                        //    if (!erasedNotesIndices.Contains(k)) noteIndicesAfterRhythm.Add(noteIndices[k]);
                        //if (erasedNotesIndices.Count == newNotesIndices.Count) noteIndicesAfterRhythm = new(noteIndices);
                        int residualCounter = 0;
                        if (erasedNotesIndices.Count > 0) erasedNotesIndicesNonRests = GetNonRestNotesIndices(erasedNotesIndices, rests);
                        if (newNotesIndices.Count > 0) newNotesIndicesNonRests = GetNonRestNotesIndices(newNotesIndices, rhythmVarRests);
                        for (int k = 0; k < nonRestIndicesVar.Count; k++)
                            if (!newNotesIndicesNonRests.Contains(k)) residualNotesIndices.Add(k);
                        for (int k = 0; k < noteIndices.Length; k++)
                            //if (!erasedNotesIndices.Contains(k))
                            if (!erasedNotesIndicesNonRests.Contains(k))
                            {
                                noteIndicesAfterRhythm[residualNotesIndices[residualCounter]] = noteIndices[k];
                                residualCounter++;
                            }
                        if (erasedNotesIndices.Count == newNotesIndices.Count)
                            for (int k = 0; k < erasedNotesIndicesNonRests.Count; k++)
                                noteIndicesAfterRhythm[newNotesIndicesNonRests[k]] = noteIndices[erasedNotesIndicesNonRests[k]];
                        else if (erasedNotesIndices.Count > newNotesIndices.Count)
                        {
                            for (int k = 0; k < newNotesIndices.Count; k++)
                            {
                                int newIndex = FindClosestNoteIndex(rhythmValuesCopy, rhythmVarValues, newNotesIndices[k], erasedNotesIndices);
                                //noteIndicesAfterRhythm[newNotesIndices[k]] = noteIndices[erasedNotesIndices[newIndex]];
                                //erasedNotesIndices.RemoveAt(k);
                                noteIndicesAfterRhythm[newNotesIndicesNonRests[k]] = noteIndices[erasedNotesIndicesNonRests[newIndex]];
                                erasedNotesIndices.RemoveAt(newIndex);
                                erasedNotesIndicesNonRests.RemoveAt(newIndex);
                            }
                            //newNotesIndices.Clear();
                        }
                        else
                        {
                            for (int k = 0; k < erasedNotesIndices.Count; k++)
                            {
                                int newIndex = FindClosestNoteIndex(rhythmVarValues, rhythmValuesCopy, erasedNotesIndices[k], newNotesIndices);
                                //noteIndicesAfterRhythm[newNotesIndices[newIndex]] = noteIndices[erasedNotesIndices[k]];
                                //newNotesIndices.RemoveAt(k);
                                noteIndicesAfterRhythm[newNotesIndicesNonRests[newIndex]] = noteIndices[erasedNotesIndicesNonRests[k]];
                                newNotesIndices.RemoveAt(newIndex);
                                newNotesIndicesNonRests.RemoveAt(newIndex);
                            }
                            //for (int k = 0; k < newNotesIndices.Count; k++) noteIndicesAfterRhythm[newNotesIndices[k]] =
                            //RollMelodyVarNote(noteIndicesAfterRhythm, newNotesIndices, newNotesIndices[k], nonRestIndicesVar, chords, strongBeats, meterup, chordLengthInStrongBeats, progressionLengthInBars, notesInScale, rhythmVarValues, stepProb[i], currentChordBias[i], allAllowedNotes, firstRootIndex, intervalProb[i], thoughtDirection[i], dirStrength[i], weightShapeIndices[i], thoughtsRising[i], thoughtsCount, i, risingOverlap[i], boomerangTurningPoint[i], genericDirectionEdgeMultiplier, weights[i], rnd);
                            for (int k = 0; k < newNotesIndicesNonRests.Count; k++) noteIndicesAfterRhythm[newNotesIndicesNonRests[k]] =
                            RollMelodyVarNote(noteIndicesAfterRhythm, newNotesIndicesNonRests, newNotesIndicesNonRests[k], newNotesIndices[k], nonRestIndicesVar, chords, strongBeats, meterup, chordLengthInStrongBeats, progressionLengthInBars, notesInScale, rhythmVarValues, stepProb[i], currentChordBias[i], allAllowedNotes, firstRootIndex, intervalProb[i], thoughtDirection[i], dirStrength[i], weightShapeIndices[i], thoughtsRising[i], thoughtsCount, i, risingOverlap[i], boomerangTurningPoint[i], genericDirectionEdgeMultiplier, weights[i], rnd);
                        }
                    }
                    else
                    {
                        noteIndicesAfterRhythm = noteIndices;
                        nonRestIndicesVar = nonRestIndices;
                        restIndicesVar = restIndices;
                        rhythmVarValues = rhythmValuesCopy;
                        //rhythmVarValues = new(rhythmValuesCopy);
                        rhythmVarRests = rests;
                    }

                    //int[] noteIndicesAfterRhythm = new int[rhythmVarValues.Count];
                    //string[] rhythmVar = Toolbox.DivideValuesAndGetNotes(rhythmVarValues, meterup, strongBeats, 0);
                    //string[] rhythmDividedVar = string.Join('z', rhythmVar).Split('x');
                    //for (int k = 0; k < rhythmDividedVar.Length - 1; k++)
                    //    if (rests[k] == false) nonRestIndices.Add(k);
                    //    else restIndices.Add(k);
                    //int[] noteIndicesVar = new int[rhythmVarValues.Count];
                    int[] noteIndicesVar = new int[nonRestIndicesVar.Count];
                    for (int k = 0; k < noteIndicesAfterRhythm.Length; k++)
                        noteIndicesVar[k] = noteIndicesAfterRhythm[k];

                    if (melodyVariation[i] != 0)
                    {
                        //if (rhythmVariation[i] == 0)
                        //{
                        //for (int k = 0; k < noteIndices.Length; k++)
                        //    if (!rhythmVarRests[k] && rnd.NextDouble() < melodyVariation[i])
                        //        varMelodyIndices.Add(k);
                        //for (int k = 0; k < varMelodyIndices.Count; k++)
                        //{
                        //    noteIndicesVar[k] = RollMelodyVarNote(noteIndices, varMelodyIndices, varMelodyIndices[k], nonRestIndicesVar, chords, strongBeats, meterup, chordLengthInStrongBeats, progressionLengthInBars, notesInScale, rhythmVarValues, stepProb[i], currentChordBias[i], allAllowedNotes, firstRootIndex, intervalProb[i], thoughtDirection[i], dirStrength[i], weightShapeIndices[i], thoughtsRising[i], thoughtsCount, i, risingOverlap[i], boomerangTurningPoint[i], genericDirectionEdgeMultiplier, weights[i], rnd);
                        //}
                        //}
                        //else
                        //{
                        //    for (int k = 0; k < newNotesIndices.Count; k++)
                        //    {

                        //    }
                        //}
                        //for (int k = 0; k < noteIndicesAfterRhythm.Length; k++)
                        rhythmValuesCopySum = rhythmVarValues.GetRange(0, rhythmVarValues.Count - endRestFactor).Sum();
                        varStart = varSection[i][0] * (double)rhythmValuesCopySum;
                        varEnd = varSection[i][1] * (double)rhythmValuesCopySum;
                        for (int k = 1; k < rhythmVarValues.Count + 1; k++)
                            if ((double)rhythmVarValues.GetRange(0, k).Sum() > varStart)
                            {
                                varStartInd = k - 1;
                                break;
                            }
                        for (int k = varStartInd + 1; k < rhythmVarValues.Count + 1; k++)
                            if ((double)rhythmVarValues.GetRange(0, k).Sum() > varEnd)
                            {
                                varEndInd = k - 1;
                                break;
                            }
                        for (int k = varStartInd; k <= varEndInd; k++)
                            //if (!rhythmVarRests[k] && rnd.NextDouble() < melodyVariation[i])
                            if (rnd.NextDouble() < melodyVariation[i])
                                varMelodyIndices.Add(k);
                        if (varMelodyIndices.Count > 0) varMelodyIndicesNonRests = GetNonRestNotesIndices(varMelodyIndices, rhythmVarRests);
                        //for (int k = 0; k < varMelodyIndices.Count; k++)
                        for (int k = 0; k < varMelodyIndicesNonRests.Count; k++)
                        {
                            noteIndicesVar[varMelodyIndicesNonRests[k]] = RollMelodyVarNote(noteIndicesVar, varMelodyIndicesNonRests, varMelodyIndicesNonRests[k], varMelodyIndices[k], nonRestIndicesVar, chords, strongBeats, meterup, chordLengthInStrongBeats, progressionLengthInBars, notesInScale, rhythmVarValues, stepProb[i], currentChordBias[i], allAllowedNotes, firstRootIndex, intervalProb[i], thoughtDirection[i], dirStrength[i], weightShapeIndices[i], thoughtsRising[i], thoughtsCount, i, risingOverlap[i], boomerangTurningPoint[i], genericDirectionEdgeMultiplier, weights[i], rnd);
                            //noteIndicesAfterRhythm[varMelodyIndicesNonRests[k]] = RollMelodyVarNote(noteIndicesAfterRhythm, varMelodyIndices, varMelodyIndicesNonRests[k], varMelodyIndices[k], nonRestIndicesVar, chords, strongBeats, meterup, chordLengthInStrongBeats, progressionLengthInBars, notesInScale, rhythmVarValues, stepProb[i], currentChordBias[i], allAllowedNotes, firstRootIndex, intervalProb[i], thoughtDirection[i], dirStrength[i], weightShapeIndices[i], thoughtsRising[i], thoughtsCount, i, risingOverlap[i], boomerangTurningPoint[i], genericDirectionEdgeMultiplier, weights[i], rnd);
                        }
                    }
                    //variationsMusic[j + 1] = GetMusic(rhythmDividedVar, nonRestIndicesVar, restIndicesVar, allAllowedNotes, noteIndicesVar);
                    //glissy
                    (List<decimal> rhythmValuesGlissVar, List<int> nonRestIndicesGlissVar, List<int> restIndicesGlissVar, List<string> allAllowedNotesGlissVar, int[] noteIndicesGlissVar) = GlissThought(rhythmVarValues, rhythmVarRests, allAllowedNotes, noteIndicesVar, glissChance[i], glissMinInterval, glissNoteLengthInQuarterNotes, glissMaxNoteCount, rnd, chords, strongBeats, meterup, chordLengthInStrongBeats, progressionLengthInBars, notesInScale, stepProb[i], currentChordBias[i], firstRootIndex, intervalProb[i], dirStrength[i], weights[i], weightShapeIndices[i]);
                    string[] rhythmVar = Toolbox.DivideValuesAndGetNotes(rhythmValuesGlissVar, meterup, strongBeats, 0);
                    string[] rhythmDividedGlissVar = string.Join('z', rhythmVar).Split('x');
                    //string[] rhythm = Toolbox.DivideValuesAndGetNotes(rhythmValuesGliss, meterup, strongBeats, 0);
                    //string[] rhythmDividedGliss = string.Join('z', rhythm).Split('x');
                    variationsMusic[j + 1] = GetMusic(rhythmDividedGlissVar, nonRestIndicesGlissVar, restIndicesGlissVar, allAllowedNotesGlissVar, noteIndicesGlissVar);
                }
                
                //(List<decimal> rhythmValuesGliss, List<int> nonRestIndicesGliss, List<int> restIndicesGliss, List<string> allAllowedNotesGliss, int[] noteIndicesGliss) = GlissThought(rhythmValuesCopy, rests, allAllowedNotes, noteIndices, glissChance[i], glissMinInterval, glissNoteLengthInQuarterNotes, glissMaxNoteCount, rnd, chords, strongBeats, meterup, chordLengthInStrongBeats, progressionLengthInBars, notesInScale, stepProb[i], currentChordBias[i], firstRootIndex, intervalProb[i], dirStrength[i], weights[i]);
                //string[] rhythm = Toolbox.DivideValuesAndGetNotes(rhythmValuesGliss, meterup, strongBeats, 0);
                //string[] rhythmDividedGliss = string.Join('z', rhythm).Split('x');
                //variationsMusic[0] = GetMusic(rhythmDividedGliss, nonRestIndicesGliss, restIndicesGliss, allAllowedNotesGliss, noteIndicesGliss);

                // późniejsze thoughts budowane coraz wyżej?

                string[] leadVocals;
                if (totalVariations[i] <= 1)
                {
                    //leadVocals = string.Join("", rhythmDividedGliss).Split('z');
                    leadVocals = string.Join("", variationsMusic[0]).Split('z');
                    //variables.Add("leadVocals" + phrase + thoughtPattern[i] + " = {");
                    variables.Add("leadVocals" + phrase + distinctThoughts[i] + " = {");
                    for (int j = 0; j < leadVocals.Length; j++)
                        variables.Add(leadVocals[j]);
                    variables.Add("}");

                    if (thoughtPattern[^1] == distinctThoughts[i] && barsToDelete > 0)
                    {
                        leadVocalsEnd = leadVocals[0..(leadVocals.Length - barsToDelete)];
                        variables.Add("leadVocalsEnd = {");
                        for (int j = 0; j < leadVocals.Length; j++)
                            variables.Add(leadVocals[j]);
                        variables.Add("}");
                    }
                }
                else
                {
                    for (int j = 0; j < totalVariations[i]; j++)
                    {
                        leadVocals = string.Join("", variationsMusic[j]).Split('z');
                        if (barsToDelete > 0 && j == totalVariations[i] - 1 && thoughtPattern[^1] == distinctThoughts[i])
                        {
                            leadVocalsEnd = leadVocals[0..(leadVocals.Length - barsToDelete)];
                            variables.Add("leadVocalsEnd = {");
                            for (int k = 0; k < leadVocals.Length; k++)
                                variables.Add(leadVocals[k]);
                            variables.Add("}");
                        }
                        else
                        {
                            variables.Add("leadVocals" + phrase + distinctThoughts[i] + alphabet[j] + " = {");
                            for (int k = 0; k < leadVocals.Length; k++)
                                variables.Add(leadVocals[k]);
                            variables.Add("}");
                        }
                    }
                }

                //string[] variablestmp = new string[leadVocals.Length + 2];
                //Array.Copy(leadVocals, 0, variablestmp, 1, leadVocals.Length);
                ////variables[0] = "leadGuitar" + phrase + " = { \\numericTimeSignature \\time " + meterup + "/4 \\tempo 4 = " + bpm + Toolbox.GetSongKey(keyIndex);
                //variablestmp[0] = "leadVocals" + phrase + " = {";
                //variablestmp[^1] += "\\bar \"||\"}";

            }
            //variables.Add("leadVocals" + phrase + " = {");
            //for (int i = 0; i < thoughtPattern.Length - 1; i++)
            //    variables.Add(@"\leadVocals" + phrase + thoughtPattern[i]);
            //if (barsToDelete > 0) variables.Add(@"\leadVocalsEnd");
            //else variables.Add(@"\leadVocals" + phrase + thoughtPattern[^1]);
            //variables.Add("}");

            int[] variationCounter = new int[thoughtsCount];
            byte[] thoughtPatternAscii = Encoding.ASCII.GetBytes(thoughtPattern);
            variables.Add("leadVocals" + phrase + " = {");
            for (int i = 0; i < thoughtPattern.Length - 1; i++)
                if (totalVariations[thoughtPatternAscii[i] - 65] <= 1)
                    variables.Add(@"\leadVocals" + phrase + thoughtPattern[i]);
                else
                {
                    variables.Add(@"\leadVocals" + phrase + thoughtPattern[i] + alphabet[variationCounter[thoughtPatternAscii[i] - 65]]);
                    variationCounter[thoughtPatternAscii[i] - 65]++;
                }
            if (barsToDelete > 0) variables.Add(@"\leadVocalsEnd");
            else //variables.Add(@"\leadVocals" + phrase + thoughtPattern[^1]);
            {
                if (totalVariations[thoughtPatternAscii[^1] - 65] <= 1)
                    variables.Add(@"\leadVocals" + phrase + thoughtPattern[^1]);
                else variables.Add(@"\leadVocals" + phrase + thoughtPattern[^1] + alphabet[variationCounter[thoughtPatternAscii[^1] - 65]]);
            }
            variables.Add("}");

            return variables.ToArray();
        }
        //private static string GetExpandedThoughtPattern(string thoughtPattern, int barsq, int progressionLengthInBars, int delayInProgressions, int[] thoughtLength)
        //{
        //    int thoughtIndex = 0;
        //    int thoughtPatternIniLen = thoughtPattern.Length;
        //    int thoughtsTotalLength = (int)Math.Ceiling((double)(barsq - delayInProgressions * progressionLengthInBars) / thoughtLength);

        //    while (thoughtPattern.Length * thoughtLength < thoughtsTotalLength)
        //    {
        //        thoughtPattern += thoughtPattern[thoughtIndex];
        //        thoughtIndex = (thoughtIndex + 1) % thoughtPatternIniLen;
        //    }
        //    while (thoughtPattern.Length > (int)Math.Floor((double)(thoughtsTotalLength / thoughtLength)))
        //    {
        //        thoughtPattern = thoughtPattern[..^1];
        //    }
        //    return thoughtPattern;
        //}
        private static (string expandedThoughtPattern, int barsToDelete) GetExpandedThoughtPattern(string thoughtPattern, int barsq, int progressionLengthInBars, int delayInProgressions, int[] thoughtLength)
        {
            int thoughtIndex = 0;
            int thoughtPatternIniLen = thoughtPattern.Length;
            int totalBars = barsq - delayInProgressions * progressionLengthInBars;
            string expandedThoughtPattern = "";
            byte[] asciiBytes = Encoding.ASCII.GetBytes(thoughtPattern);

            while (totalBars > 0)
            {
                expandedThoughtPattern += thoughtPattern[thoughtIndex];
                //totalBars -= thoughtLength[thoughtIndex] * progressionLengthInBars;
                totalBars -= thoughtLength[asciiBytes[thoughtIndex] - 65] * progressionLengthInBars;
                thoughtIndex = (thoughtIndex + 1) % thoughtPatternIniLen;
            }
            return (expandedThoughtPattern, -totalBars);
        }
        //private static List<decimal> GetStartRestSizeProbList(decimal maxRestSize, decimal restUnit)
        //{
        //    // decimal[] startRestSizes = new decimal[(int)(maxRestSize / restUnit) - 1];
        //    decimal[] startRestSizes = new decimal[(int)(maxRestSize / restUnit)];
        //    for (int i = 0; i < startRestSizes.Length; i++)
        //        startRestSizes[i] = restUnit * (i + 1);
        //    List<decimal> startRestSizeProb = new();
        //    for (int i = 0; i < startRestSizes.Length; i++)
        //        for (int j = 0; j < startRestSizes.Length - i; j++)
        //            startRestSizeProb.Add(startRestSizes[i]);
        //    return startRestSizeProb;
        //}
        private static List<decimal> GetStartRestSizeProbList(decimal maxRestSize, decimal restUnit)
        {
            decimal[] startRestSizes = new decimal[(int)maxRestSize];
            for (int i = 0; i < startRestSizes.Length; i++)
                startRestSizes[i] = restUnit * (i + 1);
            List<decimal> startRestSizeProb = new();
            for (int i = 0; i < startRestSizes.Length; i++)
                for (int j = 0; j < startRestSizes.Length - i; j++)
                    startRestSizeProb.Add(startRestSizes[i]);
            return startRestSizeProb;
        }
        private static (List<decimal> rhythmValues, List<bool> restsValues, int lastSyllableIndex) GetRhythm(decimal rhythmLength, decimal[] rhythmProbArray, Random rnd, decimal[] probs, List<int> strongBeats, int meterup, decimal startRestChance, List<decimal> startRestSizeProbArray, decimal restChance, decimal lastSyllableChance, decimal lastSyllableStart, decimal minEndRest)
        {
            List<decimal> rhythm = new();
            List<bool> rests = new();
            if ((decimal)rnd.NextDouble() < startRestChance)
            {
                rhythm.Add(startRestSizeProbArray[rnd.Next(startRestSizeProbArray.Count)]);
                rests.Add(true);
                rhythmLength -= rhythm[0];
            }
            rhythmLength -= minEndRest;

            int minInd;
            bool overrideProb = false;
            decimal randomValue;
            //int counter = 0;
            decimal initialLength = rhythmLength;
            int lastSyllableIndex = -1;

            while (rhythmLength > 0)
            {
                // if (rhythmLength + minEndRest <= initialLength * lastSyllableStart && (decimal)rnd.NextDouble() < lastSyllableChance)
                if (initialLength - rhythmLength >= initialLength * lastSyllableStart && (decimal)rnd.NextDouble() < lastSyllableChance)
                {
                    decimal endNotesValue = 0.5M;   // na razie roboczo same ósemki
                    int notesCount = 0;
                    decimal randomValueEnd = (decimal)rnd.NextDouble();
                    decimal[] endNotesThresholds = new decimal[] { 0.25M, 0.5M, 0.75M }; // roboczo taka sama szansa na 1 2 i 3 i 0
                    if (rhythmLength >= endNotesValue * 4 && randomValueEnd < endNotesThresholds[0]) notesCount = 3; // roboczo dopuszczane 1 2 lub 3 lub 0 imo
                    else if (rhythmLength >= endNotesValue * 3 && randomValueEnd < endNotesThresholds[1]) notesCount = 2;
                    else if (rhythmLength >= endNotesValue * 2 && randomValueEnd < endNotesThresholds[2]) notesCount = 1;
                    lastSyllableIndex = rhythm.Count;
                    rhythm.Add(rhythmLength - notesCount * endNotesValue);
                    rests.Add(false);
                    for (int i = 0; i < notesCount; i++)
                    {
                        rhythm.Add(endNotesValue);
                        rests.Add(false);
                    }
                    break;
                }
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
                if ((decimal)rnd.NextDouble() < restChance) rests.Add(true);
                else rests.Add(false);
                //counter++;
                rhythmLength -= newValue;
            }
            rhythm.Add(minEndRest);
            rests.Add(true);
            return (rhythm, rests, lastSyllableIndex);
        }
        private static (List<decimal> rhythmValues, List<bool> restsValues, int lastSyllableIndex) GetRhythmVariation(decimal[] rhythmProbArray, Random rnd, decimal[] probs, List<int> strongBeats, int meterup, decimal startRestChance, List<decimal> startRestSizeProbArray, decimal restChance, decimal lastSyllableChance, decimal lastSyllableStart, decimal minEndRest, List<decimal> rhythmValues, int startInd, int endInd, int lastSyllableIndexPrior, int endRestFactor)
        {
            List<decimal> rhythm = new();
            List<bool> rests = new();
            decimal rhythmLength;
            if (endInd >= lastSyllableIndexPrior)
            {
                rhythmLength = rhythmValues.GetRange(startInd, rhythmValues.Count - startInd - endRestFactor).Sum();
                endInd = rhythmValues.Count - 1 - endRestFactor;
            }
            else rhythmLength = rhythmValues.GetRange(startInd, endInd - startInd + 1).Sum();
            decimal rhythmPost = rhythmValues.GetRange(endInd + 1, rhythmValues.Count - endInd - 2).Sum();
            decimal startRest;
            if (startInd == 0 && (decimal)rnd.NextDouble() < startRestChance)
            {
                startRest = startRestSizeProbArray[rnd.Next(startRestSizeProbArray.Count)];
                if (startRest > rhythmLength) startRest = rhythmLength;
                rhythm.Add(startRest);
                rests.Add(true);
                rhythmLength -= rhythm[0];
            }
            //rhythmLength -= minEndRest;

            int minInd;
            bool overrideProb = false;
            decimal randomValue;
            //decimal initialLength = rhythmLength;
            decimal initialLength = rhythmValues.Sum() - minEndRest;
            int lastSyllableIndex = -1;
            //decimal rhythmPrior = rhythmValues.GetRange(0, startInd).Sum();

            while (rhythmLength > 0)
            {
                if (initialLength - rhythmLength - rhythmPost >= initialLength * lastSyllableStart && (decimal)rnd.NextDouble() < lastSyllableChance && endInd >= lastSyllableIndexPrior)
                {
                    decimal endNotesValue = 0.5M;   // na razie roboczo same ósemki
                    int notesCount = 0;
                    decimal randomValueEnd = (decimal)rnd.NextDouble();
                    decimal[] endNotesThresholds = new decimal[] { 0.25M, 0.5M, 0.75M }; // roboczo taka sama szansa na 1 2 i 3 i 0
                    if (rhythmLength >= endNotesValue * 4 && randomValueEnd < endNotesThresholds[0]) notesCount = 3; // roboczo dopuszczane 1 2 lub 3 lub 0 imo
                    else if (rhythmLength >= endNotesValue * 3 && randomValueEnd < endNotesThresholds[1]) notesCount = 2;
                    else if (rhythmLength >= endNotesValue * 2 && randomValueEnd < endNotesThresholds[2]) notesCount = 1;
                    lastSyllableIndex = rhythm.Count;
                    rhythm.Add(rhythmLength - notesCount * endNotesValue);
                    rests.Add(false);
                    for (int i = 0; i < notesCount; i++)
                    {
                        rhythm.Add(endNotesValue);
                        rests.Add(false);
                    }
                    break;
                }
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
                if ((decimal)rnd.NextDouble() < restChance) rests.Add(true);
                else rests.Add(false);
                //counter++;
                rhythmLength -= newValue;
            }
            if (endInd >= lastSyllableIndexPrior)
            {
                rhythm.Add(minEndRest);
                rests.Add(true);
            }
            return (rhythm, rests, lastSyllableIndex);
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
        private static string[] GetOctaveSpan(string lowNote, string highNote)
        {
            string lowSuffix = "";
            if (lowNote.EndsWith(',')) lowSuffix = lowNote[lowNote.IndexOf(',')..];
            else if (lowNote.EndsWith('\'')) lowSuffix = lowNote[lowNote.IndexOf('\'')..];
            string highSuffix = "";
            if (highNote.EndsWith(',')) highSuffix = highNote[highNote.IndexOf(',')..];
            else if (highNote.EndsWith('\'')) highSuffix = highNote[highNote.IndexOf('\'')..];
            return allVoiceOctaves[Array.IndexOf(allVoiceOctaves, lowSuffix)..(Array.IndexOf(allVoiceOctaves, highSuffix) + 1)];
        }
        //private static List<double> GetPitchProbArray(int allAllowedNotesCount, int firstNoteIndex, int lastNoteIndex, int thoughtDirection, double dirStrength, double progress, string weightShape, bool thoughtsRising, int thoughtsCount, int currentThoughtIndex, double risingOverlap, double boomerangTurningPoint)
        private static List<double> GetPitchProbArray(int allAllowedNotesCount, int firstNoteIndex, int lastNoteIndex, int thoughtDirection, double dirStrength, double progress, int weightShape, bool thoughtsRising, int thoughtsCount, int currentThoughtIndex, double risingOverlap, double boomerangTurningPoint, double genericMultiplier)
        {
            double centerValue = -1;
            //double ambitus = -1;
            switch (thoughtDirection)
            {
                case 0: // upwards
                    //ambitus = lastNoteIndex - firstNoteIndex;
                    //centerValue = progress * ambitus + firstNoteIndex;
                    centerValue = progress * (lastNoteIndex - firstNoteIndex) + firstNoteIndex;
                    break;
                case 1: // downwards
                    // centerValue = (1.0 - progress) * allAllowedNotes.Count;
                    //ambitus = firstNoteIndex - lastNoteIndex;
                    //centerValue = (1.0 - progress) * ambitus + lastNoteIndex;
                    centerValue = (1.0 - progress) * (firstNoteIndex - lastNoteIndex) + lastNoteIndex;
                    break;
                //case 2: // boomerang dud
                //    if (progress < boomerangTurningPoint) centerValue = progress / boomerangTurningPoint * (lastNoteIndex - firstNoteIndex) + firstNoteIndex;
                //    else centerValue = (1.0 - progress) / (1.0 - boomerangTurningPoint) * (lastNoteIndex - firstNoteIndex) + firstNoteIndex;
                //    break;
                //case 3: // boomerang udu
                //    if (progress < boomerangTurningPoint) centerValue = (-progress + boomerangTurningPoint) / boomerangTurningPoint * (lastNoteIndex - firstNoteIndex) + firstNoteIndex;
                //    else centerValue = (progress - boomerangTurningPoint) / (1.0 - boomerangTurningPoint) * (lastNoteIndex - firstNoteIndex) + firstNoteIndex;
                //    break;
                case 2: // boomerang dud
                    if (progress < boomerangTurningPoint) centerValue = progress / boomerangTurningPoint * (allAllowedNotesCount - 1 - firstNoteIndex) + firstNoteIndex;
                    else centerValue = (1.0 - progress) / (1.0 - boomerangTurningPoint) * (allAllowedNotesCount - 1 - lastNoteIndex) + lastNoteIndex;
                    break;
                case 3: // boomerang udu
                    if (progress < boomerangTurningPoint) centerValue = (-progress + boomerangTurningPoint) / boomerangTurningPoint * firstNoteIndex;
                    else centerValue = (progress - boomerangTurningPoint) / (1.0 - boomerangTurningPoint) * lastNoteIndex;
                    break;
                case 4: // neither
                    centerValue = (lastNoteIndex - firstNoteIndex) / 2.0 + firstNoteIndex;
                    //double quotient = 1.0 / (thoughtsCount - thoughtsCount * risingOverlap + risingOverlap);
                    //centerValue *= quotient;
                    //centerValue += currentThoughtIndex * quotient * (1.0 - risingOverlap);
                    //return Toolbox.GetGaussArray(allAllowedNotesCount, dirStrength * genericMultiplier, centerValue);
                    //return Toolbox.GetGaussArray(allAllowedNotesCount, dirStrength * genericMultiplier, (lastNoteIndex - firstNoteIndex) / 2.0 + firstNoteIndex);
                    break;
            }
            if (thoughtsRising)
            {
                double quotient = 1.0 / (thoughtsCount - thoughtsCount * risingOverlap + risingOverlap);
                centerValue *= quotient;
                centerValue += currentThoughtIndex * quotient * (1.0 - risingOverlap) * (allAllowedNotesCount - 1);
            }
            if (thoughtDirection == 4) return GetPitchProbArrayShape(weightShape, allAllowedNotesCount, dirStrength * genericMultiplier, centerValue);
            else return GetPitchProbArrayShape(weightShape, allAllowedNotesCount, dirStrength, centerValue);
        }
        //private static List<double> GetGaussArray(int count, double sigma, double mi)
        //{
        //    List<double> gaussArray = new();
        //    for (int i = 0; i < count; i++)
        //    {
        //        double var1 = (i - mi) / sigma;
        //        gaussArray.Add(1.0 / (sigma * Math.Sqrt(2.0 * Math.PI)) * Math.Exp(-var1 * var1 / 2));
        //    }
        //    return gaussArray;
        //}
        // private static List<double> GetEachNoteProbValues(List<double> pitchProb, List<double> stepProb, List<double> intervalProb, double[] weights)
        private static List<double> GetEachNoteProbValues(List<double> pitchProb, List<double> stepProb, List<double> intervalProb, int[] weights)
        {
            System.Diagnostics.Debug.Assert(pitchProb.Count == stepProb.Count && stepProb.Count == intervalProb.Count);
            List<double> probComplete = new();
            for (int i = 0; i < pitchProb.Count; i++)
                //probComplete.Add(Math.Pow(pitchProb[i], 1 - weights[0] / 10.0d) * Math.Pow(stepProb[i], 1 - weights[1] / 10.0d) * Math.Pow(intervalProb[i], 1 - weights[2] / 10.0d));
                probComplete.Add(Math.Pow(pitchProb[i], weights[0] / 10.0d) * Math.Pow(stepProb[i], weights[1] / 10.0d) * Math.Pow(intervalProb[i], weights[2] / 10.0d));
            //return probComplete;
            return Toolbox.NormalizeProbArray(probComplete);
        }
        private static List<int> ExpandIntervalProbArray(List<int> intervalProb, List<string> allAllowedNotes, int previousNoteIndex)
        {
            int maxInterval = (int)possibleIntervals.Max();
            List<int> expandedIntervalProb = new();
            for (int i = 0; i < allAllowedNotes.Count; i++)
                expandedIntervalProb.Add(0);
            for (int i = -maxInterval; i < maxInterval; i++)
                if (previousNoteIndex + i >= 0 && previousNoteIndex + i < allAllowedNotes.Count)
                    expandedIntervalProb[previousNoteIndex + i] = intervalProb[Math.Abs(i)];
            return expandedIntervalProb;
        }
        private static List<double> ExpandStepProbArray(List<double> stepProb, List<string> allAllowedNotes, int firstRootIndex)
        // private static List<double> ExpandStepProbArray<T>(List<T> stepProb, List<string> allAllowedNotes, int firstRootIndex)
        {
            List<double> expandedStepProb = new();
            for (int i = -firstRootIndex; i < 0; i++)
                expandedStepProb.Add(stepProb[^Math.Abs(i)]);
            for (int i = firstRootIndex; i < allAllowedNotes.Count; i++)
                expandedStepProb.Add(stepProb[(i - firstRootIndex) % stepProb.Count]);
            return expandedStepProb;
        }
        private static List<double> ExpandStepProbArray(List<int> stepProb, List<string> allAllowedNotes, int firstRootIndex)
        {
            List<double> expandedStepProb = new();
            for (int i = -firstRootIndex; i < 0; i++)
                expandedStepProb.Add(stepProb[^Math.Abs(i)]);
            for (int i = firstRootIndex; i < allAllowedNotes.Count; i++)
                expandedStepProb.Add(stepProb[(i - firstRootIndex) % stepProb.Count]);
            return expandedStepProb;
        }
        private static List<double> ApplyCurrentChordBias(List<int> stepProb, string[] notesInScale, string[] currentChordNotes, double probMultiplier)
        {
            List<double> currentStepProb = new();
            for (int i = 0; i < notesInScale.Length; i++)
            {
                if (currentChordNotes.Any(ele => ele == notesInScale[i])) currentStepProb.Add(stepProb[i] * probMultiplier);
                else currentStepProb.Add(stepProb[i]);
            }
            return currentStepProb;
        }
        //private static List<double> ApplyCurrentChordBias(List<int> stepProb, string[] notesInScale, string[] currentChordNotes, List<double> probMultiplier)
        //{
        //    List<double> currentStepProb = new();
        //    for (int i = 0; i < notesInScale.Length; i++)
        //    {
        //        for (int j = 0; j < currentChordNotes.Length; j++)
        //        {
        //            if (notesInScale[i] == currentChordNotes[j]) currentStepProb.Add(stepProb[i] * probMultiplier[j]);
        //            else currentStepProb.Add(stepProb[i]);
        //        }
        //    }
        //    return currentStepProb;
        //}
        private static List<double> ApplyCurrentChordBias(List<int> stepProb, string[] notesInScale, string[] currentChordNotes, List<double> probMultiplier)
        {
            List<double> currentStepProb = new();
            for (int i = 0; i < stepProb.Count; i++) currentStepProb.Add(stepProb[i]);
            int chordNoteIndex = 0;
            for (int i = 0; i < currentChordNotes.Length; i++)
            {
                if ((chordNoteIndex = Array.FindIndex(notesInScale, ele => ele == currentChordNotes[i])) >= 0)
                    currentStepProb[chordNoteIndex] *= probMultiplier[i];
            }
            return currentStepProb;
        }
        private static (List<double> chordNoteProbArray, bool forcedNote) GetForceChordNoteProbArray(List<string> allAllowedNotes, double chordChance, List<int> chordCompChances, string[] currentChordNotes, Random rnd)
        {
            List<double> chordNoteProbArray = new();
            List<double> ones = new();
            for (int i = 0; i < allAllowedNotes.Count; i++) ones.Add(1);
            if (rnd.NextDouble() < chordChance)
            {
                if (chordCompChances[0] < 0)
                {
                    for (int i = 0; i < allAllowedNotes.Count; i++)
                    {
                        if (currentChordNotes.Any(ele => allAllowedNotes[i].StartsWith(ele))) chordNoteProbArray.Add(1);
                        else chordNoteProbArray.Add(0);
                        // chordNoteProbArray = allAllowedNotes.FindAll(ele => currentChordNotes.Any(ele2 => ele.StartsWith(ele2)));
                    }
                }
                else
                {
                    string[] startNoteProbArray = Toolbox.GetProbArray(currentChordNotes, chordCompChances);
                    string firstNote = startNoteProbArray[rnd.Next(startNoteProbArray.Length)];
                    for (int i = 0; i < allAllowedNotes.Count; i++)
                    {
                        if (allAllowedNotes[i].StartsWith(firstNote)) chordNoteProbArray.Add(1);
                        else chordNoteProbArray.Add(0);
                        // chordNoteProbArray = allAllowedNotes.FindAll(ele => ele.StartsWith(firstNote));
                    }
                }
                //if (chordNoteProbArray.Count == 0) return (ones, false);
                if (chordNoteProbArray.FindIndex(ele => ele > 0) == -1) return (ones, false);
            }
            else return (ones, false);
            return (chordNoteProbArray, true);
        }
        private static string[] GetMusic(string[] rhythmDivided, List<int> nonRestIndices, List<int> restIndices, List<string> allAllowedNotes, int[] noteIndices)
        {
            string[] music = new string[rhythmDivided.Length];
            for (int i = 0; i < music.Length; i++)
                music[i] = rhythmDivided[i];
            for (int i = 0; i < nonRestIndices.Count; i++)
                music[nonRestIndices[i]] += allAllowedNotes[noteIndices[i]];
            for (int i = 0; i < restIndices.Count; i++)
                music[restIndices[i]] += "r";
            return music;
        }
        //private static int RollMelodyVarNote(int[] noteIndices, List<int> varMelodyIndices, int currentNoteIndex,/* int currentRhythmIndex,*/ List<int> nonRestIndices, Chord[] chords, List<int> strongBeats, int meterup,/* decimal valuesSum,*/ int chordLengthInStrongBeats, int progressionLengthInBars, string[] notesInScale, List<decimal> rhythmValues, List<int> stepProb, List<double> currentChordBias, List<string> allAllowedNotes, int firstRootIndex, List<int> intervalProb, int thoughtDirection, double dirStrength, int weightShapeIndex, bool thoughtsRising, int thoughtsCount, int currentThoughtIndex, double risingOverlap, double boomerangTurningPoint, double genericMultiplier, int[] weights, Random rnd)
        //private static int RollMelodyVarNote(List<int> noteIndices, List<int> varMelodyIndices, int currentNoteIndex,/* int currentRhythmIndex,*/ List<int> nonRestIndices, Chord[] chords, List<int> strongBeats, int meterup,/* decimal valuesSum,*/ int chordLengthInStrongBeats, int progressionLengthInBars, string[] notesInScale, List<decimal> rhythmValues, List<int> stepProb, List<double> currentChordBias, List<string> allAllowedNotes, int firstRootIndex, List<int> intervalProb, int thoughtDirection, double dirStrength, int weightShapeIndex, bool thoughtsRising, int thoughtsCount, int currentThoughtIndex, double risingOverlap, double boomerangTurningPoint, double genericMultiplier, int[] weights, Random rnd)
        private static int RollMelodyVarNote(int[] noteIndices, List<int> varMelodyIndicesNonRests, int currentNoteIndex, int currentRhythmIndex, List<int> nonRestIndices, Chord[] chords, List<int> strongBeats, int meterup,/* decimal valuesSum,*/ int chordLengthInStrongBeats, int progressionLengthInBars, string[] notesInScale, List<decimal> rhythmValues, List<int> stepProb, List<double> currentChordBias, List<string> allAllowedNotes, int firstRootIndex, List<int> intervalProb, int thoughtDirection, double dirStrength, int weightShapeIndex, bool thoughtsRising, int thoughtsCount, int currentThoughtIndex, double risingOverlap, double boomerangTurningPoint, double genericMultiplier, int[] weights, Random rnd)
        {
            // noteIndices - indeksy nut wylosowanych pierwotnie, odnoszących sie do allAllowedNotes
            // varMelodyIndices - lista indeksów noteIndices, w których nuty mają zostać wylosowane ponownie
            // currentNoteIndex - indeks aktualnej nuty w noteIndices (pomija pauzy)
            // currentRhythmIndex - indeks aktualnej nuty w rhythmValues
            string[] currentChordNotes = Toolbox.GetCurrentChordNotes(chords, strongBeats, meterup, rhythmValues.GetRange(0, currentRhythmIndex).Sum(), chordLengthInStrongBeats, progressionLengthInBars, notesInScale);
            //string[] currentChordNotes = Toolbox.GetCurrentChordNotes(chords, strongBeats, meterup, rhythmValues.GetRange(0, currentNoteIndex).Sum(), chordLengthInStrongBeats, progressionLengthInBars, notesInScale);
            List<double> stepProbCurrent = ApplyCurrentChordBias(stepProb, notesInScale, currentChordNotes, currentChordBias);
            List<double> stepProbNorm = Toolbox.NormalizeProbArray(ExpandStepProbArray(stepProbCurrent, allAllowedNotes, firstRootIndex));
            //List<double> intervalProbNorm = Toolbox.NormalizeProbArray(ExpandIntervalProbArray(intervalProb, allAllowedNotes, noteIndices[currentNoteIndex - 1]));
            List<double> intervalProbNorm;
            if (currentNoteIndex != 0) intervalProbNorm = Toolbox.NormalizeProbArray(ExpandIntervalProbArray(intervalProb, allAllowedNotes, noteIndices[currentNoteIndex - 1]));
            else
            {
                intervalProbNorm = new();
                for (int i = 0; i < allAllowedNotes.Count; i++)
                    intervalProbNorm.Add(1);
            }

            double lastNotePositionInQuarterNotes = (double)rhythmValues.GetRange(0, nonRestIndices[^1]).Sum();
            List<double> pitchProb = GetPitchProbArray(allAllowedNotes.Count, noteIndices[0], noteIndices[^1], thoughtDirection, dirStrength, (double)rhythmValues.GetRange(0, currentRhythmIndex).Sum() / lastNotePositionInQuarterNotes, weightShapeIndex, thoughtsRising, thoughtsCount, currentThoughtIndex, risingOverlap, boomerangTurningPoint, genericMultiplier);
            //List<double> pitchProb = GetPitchProbArray(allAllowedNotes.Count, noteIndices[0], noteIndices[^1], thoughtDirection, dirStrength, (double)rhythmValues.GetRange(0, currentNoteIndex).Sum() / lastNotePositionInQuarterNotes, weightShapeIndex, thoughtsRising, thoughtsCount, currentThoughtIndex, risingOverlap, boomerangTurningPoint, genericMultiplier);
            List<double> note1Prob = GetEachNoteProbValues(pitchProb, stepProbNorm, intervalProbNorm, weights);

            //if (varMelodyIndices.Contains(currentRhythmIndex + 1))
            if (varMelodyIndicesNonRests.Contains(currentNoteIndex + 1) || currentNoteIndex == noteIndices.Length - 1)
            {
                return Toolbox.WeightedRandom(note1Prob, rnd);
            }
            else
            {
                string[] secondNoteChordNotes = Toolbox.GetCurrentChordNotes(chords, strongBeats, meterup, rhythmValues.GetRange(0, currentRhythmIndex + 1).Sum(), chordLengthInStrongBeats, progressionLengthInBars, notesInScale);
                //string[] secondNoteChordNotes = Toolbox.GetCurrentChordNotes(chords, strongBeats, meterup, rhythmValues.GetRange(0, currentNoteIndex + 1).Sum(), chordLengthInStrongBeats, progressionLengthInBars, notesInScale);
                List<double> stepProbSecond = ApplyCurrentChordBias(stepProb, notesInScale, secondNoteChordNotes, currentChordBias);
                stepProbSecond = Toolbox.NormalizeProbArray(ExpandStepProbArray(stepProbSecond, allAllowedNotes, firstRootIndex));
                List<double> intervalProbSecond = Toolbox.NormalizeProbArray(ExpandIntervalProbArray(intervalProb, allAllowedNotes, noteIndices[currentNoteIndex + 1]));
                List<double> pitchProbSecond = GetPitchProbArray(allAllowedNotes.Count, noteIndices[0], noteIndices[^1], thoughtDirection, dirStrength, (double)rhythmValues.GetRange(0, currentRhythmIndex + 1).Sum() / lastNotePositionInQuarterNotes, weightShapeIndex, thoughtsRising, thoughtsCount, currentThoughtIndex, risingOverlap, boomerangTurningPoint, genericMultiplier);
                //List<double> pitchProbSecond = GetPitchProbArray(allAllowedNotes.Count, noteIndices[0], noteIndices[^1], thoughtDirection, dirStrength, (double)rhythmValues.GetRange(0, currentNoteIndex + 1).Sum() / lastNotePositionInQuarterNotes, weightShapeIndex, thoughtsRising, thoughtsCount, currentThoughtIndex, risingOverlap, boomerangTurningPoint, genericMultiplier);
                List<double> note2Prob = GetEachNoteProbValues(pitchProbSecond, stepProbSecond, intervalProbSecond, weights);
                return Toolbox.GetTwoSideNoteProb(note1Prob, note2Prob, rnd, noteIndices[Math.Max(currentNoteIndex - 1, 0)], noteIndices[currentNoteIndex + 1]);
            }
        }
        private static int FindClosestNoteIndex(List<decimal> rhythmBig, List<decimal> rhythmSmall, int index, List<int> indices)
        {
            decimal rhythmSum = rhythmSmall.GetRange(0, index).Sum();
            int newIndex = -1;
            //for (int i = 0; i < rhythmBig.Count; i++)
            for (int i = 0; i < indices.Count; i++)
            {
                if (rhythmBig.GetRange(0, indices[i]).Sum() > rhythmSum)
                {
                    newIndex = i;
                    break;
                }
            }
            //if (newIndex == -1) return rhythmBig.Count - 1;
            if (newIndex == -1) return indices.Count - 1;
            if (newIndex == 0) return newIndex;
            if (rhythmBig.GetRange(0, indices[newIndex]).Sum() - rhythmSum > rhythmSum - rhythmBig.GetRange(0, indices[newIndex - 1]).Sum()) return newIndex - 1;
            else return newIndex;
        }
        private static List<int> GetNonRestNotesIndices(List<int> indices, List<bool> rests)
        {
            // wszystkie elementy w indices muszą być mniejsze niż wielkość rests; mają to być indeksy wybranych nut z listy wielkości rests
            System.Diagnostics.Debug.Assert(indices.Count <= rests.Count && indices.Max() < rests.Count);
            List<int> nonRestNotesIndices = new();
            int restPenalty = 0;
            int indicesCounter = 0;
            for (int i = 0; i < rests.Count; i++)
            {
                if (rests[i])
                {
                    if (i == indices[indicesCounter])
                    {
                        indicesCounter++;
                        if (indicesCounter > indices.Count - 1) break;
                    }
                    restPenalty++;
                }
                else if (i == indices[indicesCounter])
                {
                    nonRestNotesIndices.Add(indices[indicesCounter] - restPenalty);
                    indicesCounter++;
                    if (indicesCounter > indices.Count - 1) break;
                }
            }
            return nonRestNotesIndices;
        }
        //private static (List<decimal> rhythmValuesGliss, List<int> nonRestIndicesGliss, List<int> restIndicesGliss, List<string> allAllowedNotesGliss, int[] noteIndicesGliss) GlissThought(List<decimal> rhythmValues, List<int> nonRestIndices, List<int> restIndices, List<string> allAllowedNotes, int[] noteIndices, decimal glissChanceM, int glissMinInterval, double glissNoteLengthInQuarterNotes, int[] glissMaxNoteCount, Random rnd)
        private static (List<decimal> rhythmValuesGliss, List<int> nonRestIndicesGliss, List<int> restIndicesGliss, List<string> allAllowedNotesGliss, int[] noteIndicesGliss) GlissThought(List<decimal> rhythmValues, List<bool> rests, List<string> allAllowedNotes, int[] noteIndices, decimal glissChanceM, int glissMinInterval, double glissNoteLengthInQuarterNotes, int[] glissMaxNoteCount, Random rnd, Chord[] chords, List<int> strongBeats, int meterup, int chordLengthInStrongBeats, int progressionLengthInBars, string[] notesInScale, List<int> stepProb, List<double> currentChordBias, int firstRootIndex, List<int> intervalProb, double dirStrength, int[] weights, int weightShapeIndex)
        {
            //int[] noteIndicesGliss = new int[noteIndices.Length];
            //for (int i = 0; i < noteIndices.Length; i++) noteIndicesGliss[i] = noteIndices[i];
            List<int> noteIndicesGliss = new(noteIndices);
            List<decimal> rhythmValuesGliss = new(rhythmValues);
            List<bool> restsGliss = new(rests);
            List<int> nonRestIndicesGliss = new();
            List<int> restIndicesGliss = new();
            List<string> allAllowedNotesGliss = new(allAllowedNotes);
            double glissChance = (double)glissChanceM;
            //for (int i = 1; i < noteIndices.Length; i++)
            //    if (Math.Abs(noteIndices[i] - noteIndices[i - 1]) >= glissMinInterval && rnd.NextDouble() < glissChance)
            for (int i = 1; i < noteIndicesGliss.Count; i++)
                if (Math.Abs(noteIndicesGliss[i] - noteIndicesGliss[i - 1]) >= glissMinInterval && rnd.NextDouble() < glissChance)
                    //SingleGliss(rhythmValuesGliss, nonRestIndicesGliss, restIndicesGliss, allAllowedNotesGliss, noteIndicesGliss, i - 1, glissNoteLengthInQuarterNotes, glissMaxNoteCount, rnd);
                    SingleGliss(rhythmValuesGliss, restsGliss, allAllowedNotesGliss, noteIndicesGliss, i - 1, glissNoteLengthInQuarterNotes, glissMaxNoteCount, rnd, chords, strongBeats, meterup, chordLengthInStrongBeats, progressionLengthInBars, notesInScale, stepProb, currentChordBias, firstRootIndex, intervalProb, dirStrength, weights, weightShapeIndex);
            for (int i = 0; i < restsGliss.Count; i++)
                if (restsGliss[i]) restIndicesGliss.Add(i);
                else nonRestIndicesGliss.Add(i);
            System.Diagnostics.Debug.Assert(rhythmValuesGliss.Count == restsGliss.Count);
            System.Diagnostics.Debug.Assert(nonRestIndicesGliss.Count == noteIndicesGliss.Count);
            return (rhythmValuesGliss, nonRestIndicesGliss, restIndicesGliss, allAllowedNotesGliss, noteIndicesGliss.ToArray());
        }
        //private static void SingleGliss(List<decimal> rhythmValues, List<int> nonRestIndices, List<int> restIndices, List<string> allAllowedNotes, List<int> noteIndices, int melodyIndex, double glissNoteLengthInQuarterNotes, int[] glissMaxNoteCount, Random rnd)
        private static void SingleGliss(List<decimal> rhythmValues, List<bool> rests, List<string> allAllowedNotes, List<int> noteIndices, int melodyIndex, double glissNoteLengthInQuarterNotes, int[] glissMaxNoteCount, Random rnd, Chord[] chords, List<int> strongBeats, int meterup, int chordLengthInStrongBeats, int progressionLengthInBars, string[] notesInScale, List<int> stepProb, List<double> currentChordBias, int firstRootIndex, List<int> intervalProb, double dirStrength, int[] weights, int weightShapeIndex)
        {
            decimal initialRhythmLength = rhythmValues.Sum();
            decimal glissNoteLength = (decimal)glissNoteLengthInQuarterNotes;
            int interval = noteIndices[melodyIndex + 1] - noteIndices[melodyIndex];
            //int interval = Math.Abs(noteIndices[melodyIndex + 1] - noteIndices[melodyIndex]);
            bool upwards = interval >= 0;
            int secondIndex;
            interval = Math.Abs(interval);
            int restsPenalty = 0;
            //for (int i = 0; i < restIndices.Count; i++)
            //    if (restIndices[i] <= melodyIndex + restsPenalty)
            //        restsPenalty++;
            //    else break;
            for (int i = 0; i < rests.Count; i++)
            {
                if (rests[i]) restsPenalty++;
                if (i >= melodyIndex + restsPenalty) break;
            }
            int rhythmIndex = melodyIndex + restsPenalty;
            if (rests[rhythmIndex + 1]) return;
            int maxNoteCount = Math.Min(glissMaxNoteCount[interval], (int)Math.Floor(rhythmValues[rhythmIndex] / glissNoteLength) - 1);
            maxNoteCount = Math.Min(maxNoteCount, interval - 1);
            if (maxNoteCount <= 0) return;
            int[] noteCountProbArray = new int[maxNoteCount];
            for (int i = 0; i < maxNoteCount; i++) noteCountProbArray[i] = i + 1;
            int noteCount = noteCountProbArray[rnd.Next(maxNoteCount)];
            decimal noteLengthPrior = rhythmValues[rhythmIndex];
            int targetNoteMelodyIndex = noteIndices[melodyIndex + 1];
            //rhythmValues.RemoveAt(rhythmIndex);
            //rhythmValues.Insert(rhythmIndex, noteLengthPrior - noteCount * glissNoteLength);
            rhythmValues[rhythmIndex] = noteLengthPrior - noteCount * glissNoteLength;
            for (int i = 0; i < noteCount; i++)
            {
                rhythmValues.Insert(rhythmIndex + 1 + i, glissNoteLength);
                //nonRestIndices.Insert(rhythmIndex - restsPenalty, rhythmIndex);
                rests.Insert(rhythmIndex + 1 + i, false);
                if (upwards) secondIndex = targetNoteMelodyIndex - noteCount + 1 + i;
                else secondIndex = targetNoteMelodyIndex + noteCount - 1 - i;
                noteIndices.Insert(melodyIndex + 1 + i, GlissNoteRoll(noteIndices[melodyIndex + i], secondIndex, i == noteCount - 1, rhythmIndex + i, chords, strongBeats, meterup, chordLengthInStrongBeats, progressionLengthInBars, notesInScale, rhythmValues, stepProb, currentChordBias, allAllowedNotes, firstRootIndex, intervalProb, dirStrength, weights, rnd, weightShapeIndex));
            }
            System.Diagnostics.Debug.Assert(initialRhythmLength == rhythmValues.Sum());
        }
        private static int GlissNoteRoll(int firstNoteIndex, int secondNoteIndex, bool lastNote, int currentRhythmIndex, Chord[] chords, List<int> strongBeats, int meterup, int chordLengthInStrongBeats, int progressionLengthInBars, string[] notesInScale, List<decimal> rhythmValues, List<int> stepProb, List<double> currentChordBias, List<string> allAllowedNotes, int firstRootIndex, List<int> intervalProb, double dirStrength, int[] weights, Random rnd, int weightShapeIndex)
        {
            string[] currentChordNotes = Toolbox.GetCurrentChordNotes(chords, strongBeats, meterup, rhythmValues.GetRange(0, currentRhythmIndex).Sum(), chordLengthInStrongBeats, progressionLengthInBars, notesInScale);
            List<double> stepProbCurrent = ApplyCurrentChordBias(stepProb, notesInScale, currentChordNotes, currentChordBias);
            List<double> stepProbNorm = Toolbox.NormalizeProbArray(ExpandStepProbArray(stepProbCurrent, allAllowedNotes, firstRootIndex));
            List<double> intervalProbNorm = Toolbox.NormalizeProbArray(ExpandIntervalProbArray(intervalProb, allAllowedNotes, firstNoteIndex));
            List<double> pitchProb = GetPitchProbArrayShape(weightShapeIndex, allAllowedNotes.Count, dirStrength, (secondNoteIndex + firstNoteIndex) / 2.0);
            List<double> note1Prob = GetEachNoteProbValues(pitchProb, stepProbNorm, intervalProbNorm, weights);
            if (secondNoteIndex >= firstNoteIndex)
            {
                for (int i = 0; i < note1Prob.Count; i++)
                    if (i >= secondNoteIndex || i <= firstNoteIndex) note1Prob[i] = 0;
            }
            else
                for (int i = 0; i < note1Prob.Count; i++)
                    if (i <= secondNoteIndex || i >= firstNoteIndex) note1Prob[i] = 0;

            if (!lastNote) return Toolbox.WeightedRandom(note1Prob, rnd);
            string[] secondNoteChordNotes = Toolbox.GetCurrentChordNotes(chords, strongBeats, meterup, rhythmValues.GetRange(0, currentRhythmIndex + 1).Sum(), chordLengthInStrongBeats, progressionLengthInBars, notesInScale);
            List<double> stepProbSecond = ApplyCurrentChordBias(stepProb, notesInScale, secondNoteChordNotes, currentChordBias);
            stepProbSecond = Toolbox.NormalizeProbArray(ExpandStepProbArray(stepProbSecond, allAllowedNotes, firstRootIndex));
            List<double> intervalProbSecond = Toolbox.NormalizeProbArray(ExpandIntervalProbArray(intervalProb, allAllowedNotes, secondNoteIndex));
            List<double> pitchProbSecond = GetPitchProbArrayShape(weightShapeIndex, allAllowedNotes.Count, dirStrength, (secondNoteIndex + firstNoteIndex) / 2.0);
            List<double> note2Prob = GetEachNoteProbValues(pitchProbSecond, stepProbSecond, intervalProbSecond, weights);
            return Toolbox.GetTwoSideNoteProb(note1Prob, note2Prob, rnd, firstNoteIndex, secondNoteIndex);
        }
        private static List<double> GetPitchProbArrayShape(int weightShape, int count, double invertedSigma, double mi)
        {
            return weightShape switch
            {
                1 => Toolbox.GetTriangleArray(count, invertedSigma, mi),
                2 => Toolbox.GetSineArray(count, invertedSigma, mi),
                _ => Toolbox.GetGaussArray(count, invertedSigma, mi),
            };
        }

        private static readonly decimal[] possibleValues =
        {
            3, 2.75M, 2.5M, 2.25M, 2, 1.75M, 1.5M, 1.25M, 1, 0.75M, 0.5M, 0.25M
        };
        private static readonly string[] allVoiceOctaves =
        {
            ",,", ",", "", "'", "''", "'''", "''''"
        };
        private static readonly decimal[] possibleIntervals =
        {
            0, 1, 2, 3, 4, 5, 6, 7
        };
        private static readonly string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    }
}
