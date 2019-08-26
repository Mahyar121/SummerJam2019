﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Character
{

    // Player Attributes
    [SerializeField] private HealthStat healthStat;
    [SerializeField] public Stat clawsStat;
    [SerializeField] public Stat hornsStat;
    [SerializeField] public Stat spikeStat;
    [SerializeField] public Stat fishyStat;
    [SerializeField] public Stat sneakyStat;
    // Objects for Horns
    [SerializeField] public GameObject HornsObject;
    [SerializeField] public GameObject VFXCharge;
    // Objects for Claws
    [SerializeField] public GameObject LeftClaw;
    [SerializeField] public GameObject RightClaw;
    [SerializeField] public GameObject VFXClaw;
    // Objects for Spikes
    [SerializeField] public GameObject VFXSpike;
    [SerializeField] public GameObject SpikeObject;
    [SerializeField] public GameObject SpikeProjectile;
    [SerializeField] public GameObject[] SpikeSpawnLocs;

    // Charge logic
    private Vector3 chargeDestination;
    private float initialChargeTime = 1f;
    private float currentChargeTime;
    private float chargeSpeed = 20f;
    private bool isCharging = false;

    // Claw logic
    private float initialClawTime = 1f;
    private float currentClawTime;
    private bool isClawing = false;

    // Impale logic
    private float initialImpalingTime = 1f;
    private float currentImpalingTime;
    private bool isImpaling = false;

    // character top down movement
    private Vector3 inputMovement;
    public bool FreezeControls { get; set; }
    public bool HasClaws { get; set; }
    public bool HasHorns { get; set; }
    public bool HasSpikes { get; set; }
    public Transform StartPosition { get; set; }
    public bool IsImpaling { get { return IsImpaling; } }
    public float Damage { get { return damage; } } 
    public float Health { get { return health; } set { health = value; } }
    public float Claws { get { return claws; }  set { claws = value; } }
    public float ClawsLevel { get { return clawsLevel; } set { clawsLevel = value; } }
    public float Horns { get { return horns; } set { horns = value; } }
    public float HornsLevel { get { return hornsLevel; } set { hornsLevel = value; } }
    public float Spike { get { return spike; } set { spike = value; } }
    public float SpikeLevel { get { return spikeLevel; } set { spikeLevel = value; } }
    public float Fishy { get { return fishy; } set { fishy = value; } }
    public float FishyLevel { get { return fishyLevel; } set { fishyLevel = value; } }
    public float Sneaky { get { return sneaky; } set { sneaky = value; } }
    public float SneakyLevel { get { return sneakyLevel; } set { sneakyLevel = value; } }
    public Animator MyAnimator { get; set; }
    public Rigidbody2D MyRigidBody2D { get; set; }
    public Transform MyTransform { get; set; }


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
        VFXCharge.SetActive(false);
        MyAnimator = GetComponent<Animator>();
        MyTransform = GetComponent<Transform>();
        StartPosition = GetComponent<Transform>();
        MyRigidBody2D = GetComponent<Rigidbody2D>();
        FreezeControls = false;
        Instance.StartPosition.position = SceneManager.Instance.RandomizePlayerSpawn().position;
    }


    // Put anything non physics related that needs updating here
    private void Update()
    {

        HandleInput();
        HandleChargingCooldown();
        HandleClawingCooldown();
        HandleImpalingCooldown();
        PlayerTraitLevelerHandler();

    }

    // Put anything physics related that needs updating here
    private void FixedUpdate()
    {
        HandleMovement();
    }

    public override void Initialize()
    {
        healthStat.Initialize();
        InitializeStat(Instance.clawsStat, Instance.Claws);
        InitializeStat(Instance.hornsStat, Instance.Horns);
        InitializeStat(Instance.spikeStat, Instance.Spike);
        InitializeStat(Instance.fishyStat, Instance.Fishy);
        InitializeStat(Instance.sneakyStat, Instance.Sneaky);
        MyTransform = Instance.StartPosition;
        RandomCharacterTraitSelection();
        Instance.VFXCharge.SetActive(false);
        Instance.VFXClaw.SetActive(false);
    }

    // Will handle the top down movement of the player
    private void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        MyRigidBody2D.velocity = new Vector2(horizontal * movementSpeed, vertical * movementSpeed);
        MyAnimator.SetFloat("SpeedX", horizontal);
        MyAnimator.SetFloat("SpeedY", vertical);

        if (MyAnimator.GetFloat("SpeedX") == 0 && MyAnimator.GetFloat("SpeedY") == 0 && isCharging == false) 
        {
            MyAnimator.SetBool("IsIdle", true);
        }
        else
        {
            MyAnimator.SetBool("IsIdle", false);
        }


    }
   
    private void HandleInput()
    {
        // Player presses the attack button
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // if the player has claws do the claws animation
            if (Instance.HasClaws == true)
            {
                if (isClawing == false)
                {
                    if (MyAnimator.GetBool("FacingNorth") == true)
                    {
                        isClawing = true;
                        currentChargeTime = initialChargeTime;
                        Instance.VFXClaw.transform.localPosition = new Vector3(0, -3f, 0);
                        Instance.VFXClaw.transform.localRotation = Quaternion.Euler(0, 0, 90);
                        Instance.VFXClaw.transform.localScale = new Vector3(0.5f, 0.5f, 2);
                        VFXClaw.SetActive(true);
                        PlayerController.Instance.VFXClaw.GetComponent<ParticleSystem>().Play();
                        VFXClaw.GetComponent<Animator>().SetTrigger("ClawAttack");
                        Debug.Log("Striked with claws!");
                    }
                    if (MyAnimator.GetBool("FacingSouth") == true)
                    {
                        isClawing = true;
                        currentChargeTime = initialChargeTime;
                        Instance.VFXClaw.transform.localPosition = new Vector3(0, 3f, 0);
                        Instance.VFXClaw.transform.localRotation = Quaternion.Euler(0, 0, 90);
                        Instance.VFXClaw.transform.localScale = new Vector3(-0.5f, -0.5f, 2);
                        VFXClaw.SetActive(true);
                        PlayerController.Instance.VFXClaw.GetComponent<ParticleSystem>().Play();
                        VFXClaw.GetComponent<Animator>().SetTrigger("ClawAttack");
                        Debug.Log("Striked with claws!");
                    }
                    if (MyAnimator.GetBool("FacingWest") == true)
                    {
                        isClawing = true;
                        currentChargeTime = initialChargeTime;
                        Instance.VFXClaw.transform.localPosition = new Vector3(3f, 0, 0);
                        Instance.VFXClaw.transform.localRotation = Quaternion.Euler(0, 0, 0);
                        Instance.VFXClaw.transform.localScale = new Vector3(-0.5f, -0.5f, 2);
                        VFXClaw.SetActive(true);
                        PlayerController.Instance.VFXClaw.GetComponent<ParticleSystem>().Play();
                        VFXClaw.GetComponent<Animator>().SetTrigger("ClawAttack");
                        Debug.Log("Striked with claws!");
                    }
                    if (MyAnimator.GetBool("FacingEast") == true)
                    {
                        isClawing = true;
                        currentChargeTime = initialChargeTime;
                        Instance.VFXClaw.transform.localPosition = new Vector3(-3f, 0, 0);
                        Instance.VFXClaw.transform.localRotation = Quaternion.Euler(0, 0, 0);
                        Instance.VFXClaw.transform.localScale = new Vector3(0.5f, 0.5f, 2);
                        VFXClaw.SetActive(true);
                        PlayerController.Instance.VFXClaw.GetComponent<ParticleSystem>().Play();
                        VFXClaw.GetComponent<Animator>().SetTrigger("ClawAttack");
                        Debug.Log("Striked with claws!");
                    }
                }

            }
            // if player has horns do the horns animation
            if (Instance.HasHorns == true)
            {
                if (isCharging == false && FreezeControls == false)
                {
                    if (MyAnimator.GetBool("FacingNorth") == true)
                    {
                        isCharging = true;
                        currentChargeTime = initialChargeTime;
                        Instance.VFXCharge.transform.localPosition = new Vector3(0.2f, -2f, 0);
                        Instance.VFXCharge.transform.localRotation = Quaternion.Euler(0, 0, 90);
                        Instance.VFXCharge.transform.localScale = new Vector3(-2, 3, 2);
                        // Changes distance he moves to (the position.y + xx)
                        chargeDestination = new Vector3(MyTransform.position.x, MyTransform.position.y + chargeSpeed, MyTransform.position.z);
                        VFXCharge.SetActive(true);
                        VFXCharge.GetComponent<Animator>().SetTrigger("HornsAttack");
                        Debug.Log("Striked with horns!");
                    }
                    if (MyAnimator.GetBool("FacingSouth") == true)
                    {
                        isCharging = true;
                        currentChargeTime = initialChargeTime;
                        Instance.VFXCharge.transform.localPosition = new Vector3(0.2f, 2, 0);
                        Instance.VFXCharge.transform.localRotation = Quaternion.Euler(0, 0, 90);
                        Instance.VFXCharge.transform.localScale = new Vector3(2, 3, 2);
                        // Changes distance he moves to (the position.y - xx)
                        chargeDestination = new Vector3(MyTransform.position.x, MyTransform.position.y - chargeSpeed, MyTransform.position.z);
                        VFXCharge.SetActive(true);
                        VFXCharge.GetComponent<Animator>().SetTrigger("HornsAttack");
                        Debug.Log("Striked with horns!");
                    }
                    if (MyAnimator.GetBool("FacingWest") == true)
                    {
                        isCharging = true;
                        currentChargeTime = initialChargeTime;
                        Instance.VFXCharge.transform.localPosition = new Vector3(2f, 0, 0);
                        Instance.VFXCharge.transform.localRotation = Quaternion.Euler(0, 0, 0);
                        Instance.VFXCharge.transform.localScale = new Vector3(2, 3, 2);
                        // Changes distance he moves to (the position.x - xx)
                        chargeDestination = new Vector3(MyTransform.position.x - chargeSpeed, MyTransform.position.y, MyTransform.position.z);
                        VFXCharge.SetActive(true);
                        VFXCharge.GetComponent<Animator>().SetTrigger("HornsAttack");
                        Debug.Log("Striked with horns!");
                    }
                    if (MyAnimator.GetBool("FacingEast") == true)
                    {
                        isCharging = true;
                        currentChargeTime = initialChargeTime;
                        Instance.VFXCharge.transform.localPosition = new Vector3(-2f, 0, 0);
                        Instance.VFXCharge.transform.localRotation = Quaternion.Euler(0, 0, 0);
                        Instance.VFXCharge.transform.localScale = new Vector3(-2, 3, 2);
                        // Changes distance he moves to (the position.x + xx)
                        chargeDestination = new Vector3(MyTransform.position.x + chargeSpeed, MyTransform.position.y, MyTransform.position.z);
                        VFXCharge.SetActive(true);
                        VFXCharge.GetComponent<Animator>().SetTrigger("HornsAttack");
                        Debug.Log("Striked with horns!");
                    }
                }
            }
            // if player has spikes do the spikes animation
            if (Instance.HasSpikes == true)
            {
                if (isImpaling == false)
                {
                    isImpaling = true;
                    currentImpalingTime = initialImpalingTime;
                    foreach (GameObject spikeSpawnLoc in SpikeSpawnLocs)
                    {
                        Instantiate(SpikeProjectile, spikeSpawnLoc.transform);
                    }
                    Debug.Log("Striked with spikes!");                
                }
            }
        }
        if (Instance.FreezeControls == false)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                MyAnimator.SetBool("FacingNorth", true);
                MyAnimator.SetBool("FacingSouth", false);
                MyAnimator.SetBool("FacingWest", false);
                MyAnimator.SetBool("FacingEast", false);
                MyAnimator.SetBool("IsIdle", false);

            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                MyAnimator.SetBool("FacingNorth", false);
                MyAnimator.SetBool("FacingSouth", false);
                MyAnimator.SetBool("FacingWest", true);
                MyAnimator.SetBool("FacingEast", false);
                MyAnimator.SetBool("IsIdle", false);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                MyAnimator.SetBool("FacingNorth", false);
                MyAnimator.SetBool("FacingSouth", true);
                MyAnimator.SetBool("FacingWest", false);
                MyAnimator.SetBool("FacingEast", false);
                MyAnimator.SetBool("IsIdle", false);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                MyAnimator.SetBool("FacingNorth", false);
                MyAnimator.SetBool("FacingSouth", false);
                MyAnimator.SetBool("FacingWest", false);
                MyAnimator.SetBool("FacingEast", true);
                MyAnimator.SetBool("IsIdle", false);
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            RandomCharacterTraitSelection();
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
                Instance.HasHorns = false;
                Instance.HasSpikes = false;
                Debug.Log("Player got claws!");
                Instance.HornsObject.SetActive(false);
                Instance.LeftClaw.SetActive(true);
                Instance.RightClaw.SetActive(true);
                Instance.SpikeObject.SetActive(false);
                Instance.VFXSpike.SetActive(false);
                Instance.Claws = 0;
                break;
            case 2:
                Instance.HasHorns = true;
                Instance.HasClaws = false;
                Instance.HasSpikes = false;
                Debug.Log("Player got horns!");
                Instance.HornsObject.SetActive(true);
                Instance.LeftClaw.SetActive(false);
                Instance.RightClaw.SetActive(false);
                Instance.SpikeObject.SetActive(false);
                Instance.VFXSpike.SetActive(false);
                Instance.Horns = 0;
                break;
            case 3:
                Instance.HasSpikes = true;
                Instance.VFXSpike.SetActive(true);
                Instance.HasHorns = false;
                Instance.HasClaws = false;
                Debug.Log("Player got spikes!");
                Instance.HornsObject.SetActive(false);
                Instance.LeftClaw.SetActive(false);
                Instance.RightClaw.SetActive(false);
                Instance.SpikeObject.SetActive(true);
                Instance.Spike = 0;
                break;
            default:
                Instance.HasClaws = true;
                Instance.HasHorns = false;
                Instance.HasSpikes = false;
                Debug.Log("Defaulted the player to claws!");
                Instance.HornsObject.SetActive(false);
                Instance.LeftClaw.SetActive(true);
                Instance.RightClaw.SetActive(true);
                Instance.SpikeObject.SetActive(false);
                Instance.VFXSpike.SetActive(false);
                Instance.Claws = 0;
                break;
        }
      
    }

    private void PlayerTraitLevelerHandler()
    {
        if (Instance.Claws >= epRequired && Instance.ClawsLevel != 3)
        {
            Instance.Claws = 0;
            Instance.clawsStat.CurrentStatValue = Instance.Claws;
            InitializeStat(Instance.clawsStat, Instance.Claws);
            Instance.ClawsLevel++;
            UpdateCharacterStats();
        }
        if (Instance.Horns >= epRequired && Instance.HornsLevel != 3)
        {
            Instance.Horns = 0;
            Instance.hornsStat.CurrentStatValue = Instance.Horns;
            InitializeStat(Instance.hornsStat, Instance.Horns);
            Instance.HornsLevel++;
            UpdateCharacterStats();
        }
        if (Instance.Spike >= epRequired && Instance.SpikeLevel != 3)
        {
            Instance.Spike = 0;
            Instance.spikeStat.CurrentStatValue = Instance.Spike;
            InitializeStat(Instance.spikeStat, Instance.Spike);
            Instance.SpikeLevel++;
            UpdateCharacterStats();
        }
        if (Instance.Fishy >= epRequired && Instance.FishyLevel !=3)
        {
            Instance.Fishy = 0;
            Instance.fishyStat.CurrentStatValue = Instance.Fishy;
            InitializeStat(Instance.fishyStat, Instance.Fishy);
            Instance.FishyLevel++;
            UpdateCharacterStats();
        }
        if (Instance.Sneaky >= epRequired && Instance.SneakyLevel !=3)
        {
            Instance.Sneaky = 0;
            Instance.sneakyStat.CurrentStatValue = Instance.Sneaky;
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

    private void HandleChargingCooldown()
    {
        if (isCharging == true)
        {
            if (currentChargeTime > 0)
            {
                currentChargeTime -= Time.deltaTime;
                Vector3 direction = chargeDestination - transform.position;
                Instance.MyRigidBody2D.AddForceAtPosition(direction * chargeSpeed, Instance.MyTransform.position);
            } else if (currentChargeTime <= 0)
            {
                currentChargeTime = initialChargeTime;
                isCharging = false;
            }
        }
    }

    private void HandleClawingCooldown()
    {
        if (isClawing == true)
        {
            if (currentClawTime > 0)
            {
                currentClawTime -= Time.deltaTime;
            }
            else if (currentClawTime <= 0)
            {
                currentClawTime = initialClawTime;
                isClawing = false;
            }
        }
    }

    private void HandleImpalingCooldown()
    {
        if (isImpaling == true)
        {
            if (currentImpalingTime > 0)
            {
                currentImpalingTime -= Time.deltaTime;
            }
            else if (currentImpalingTime <= 0)
            {
                currentImpalingTime = initialImpalingTime;
                isImpaling = false;
            }
        }
    }
}
