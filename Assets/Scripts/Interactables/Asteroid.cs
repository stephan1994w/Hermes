using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    void Start() {
        var euler = transform.eulerAngles;
        euler.z = Random.Range(0.0f, 360.0f);
        transform.eulerAngles = euler;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate ( Vector3.up * ( 10f * Time.deltaTime ) );
    }
}
