using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawScratch : MonoBehaviour
{
    private ParticleSystem scratchVFX;
    

    // Start is called before the first frame update
    void Start()
    {
        scratchVFX = gameObject.GetComponent<ParticleSystem>();
    }

    public void ScratchAttack()
    {
        scratchVFX.Play();
    }
}
