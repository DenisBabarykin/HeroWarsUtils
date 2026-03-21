namespace StatisticsFormApp
{
    partial class MainWindow
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
            menuStrip = new MenuStrip();
            ToolStripMenuItemFile = new ToolStripMenuItem();
            ToolStripMenuItemExportExcel = new ToolStripMenuItem();
            ToolStripMenuItemParams = new ToolStripMenuItem();
            groupBoxActivity = new GroupBox();
            textBoxActivity = new TextBox();
            groupBoxTitanit = new GroupBox();
            textBoxTitanit = new TextBox();
            menuStrip.SuspendLayout();
            groupBoxActivity.SuspendLayout();
            groupBoxTitanit.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip
            // 
            menuStrip.ImageScalingSize = new Size(24, 24);
            menuStrip.Items.AddRange(new ToolStripItem[] { ToolStripMenuItemFile, ToolStripMenuItemParams });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(1027, 33);
            menuStrip.TabIndex = 3;
            menuStrip.Text = "menuStrip1";
            // 
            // ToolStripMenuItemFile
            // 
            ToolStripMenuItemFile.DropDownItems.AddRange(new ToolStripItem[] { ToolStripMenuItemExportExcel });
            ToolStripMenuItemFile.Name = "ToolStripMenuItemFile";
            ToolStripMenuItemFile.Size = new Size(69, 29);
            ToolStripMenuItemFile.Text = "Файл";
            // 
            // ToolStripMenuItemExportExcel
            // 
            ToolStripMenuItemExportExcel.Enabled = false;
            ToolStripMenuItemExportExcel.Name = "ToolStripMenuItemExportExcel";
            ToolStripMenuItemExportExcel.Size = new Size(239, 34);
            ToolStripMenuItemExportExcel.Text = "Экспорт в Excel";
            ToolStripMenuItemExportExcel.Click += ToolStripMenuItemExportExcel_Click;
            // 
            // ToolStripMenuItemParams
            // 
            ToolStripMenuItemParams.Name = "ToolStripMenuItemParams";
            ToolStripMenuItemParams.Size = new Size(123, 29);
            ToolStripMenuItemParams.Text = "Параметры";
            ToolStripMenuItemParams.Click += ToolStripMenuItemParams_Click;
            // 
            // groupBoxActivity
            // 
            groupBoxActivity.Controls.Add(textBoxActivity);
            groupBoxActivity.Location = new Point(12, 36);
            groupBoxActivity.Name = "groupBoxActivity";
            groupBoxActivity.Size = new Size(500, 752);
            groupBoxActivity.TabIndex = 4;
            groupBoxActivity.TabStop = false;
            groupBoxActivity.Text = "Активность (csv)";
            // 
            // textBoxActivity
            // 
            textBoxActivity.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBoxActivity.Location = new Point(6, 30);
            textBoxActivity.Multiline = true;
            textBoxActivity.Name = "textBoxActivity";
            textBoxActivity.ScrollBars = ScrollBars.Both;
            textBoxActivity.Size = new Size(488, 716);
            textBoxActivity.TabIndex = 0;
            textBoxActivity.TextChanged += textBoxActivity_TextChanged;
            // 
            // groupBoxTitanit
            // 
            groupBoxTitanit.Controls.Add(textBoxTitanit);
            groupBoxTitanit.Location = new Point(518, 36);
            groupBoxTitanit.Name = "groupBoxTitanit";
            groupBoxTitanit.Size = new Size(500, 752);
            groupBoxTitanit.TabIndex = 5;
            groupBoxTitanit.TabStop = false;
            groupBoxTitanit.Text = "Титанит (csv)";
            // 
            // textBoxTitanit
            // 
            textBoxTitanit.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBoxTitanit.Location = new Point(6, 30);
            textBoxTitanit.Multiline = true;
            textBoxTitanit.Name = "textBoxTitanit";
            textBoxTitanit.ScrollBars = ScrollBars.Both;
            textBoxTitanit.Size = new Size(487, 716);
            textBoxTitanit.TabIndex = 1;
            textBoxTitanit.TextChanged += textBoxTitanit_TextChanged;
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1027, 800);
            Controls.Add(groupBoxTitanit);
            Controls.Add(groupBoxActivity);
            Controls.Add(menuStrip);
            MainMenuStrip = menuStrip;
            Margin = new Padding(4);
            MaximizeBox = false;
            Name = "MainWindow";
            Text = "Менеджер статистики";
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            groupBoxActivity.ResumeLayout(false);
            groupBoxActivity.PerformLayout();
            groupBoxTitanit.ResumeLayout(false);
            groupBoxTitanit.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private MenuStrip menuStrip;
        private ToolStripMenuItem ToolStripMenuItemParams;
        private ToolStripMenuItem ToolStripMenuItemFile;
        private ToolStripMenuItem ToolStripMenuItemExportExcel;
        private GroupBox groupBoxActivity;
        private TextBox textBoxActivity;
        private GroupBox groupBoxTitanit;
        private TextBox textBoxTitanit;
    }
}
