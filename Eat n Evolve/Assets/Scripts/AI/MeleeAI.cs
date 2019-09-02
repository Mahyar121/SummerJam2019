using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAI : Character
{


    [SerializeField] private float meleeRange = 5f;
    [SerializeField] private GameObject foodObject;
    //[SerializeField] public GameObject VFXClaw;
    [SerializeField] public GameObject LeftClaw;
    [SerializeField] public GameObject RightClaw;

    // Objects for Spikes
    //[SerializeField] public GameObject VFXSpike;
    //[SerializeField] public GameObject SpikeObject;
    //[SerializeField] public GameObject SpikeProjectile;
    //[SerializeField] public GameObject[] SpikeSpawnLocs;

    public SpriteRenderer EnemySpriteRenderer { get; set; }
    public GameObject Target { get; set; }
    public Rigidbody2D MyRigidbody2D { get; set; }
    public Animator MyAnimator { get; set; }
    public bool IsDead { get { return health <= 0; } }
    public float Damage { get { return damage; } }
    public float Health { get { return health; } set { health = value; } }
    public float Claws { get { return claws; } set { claws = value; } }
    public float ClawsLevel { get { return clawsLevel; } set { clawsLevel = value; } }
    public float Horns { get { return horns; } set { horns = value; } }
    public float HornsLevel { get { return hornsLevel; } set { hornsLevel = value; } }
    public float Spike { get { return spike; } set { spike = value; } }
    public float SpikeLevel { get { return spikeLevel; } set { spikeLevel = value; } }
    public float Fishy { get { return fishy; } set { fishy = value; } }
    public float FishyLevel { get { return fishyLevel; } set { fishyLevel = value; } }
    public float Sneaky { get { return sneaky; } set { sneaky = value; } }
    public float SneakyLevel { get { return sneakyLevel; } set { sneakyLevel = value; } }

    private float wanderTimer;
    private float wanderTimerSpeed;
    private int wanderTimerState;
    private int wanderTimerMax;
    private float idleTimer;
    private float idleDuration;
    private float movingTimer;
    private float movingDuration;
    private float attackTimer;
    private float attackCooldown = 2;
    private bool isImpaling = false;
    private bool canAttack = true;
    private Vector2 randomDirection;
    private EdgeCollider2D meleeAttackCollider;
    bool isMoving;
    bool isIdle;
    bool fight;

    private float initialImpalingTime = 1f;
    private float currentImpalingTime;

    public bool InMeleeRange
    {
        get
        {
            if (Target != null)
            {
                //Debug.Log("Distance: " + Vector3.Distance(transform.position, Target.transform.position));
                return Vector3.Distance(transform.position, Target.transform.position) <= meleeRange;
            }
            return false;
        }
    }

    //public bool 

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        meleeAttackCollider = GetComponent<EdgeCollider2D>();
        foodObject.SetActive(false);
        randomDirection = new Vector2(0, 0);
        MyRigidbody2D = GetComponent<Rigidbody2D>();
        isIdle = true;
        isMoving = false;
        Target = null;
        wanderTimer = 0;
        wanderTimerSpeed = 20f;
        wanderTimerState = 0;
        wanderTimerMax = 1;
        idleTimer = 0;
        //VFXClaw.SetActive(false);
        // idle timer before it decides to mvoe again
        idleDuration = Random.Range(1, 10);
        movingTimer = 0;
        // moving timer before it changes direction
        movingDuration = Random.Range(1, 10);
        EnemySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void Initialize() //required for some fucking reason
    {
    }

    private void Update()
    {
        if (MyAnimator == null)
        {
            MyAnimator = GetComponent<Animator>();
        }
        if (!IsDead)
        {
            if (Target == null)
            {

                ChangeBetweenIdleAndWanderState();
                Wander();
            }
            else if (Target != null && InMeleeRange)
            {
                if (FightorFlight())
                {
                    MoveToPlayer();
                }
                else
                {
                    MoveAwayPlayer();
                }
            }
        }
        else
        {
            Death();
        }
    }

    private void Wander()
    {
        if (wanderTimerState == 0)
        {
            wanderTimer += 0.1f * Time.deltaTime * wanderTimerSpeed;
            if (wanderTimer >= wanderTimerMax)
            {
                wanderTimer = 0;
                if (isMoving)
                {
                    int movDir = Random.Range(0, 8);
                    RandomizeAIDirection(movDir);

                }
                else if (isIdle)
                {
                    randomDirection.x = 0;
                    randomDirection.y = 0;
                }
            }
            // Movement
            float velocityX = randomDirection.x * Time.smoothDeltaTime * movementSpeed;
            float velocityY = randomDirection.y * Time.smoothDeltaTime * movementSpeed;
            //Debug.Log("VelocityX is " + velocityX);
            //Debug.Log("VelocityY is " + velocityY);
            if (MyAnimator != null)
            {
                MyAnimator.SetFloat("SpeedX", velocityX);
                MyAnimator.SetFloat("SpeedY", velocityY);
            }
            MyRigidbody2D.AddForceAtPosition(new Vector3(velocityX, velocityY, 0) * movementSpeed, transform.position);
        }
    }

    private void ChangeBetweenIdleAndWanderState()
    {
        if (isIdle)
        {
            idleTimer += Time.deltaTime;
            if (idleTimer >= idleDuration)
            {
                //Debug.Log("Is Moving Now");
                isMoving = true;
                isIdle = false;
                idleTimer = 0;
            }
        }
        if (isMoving)
        {
            movingTimer += Time.deltaTime;
            if (movingTimer >= movingDuration)
            {
                //Debug.Log("Is Idle Now");
                isIdle = true;
                isMoving = false;
                movingTimer = 0;
            }
        }
    }

    // Randomizes direction AI goes
    private void RandomizeAIDirection(int movementDirection)
    {
        if (MyAnimator == null)
        {
            return;
        }
        Vector2 tempVector2 = new Vector2(0, 0);
        // Up
        if (movementDirection == 0)
        {
            tempVector2.x = 0;
            tempVector2.y = 1;
            MyAnimator.SetBool("FacingNorth", true);
            MyAnimator.SetBool("FacingSouth", false);
            MyAnimator.SetBool("FacingWest", false);
            MyAnimator.SetBool("FacingEast", false);
        }
        // Down
        if (movementDirection == 1)
        {
            tempVector2.x = 0;
            tempVector2.y = -1;
            MyAnimator.SetBool("FacingWest", false);
            MyAnimator.SetBool("FacingNorth", false);
            MyAnimator.SetBool("FacingEast", false);
            MyAnimator.SetBool("FacingSouth", true);
        }
        // Left
        if (movementDirection == 2)
        {
            tempVector2.x = -1;
            tempVector2.y = 0;
            MyAnimator.SetBool("FacingWest", true);
            MyAnimator.SetBool("FacingNorth", false);
            MyAnimator.SetBool("FacingEast", false);
            MyAnimator.SetBool("FacingSouth", false);

        }
        // Right
        if (movementDirection == 3)
        {
            tempVector2.x = 1;
            tempVector2.y = 0;
            MyAnimator.SetBool("FacingEast", true);
            MyAnimator.SetBool("FacingWest", false);
            MyAnimator.SetBool("FacingNorth", false);
            MyAnimator.SetBool("FacingSouth", false);
        }
        //UpRight
        if (movementDirection == 4)
        {
            tempVector2.x = 1;
            tempVector2.y = 1;
            MyAnimator.SetBool("FacingNorth", true);
            MyAnimator.SetBool("FacingSouth", false);
            MyAnimator.SetBool("FacingWest", false);
            MyAnimator.SetBool("FacingEast", false);
        }
        //DownRight
        if (movementDirection == 5)
        {
            tempVector2.x = 1;
            tempVector2.y = -1;
            MyAnimator.SetBool("FacingWest", false);
            MyAnimator.SetBool("FacingNorth", false);
            MyAnimator.SetBool("FacingEast", false);
            MyAnimator.SetBool("FacingSouth", true);
        }
        //DownLeft
        if (movementDirection == 6)
        {
            tempVector2.x = -1;
            tempVector2.y = -1;
            MyAnimator.SetBool("FacingWest", false);
            MyAnimator.SetBool("FacingNorth", false);
            MyAnimator.SetBool("FacingEast", false);
            MyAnimator.SetBool("FacingSouth", true);
        }
        //UpLeft
        if (movementDirection == 7)
        {
            tempVector2.x = -1;
            tempVector2.y = 1;
            MyAnimator.SetBool("FacingNorth", true);
            MyAnimator.SetBool("FacingSouth", false);
            MyAnimator.SetBool("FacingWest", false);
            MyAnimator.SetBool("FacingEast", false);
        }

        randomDirection.x = tempVector2.x;
        randomDirection.y = tempVector2.y;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerAttack" && IsDead == false)
        {
            Debug.Log("CurrentHP Before Damage: " + health);
            Debug.Log("I took " + PlayerController.Instance.Damage);
            health -= PlayerController.Instance.Damage;
            Debug.Log("CurrentHP After Damage: " + health);
        }
    }

    public override void Death()
    {
        if (health <= 0)
        {
            BoxCollider2D[] boxColliders = GetComponents<BoxCollider2D>();
            foreach (BoxCollider2D box in boxColliders) { box.enabled = false; }
            SpriteRenderer[] MySpriteRenderers = GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer sprite in MySpriteRenderers) { sprite.enabled = false; }
            GetComponent<SpriteRenderer>().enabled = false;
            MyRigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
            foodObject.SetActive(true);
            foodObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    public bool FightorFlight()
    {
        int fightorFlightint = Random.Range(0, 10);
            if (fightorFlightint >= 5)
            {
            fight = true;
            Debug.Log("We should be running to player");
            }
            else
            {
            fight = false;
            Debug.Log("We should be running");
            }
        return fight;
    }

    private void MoveToPlayer()
    {
         Debug.Log("Moving to");
            Vector3 directionOfCharacter = Target.transform.position - transform.position;
            directionOfCharacter = directionOfCharacter.normalized;
            //Debug.Log(directionOfCharacter);
            MyRigidbody2D.AddForceAtPosition(directionOfCharacter, transform.position);
    }

    private void MoveAwayPlayer()
    {
        Debug.Log("Moving away from player");
        Vector3 directionOfCharacter = Target.transform.position - transform.position;
        directionOfCharacter = directionOfCharacter.normalized;
        //Debug.Log(directionOfCharacter);
        MyRigidbody2D.AddForceAtPosition(directionOfCharacter * -1, transform.position);
    }

    private void Attack()
    {
        attackTimer += Time.smoothDeltaTime;

        Vector2 directionToPlayer = new Vector2(Target.transform.position.x - transform.position.x, Target.transform.position.y - transform.position.y).normalized;

        Vector3Int truncatedDir = new Vector3Int(Mathf.RoundToInt(directionToPlayer.x), Mathf.RoundToInt(directionToPlayer.y), 0);
        float localScaleX = 0.5f;
        float localScaleY = 0.5f;
        if (directionToPlayer.x < 0)
        {
            localScaleX *= -1f;
        }
        if (directionToPlayer.y < 0)
        {
            localScaleY *= -1f;
        }


        if (attackTimer >= attackCooldown)
        {
            attackTimer = 0;
            canAttack = true;
        }
        if (canAttack)
        {

            //if (isImpaling == false)
            //{
            //    isImpaling = true;
            //    currentImpalingTime = initialImpalingTime;
            //    foreach (GameObject spikeSpawnLoc in SpikeSpawnLocs)
            //    {
            //        spikeSpawnLoc.transform.position = transform.position + spikeSpawnLoc.GetComponent<spikeFollow>().offset;
            //        Instantiate(SpikeProjectile, transform.position + spikeSpawnLoc.GetComponent<spikeFollow>().offset, Quaternion.identity, transform);
            //    }
            //    //Debug.Log("Striked with spikes!");
            //}

    //        //canAttack = false;
    //        //Debug.Log($"Attack {truncatedDir.x}, {truncatedDir.y}");
    ///*        VFXClaw.transform.position = transform.position + Vector3.Scale(trunca*/tedDir, new Vector3(3f * truncatedDir.x, 3f * truncatedDir.y, 0));
    //        ////VFXClaw.transform.localRotation = Quaternion.Euler(0, 0, 90);
    ///*        //VFXClaw.transform.localScale = new Vector3(localScaleX, localScaleY,*/ 2);
            
    //        //attackTimer = 0;
    //        //VFXClaw.SetActive(true);
    //        //ParticleSystem test = VFXClaw.GetComponent<ParticleSystem>();
    //        //if (test != null)
    //        //{
    //            //test.Play();
    //        //}
    //        //VFXClaw.GetComponent<Animator>().SetTrigger("ClawAttack");
    //        //// once there is attack animation -> Animator.SetTrigger("attack");
        }

    }

}
