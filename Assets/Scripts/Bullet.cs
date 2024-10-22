using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviour
{
    public int damage = 10; // Damage value for the bullet

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the object hit is tagged as "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            PhotonView target = collision.gameObject.GetComponent<PhotonView>();
            if (target != null)
            {
                // Apply damage to the player using an RPC
                target.RPC("TakeDamage", RpcTarget.AllBuffered, damage);
            }
        }

        // Destroy the bullet after it hits something
        PhotonNetwork.Destroy(gameObject);
    }
}
