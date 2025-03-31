using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    public int maxHealth = 100;
    public int currentHealth;
    public ElementalAttack currentElementalAttack;
    public int currentElementalAttackIndex;

    [SerializeField] Animator anim;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

    }

    private void Start()
    {
        currentHealth = maxHealth;
        UIManager.Instance.UpdatePlayerHealth();
        anim = GetComponent<Animator>();
#if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_STANDALONE_LINUX
        SetCurrentElemental(0);
#endif
        UIManager.Instance.UpdateAttackStats();
    }

    public void TakeDamage(Damage damage)
    {
        int damageAmount = damage.CalculateDamageOnPlayer(this);

        currentHealth -= damageAmount;

        Debug.Log($"Player took {damageAmount} damage! Current Health: {currentHealth}");

        UIManager.Instance.UpdatePlayerHealth();

        SessionManager.Instance.AddTakenDamage(damageAmount);

        anim.SetTrigger("Damage");

        if (currentHealth <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }

    /*public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }*/

    public void SetCurrentElemental(int index)
    {
#if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_STANDALONE_LINUX
        currentElementalAttackIndex = index;
        Debug.Log($"Set elemental to index:{ index}");
        currentElementalAttack = (GameManager.Instance.elementalAttacks[index]);
        UIManager.Instance.UpdateElementalTypeIcon();
        UIManager.Instance.UpdateElementalLevelText();
#else
        return;
#endif
    }
    
}

