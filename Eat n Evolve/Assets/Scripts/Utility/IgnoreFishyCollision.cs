using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreFishyCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), PlayerController.Instance.GetComponent<Collider2D>(), true);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
