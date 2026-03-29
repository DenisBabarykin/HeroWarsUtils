using StatisticsManagement;

namespace StatisticsFormApp;

public partial class MainWindow : Form
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void ToolStripMenuItemParams_Click(object sender, EventArgs e)
    {
        new ParamsForm().ShowDialog();
    }

    private void textBoxActivity_TextChanged(object sender, EventArgs e)
    {
        CheckExportAbility();
    }

    private void textBoxTitanit_TextChanged(object sender, EventArgs e)
    {
        CheckExportAbility();
    }

    private void ToolStripMenuItemExportExcel_Click(object sender, EventArgs e)
    {
        var dialog = new SaveFileDialog
        {
            Filter = "Excel файлы (*.xlsx)|*.xlsx",
            DefaultExt = "xlsx",
            FileName = $"Смотр {DateTime.Now:dd.MM.yyyy}",
            InitialDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads")
        };

        if (dialog.ShowDialog() == DialogResult.OK)
        {
            new StatisticsFacade().Process(textBoxActivity.Text, 
                textBoxTitanit.Text, 
                dialog.FileName, 
                new StatisticsConfig(Params.DailyActivity, Params.WeeklyActivity, Params.DailyTitanit, Params.WeeklyTitanit));
        }
    }

    private void CheckExportAbility()
    {
        if (string.IsNullOrWhiteSpace(textBoxActivity.Text) || string.IsNullOrWhiteSpace(textBoxTitanit.Text))
        {
            ToolStripMenuItemExportExcel.Enabled = false;
        }
        else
        {
            ToolStripMenuItemExportExcel.Enabled = true;
        }
    }
}
