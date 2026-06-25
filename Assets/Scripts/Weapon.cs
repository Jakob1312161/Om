using UnityEngine;
using System;

public class Weapon : MonoBehaviour
{
    public ParticleSystem muzzleFlash;

    public AudioSource gunAudio;
    public AudioClip shootClip;

    public GameObject hitEffect;
    public GameObject bulletHolePrefab;

    public static event Action OnShoot;

    public Camera playerCamera;

    public float damage = 25f;
    public float range = 100f;
    public float fireRate = 10f;

    [Header("Weapon Recoil")]
    public float recoilDistance = 0.05f;
    public float recoilReturnSpeed = 10f;
    public float recoilKickSpeed = 20f;

    private Vector3 originalPosition;
    private Vector3 targetPosition;

    private float nextFireTime = 0f;

    void Start()
    {
        originalPosition = transform.localPosition;
        targetPosition = originalPosition;
    }

    void Update()
    {
        transform.localPosition = Vector3.Lerp(
            transform.localPosition,
            targetPosition,
            recoilKickSpeed * Time.deltaTime);

        targetPosition = Vector3.Lerp(
            targetPosition,
            originalPosition,
            recoilReturnSpeed * Time.deltaTime);

        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        muzzleFlash.Play();
        gunAudio.PlayOneShot(shootClip);

        RaycastHit hit;

        if (Physics.Raycast(
            playerCamera.transform.position,
            playerCamera.transform.forward,
            out hit,
            range))
        {
            Instantiate(
                hitEffect,
                hit.point,
                Quaternion.LookRotation(hit.normal));

            Instantiate(
                bulletHolePrefab,
                hit.point + hit.normal * 0.001f,
                Quaternion.LookRotation(hit.normal));

            Target target = hit.transform.GetComponent<Target>();

            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }

        OnShoot?.Invoke();
    }
}