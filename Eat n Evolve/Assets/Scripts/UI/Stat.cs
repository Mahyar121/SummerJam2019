using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stat
{
    [SerializeField] private BarController bar;
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
            currentStatValue = Mathf.Clamp(value, 0, MaxStatValue);
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
            this.maxStatValue = value;
            bar.MaxValue = maxStatValue;
        }
    }

    public void Initialize(float currentStat, float maxStat)
    {
        MaxStatValue = currentStat;
        CurrentStatValue = maxStat;
    }

}
