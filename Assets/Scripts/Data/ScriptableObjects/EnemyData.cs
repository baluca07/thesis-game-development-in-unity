using UnityEngine;


[CreateAssetMenu(fileName = "NewEnemy", menuName = "Game/Enemy")]
public class EnemyData : ScriptableObject
{
    [Header("Basic Stats")]
    public string enemyName;
    public int health;
    public int baseDamage;

    [Header("Damage Category")]
    public DamageCategory damageCategory;

    [Header("Damage Type")]
    public ElementalDamageType elementalDamageType;

    [Header("Defense Stats")]
    public int physicalShield;
    public int magicShield;
}

