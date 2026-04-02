using SoftWx.Match;
using System;
using System.Collections.Generic;
using System.Text;

namespace StatisticsManagement.StringComparation;

internal class SoftWxDamerauComparator : IStringDistanceComparator
{
    public int CalcDistance(string str1, string str2)
    {
        return Distance.DamerauOSA(str1, str2);
    }
}
