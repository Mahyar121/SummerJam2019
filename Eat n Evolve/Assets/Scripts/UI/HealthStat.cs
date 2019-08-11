using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HealthStat
{
    [SerializeField] public HealthBarController bar;
    [SerializeField] private float maxHp;
    [SerializeField] private float currentHp;

    public float CurrentHp
    {
        get
        {
            return currentHp;
        }
        set
        {
            currentHp = value;
            bar.Value = currentHp;
        }
    }

    public float MaxHp
    {
        get
        {
            return maxHp;
        }
        set
        {
            maxHp = value;
            bar.MaxValue = maxHp;
        }
    }

    public void Initialize()
    {
        CurrentHp = currentHp;
        MaxHp = maxHp;
    }
}
