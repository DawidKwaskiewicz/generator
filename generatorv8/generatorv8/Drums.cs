using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace generatorv8
{
    public class Drums
    {
        public static string[][] GetDrums(List<int> strongBeats, int meterup, int barsq, bool forceKickOn1, int beatlength, int bpm, char phrase)
        {
            Random rnd = new();
            int[] meters = new int[strongBeats.Count];
            for (int i = 0; i < strongBeats.Count - 1; i++)
                meters[i] = strongBeats[i + 1] - strongBeats[i];
            meters[strongBeats.Count - 1] = meterup - strongBeats[^1];
            string drumsUpBar = "";
            string drumsDownBar = "";

            int iterations = (int)Math.Ceiling((double)barsq / beatlength);
            int barsToDelete = iterations * beatlength - barsq;
            string[] drumsUp = new string[iterations];
            string[] drumsDown = new string[iterations];

            for (int j = 0; j < beatlength; j++)
            {
                for (int i = 0; i < strongBeats.Count; i++)
                {
                    Beat beat1 = new();
                    Beat beat2 = new();
                    if (i == 0 && j == 0 && forceKickOn1)
                    {
                        Beat[] beats;
                        if (meters[i] == 2)
                        {
                            beats = GetKickOn1Beats(beats2kick);
                            beat1 = beats[rnd.Next(beats.Length)];
                            beat2 = beats2snare[rnd.Next(beats2snare.Length)];
                        }
                        else if (meters[i] == 3)
                        {
                            beats = GetKickOn1Beats(beats3kick);
                            beat1 = beats[rnd.Next(beats.Length)];
                            beat2 = beats3snare[rnd.Next(beats3snare.Length)];
                        }
                    }
                    else
                    {
                        if (meters[i] == 2)
                        {
                            beat1 = beats2kick[rnd.Next(beats2kick.Length)];
                            beat2 = beats2snare[rnd.Next(beats2snare.Length)];
                        }
                        else if (meters[i] == 3)
                        {
                            beat1 = beats3kick[rnd.Next(beats3kick.Length)];
                            beat2 = beats3snare[rnd.Next(beats3snare.Length)];
                        }
                    }
                    drumsUpBar += beat1.up + beat2.up;
                    drumsDownBar += beat1.down + beat2.down;
                }
                drumsUpBar += "| ";
                drumsDownBar += "| ";
            }

            for (int i = 0; i < iterations; i++)
            {
                drumsUp[i] = drumsUpBar;
                drumsDown[i] = drumsDownBar;
            }
            string[] drumsUpLastBeat = drumsUp[iterations - 1].Split("|");
            string[] drumsDownLastBeat = drumsDown[iterations - 1].Split("|");
            drumsUp[iterations - 1] = "";
            drumsDown[iterations - 1] = "";
            for (int i = 0; i < beatlength - barsToDelete; i++)
            {
                drumsUp[iterations - 1] += drumsUpLastBeat[i] + "|";
                drumsDown[iterations - 1] += drumsDownLastBeat[i] + "|";
            }

            drumsUp[^1] += "\\bar \"||\"}}";
            drumsDown[^1] += "\\bar \"||\"}}";

            //string[] variables = Toolbox.JoinMultipleArrays(new string[][] { new string[] { "drumsUp" + phrase + " = { \\drummode {" }, drumsUp, new string[] { "drumsDown" + phrase + " = { \\drummode {" }, drumsDown });
            //drumsUp = Toolbox.JoinMultipleArrays(new string[][] { new string[] { "drumsUp" + phrase + @" = { \drummode { \numericTimeSignature \time " + meterup + "/4 \\tempo 4 = " + bpm }, drumsUp } );
            //drumsDown = Toolbox.JoinMultipleArrays(new string[][] { new string[] { "drumsDown" + phrase + @" = { \drummode { \numericTimeSignature \time " + meterup + "/4 \\tempo 4 = " + bpm }, drumsDown } );
            drumsUp = Toolbox.JoinMultipleArrays(new string[][] { new string[] { "drumsUp" + phrase + @" = { \drummode { " }, drumsUp });
            drumsDown = Toolbox.JoinMultipleArrays(new string[][] { new string[] { "drumsDown" + phrase + @" = { \drummode { " }, drumsDown });

            string[] music = new string[]
            {
                "\\new DrumStaff \\with { instrumentName = \"Drums\" } <<",
                @"  \drummode { \numericTimeSignature \time " + meterup + "/4 \\tempo 4 = " + bpm,
                "    << { \\drumsUp }",
                @"      \\",
                "       { \\drumsDown } >>",
                "  }",
                ">>"
            };
            //return new string[][] { variables, music };
            return new string[][] { drumsUp, drumsDown };
        }
        private static readonly Beat[] beats2kick = new[]
        {
            new Beat("hh8 hh8 ", "r4 ", "kick"),
            new Beat("hh8 hh8 ", "bd4 ", "kick"),
            new Beat("hh8 hh8 ", "r8 bd8 ", "kick"),
            new Beat("hh8 hh8 ", "bd8 bd8 ", "kick"),
            new Beat("hh8 hh8 ", "bd8. bd16 ", "kick"),
            new Beat("hh8 hh8 ", "r8. bd16 ", "kick")
        };
        private static readonly Beat[] beats2snare = new[]
        {
            new Beat("hh8 hh8 ", "sn4 ", "snare"),
            new Beat("hh8 hh8 ", "sn8 bd8 ", "snare"),
            new Beat("hh8 hh8 ", "sn8. bd16 ", "snare"),
            new Beat("hh8 hh8 ", "sn16 bd8. ", "snare"),
            new Beat("hh8 hh8 ", "sn8 sn8 ", "snare"),
            new Beat("hh8 hh8 ", "sn8. sn16 ", "snare")
        };
        private static readonly Beat[] beats3kick = new[]
        {
            new Beat("hh8 hh8 hh8 ", "bd4. ", "kick"),
            new Beat("hh8 hh8 hh8 ", "bd8 bd8 r8 ", "kick"),
            new Beat("hh8 hh8 hh8 ", "bd8 bd8 bd8 ", "kick"),
            new Beat("hh8 hh8 hh8 ", "bd8 r8 bd8 ", "kick"),
            new Beat("hh8 hh8 hh8 ", "r8 bd8 bd8 ", "kick"),
            new Beat("hh8 hh8 hh8 ", "r8 bd8 r8 ", "kick"),
            new Beat("hh8 hh8 hh8 ", "r4 bd8 ", "kick")
        };
        private static readonly Beat[] beats3snare = new[]
        {
            new Beat("hh8 hh8 hh8 ", "sn4. ", "snare"),
            new Beat("hh8 hh8 hh8 ", "sn8 bd8 r8 ", "snare"),
            new Beat("hh8 hh8 hh8 ", "sn8 bd8 bd8 ", "snare"),
            new Beat("hh8 hh8 hh8 ", "sn8 r8 bd8 ", "snare")
        };
        public struct Beat
        {
            public Beat(string up, string down, string type)
            {
                this.up = up;
                this.down = down;
                this.type = type;
            }
            public readonly string up;
            public readonly string down;
            public readonly string type;
        }
        private static Beat[] GetKickOn1Beats(Beat[] beat)
        {
            List<int> beatsInd = new();
            for (int i = 0; i < beat.Length; i++)
                if (beat[i].down.StartsWith("bd"))
                    beatsInd.Add(i);
            Beat[] beatsNew = new Beat[beatsInd.Count];
            for (int i = 0; i < beatsInd.Count; i++)
                beatsNew[i] = beat[beatsInd[i]];
            return beatsNew;
        }
    }
}
