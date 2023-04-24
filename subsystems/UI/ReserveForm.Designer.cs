using Entity;
using Controller;
namespace Boundary
{
    partial class ReserveForm
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
            components = new System.ComponentModel.Container();
            label1 = new Label();
            monthCalendar1 = new MonthCalendar();
            From = new ListBox();
            To = new ListBox();
            button1 = new Button();
            label2 = new Label();
            roomBindingSource = new BindingSource(components);
            panel1 = new Panel();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            ((System.ComponentModel.ISupportInitialize)roomBindingSource).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(37, 42);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(157, 20);
            label1.TabIndex = 4;
            label1.Text = "Room Reservation";
            label1.Click += label1_Click;
            // 
            // monthCalendar1
            // 
            monthCalendar1.Location = new Point(326, 149);
            monthCalendar1.Margin = new Padding(10);
            monthCalendar1.Name = "monthCalendar1";
            monthCalendar1.SelectionRange = new SelectionRange(new DateTime(2023, 4, 24, 0, 0, 0, 0), new DateTime(2023, 4, 30, 0, 0, 0, 0));
            monthCalendar1.TabIndex = 5;
            monthCalendar1.DateChanged += monthCalendar1_DateChanged;
            // 
            // From
            // 
            From.FormattingEnabled = true;
            From.ItemHeight = 15;
            From.Location = new Point(640, 216);
            From.Margin = new Padding(4, 3, 4, 3);
            From.Name = "From";
            From.Size = new Size(240, 19);
            From.TabIndex = 7;
            From.SelectedIndexChanged += From_SelectedIndexChanged;
            // 
            // To
            // 
            To.FormattingEnabled = true;
            To.ItemHeight = 15;
            To.Location = new Point(640, 292);
            To.Margin = new Padding(4, 3, 4, 3);
            To.Name = "To";
            To.Size = new Size(240, 19);
            To.TabIndex = 8;
            To.SelectedIndexChanged += To_SelectedIndexChanged;
            // 
            // button1
            // 
            button1.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point);
            button1.Location = new Point(326, 397);
            button1.Margin = new Padding(4, 3, 4, 3);
            button1.Name = "button1";
            button1.Size = new Size(214, 65);
            button1.TabIndex = 9;
            button1.Text = "Make Reservation";
            button1.UseVisualStyleBackColor = true;
            button1.Click += submit;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = SystemColors.ControlLightLight;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(640, 149);
            label2.Name = "label2";
            label2.Size = new Size(52, 21);
            label2.TabIndex = 10;
            label2.Text = "Time:";
            label2.Click += label2_Click;
            // 
            // roomBindingSource
            // 
            roomBindingSource.DataSource = typeof(Room);
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.Window;
            panel1.Controls.Add(label6);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label3);
            panel1.Location = new Point(37, 149);
            panel1.Name = "panel1";
            panel1.Size = new Size(239, 162);
            panel1.TabIndex = 11;
            panel1.Paint += panel1_Paint;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = SystemColors.ControlLightLight;
            label6.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label6.Location = new Point(158, 85);
            label6.Name = "label6";
            label6.Size = new Size(137, 21);
            label6.TabIndex = 15;
            label6.Text = "Placeholder Text";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = SystemColors.ControlLightLight;
            label5.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label5.Location = new Point(158, 44);
            label5.Name = "label5";
            label5.Size = new Size(137, 21);
            label5.TabIndex = 14;
            label5.Text = "Placeholder Text";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = SystemColors.ControlLightLight;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(25, 85);
            label4.Name = "label4";
            label4.Size = new Size(75, 21);
            label4.TabIndex = 13;
            label4.Text = "Building:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = SystemColors.ControlLightLight;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(25, 44);
            label3.Name = "label3";
            label3.Size = new Size(127, 21);
            label3.TabIndex = 12;
            label3.Text = "Room Number:";
            // 
            // ReserveForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(933, 519);
            Controls.Add(panel1);
            Controls.Add(label2);
            Controls.Add(button1);
            Controls.Add(To);
            Controls.Add(From);
            Controls.Add(monthCalendar1);
            Controls.Add(label1);
            Margin = new Padding(4, 3, 4, 3);
            Name = "ReserveForm";
            Text = "ReserveForm";
            ((System.ComponentModel.ISupportInitialize)roomBindingSource).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MonthCalendar monthCalendar1;
        private System.Windows.Forms.ListBox From;
        private System.Windows.Forms.ListBox To;
        private System.Windows.Forms.Button button1;
        private Label label2;
        private BindingSource roomBindingSource;
        private Panel panel1;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
    }
}