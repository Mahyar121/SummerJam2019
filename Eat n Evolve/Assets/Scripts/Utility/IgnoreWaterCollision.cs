using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreWaterCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] waterGameObjects = GameObject.FindGameObjectsWithTag("Water");
        foreach (GameObject water in waterGameObjects)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), water.GetComponent<Collider2D>(), true);
        }
    }

}
