namespace zad_1_posk
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            label1 = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            kolor = new ComboBox();
            panel_analog = new Panel();
            comboBox1 = new ComboBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 20F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(104, 76);
            label1.Name = "label1";
            label1.Size = new Size(173, 54);
            label1.TabIndex = 0;
            label1.Text = "00:00:00";
            label1.Visible = false;
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            // 
            // kolor
            // 
            kolor.FormattingEnabled = true;
            kolor.Items.AddRange(new object[] { "niebieski", "czerwony", "zielony" });
            kolor.Location = new Point(532, 1);
            kolor.Name = "kolor";
            kolor.Size = new Size(182, 33);
            kolor.TabIndex = 1;
            kolor.SelectedIndexChanged += combotheme_SelectedIndexChanged;
            // 
            // panel_analog
            // 
            panel_analog.BackColor = SystemColors.ControlLightLight;
            panel_analog.Location = new Point(24, 150);
            panel_analog.Name = "panel_analog";
            panel_analog.Size = new Size(320, 239);
            panel_analog.TabIndex = 2;
            panel_analog.Visible = false;
            panel_analog.Paint += panel_analog_Paint;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "cyfrowy", "analogowy" });
            comboBox1.Location = new Point(344, 1);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(182, 33);
            comboBox1.TabIndex = 0;
            comboBox1.Text = "typ zegara";
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(comboBox1);
            Controls.Add(panel_analog);
            Controls.Add(kolor);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private System.Windows.Forms.Timer timer1;
        private ComboBox kolor;
        private Panel panel_analog;
        private ComboBox comboBox1;
    }
}
