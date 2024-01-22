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
    public partial class StrongBeats : Form
    {
        private static CheckBox[] checks;
        private static readonly List<int> meters = new();
        private static readonly List<int> metersStart = new();
        private static int meterup;
        public static bool SuppressCheckedChanged { get; set; }
        public bool Exists { get; set; }
        private readonly Main formMain;
        public StrongBeats(Main formMain)
        {
            InitializeComponent();
            checks = new CheckBox[20]
            {
                checkBox1, checkBox2, checkBox3, checkBox4, checkBox5,
                checkBox6, checkBox7, checkBox8, checkBox9, checkBox10,
                checkBox11, checkBox12, checkBox13, checkBox14, checkBox15,
                checkBox16, checkBox17, checkBox18, checkBox19, checkBox20
            };
            SuppressCheckedChanged = false;
            Exists = true;
            this.formMain = formMain;
        }
        public static void UpdateChecks(List<int> strongBeats, int _meterup)
        {
            SuppressCheckedChanged = true;
            meterup = _meterup;
            meters.Clear();
            metersStart.Clear();
            for (int i = 0; i < strongBeats.Count; i++)
            {
                checks[strongBeats[i]].Checked = true;
                metersStart.Add(strongBeats[i] + 1);
                if (i != strongBeats.Count - 1) meters.Add(strongBeats[i + 1] - strongBeats[i]);
                else meters.Add(meterup - strongBeats[i]);
            }
            for (int i = 0; i < checks.Length; i++)
                if (!strongBeats.Contains(i)) checks[i].Checked = false;
            for (int i = 0; i < meterup; i++)
                checks[i].Visible = true;
            for (int i = meterup; i < checks.Length; i++)
                checks[i].Visible = false;
            checks[meterup - 1].AutoCheck = false;
            SuppressCheckedChanged = false;

            //SuppressCheckedChanged = true;
            //List<int> strongBeats = new();
            //strongBeats.Add(0);
            //int metersum = 0;
            ////int meterup = FormMain.Meterup;
            //checks[meterup - 1].AutoCheck = false;

            ////if (meterup > 3)
            ////    while (metersum < meterup)
            ////    {
            ////        if (meterup - metersum == 3) break;
            ////        if (meterup - metersum > 4)
            ////        {
            ////            metersum += 3;
            ////            if (metersum == meterup) break;
            ////            strongBeats.Add(metersum);
            ////        }
            ////        else
            ////        {
            ////            metersum += 2;
            ////            if (metersum == meterup) break;
            ////            strongBeats.Add(metersum);
            ////        }
            ////    }

            //if (meterup % 3 == 0)
            //    for (; metersum < meterup; metersum += 3)
            //        strongBeats.Add(metersum);
            //else
            //{
            //    while (metersum < meterup)
            //    {
            //        if (meterup - metersum == 3)
            //        {
            //            strongBeats.Add(metersum);
            //            break;
            //        }
            //        else
            //        {
            //            strongBeats.Add(metersum);
            //            metersum += 2;
            //        }
            //    }
            //}

            //meters.Clear();
            //metersStart.Clear();
            //for (int i = 0; i < meterup; i++)
            //{
            //    if (strongBeats.Contains(i))
            //        checks[i].Checked = true;
            //    else checks[i].Checked = false;
            //    checks[i].Visible = true;
            //}

            //for (int i = meterup; i < checks.Length; i++)
            //{
            //    checks[i].Visible = false;
            //}

            //metersStart.Add(1);
            //for (int i = 0; i < strongBeats.Count; i++)
            //    if (i > 0)
            //    {
            //        meters.Add(strongBeats[i] - strongBeats[i - 1]);
            //        metersStart.Add(meters.Sum() + 1);
            //    }
            //meters.Add(meterup - strongBeats.Last());
            //SuppressCheckedChanged = false;
        }

        private void CheckedChanged(object sender, EventArgs e)
        {
            if (SuppressCheckedChanged) return;
            SuppressCheckedChanged = true;
            CheckBox checkBox = sender as CheckBox;
            int checkIndex = Array.IndexOf(checks, checkBox) + 1;
            int meterIndex = -1;
            //int changeIndex = -1;


            if (checkBox.Checked)
            {
                if (checkIndex > 1)
                {
                    if (checks[checkIndex - 2].Checked)
                    {
                        for (int i = 0; i < metersStart.Count; i++)
                        {
                            if (checkIndex < metersStart[i])
                            {
                                meterIndex = i - 1;
                                break;
                            }
                        }
                        if (meterIndex == -1) meterIndex = metersStart.Count - 1;
                        //if (meters.GetRange(0, meterIndex).Contains(3))
                        //{
                        //    changeIndex = meters.GetRange(0, meterIndex).LastIndexOf(3);
                        //    meters[changeIndex] = 2;
                        //    checks[metersStart[changeIndex + 1] - 1].Checked = false;
                        //    metersStart[changeIndex + 1]--;
                        //    checks[metersStart[changeIndex + 1] - 1].Checked = true;
                        //    meters[meterIndex] = 2;
                        //    meters.Insert(meterIndex + 1, 2);
                        //    metersStart.Insert(meterIndex + 1, checkIndex);
                        //    for (int i = 2; i < meterIndex - changeIndex + 1; i++)
                        //    {
                        //        checks[metersStart[changeIndex + i] - 1].Checked = false;
                        //        metersStart[changeIndex + i]--;
                        //        checks[metersStart[changeIndex + i] - 1].Checked = true;
                        //    }
                        //}
                        if (meters[meterIndex - 1] == 3)
                        {
                            meters[meterIndex - 1] = 2;
                            checks[metersStart[meterIndex] - 1].Checked = false;
                            metersStart[meterIndex]--;
                            checks[metersStart[meterIndex] - 1].Checked = true;
                            meters[meterIndex] = 2;
                            meters.Insert(meterIndex + 1, 2);
                            metersStart.Insert(meterIndex + 1, checkIndex);
                        }
                        else
                        {
                            meters[meterIndex] = 2;
                            meters[meterIndex - 1] = 3;
                            metersStart[meterIndex]++;
                            checks[checkIndex - 2].Checked = false;
                        }
                    }
                    else if (checkIndex > 2)
                    {
                        if (checks[checkIndex - 3].Checked)
                        {
                            for (int i = 0; i < metersStart.Count; i++)
                            {
                                if (checkIndex < metersStart[i])
                                {
                                    meterIndex = i - 1;
                                    break;
                                }
                            }
                            meters[meterIndex] = 2;
                            meters.Insert(meterIndex + 1, 2);
                            metersStart.Insert(meterIndex + 1, checkIndex);
                        }
                    }
                }
                if (checks[checkIndex].Checked)
                {
                    for (int i = 0; i < metersStart.Count; i++)
                    {
                        if (checkIndex < metersStart[i])
                        {
                            meterIndex = i - 1;
                            break;
                        }
                    }
                    //if (meters.GetRange(meterIndex + 1, meters.Count - meterIndex - 1).Contains(3))
                    //{
                    //    changeIndex = meters.GetRange(meterIndex + 1, meters.Count - meterIndex - 1).IndexOf(3) + meterIndex + 1;
                    //    for (int i = meterIndex + 1; i < changeIndex + 1; i++)
                    //    {
                    //        checks[metersStart[i] - 1].Checked = false;
                    //        metersStart[i]++;
                    //        checks[metersStart[i] - 1].Checked = true;
                    //        if (i == changeIndex) meters[i] = 2;
                    //    }
                    //}
                    if (meters[meterIndex + 1] == 3)
                    {
                        meters[meterIndex + 1] = 2;
                        checks[metersStart[meterIndex + 1] - 1].Checked = false;
                        metersStart[meterIndex + 1]++;
                        checks[metersStart[meterIndex + 1] - 1].Checked = true;
                    }
                    //else
                    //{
                    //    checks[metersStart[^1] - 1].Checked = false;
                    //    meters.RemoveAt(meters.Count - 1);
                    //    metersStart.RemoveAt(metersStart.Count - 1);
                    //    meters[^1] = 3;
                    //    for (int i = meterIndex + 1; i < meters.Count; i++)
                    //    {
                    //        checks[metersStart[i] - 1].Checked = false;
                    //        metersStart[i]++;
                    //        checks[metersStart[i] - 1].Checked = true;
                    //    }
                    //}
                    else if (meters[meterIndex + 1] == 2)
                    {
                        checks[metersStart[meterIndex + 1] - 1].Checked = false;
                        meters.RemoveAt(meterIndex + 1);
                        metersStart.RemoveAt(meterIndex + 1);
                        meters[meterIndex] = 3;
                    }
                }
            }
            else
            {
                checkBox.Checked = true;
            }
            SuppressCheckedChanged = false;
            List<int> strongBeats = new();
            for (int i = 0; i < metersStart.Count; i++)
                strongBeats.Add(metersStart[i] - 1);
            formMain.StrongBeats[formMain.CurrentPhrase] = strongBeats;

            if (formMain.CurrentPhrase == formMain.PhrasesAllowed.Length)
                for (int i = 0; i < formMain.PhrasesAllowed.Length; i++)
                    formMain.StrongBeats[i] = strongBeats;
        }
        public static void EnableCheck(int checkIndex)
        {
            checks[checkIndex].AutoCheck = true;
        }

        private void StrongBeats_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }
    }
}
