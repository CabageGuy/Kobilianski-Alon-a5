using UnityEngine;

public class Teleport : MonoBehaviour
{
    // Possible directions to teleport
    public enum TeleportDirection
    {
        Left,
        Right, 
        Up,
        Down,
    }

    // Offset to move colliding object by
    public Vector2 offset;
    // Direction this trigger will teleport the object to
    public TeleportDirection direction;

    private void CheckCollision(Collider2D collider2D)
    {
        // Get rigidbody of moving object
        Rigidbody2D rb2d = collider2D.attachedRigidbody;
        // Check if object should be teleported.
        // Here, "Left" means "move the object to the left", NOT "this is left edge".
        bool doLeftTeleport  = direction == TeleportDirection.Left  && rb2d.linearVelocityX > 0;
        bool doRightTeleport = direction == TeleportDirection.Right && rb2d.linearVelocityX < 0;
        bool doUpTeleport    = direction == TeleportDirection.Up    && rb2d.linearVelocityY < 0;
        bool doDownTeleport  = direction == TeleportDirection.Down  && rb2d.linearVelocityY > 0;
        // Check if any one condition is met
        bool doTeleport = doLeftTeleport || doRightTeleport || doUpTeleport || doDownTeleport;
        if (doTeleport)
        {
            // Calculate the new position: existing position with offset
            Vector2 playerPosition = collider2D.transform.position;
            Vector2 newPosition = playerPosition + offset;
            // Move object using physics engine
            rb2d.MovePosition(newPosition);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        CheckCollision(collider2D);
    }

    private void OnTriggerStay2D(Collider2D collider2D)
    {
        CheckCollision(collider2D);
    }
}
