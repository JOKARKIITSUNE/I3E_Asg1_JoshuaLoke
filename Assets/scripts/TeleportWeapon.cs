/* * [Your Mandatory File Header Goes Here]
 */

using UnityEngine;
using StarterAssets;
using UnityEngine.InputSystem;

/// <summary>
/// Spawns a physical projectile and shoots it forward.
/// </summary>
public class TeleportWeapon : MonoBehaviour
{
    /// <summary>The prefab of the orb we created.</summary>
    [SerializeField] private GameObject orbPrefab;
    
    /// <summary>Where the orb spawns (should be attached to the camera).</summary>
    [SerializeField] private Transform firePoint;
    
    /// <summary>How fast the orb is thrown.</summary>
    [SerializeField] private float shootForce = 20f;
    
    /// <summary>Reference to the Starter Assets Controller.</summary>
    [SerializeField] private FirstPersonController starterAssetsController;

    private bool canShoot = true;

    void Update()
    {
        // Reset ability to shoot when touching the ground
        if (starterAssetsController.Grounded)
        {
            canShoot = true;
        }

        // Check for mouse click
        if (Mouse.current.leftButton.wasPressedThisFrame && canShoot)
        {
            ShootOrb();
        }
    }

    /// <summary>
    /// Instantiates the orb prefab and applies forward physical force.
    /// </summary>
    private void ShootOrb()
    {
        canShoot = false;

        // Spawn the orb at the firePoint's exact position and rotation
        GameObject spawnedOrb = Instantiate(orbPrefab, firePoint.position, firePoint.rotation);

        // Get the Rigidbody of the orb and push it forward
        Rigidbody orbRb = spawnedOrb.GetComponent<Rigidbody>();
        if (orbRb != null)
        {
            orbRb.linearVelocity = firePoint.forward * shootForce;
        }
    }
}