using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float speed = 5f;

    void Start()
    {
        
    }

    void Update()
    {
        transform.rotation = player.rotation;
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
