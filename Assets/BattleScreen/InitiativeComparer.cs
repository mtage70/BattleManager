using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitiativeComparer : System.Collections.IComparer{

    

    public int Compare(object x, object y)
    {
        if (((Character)x).initiativeRoll < ((Character)y).initiativeRoll)
        {
            return -1;
        }
        else if (((Character)x).initiativeRoll > ((Character)y).initiativeRoll)
        {
            return 1;
        }
        else return 0;
    }
}
