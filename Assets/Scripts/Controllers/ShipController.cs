using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShipController : BaseIsometricController
{
    [SerializeField] private float maxVelocity = 10f;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private TextMeshProUGUI velocityText;

    private Plane plane;

    private Vector3 horizontalVelocity, verticalVelocity, horizontalInput, verticalInput, rotationInput, inputDirection;

    protected override void Start()
    {
        base.Start();
        plane = new Plane(Vector3.up, Vector3.zero);
    }

    protected override void Look()
    {
        Quaternion lookRotation = new Quaternion(); 
        rotationInput = inputManager.GetShipRotation();
        var lookRot = _camera.transform.TransformDirection(rotationInput);
        lookRot = Vector3.ProjectOnPlane(lookRot, Vector3.up);
        if(lookRot != Vector3.zero)
        {
            lookRotation = Quaternion.LookRotation(lookRot);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime / 2f);
        }
    }

    protected override void Move(Vector3 direction)
    {
        rightMovement = right * speed * Time.deltaTime * direction.x;
        upMovement = forward * speed * Time.deltaTime * direction.z;
        
        horizontalVelocity = new Vector3();
        verticalVelocity = new Vector3();

        horizontalInput = rb.velocity + rightMovement;
        verticalInput = rb.velocity + upMovement;

        if(horizontalInput.x < maxVelocity && horizontalInput.x > -maxVelocity)
        {
            horizontalVelocity.x = rightMovement.x;
        }
        
        if(horizontalInput.z < maxVelocity && horizontalInput.z > -maxVelocity)
        {
            horizontalVelocity.z = rightMovement.z;
        }

        if(verticalInput.x < maxVelocity && verticalInput.x > -maxVelocity)
        {
            verticalVelocity.x = upMovement.x;
        }
        
        if(verticalInput.z < maxVelocity && verticalInput.z > -maxVelocity)
        {
            verticalVelocity.z = upMovement.z;
        }

        rb.velocity += horizontalVelocity;
        rb.velocity += verticalVelocity;

        velocityText.text = "Ship velocity: " + rb.velocity;
    }

    protected void OnCollisionEnter(Collision collision) 
    {
        WreckWall wall = collision.gameObject.GetComponent<WreckWall>();
        if(wall)
        {
            playerStateManager.SetOnFoot(wall);
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}
