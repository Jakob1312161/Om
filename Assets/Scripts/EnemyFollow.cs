using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    private Transform player;
    private PlayerHealth playerHealth;
    private NavMeshAgent agent;

    public float damage = 10f;
    public float attackCooldown = 1f;

    private float nextAttackTime;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();

        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (player == null)
            return;

        agent.SetDestination(player.position);

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            if (Time.time >= nextAttackTime)
            {
                nextAttackTime = Time.time + attackCooldown;

                playerHealth.TakeDamage(damage);
            }
        }
    }
}