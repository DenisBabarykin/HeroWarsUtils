using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace StatisticsFormApp;

public partial class ParamsForm : Form
{
    public ParamsForm()
    {
        InitializeComponent();
    }

    private void ParamsForm_Load(object sender, EventArgs e)
    {
       DisplayValues();
    }

    private void buttonApply_Click(object sender, EventArgs e)
    {
        try
        {
            Params.DailyActivity = double.Parse(textBoxDailyActivity.Text);
            Params.WeeklyActivity = double.Parse(textBoxWeeklyActivity.Text);
            Params.DailyTitanit = double.Parse(textBoxDailyTitanit.Text);
            Params.WeeklyTitanit = double.Parse(textBoxWeeklyTitanit.Text);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Type: {ex.GetType().FullName}. Message: {ex.Message}");
        }
    }

    private void btnReset_Click(object sender, EventArgs e)
    {
        Params.Reset();
        DisplayValues();
    }

    private void DisplayValues()
    {
        textBoxDailyActivity.Text = Params.DailyActivity.ToString();
        textBoxWeeklyActivity.Text = Params.WeeklyActivity.ToString();
        textBoxDailyTitanit.Text = Params.DailyTitanit.ToString();
        textBoxWeeklyTitanit.Text = Params.WeeklyTitanit.ToString();
    }
}
