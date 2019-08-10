﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{

    // Universal Character stats
    [SerializeField] protected int coreLevelMultiplier = 2;
    [SerializeField] protected int damage = 5;
    [SerializeField] protected float movementSpeed = 5f;
    [SerializeField] protected int epRequired = 10;
    [SerializeField] protected int claws = 0;
    [SerializeField] protected int clawsLevel = 0;
    [SerializeField] protected int horns = 0;
    [SerializeField] protected int hornsLevel = 0;
    [SerializeField] protected int spike = 0;
    [SerializeField] protected int spikeLevel = 0;
    [SerializeField] protected int scenty = 0;
    [SerializeField] protected int scentyLevel = 0;
    [SerializeField] protected int fishy = 0;
    [SerializeField] protected int fishyLevel = 0;
    [SerializeField] protected int stinky = 0;
    [SerializeField] protected int stinkyLevel = 0;
    [SerializeField] protected int sneaky = 0;
    [SerializeField] protected int sneakyLevel = 0;

    public abstract void Death();
    public abstract void Initialize();

    public virtual void Start()
    {
        UpdateCharacterStats();
    }

    public virtual void Update()
    {
        UpdateCharacterStats();
    }

    private void UpdateCharacterStats()
    {
        if (clawsLevel != 0)
        {
            damage = damage * coreLevelMultiplier * clawsLevel;
        }
        if (hornsLevel != 0)
        {
            damage = damage * coreLevelMultiplier * hornsLevel;
        }
        if (spikeLevel != 0)
        {
            damage = damage * coreLevelMultiplier * spikeLevel;
        }
    }
}
