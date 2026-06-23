using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Camera playerCamera;

    public float damage = 25f;
    public float range = 100f;

    public float fireRate = 10f;

    private float nextFireTime = 0f;

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;

        if (Physics.Raycast(
            playerCamera.transform.position,
            playerCamera.transform.forward,
            out hit,
            range))
        {
            Target target =
                hit.transform.GetComponent<Target>();

            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
    }
}