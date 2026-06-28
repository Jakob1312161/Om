using UnityEngine;
using System;
using System.Collections;
using TMPro;

public class Weapon : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text ammoText;

    [Header("Audio")]
    public AudioSource gunAudio;
    public AudioClip shootClip;
    public AudioClip reloadClip;

    public static event Action OnShoot;

    [Header("References")]
    public Camera playerCamera;

    [Header("Weapon Stats")]
    public float damage = 25f;
    public float range = 100f;
    public float fireRate = 10f;

    [Header("Ammo")]
    public int magazineSize = 30;
    public int reserveAmmo = 90;
    public float reloadTime = 2f;

    private int currentAmmo;
    private bool isReloading = false;

    [Header("Weapon Recoil")]
    public float recoilDistance = 0.05f;
    public float recoilReturnSpeed = 8f;
    public float recoilKickSpeed = 25f;

    private Vector3 originalPosition;
    private Vector3 targetPosition;

    private float nextFireTime;

    void Start()
    {
        currentAmmo = magazineSize;

        originalPosition = transform.localPosition;
        targetPosition = originalPosition;

        UpdateAmmoUI();

        currentAmmo = magazineSize;

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

        if (isReloading)
            return;

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            if (currentAmmo > 0)
            {
                nextFireTime = Time.time + (1f / fireRate);
                Shoot();
            }
            else
            {
                Debug.Log("Magazin leer! Drücke R zum Nachladen.");
            }
        }
    }

    void Shoot()
    {
        currentAmmo--;
        UpdateAmmoUI();

        if (gunAudio != null && shootClip != null)
            gunAudio.PlayOneShot(shootClip);

        targetPosition = originalPosition - Vector3.forward * recoilDistance;

        if (playerCamera != null)
        {
            RaycastHit hit;

            if (Physics.Raycast(playerCamera.transform.position,
                                playerCamera.transform.forward,
                                out hit,
                                range))
            {
                Target target = hit.transform.GetComponent<Target>();

                if (target != null)
                {
                    target.TakeDamage(damage);
                }
            }
        }

        OnShoot?.Invoke();

        Debug.Log("Munition: " + currentAmmo + " / " + reserveAmmo);
    }

    IEnumerator Reload()
    {
        if (currentAmmo == magazineSize)
            yield break;

        if (reserveAmmo <= 0)
        {
            Debug.Log("Keine Munition mehr!");
            yield break;
        }

        isReloading = true;

        Debug.Log("Nachladen...");

        if (gunAudio != null && reloadClip != null)
            gunAudio.PlayOneShot(reloadClip);

        yield return new WaitForSeconds(reloadTime);

        int ammoNeeded = magazineSize - currentAmmo;
        int ammoToLoad = Mathf.Min(ammoNeeded, reserveAmmo);

        currentAmmo += ammoToLoad;
        reserveAmmo -= ammoToLoad;
        UpdateAmmoUI();

        isReloading = false;

        Debug.Log("Nachgeladen: " + currentAmmo + " / " + reserveAmmo);
    }

    void UpdateAmmoUI()
    {
        if (ammoText != null)
        {
            ammoText.text = currentAmmo + " / " + reserveAmmo;
        }
    }

    public void AddAmmo(int amount)
    {
        reserveAmmo += amount;

        UpdateAmmoUI();
    }
}