
using UnityEngine;
using UnityEngine.UI;
using static GameManager;

public class EnemyStats : MonoBehaviour
{
    [Header("Basic Stats")]
    public string enemyName;
    public int maxHealth;
    public int baseDamage;
    public float attackRange = 1.2f;

    [Header("Damage Type")]
    public ElementalDamageType elementalDamageType;

    public int currentHealth;

    private Animator anim;

    [SerializeField] private Slider healthUI;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        healthUI.maxValue = maxHealth;
        healthUI.value = maxHealth;

        audioSource = GetComponent<AudioSource>();
    }
    public void TakeDamage(Damage damage)
    {
        int damageAmount = damage.CalculateDamageOnEnemy(this);

        currentHealth -= damageAmount;
        healthUI.value = currentHealth;

        Debug.Log($"{enemyName} took {damageAmount} damage! Current Health: {currentHealth}");
        SessionManager.Instance.AddDealtDamage(damageAmount);
        anim.SetTrigger("Damage");

        AudioController.Instance.PlayDamageSound(audioSource);
        if (currentHealth <= 0)
        {
            Die();
        }
    }


    private void Die()
    {
        Debug.Log($"{enemyName} has been defeated!");
        SessionManager.Instance.IncrementKilledEnemies();
        GameManager.Instance.AddEnemyKillToPlayerElementalStat(elementalDamageType);
        UIManager.Instance.UpdateAttackStats();


        GameManager.Instance.currentRoom.EnemyDefeated();
        Destroy(gameObject);
    }



}
