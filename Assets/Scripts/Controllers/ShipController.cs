using UnityEngine;
using System.Collections;

public class ShipController : BaseController
{
    [SerializeField] private Rigidbody rb;
    private bool adjustThrustX = false; 
    private Vector3 thrust = Vector3.zero; 
    private float throttle = 0f; 
    
    [Range(0,50)] 
    public float throttleIncrease = 0.25f; 
 
    [Range(0,500f)] 
    public float maxThrottle = 4f; 
 
    [Range(-500,100f)] 
    public float minThrottle = -2f; 
    bool adjustThrustZ;

    [SerializeField] private float lookRotateSpeed = 90f;
    
    private Vector2 mouseInput;
    private Vector2 screenCenter;
    private Vector2 mouseDistance;
    private float rollInput;
    [SerializeField] private float rollSpeed = 90f;
    [SerializeField] private float rollAcceleration = 3.5f;

    private float forwardSpeed = 25f;
    private float activeForwardSpeed;

    protected override void Start()
    {
        base.Start();
        screenCenter.x = Screen.width * 0.5f;
        screenCenter.y = Screen.height * 0.5f;
    }

    private void Update() 
    {
        mouseInput = inputManager.GetMousePosition();
        mouseDistance.x = (mouseInput.x - screenCenter.x) / screenCenter.y;
        mouseDistance.y = (mouseInput.y - screenCenter.y) / screenCenter.y;
        mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1f);

        rollInput = Mathf.Lerp(rollInput, inputManager.GetShipRoll(), rollAcceleration * Time.deltaTime);

        activeForwardSpeed = inputManager.GetMovementVertical() * forwardSpeed;

        thrust.z = inputManager.GetMovementVertical(); 
        adjustThrustZ = Mathf.Abs(thrust.z) > 0.1f; 
        throttle += thrust.z * throttleIncrease; 
        throttle = Mathf.Clamp(throttle, minThrottle, maxThrottle); 
        
    }

    private void FixedUpdate()
    {
        transform.Rotate(-mouseDistance.y * lookRotateSpeed * Time.deltaTime, mouseDistance.x * lookRotateSpeed * Time.deltaTime, rollInput * rollSpeed * Time.deltaTime, Space.Self);
        
        // transform.position += transform.forward * activeForwardSpeed * Time.deltaTime;
        
        
        // rb.AddTorque(-mouseDistance.y * lookRotateSpeed * Time.deltaTime, mouseDistance.x * lookRotateSpeed * Time.deltaTime, rollInput * rollSpeed * Time.deltaTime,ForceMode.Force);
        // rb.AddForce(transform.forward * activeForwardSpeed * Time.deltaTime, ForceMode.Force);
        if(adjustThrustZ) 
        { 
            rb.AddForce(transform.forward * (Mathf.Abs(thrust.z) * throttle), ForceMode.Force); 
        } 
    }
}