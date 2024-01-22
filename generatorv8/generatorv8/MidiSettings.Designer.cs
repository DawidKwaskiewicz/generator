
namespace generatorv8
{
    partial class MidiSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.checkTransposeLead = new System.Windows.Forms.CheckBox();
            this.checkTransposeRhythm = new System.Windows.Forms.CheckBox();
            this.checkTransposeBass = new System.Windows.Forms.CheckBox();
            this.buttonApply = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // checkTransposeLead
            // 
            this.checkTransposeLead.AutoSize = true;
            this.checkTransposeLead.Location = new System.Drawing.Point(13, 13);
            this.checkTransposeLead.Name = "checkTransposeLead";
            this.checkTransposeLead.Size = new System.Drawing.Size(243, 24);
            this.checkTransposeLead.TabIndex = 0;
            this.checkTransposeLead.Text = "Transpose lead guitar +1 octave";
            this.checkTransposeLead.UseVisualStyleBackColor = true;
            // 
            // checkTransposeRhythm
            // 
            this.checkTransposeRhythm.AutoSize = true;
            this.checkTransposeRhythm.Location = new System.Drawing.Point(13, 43);
            this.checkTransposeRhythm.Name = "checkTransposeRhythm";
            this.checkTransposeRhythm.Size = new System.Drawing.Size(260, 24);
            this.checkTransposeRhythm.TabIndex = 0;
            this.checkTransposeRhythm.Text = "Transpose rhythm guitar +1 octave";
            this.checkTransposeRhythm.UseVisualStyleBackColor = true;
            // 
            // checkTransposeBass
            // 
            this.checkTransposeBass.AutoSize = true;
            this.checkTransposeBass.Location = new System.Drawing.Point(13, 73);
            this.checkTransposeBass.Name = "checkTransposeBass";
            this.checkTransposeBass.Size = new System.Drawing.Size(243, 24);
            this.checkTransposeBass.TabIndex = 0;
            this.checkTransposeBass.Text = "Transpose bass guitar +1 octave";
            this.checkTransposeBass.UseVisualStyleBackColor = true;
            // 
            // buttonApply
            // 
            this.buttonApply.Location = new System.Drawing.Point(13, 108);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(94, 29);
            this.buttonApply.TabIndex = 1;
            this.buttonApply.Text = "Apply";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(179, 108);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(94, 29);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // MidiSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(288, 149);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonApply);
            this.Controls.Add(this.checkTransposeBass);
            this.Controls.Add(this.checkTransposeRhythm);
            this.Controls.Add(this.checkTransposeLead);
            this.Name = "MidiSettings";
            this.Text = "MIDI settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MidiSettings_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkTransposeLead;
        private System.Windows.Forms.CheckBox checkTransposeRhythm;
        private System.Windows.Forms.CheckBox checkTransposeBass;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.Button buttonCancel;
    }
}