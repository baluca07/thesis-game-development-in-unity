using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    [Header("Manualy Managed")]
    [Header("Basic Stats")]
    public string weaponName;
    public int baseDamage;
    public float damageSpeed;
    public float attackCoolDown;

    [Header("Damage Category")]
    public DamageCategory damageCategory;

    [Header("Damage Type")]
    public ElementalDamageType elementalDamageType;

    [Header("Need to add DamageZone")]
    [SerializeField] GameObject damageZonePrefab;

    [Header("Need to add Sprite")]
    [SerializeField] SpriteRenderer sprite;
    private Color originalSpriteColor;

    public Damage damage;

    private void Start()
    {
        damage = new Damage(damageCategory, elementalDamageType, baseDamage);
        sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = true;
        originalSpriteColor = sprite.color;
        sprite.color = Color.clear;
    }
    public void PerformMeleeAttack(ref bool isMeleeAttack)
    {
        isMeleeAttack = true;
        Debug.Log("Attack performed");
        StartCoroutine(SetDamageZone(damageSpeed));
        isMeleeAttack = false;
    }


    private IEnumerator SetDamageZone(float damageSpeed)
    {
        if (damageZonePrefab != null)
        {
            sprite.color = originalSpriteColor;
            GameObject damageZone = Instantiate(damageZonePrefab, transform.position, transform.rotation);

            DamageZone damageZoneScript = damageZone.GetComponent<DamageZone>();

            if (damageZoneScript != null)
            {
                damageZoneScript.damage = damage;  // Passing the damage
            }

            yield return new WaitForSeconds(damageSpeed);

            Destroy(damageZone);

            sprite.color = Color.clear;
        }
        else
        {
            Debug.LogWarning("Damage Zone is not assigned!");
        }
    }
}
