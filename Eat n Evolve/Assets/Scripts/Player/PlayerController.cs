﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Character
{
    // TODO: Add RandomCharacterTraitSelection() for when you kill the enemy enough times

    // Player Attributes
    [SerializeField] private HealthStat healthStat;
    [SerializeField] public Stat clawsStat;
    [SerializeField] private Stat hornsStat;
    [SerializeField] private Stat spikeStat;
    [SerializeField] private Stat scentyStat;
    [SerializeField] private Stat fishyStat;
    [SerializeField] private Stat stinkyStat;
    [SerializeField] private Stat sneakyStat;

    // character top down movement
    private Vector3 inputMovement;

    public bool HasClaws { get; set; }
    public bool HasHorns { get; set; }
    public bool HasSpikes { get; set; }
    public Transform StartPosition { get; set; }
    public float Health { get { return health; } set { health = value; } }
    public float Claws { get { return claws; }  set { claws = value; } }
    public float ClawsLevel { get { return clawsLevel; } set { clawsLevel = value; } }
    public float Horns { get { return horns; } set { horns = value; } }
    public float HornsLevel { get { return hornsLevel; } set { hornsLevel = value; } }
    public float Spike { get { return spike; } set { spike = value; } }
    public float SpikeLevel { get { return spikeLevel; } set { spikeLevel = value; } }
    public float Scenty { get { return scenty; } set { scenty = value; } }
    public float ScentyLevel { get { return scentyLevel; } set { scentyLevel = value; } }
    public float Fishy { get { return fishy; } set { fishy = value; } }
    public float FishyLevel { get { return fishyLevel; } set { fishyLevel = value; } }
    public float Stinky { get { return stinky; } set { stinky = value; } }
    public float StinkyLevel { get { return stinkyLevel; } set { stinkyLevel = value; } }
    public float Sneaky { get { return sneaky; } set { sneaky = value; } }
    public float SneakyLevel { get { return sneakyLevel; } set { sneakyLevel = value; } }

    // Creates a singleton of the Player so we dont make multiple instances of the player
    private static PlayerController instance;
    public static PlayerController Instance
    {
        get
        {
            if (!instance) { instance = GameObject.FindObjectOfType<PlayerController>(); }
            return instance;
        }
    }

    // Start is called before the first frame update
    public override void Start()
    {
        Initialize();
        base.Start();
        MyTransform = GetComponent<Transform>();
        MyRigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Put anything non physics related that needs updating here
    private void Update()
    {
        HandleInput();
        PlayerTraitLevelerHandler();
    }

    // Put anything physics related that needs updating here
    private void FixedUpdate()
    {
        HandleMovement();
    }

    public override void Initialize()
    {
        // needs barcontroller added otherwise it dies
        healthStat.Initialize();
        InitializeStat(Instance.clawsStat, Instance.Claws);
        InitializeStat(Instance.hornsStat, Instance.Horns);
        InitializeStat(Instance.spikeStat, Instance.Spike);
        InitializeStat(Instance.scentyStat, Instance.Scenty);
        InitializeStat(Instance.fishyStat, Instance.Fishy);
        InitializeStat(Instance.stinkyStat, Instance.Stinky);
        InitializeStat(Instance.sneakyStat, Instance.Sneaky);
        MyTransform = Instance.StartPosition;
        RandomCharacterTraitSelection();

    }

    // Will handle the top down movement of the player
    private void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        MyRigidBody2D.velocity = new Vector2(horizontal * movementSpeed, vertical * movementSpeed);
    }

    private void HandleInput()
    {
        // This is the attack
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // if the player has claws do the claws animation
            if (Instance.HasClaws)
            {
                Debug.Log("Striked with claws!");
            }
            // if player has horns do the horns animation
            if (Instance.HasHorns)
            {
                Debug.Log("Striked with horns!");
            }
            // if player has spikes do the spikes animation
            if (Instance.HasSpikes)
            {
                Debug.Log("Striked with spikes!");
            }
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (Instance.ClawsLevel < 3)
            {
                Debug.Log("Before Claw Value: " + clawsStat.CurrentStatValue);
                Instance.Claws += 10f;
                Instance.clawsStat.CurrentStatValue = Instance.Claws;
                Debug.Log("After Claw Value: " + clawsStat.CurrentStatValue);
            }

        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (Instance.HornsLevel < 3)
            {
                hornsStat.CurrentStatValue += 10;
            }
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            spikeStat.CurrentStatValue += 10;
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            scentyStat.CurrentStatValue += 10;
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            fishyStat.CurrentStatValue += 10;
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            stinkyStat.CurrentStatValue += 10;
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            sneakyStat.CurrentStatValue += 10;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (healthStat.CurrentHp > 0)
            {
                health -= 10;
                healthStat.CurrentHp = health;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }

    // when the player dies
    public override void Death()
    {
        //Initialize();
    }

    // Randomly selects a character trait for the player
    public void RandomCharacterTraitSelection()
    {
        Instance.HasClaws = false;
        Instance.HasHorns = false;
        Instance.HasSpikes = false;

        int characterTraitState = Random.Range(1, 4);
        Debug.Log("Trait Number:" + characterTraitState);
        switch (characterTraitState) {
            case 1:
                Instance.HasClaws = true;
                Debug.Log("Player got claws!");
                break;
            case 2:
                Instance.HasHorns = true;
                Debug.Log("Player got horns!");
                break;
            case 3:
                Instance.HasSpikes = true;
                Debug.Log("Player got spikes!");
                break;
            default:
                Instance.HasClaws = true;
                Debug.Log("Defaulted the player to claws!");
                break;
        }
      
    }

    private void PlayerTraitLevelerHandler()
    {
        if (Instance.Claws >= epRequired && Instance.ClawsLevel != 3)
        {
            Instance.Claws = 0;
            InitializeStat(Instance.clawsStat, Instance.Claws);
            Instance.ClawsLevel++;
            UpdateCharacterStats();
        }
        if (Instance.Horns >= epRequired && Instance.HornsLevel != 3)
        {
            Instance.Horns = 0;
            InitializeStat(Instance.hornsStat, Instance.Horns);
            Instance.HornsLevel++;
            UpdateCharacterStats();
        }
        if (Instance.Spike >= epRequired && Instance.SpikeLevel != 3)
        {
            Instance.Spike = 0;
            InitializeStat(Instance.spikeStat, Instance.Spike);
            Instance.SpikeLevel++;
            UpdateCharacterStats();
        }
        if (Instance.Scenty >= epRequired && Instance.ScentyLevel !=3)
        {
            Instance.Scenty = 0;
            InitializeStat(Instance.scentyStat, Instance.Scenty);
            Instance.ScentyLevel++;
            UpdateCharacterStats();
        }
        if (Instance.Fishy >= epRequired && Instance.FishyLevel !=3)
        {
            Instance.Fishy = 0;
            InitializeStat(Instance.fishyStat, Instance.Fishy);
            Instance.FishyLevel++;
            UpdateCharacterStats();
        }
        if (Instance.Stinky >= epRequired && Instance.StinkyLevel !=3)
        {
            Instance.Stinky = 0;
            InitializeStat(Instance.stinkyStat, Instance.Stinky);
            Instance.StinkyLevel++;
            UpdateCharacterStats();
        }
        if (Instance.Sneaky >= epRequired && Instance.SneakyLevel !=3)
        {
            Instance.Sneaky = 0;
            InitializeStat(Instance.sneakyStat, Instance.Sneaky);
            Instance.SneakyLevel++;
            UpdateCharacterStats();
        }
    }

    private void InitializeStat(Stat stat, float currentStat)
    {
        stat.CurrentStatValue = currentStat;
        stat.MaxStatValue = epRequired;
    }
}
