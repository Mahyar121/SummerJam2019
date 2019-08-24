using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float xMin = -337.1697f, xMax = -112.9347f, yMin = 54.23063f, yMax = 236.2817f;
    private Transform target;
    public Transform Target { get { return target; } set { target = value; } }

    private void Start()
    {
        target = GameObject.FindObjectOfType<PlayerController>().transform;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        transform.position = new Vector3(
            Mathf.Clamp(target.position.x, xMin, xMax),
            Mathf.Clamp(target.position.y, yMin, yMax),
            transform.position.z
            );
    }
}
