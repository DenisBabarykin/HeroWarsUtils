using System;
using System.Collections.Generic;
using System.Text;

namespace StatisticsManagement.InputModels;

internal class InputPlayer
{
    public string Name { get; set; } = "";
    public List<InputDay> InputDays { get; set; } = new List<InputDay>(7);
}
