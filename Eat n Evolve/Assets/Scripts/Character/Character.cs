using System.Collections;
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

    public Animator MyAnimator { get; set; }
    public Rigidbody2D MyRigidBody2D { get; set; }
    public Transform MyTransform { get; set; }
    public SpriteRenderer MySpriteRender { get; set; }
    public BoxCollider2D MyBoxColliderTrigger2D { get; set; }
    public BoxCollider2D MyBoxCollider2D { get; set; }
    public abstract void Death();
    public abstract void Initialize();

    public virtual void Start()
    {
        MyAnimator = gameObject.AddComponent<Animator>() as Animator;
        MyRigidBody2D = gameObject.AddComponent<Rigidbody2D>() as Rigidbody2D;
        MyRigidBody2D.bodyType = RigidbodyType2D.Kinematic;
        MySpriteRender = gameObject.AddComponent<SpriteRenderer>() as SpriteRenderer;
        MyBoxColliderTrigger2D = gameObject.AddComponent<BoxCollider2D>() as BoxCollider2D;
        MyBoxColliderTrigger2D.isTrigger = true;
        MyBoxCollider2D = gameObject.AddComponent<BoxCollider2D>() as BoxCollider2D;
        MyBoxCollider2D.enabled = true;
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
