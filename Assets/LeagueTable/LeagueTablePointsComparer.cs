

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeagueTablePointsComparer : System.Collections.IComparer
{
    public int Compare(object x, object y)
    {
        if (((Team)x).points < ((Team)y).points)
        {
            return 1;
        }
        else if (((Team)x).points > ((Team)y).points)
        {
            return -1;
        }
        else return 0;
    }
}
