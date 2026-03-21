namespace StatisticsFormApp
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ToolStripMenuItemParams_Click(object sender, EventArgs e)
        {
            var paramsForm = new ParamsForm();
            paramsForm.ShowDialog();
        }
    }
}
