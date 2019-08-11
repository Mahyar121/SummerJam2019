using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Character
{
    // TODO: Add RandomCharacterTraitSelection() for when you kill the enemy enough times

    // Player Attributes
    [SerializeField] private Stat healthStat;
    [SerializeField] private Stat clawsStat;
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
    public int Claws { get { return claws; }  set { claws = value; } }
    public int ClawsLevel { get { return clawsLevel; } set { clawsLevel = value; } }
    public int Horns { get { return horns; } set { horns = value; } }
    public int HornsLevel { get { return hornsLevel; } set { hornsLevel = value; } }
    public int Spike { get { return spike; } set { spike = value; } }
    public int SpikeLevel { get { return spikeLevel; } set { spikeLevel = value; } }
    public int Scenty { get { return scenty; } set { scenty = value; } }
    public int ScentyLevel { get { return scentyLevel; } set { scentyLevel = value; } }
    public int Fishy { get { return fishy; } set { fishy = value; } }
    public int FishyLevel { get { return fishyLevel; } set { fishyLevel = value; } }
    public int Stinky { get { return stinky; } set { stinky = value; } }
    public int StinkyLevel { get { return stinkyLevel; } set { stinkyLevel = value; } }
    public int Sneaky { get { return sneaky; } set { sneaky = value; } }
    public int SneakyLevel { get { return sneakyLevel; } set { sneakyLevel = value; } }

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
        base.Start();
        Initialize();
        MyTransform = GetComponent<Transform>();
        MyRigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Put anything non physics related that needs updating here
    public override void Update()
    {
        base.Update();
        HandleInput();
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
        clawsStat.Initialize();
        hornsStat.Initialize();
        spikeStat.Initialize();
        scentyStat.Initialize();
        fishyStat.Initialize();
        stinkyStat.Initialize();
        sneakyStat.Initialize();
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
            clawsStat.CurrentStatValue += 10;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            hornsStat.CurrentStatValue += 10;
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
            healthStat.CurrentStatValue -= 10;
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
        Initialize();
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
        if (Instance.Claws >= epRequired)
        {
            Instance.Claws = 0;
            Instance.ClawsLevel++;
        }
        if (Instance.Horns >= epRequired)
        {
            Instance.Horns = 0;
            Instance.HornsLevel++;
        }
        if (Instance.Spike >= epRequired)
        {
            Instance.Spike = 0;
            Instance.SpikeLevel++;
        }
        if (Instance.Scenty >= epRequired)
        {
            Instance.Scenty = 0;
            Instance.ScentyLevel++;
        }
        if (Instance.Fishy >= epRequired)
        {
            Instance.Fishy = 0;
            Instance.FishyLevel++;
        }
        if (Instance.Stinky >= epRequired)
        {
            Instance.Stinky = 0;
            Instance.StinkyLevel++;
        }
        if (Instance.Sneaky >= epRequired)
        {
            Instance.Sneaky = 0;
            Instance.SneakyLevel++;
        }
    }
}
