using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public int ammoAmount = 30;

    private void OnTriggerEnter(Collider other)
    {
        Weapon weapon = other.GetComponentInChildren<Weapon>();

        if (weapon != null)
        {
            weapon.AddAmmo(ammoAmount);
            Destroy(gameObject);
        }
    }
}