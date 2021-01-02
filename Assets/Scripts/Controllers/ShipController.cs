using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShipController : BaseController
{   
    [SerializeField] private Rigidbody rb;

    private bool adjustPitch = false;
    private bool adjustYaw = false;
    private bool adjustThrustX = false;
    private bool adjustThrustY = false;
    private bool adjustThrustZ = false;    

    private float pitch = 0.0f;
    private float yaw = 0.0f;
    private float roll = 0.0f;
    private float pitchDiff = 0.0f;

    private float rotationEasing = 0.2f;

    private Vector3 thrust = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private float throttle = 0f;

    [Range(0,50)]
    public float throttleIncrease = 0.25f;

    [Range(0,500f)]
    public float maxThrottle = 4f;

    [Range(-500,100f)]
    public float minThrottle = -2f;

    [Range(0, 100f)]
    public float pitchStrength = 15f;

    [Range(0, 100f)]
    public float yawStrength = 1.5f;
    
    [Range(0, 10f)]
    public float rollStrength = 1.5f;
    
    float qtrScreenH;
    float qtrScreenW;
    Vector3 centerScreen;
    private Vector3 targetRotation;
    private Vector2 mousePosition;
    
    protected override void Start() {
        base.Start();
        centerScreen = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        qtrScreenH = Screen.height * 0.25f;
        qtrScreenW = Screen.width * 0.25f;
	}

    private void Update()
    {
        pitch = GetPitch();
        thrust.z = inputManager.GetMovementVertical();
        mousePosition = inputManager.GetMousePosition();
        adjustPitch = Mathf.Abs(pitch) > 0.1f;
        adjustThrustX = Mathf.Abs(thrust.x) > 0.1f;
        adjustThrustY = thrust.y != 0;
        adjustThrustZ = Mathf.Abs(thrust.z) > 0.1f;

        throttle += thrust.z * throttleIncrease;
        throttle = Mathf.Clamp(throttle, minThrottle, maxThrottle);
    }

    private void FixedUpdate() {

        if(adjustPitch)
        {
            rb.AddTorque(transform.right * (-pitch * pitchStrength), ForceMode.Force);
        }
        // else
        // {
        //     Vector3 brakingTorque = -rb.angularVelocity * .05f;
        //     rb.AddTorque(-brakingTorque);

        //     if(brakingTorque.Round(1)== Vector3.zero)
        //     {
        //         rb.angularVelocity = Vector3.zero;
        //     }

        // }

        if(adjustThrustZ)
        {
            rb.AddForce(transform.forward * (Mathf.Abs(thrust.z) * throttle), ForceMode.Force);
        }
    }

    // private void Update() {
    //     thrust.z = inputManager.GetMovementVertical(); 
    //     adjustThrustZ = Mathf.Abs(thrust.z) > 0.1f;

    //     throttle += thrust.z * throttleIncrease;
    //     throttle = Mathf.Clamp(throttle, minThrottle, maxThrottle);

    //     mousePosition = inputManager.GetMousePosition();

    //     float roll = inputManager.GetMovementHorizontal();
    //     targetRotation.z = -roll * 4;
    //     targetRotation.x = GetPitch();
    // }

    private float GetPitch()
    {
        pitchDiff = -(centerScreen.y - mousePosition.y);
        pitchDiff = Mathf.Clamp(pitchDiff, -qtrScreenH, qtrScreenH);
        return (pitchDiff / qtrScreenH);
    }

}

 static class ExtensionMethods
 {
     /// <summary>
     /// Rounds Vector3.
     /// </summary>
     /// <param name="vector3"></param>
     /// <param name="decimalPlaces"></param>
     /// <returns></returns>
     public static Vector3 Round(this Vector3 vector3, int decimalPlaces = 2)
     {
         float multiplier = 1;
         for (int i = 0; i < decimalPlaces; i++)
         {
             multiplier *= 10f;
         }
         return new Vector3(
             Mathf.Round(vector3.x * multiplier) / multiplier,
             Mathf.Round(vector3.y * multiplier) / multiplier,
             Mathf.Round(vector3.z * multiplier) / multiplier);
     }
 }
