using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stat
{
    [SerializeField] public BarController bar;
    private float maxStatValue;
    private float currentStatValue;

    public float CurrentStatValue
    {
        get
        {
            return currentStatValue;
        }
        set
        {
            currentStatValue = value;
            bar.Value = currentStatValue;
        }
    }

    public float MaxStatValue
    {
        get
        {
            return maxStatValue;
        }
        set
        {
            maxStatValue = value;
            bar.MaxValue = maxStatValue;
        }
    }

}
