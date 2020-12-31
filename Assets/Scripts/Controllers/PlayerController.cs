﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseIsometricController
{
    [SerializeField] private float wallSpawnOffset;
    protected override void Look()
    {
        Vector3 totalDirection = Vector3.Normalize(rightMovement + upMovement);

        if(totalDirection!=Vector3.zero)
        {   
            Quaternion lookRotation = Quaternion.LookRotation(totalDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
        }
    }

    protected override void Move(Vector3 direction)
    {
        rightMovement = right * speed * Time.deltaTime * direction.x;
        upMovement = forward * speed * Time.deltaTime * direction.z;

        transform.position += rightMovement;
        transform.position += upMovement;
    }

    public void SetStartingPosition(WreckWall wall)
    {
        Vector3 newPosition = wall.transform.position + (wall.transform.right * wallSpawnOffset);
        transform.position = new Vector3(newPosition.x, 0, newPosition.z);
    }
}