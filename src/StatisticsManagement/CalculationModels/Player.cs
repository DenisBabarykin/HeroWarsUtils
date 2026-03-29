using System;
using System.Collections.Generic;
using System.Text;

namespace StatisticsManagement.CalculationModels;

internal class Player
{
    public string Name { get; } = "UnknownPlayer";

    public List<double> ActivityPoints { get; } = [];

    public List<double> TitanitPoints { get; } = [];

}
