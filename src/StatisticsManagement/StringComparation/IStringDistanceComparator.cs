using System;
using System.Collections.Generic;
using System.Text;

namespace StatisticsManagement.StringComparation;

internal interface IStringDistanceComparator
{
    int CalcDistance(string str1, string str2);
}
