namespace StatisticsFormApp
{
    partial class ParamsForm
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
            groupBoxActivityParams = new GroupBox();
            textBoxDailyActivity = new TextBox();
            textBoxWeeklyActivity = new TextBox();
            labelWeeklyActivity = new Label();
            labelDailyActivity = new Label();
            groupBoxTitanitParams = new GroupBox();
            textBoxDailyTitanit = new TextBox();
            textBoxWeeklyTitanit = new TextBox();
            labelWeeklyTitanit = new Label();
            labelDailyTitanit = new Label();
            buttonApply = new Button();
            btnReset = new Button();
            groupBoxActivityParams.SuspendLayout();
            groupBoxTitanitParams.SuspendLayout();
            SuspendLayout();
            // 
            // groupBoxActivityParams
            // 
            groupBoxActivityParams.Controls.Add(textBoxDailyActivity);
            groupBoxActivityParams.Controls.Add(textBoxWeeklyActivity);
            groupBoxActivityParams.Controls.Add(labelWeeklyActivity);
            groupBoxActivityParams.Controls.Add(labelDailyActivity);
            groupBoxActivityParams.Location = new Point(12, 12);
            groupBoxActivityParams.Name = "groupBoxActivityParams";
            groupBoxActivityParams.Size = new Size(243, 98);
            groupBoxActivityParams.TabIndex = 4;
            groupBoxActivityParams.TabStop = false;
            groupBoxActivityParams.Text = "Норма очков Активности";
            // 
            // textBoxDailyActivity
            // 
            textBoxDailyActivity.Location = new Point(7, 56);
            textBoxDailyActivity.Margin = new Padding(4);
            textBoxDailyActivity.Name = "textBoxDailyActivity";
            textBoxDailyActivity.Size = new Size(65, 31);
            textBoxDailyActivity.TabIndex = 0;
            // 
            // textBoxWeeklyActivity
            // 
            textBoxWeeklyActivity.Location = new Point(131, 56);
            textBoxWeeklyActivity.Margin = new Padding(4);
            textBoxWeeklyActivity.Name = "textBoxWeeklyActivity";
            textBoxWeeklyActivity.Size = new Size(65, 31);
            textBoxWeeklyActivity.TabIndex = 2;
            // 
            // labelWeeklyActivity
            // 
            labelWeeklyActivity.AutoSize = true;
            labelWeeklyActivity.Location = new Point(131, 27);
            labelWeeklyActivity.Margin = new Padding(4, 0, 4, 0);
            labelWeeklyActivity.Name = "labelWeeklyActivity";
            labelWeeklyActivity.Size = new Size(89, 25);
            labelWeeklyActivity.TabIndex = 3;
            labelWeeklyActivity.Text = "В неделю";
            // 
            // labelDailyActivity
            // 
            labelDailyActivity.AutoSize = true;
            labelDailyActivity.Location = new Point(7, 27);
            labelDailyActivity.Margin = new Padding(4, 0, 4, 0);
            labelDailyActivity.Name = "labelDailyActivity";
            labelDailyActivity.Size = new Size(65, 25);
            labelDailyActivity.TabIndex = 1;
            labelDailyActivity.Text = "В день";
            // 
            // groupBoxTitanitParams
            // 
            groupBoxTitanitParams.Controls.Add(textBoxDailyTitanit);
            groupBoxTitanitParams.Controls.Add(textBoxWeeklyTitanit);
            groupBoxTitanitParams.Controls.Add(labelWeeklyTitanit);
            groupBoxTitanitParams.Controls.Add(labelDailyTitanit);
            groupBoxTitanitParams.Location = new Point(12, 116);
            groupBoxTitanitParams.Name = "groupBoxTitanitParams";
            groupBoxTitanitParams.Size = new Size(243, 98);
            groupBoxTitanitParams.TabIndex = 5;
            groupBoxTitanitParams.TabStop = false;
            groupBoxTitanitParams.Text = "Норма очков Титанита";
            // 
            // textBoxDailyTitanit
            // 
            textBoxDailyTitanit.Location = new Point(7, 56);
            textBoxDailyTitanit.Margin = new Padding(4);
            textBoxDailyTitanit.Name = "textBoxDailyTitanit";
            textBoxDailyTitanit.Size = new Size(65, 31);
            textBoxDailyTitanit.TabIndex = 0;
            // 
            // textBoxWeeklyTitanit
            // 
            textBoxWeeklyTitanit.Location = new Point(131, 56);
            textBoxWeeklyTitanit.Margin = new Padding(4);
            textBoxWeeklyTitanit.Name = "textBoxWeeklyTitanit";
            textBoxWeeklyTitanit.Size = new Size(65, 31);
            textBoxWeeklyTitanit.TabIndex = 2;
            // 
            // labelWeeklyTitanit
            // 
            labelWeeklyTitanit.AutoSize = true;
            labelWeeklyTitanit.Location = new Point(131, 27);
            labelWeeklyTitanit.Margin = new Padding(4, 0, 4, 0);
            labelWeeklyTitanit.Name = "labelWeeklyTitanit";
            labelWeeklyTitanit.Size = new Size(89, 25);
            labelWeeklyTitanit.TabIndex = 3;
            labelWeeklyTitanit.Text = "В неделю";
            // 
            // labelDailyTitanit
            // 
            labelDailyTitanit.AutoSize = true;
            labelDailyTitanit.Location = new Point(7, 27);
            labelDailyTitanit.Margin = new Padding(4, 0, 4, 0);
            labelDailyTitanit.Name = "labelDailyTitanit";
            labelDailyTitanit.Size = new Size(65, 25);
            labelDailyTitanit.TabIndex = 1;
            labelDailyTitanit.Text = "В день";
            // 
            // buttonApply
            // 
            buttonApply.Location = new Point(12, 220);
            buttonApply.Name = "buttonApply";
            buttonApply.Size = new Size(112, 34);
            buttonApply.TabIndex = 6;
            buttonApply.Text = "Применить";
            buttonApply.UseVisualStyleBackColor = true;
            buttonApply.Click += buttonApply_Click;
            // 
            // btnReset
            // 
            btnReset.Location = new Point(143, 220);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(112, 34);
            btnReset.TabIndex = 7;
            btnReset.Text = "Сбросить";
            btnReset.UseVisualStyleBackColor = true;
            btnReset.Click += btnReset_Click;
            // 
            // ParamsForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(268, 264);
            Controls.Add(btnReset);
            Controls.Add(buttonApply);
            Controls.Add(groupBoxTitanitParams);
            Controls.Add(groupBoxActivityParams);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ParamsForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Параметры";
            Load += ParamsForm_Load;
            groupBoxActivityParams.ResumeLayout(false);
            groupBoxActivityParams.PerformLayout();
            groupBoxTitanitParams.ResumeLayout(false);
            groupBoxTitanitParams.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBoxActivityParams;
        private TextBox textBoxDailyActivity;
        private TextBox textBoxWeeklyActivity;
        private Label labelWeeklyActivity;
        private Label labelDailyActivity;
        private GroupBox groupBoxTitanitParams;
        private TextBox textBoxDailyTitanit;
        private TextBox textBoxWeeklyTitanit;
        private Label labelWeeklyTitanit;
        private Label labelDailyTitanit;
        private Button buttonApply;
        private Button btnReset;
    }
}