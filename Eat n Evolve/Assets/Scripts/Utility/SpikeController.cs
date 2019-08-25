using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeController : MonoBehaviour
{
    private float currentDeathTimer = 1f;

    private void Start()
    {
        currentDeathTimer = 1f;
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.Translate(Vector3.up * Time.smoothDeltaTime);
        HandleKillingSpike();
    }

    private void HandleKillingSpike()
    {
        if (currentDeathTimer > 0)
        {
            currentDeathTimer -= Time.deltaTime;
        }
        else if (currentDeathTimer <= 0)
        {
            Debug.Log("Destroyed Spike Projectile");
            Destroy(gameObject);
        }
    }
}
