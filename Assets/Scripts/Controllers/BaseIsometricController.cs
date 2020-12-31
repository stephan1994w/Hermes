using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public abstract class BaseIsometricController : MonoBehaviour
{
    [SerializeField] protected float speed = 4f;
    [SerializeField] protected CinemachineVirtualCamera virtualCamera; 

    protected Vector3 forward, right, direction, rightMovement, upMovement;
    protected PlayerControls playerControls;
    protected InputManager inputManager;
    protected PlayerStateManager playerStateManager;
    protected Camera _camera;
    protected bool isActive;
    public bool IsActive
    {
        get => isActive;
        set {
            virtualCamera.gameObject.SetActive(value);
            isActive = value;
        }
    }

    protected virtual void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable() {
        playerControls.Enable();
    }
    
    protected virtual void Start()
    {
        _camera = Camera.main;
        inputManager = InputManager.Instance;
        playerStateManager = PlayerStateManager.Instance;

        forward = _camera.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
    }

    private void Update()
    {
        float horizontalInput = inputManager.GetMovementHorizontal();
        float veritcalInput = inputManager.GetMovementVertical();
        
        direction = new Vector3(horizontalInput, 0, veritcalInput);

        if(isActive)
        {
            Move(direction);
            Look();
        }
    }

    private void OnDisable() {
        playerControls.Disable();
    }
    protected abstract void Move(Vector3 direction);

    protected abstract void Look();
}
