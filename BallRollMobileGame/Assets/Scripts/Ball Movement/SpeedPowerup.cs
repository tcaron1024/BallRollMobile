/*****************************************************************************
// File Name :         SpeedPowerup.cs
// Author :            Kyle Grenier
// Creation Date :     07/31/2021
//
// Brief Description : Increases the ball's speed for a short duration.
*****************************************************************************/

public class SpeedPowerup : IPowerUp
{
    private float originalSpeed;
    private const float SPEED_INCREASE = 3;
    private const float DEACTIVATION_TIME = 2;

    private IBallMovementBehaviour movement;

    /// <summary>
    /// Increases the ball's base speed by a set amount.
    /// </summary>
    protected override void Activate()
    {
        movement = GetComponent<IBallMovementBehaviour>();

        originalSpeed = movement.BallSpeed;
        movement.UpdateBallSpeed(originalSpeed + SPEED_INCREASE);
    }
    
    /// <summary>
    /// Reverts the ball's speed to its original speed and destorys 
    /// the powerup component.
    /// </summary>
    protected override void Deactivate()
    {
        movement.UpdateBallSpeed(originalSpeed);
        movement.UpdateCurrentForwardSpeed(movement.CurrentForwardSpeed - SPEED_INCREASE);
        Destroy(this);
    }

    /// <summary>
    /// Returns the time in seconds until the powerup wears off.
    /// </summary>
    /// <returns>The time in seconds until the powerup wears off.</returns>
    protected override float GetDeactivationTime()
    {
        return DEACTIVATION_TIME;
    }
}
