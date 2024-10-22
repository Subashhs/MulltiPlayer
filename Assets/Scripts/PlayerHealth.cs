using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviourPun
{
    public int maxHealth = 100;
    private int currentHealth;

    public Text playerNameText;
    public Text healthText;

    private void Start()
    {
        currentHealth = maxHealth;

        if (photonView.IsMine)
        {
            playerNameText.text = PhotonNetwork.NickName;
        }
        else
        {
            playerNameText.text = photonView.Owner.NickName;
        }

        UpdateHealthText();
    }

    [PunRPC]
    public void TakeDamage(int damage)
    {
        if (photonView.IsMine)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Die();
            }

            UpdateHealthText();
        }
    }

    void UpdateHealthText()
    {
        healthText.text = "Health: " + currentHealth.ToString();
    }

    void Die()
    {
        PhotonNetwork.Destroy(gameObject); // Destroy the player when health reaches 0
    }
}
