using UnityEngine;
using Photon.Pun;

public class PlayerShooting : MonoBehaviourPun
{
    public GameObject bulletPrefab; // Assign this in the Inspector
    public Transform bulletSpawnPoint; // The point from which the bullet will be spawned
    public float bulletSpeed = 20f; // Speed of the bullet
    public float shootCooldown = 0.1f; // Time between shots
    private float lastShootTime; // Time when the last shot was fired

    private void Update()
    {
        // Check if the player is the local player and can shoot
        if (photonView.IsMine && CanShoot())
        {
            Shoot();
            lastShootTime = Time.time; // Update the last shoot time
        }
    }

    private bool CanShoot()
    {
        // Allow shooting with both left (0) and right (1) mouse buttons
        return (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) && Time.time > lastShootTime + shootCooldown;
    }

    private void Shoot()
    {
        // Check if bulletPrefab is assigned
        if (bulletPrefab == null)
        {
            Debug.LogError("Bullet prefab is not assigned!");
            return; // Exit if the prefab is not set
        }

        // Instantiate the bullet from the Resources folder
        GameObject bullet = PhotonNetwork.Instantiate(bulletPrefab.name, bulletSpawnPoint.position, bulletSpawnPoint.rotation, 0);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.velocity = bulletSpawnPoint.forward * bulletSpeed; // Set the bullet's velocity
        }

        // Destroy the bullet after 5 seconds to avoid clutter
        Destroy(bullet, 5f);
    }
}
