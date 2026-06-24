using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    private Transform player;
    public float speed = 3f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            player.position,
            speed * Time.deltaTime
        );
    }
}