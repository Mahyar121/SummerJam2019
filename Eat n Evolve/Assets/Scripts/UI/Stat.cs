using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stat
{
    [SerializeField] private BarController bar;
    [SerializeField] private float maxStatValue;
    [SerializeField] private float currentStatValue;

    public float CurrentHp
    {
        get
        {
            return currentStatValue;
        }
        set
        {
            this.currentStatValue = Mathf.Clamp(value, 0, MaxHp);
            bar.Value = currentStatValue;
        }
    }

    public float MaxHp
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

    public void Initialize()
    {
        this.MaxHp = maxStatValue;
        this.CurrentHp = currentStatValue;
    }
}
