/* * [Your Mandatory File Header Goes Here]
 */

using UnityEngine;

/// <summary>
/// Handles the projectile's collision and teleports the player if it hits a valid wall.
/// </summary>
public class OrbCollision : MonoBehaviour
{
    /// <summary>Distance away from the wall the player will spawn.</summary>
    [SerializeField] private float wallOffset = 1.5f;

    /// <summary>
    /// Detects when the orb hits another physical object.
    /// </summary>
    private void OnCollisionEnter(Collision collision)
    {
        // 1. Check if the object we hit is allowed to be teleported to
        if (collision.gameObject.CompareTag("TeleportWall"))
        {
            // 2. Find the Player in the scene
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            
            if (player != null)
            {
                // 3. Temporarily turn off the controller to force the position change
                CharacterController cc = player.GetComponent<CharacterController>();
                if (cc != null) cc.enabled = false;

                // 4. Calculate exactly where the orb hit, and push out slightly so the player isn't stuck inside the wall
                Vector3 spawnPosition = collision.contacts[0].point + (collision.contacts[0].normal * wallOffset);
                player.transform.position = spawnPosition;

                // 5. Turn the controller back on
                if (cc != null) cc.enabled = true;
            }
        }

        // 6. Destroy the orb no matter what it hits, so it doesn't bounce around forever
        Destroy(gameObject);
    }
}