using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{

    // Universal Character stats
    [SerializeField] protected float coreLevelMultiplier = 2f;
    [SerializeField] protected float damage = 5f;
    [SerializeField] protected float movementSpeed = 5f;
    [SerializeField] protected float health = 100f;
    [SerializeField] protected float epRequired = 100f;
    [SerializeField] protected float claws = 0;
    [SerializeField] protected float clawsLevel = 0;
    [SerializeField] protected float horns = 0;
    [SerializeField] protected float hornsLevel = 0;
    [SerializeField] protected float spike = 0;
    [SerializeField] protected float spikeLevel = 0;
    [SerializeField] protected float scenty = 0;
    [SerializeField] protected float scentyLevel = 0;
    [SerializeField] protected float fishy = 0;
    [SerializeField] protected float fishyLevel = 0;
    [SerializeField] protected float stinky = 0;
    [SerializeField] protected float stinkyLevel = 0;
    [SerializeField] protected float sneaky = 0;
    [SerializeField] protected float sneakyLevel = 0;

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
        UpdateCharacterStats();
    }

    public virtual void UpdateCharacterStats()
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
