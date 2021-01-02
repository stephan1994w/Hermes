using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public abstract class BaseController : MonoBehaviour 
{
    [SerializeField] protected CinemachineVirtualCamera virtualCamera; 
   
    protected PlayerControls playerControls;
    protected InputManager inputManager;
    protected PlayerStateManager playerStateManager;
    protected Camera _camera;
    protected bool isActive;

    public virtual void Reactivate()
    {
        isActive = true;
        virtualCamera.gameObject.SetActive(true);
    }

    public virtual void Deactivate()
    {
        isActive = false;
        virtualCamera.gameObject.SetActive(false);
    }

    protected virtual void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable() {
        playerControls.Enable();
    }
    
    private void OnDisable() {
        playerControls.Disable();
    }
    
    protected virtual void Start()
    {
        _camera = Camera.main;
        inputManager = InputManager.Instance;
        playerStateManager = PlayerStateManager.Instance;
    }
}
