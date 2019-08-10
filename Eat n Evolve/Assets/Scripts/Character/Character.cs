using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{

    [SerializeField] protected float movementSpeed = 5f;


    public abstract void Death();
    public abstract void Initialize();
}
