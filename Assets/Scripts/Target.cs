using UnityEngine;

public class Target : MonoBehaviour
{
    public EnemySpawner spawner;

    [Header("Drops")]
    public GameObject ammoPickupPrefab;
    public GameObject healthPickupPrefab;

    [Range(0, 100)]
    public float ammoDropChance = 15f;
    public float healthDropChance = 5f;

    public float health = 100f;

    private bool isDead = false;

    public void TakeDamage(float damage)
    {
        if (isDead)
            return;

        health -= damage;

        Debug.Log("Leben: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;

        if (Random.value < ammoDropChance / 100f)
        {
            Instantiate(ammoPickupPrefab, transform.position, Quaternion.identity);
        }

        if (Random.value < healthDropChance / 100f)
        {
            Instantiate(healthPickupPrefab, transform.position, Quaternion.identity);
        }

        if (spawner != null)
        {
            spawner.EnemyKilled();
        }

        Destroy(gameObject);
    }
}