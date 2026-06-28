using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public float healAmount = 25f;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            playerHealth.Heal(healAmount);
            Destroy(gameObject);
        }
    }
}