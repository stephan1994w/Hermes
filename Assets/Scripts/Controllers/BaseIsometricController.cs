using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public abstract class BaseIsometricController : BaseController
{
    [SerializeField] protected float speed = 4f;

    protected Vector3 forward, right, direction, rightMovement, upMovement;
    protected virtual void Start()
    {
        base.Start();

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

    protected abstract void Move(Vector3 direction);

    protected abstract void Look();
}
