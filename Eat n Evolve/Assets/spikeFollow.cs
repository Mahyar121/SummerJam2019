using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikeFollow : MonoBehaviour
{
    public Vector3 offset;
    private Transform parentTransform;
    // Start is called before the first frame update
    void Start()
    {
        parentTransform = GetComponentInParent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = parentTransform.position + offset;
    }
}
