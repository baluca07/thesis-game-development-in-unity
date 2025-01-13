using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class RangedEnemyAttack : EnemyAttack
{
    private EnemyStats stats;
    private Damage damage;

    [SerializeField] GameObject player;

    [Header("Projectile")]
    public float projectileSpeed;
    public float projectileRange;
    [SerializeField] GameObject projectilePrefab;
    public GameObject firePoint;
    private float orbitRadius = 0;

    private void Start()
    {
        stats = GetComponent<EnemyStats>();
        player = GameObject.FindGameObjectWithTag("Player");
        damage = new Damage(stats.damageCategory, stats.elementalDamageType, stats.baseDamage);
        orbitRadius = Vector3.Distance(transform.position, firePoint.transform.position);
    }

    private void Update()
    {
        AimAtPlayer();
    }

    private void AimAtPlayer()
    {
        // Sz�m�tsd ki a j�t�kos ir�ny�ba mutat� vektort
        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;

        // �ll�tsd be a firePoint poz�ci�j�t az ellens�gt�l az orbitRadius t�vols�gra
        firePoint.transform.position = transform.position + directionToPlayer * orbitRadius;

        // Forgasd a firePoint-ot �gy, hogy a j�t�kos fel� n�zzen
        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        firePoint.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public override void Attack() {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.transform.position, firePoint.transform.rotation);
        Projectile projectileScript = projectile.GetComponent<Projectile>();

        Vector3 direction = (player.transform.position - firePoint.transform.position).normalized;

        if (projectileScript != null)
        {
            projectileScript.damage = damage;
            projectileScript.attackRange = projectileRange;
            projectileScript.speed = projectileSpeed;
            projectileScript.owner = Projectile.ProjectileOwner.Enemy;
        }
        Debug.Log("Shoot projectile.");
    }
}

