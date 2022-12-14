using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class TankPawn : Pawn
{
    //Shooting Sound
    public AudioSource shot;

    // Start is called before the first frame update
    protected override void Start()
    {
        shotDelay = 2;
        base.Start();
    }

    protected override void Update()
    {
        timeSinceLastShot = Time.time - timeOfLastShot;
        if (timeSinceLastShot > shotDelay)
        {
            canShoot = true;
        }
            base.Update();
    }

    //Moving and shooting for the tank
    #region Movement

    //Moves forward
    public override void MoveForward()
    {
        mover.Move(transform.forward, moveSpeed, maxMoveSpeed, "forward");
    }

    //Moves backwards
    public override void MoveBackward()
    {
        mover.Move(transform.forward, -moveSpeed, maxBackwardMoveSpeed, "backward");
    }

    //Turns Right
    public override void RotateClockwise()
    {
        mover.Rotate(turnSpeed);
    }

    //Turns Left
    public override void RotateCounterClockwise()
    {
        mover.Rotate(-turnSpeed);
    }

    //Helicopter Stuff
    public override void MoveUp()
    {
        Debug.Log("Not for tank");
    }

    public override void MoveDown()
    {
        Debug.Log("Not for tank");
    }

    public override void rollRight()
    {
        Debug.Log("Not for tank");
    }

    public override void rollLeft()
    {
        Debug.Log("Not for tank");
    }

    public override void hover()
    {
        Debug.Log("Not for tank");
    }

    //Shoots
    public override void shoot()
    {
        if (canShoot == true)
        {
            shot.Play();
            shooter.Shoot(shellPrefab, fireForce, damageDone, shellLifespan);
            timeOfLastShot = Time.time;
            canShoot = false;
            if (noiseMaker != null)
            {
                noiseMaker.currentVolumeDistance = noiseMaker.volumeDistance;
            }
        }
        if (noiseMaker != null)
        {
            noiseMaker.currentVolumeDistance = 0;
        }
    }

    //For AI Rotation
    public override void RotateTowards(Vector3 targetPosition)
    {
        Vector3 vectorToTarget = targetPosition - transform.position;

        Quaternion targetRotation = Quaternion.LookRotation(vectorToTarget, Vector3.up);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

    }
    #endregion

}
