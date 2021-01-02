using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager _instance;
    public static InputManager Instance => _instance;
    private PlayerControls playerControls;

    private Vector2 lookPosition = new Vector2();

    private void Awake()
    {
        if(_instance!=null & _instance!=this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        playerControls = new PlayerControls();

        // playerControls.Ship.Rotation.performed += ctx => lookPosition = ctx.ReadValue<Vector2>();
    }

    private void OnEnable() {
        playerControls.Enable();
    }
    private void OnDisable() {
        playerControls.Disable();
    }
    
    public float GetMovementHorizontal()
    {
       return playerControls.Movement.Horizontal.ReadValue<float>();
    }

    public float GetMovementVertical()
    {
       return playerControls.Movement.Vertical.ReadValue<float>();
    }

    public float GetShipRoll()
    {
       return playerControls.Ship.Roll.ReadValue<float>();
    }

    public Vector3 GetMousePosition()
    {
        return playerControls.Ship.Look.ReadValue<Vector2>();
    }

    public Vector2 GetFPSMovement()
    {
        return playerControls.FirstPerson.Movement.ReadValue<Vector2>();
    }

    public Vector2 GetFPSMovementNouseDelete()
    {
        return playerControls.FirstPerson.Look.ReadValue<Vector2>();
    }

    public bool PlayerJumped()
    {
        return playerControls.FirstPerson.Jump.triggered;
    }
}
