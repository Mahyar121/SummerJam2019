using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreBushCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] bushGameObjects = GameObject.FindGameObjectsWithTag("Bush");
        foreach (GameObject bush in bushGameObjects)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), bush.GetComponent<Collider2D>(), true);
        }
    }

}
