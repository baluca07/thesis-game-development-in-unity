using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private MeleeWeapon currentMeleeWeapon;
    [SerializeField] private RangedWeapon currentRangedWeapon;
    private void Start()
    {
        UpdateWeapon();
    }

    public void UpdateWeapon()
    {
        GameObject activeWeapon = FindActiveWeapon();
        if (activeWeapon != null)
        {
            if (activeWeapon.CompareTag("Melee"))
            {
                currentMeleeWeapon = activeWeapon.GetComponent<MeleeWeapon>();
                currentRangedWeapon = null;
                //Debug.Log($"Equipped Melee Weapon: {currentMeleeWeapon?.name}");
            }
            else if (activeWeapon.CompareTag("Ranged"))
            {
                currentRangedWeapon = activeWeapon.GetComponent<RangedWeapon>();
                currentMeleeWeapon = null;
                //Debug.Log($"Equipped Ranged Weapon: {currentRangedWeapon?.name}");
            }
        }
        else
        {
            Debug.LogWarning("No active weapon found.");
        }
    }

    private GameObject FindActiveWeapon()
    {
        foreach (Transform child in transform)
        {
            //Debug.Log($"Checking child: {child.name}");
            if (child.gameObject.CompareTag("Melee") || child.gameObject.CompareTag("Ranged"))
            {
                return child.gameObject;
            }
        }
        return null;
    }

    public MeleeWeapon GetMeleeWeapon() => currentMeleeWeapon;
    public RangedWeapon GetRangedWeapon() => currentRangedWeapon;
}
