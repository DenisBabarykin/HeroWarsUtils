namespace StatisticsFormApp;

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
            MessageBox.Show("Сохранено в: " + dialog.FileName);
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
