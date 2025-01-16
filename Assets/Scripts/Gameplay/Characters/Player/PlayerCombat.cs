using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    private WeaponManager weaponManager;
    private HashSet<EnemyStats> hitEnemies = new HashSet<EnemyStats>();
    [SerializeField] Animator animatior;

    private void Start()
    {
        foreach(Transform child in transform)
        {
            if (child.gameObject.CompareTag("Weapon"))
            {
                weaponManager = child.gameObject.GetComponent<WeaponManager>();
            }
        }
        if (weaponManager == null)
        {
            Debug.LogError("WeaponManager is missing on the player.");
            return;
        }

        animatior = GetComponent<Animator>();
    }

    private void Update()
    {
        animatior.SetInteger("Weapon", (int)weaponManager.activeWeaponType);
        if(weaponManager.currentWeapon != null )
        {
            if(weaponManager.currentMeleeWeapon !=null) {
                animatior.SetBool("Melee", true);
            }
            else
            {
                animatior.SetBool("Melee", false);
            }
        }
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        /*
            //Aim Spell Logic
            if (context.started)
            {
                isAim = true;
                //Debug.Log("Aim started");
                //Debug.Log("Aim started");
            }
            else if (context.canceled)
            {
                isAim = false;
                //Debug.Log("Aim canceled");
            }
        */
        Debug.LogWarning("Aim Spell: not implemented yet");
    }



    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            //animatior.SetTrigger("Attack"); -->Weaponba
            weaponManager.Attack();
        }
        //hitEnemies.Clear(); -->Weaponba
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Object triggered: {collision.name}");

        if (collision.CompareTag("Enemy"))
        {
            EnemyStats enemy = collision.GetComponent<EnemyStats>();
            Debug.Log($"Enemy collided: {enemy.enemyName}");
            if (!hitEnemies.Contains(enemy))
            {
                hitEnemies.Add(enemy);
                Debug.Log($"Enemy added to hitEnemies: {enemy.enemyName}");
                enemy.TakeDamage(weaponManager.currentDamage);
                Debug.Log($"Enemy damaged: {enemy.enemyName}");
            }
            else
            {
                Debug.Log($"Enemy is already hit by player: {enemy.enemyName}");
            }

            Debug.Log(hitEnemies.Count);
        }
    }
}
