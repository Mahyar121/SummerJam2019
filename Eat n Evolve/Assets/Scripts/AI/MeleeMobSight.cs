using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeMobSight : MonoBehaviour
{
    private MeleeAI meleeAI;

    private void Start()
    {
        meleeAI = GetComponentInParent<MeleeAI>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") { meleeAI.Target = collision.gameObject; }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player") { meleeAI.Target = null; }
    }
}
