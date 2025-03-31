using System.Collections;
using UnityEngine;

public class PlayerRangedCombat : MonoBehaviour
{
    public static PlayerRangedCombat Instance;

    [Header("References")]
    [SerializeField] private Animator anim;

    [Header("Projectile")]
    public float projectileSpeed;
    public float projectileRange;
    [SerializeField] private GameObject[] projectilePrefabs;
    [SerializeField] private Transform aim;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float attackCoolDown = 3;

    private bool isOnCooldown = false;
    private bool isAiming = false;
    private Quaternion originalPlayerRotation;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        anim = GetComponent<Animator>();

        aim.gameObject.SetActive(false);

    }

    private void Update()
    {
        if (isAiming) Aim();
#if UNITY_ANDROID || UNITY_IOS
        if ((CanUseElementalRangedAttack(GameManager.Instance.elementalAttacks[0]) ||
            CanUseElementalRangedAttack(GameManager.Instance.elementalAttacks[1]) ||
            CanUseElementalRangedAttack(GameManager.Instance.elementalAttacks[2]) ||
            CanUseElementalRangedAttack(GameManager.Instance.elementalAttacks[3]))
            && !isOnCooldown)   Aim();
#endif
    }
#if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_STANDALONE_LINUX

    private bool facingRight = true;

    public void StartAim()
    {
        if (CanUseElementalRangedAttack(PlayerStats.Instance.currentElementalAttack))
        {
            originalPlayerRotation = transform.rotation;

            if (originalPlayerRotation.y == 0) facingRight = true;
            else facingRight = false;

            anim.SetTrigger("Aim");
            isAiming = true;
            aim.gameObject.SetActive(true);
            PlayerController.Instance.SetMovement(false);
        }
    }

    public void StopAimAndAttack()
    {
        if (!isAiming) return;

        isAiming = false;
        aim.gameObject.SetActive(false);
        anim.SetTrigger("Shoot");
        ShootProjectile(PlayerStats.Instance.currentElementalAttackIndex);
    }
#endif
    private void Aim()
    {
#if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_STANDALONE_LINUX

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Vector3 aimDirection = (mousePosition - aim.transform.position).normalized;

        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        bool shouldFlip;
        if (facingRight) shouldFlip = mousePosition.x < transform.position.x;
        else shouldFlip = mousePosition.x > transform.position.x;

        aim.transform.rotation = Quaternion.Euler(0, 0, shouldFlip ? angle + 180 : angle);

        transform.localScale = new Vector3(shouldFlip ? -1 : 1, 1, 1);
#elif UNITY_ANDROID || UNITY_IOS
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearestEnemy = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject potentialEnemy in enemies)
        {
            Vector3 directionToTarget = potentialEnemy.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                nearestEnemy = potentialEnemy;
            }
        }

        if (nearestEnemy != null)
        {
            Vector3 aimDirection = (nearestEnemy.transform.position - aim.transform.position).normalized;
            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

            aim.transform.rotation = Quaternion.Euler(0, 0, angle);
            aim.gameObject.SetActive(true);
        }
        else
        {
            // If no enemies are found, you might want to set a default aim direction
            aim.transform.rotation = Quaternion.identity; // Or any other default rotation
        }
#endif
    }

    private void ShootProjectile(int elementalAttackIndex)
    {
        Vector3 aimEulerAngles = aim.rotation.eulerAngles;

        Quaternion normalizedRotation = aim.localToWorldMatrix.rotation;

        GameObject projectile = Instantiate(projectilePrefabs[elementalAttackIndex], firePoint.position, normalizedRotation);
        Projectile projectileScript = projectile.GetComponent<Projectile>();

        if (projectileScript != null)
        {
            projectileScript.damage = GameManager.Instance.elementalAttacks[elementalAttackIndex].GetDamage();
            projectileScript.attackRange = projectileRange;
            projectileScript.speed = projectileSpeed;
            projectileScript.owner = Projectile.ProjectileOwner.Player;
        }
        StartCoroutine(CooldownTimer());
        UIManager.Instance.StartCountRangedCooldown(attackCoolDown);
    }

    private IEnumerator CooldownTimer()
    {
        aim.gameObject.SetActive(false);
        isOnCooldown = true;
        yield return new WaitForSeconds(attackCoolDown);
        isOnCooldown = false;
    }

    public void EnableMovement()
    {
        transform.rotation = originalPlayerRotation;
        transform.localScale = new Vector3(1, 1, 1);
        RestoreOriginalRotation();
        PlayerController.Instance.SetMovement(true);
    }

    private void RestoreOriginalRotation()
    {
        transform.rotation = originalPlayerRotation;
        transform.localScale = new Vector3(1, 1, 1);
    }

    public void FireAttack()
    {
        if (CanUseElementalRangedAttack(GameManager.Instance.elementalAttacks[0])) StartCoroutine(MobileShootAnimation(0));
    }
    public void WaterAttack()
    {
        if (CanUseElementalRangedAttack(GameManager.Instance.elementalAttacks[1])) StartCoroutine(MobileShootAnimation(1));
    }
    public void AirAttack()
    {
        if (CanUseElementalRangedAttack(GameManager.Instance.elementalAttacks[2])) StartCoroutine(MobileShootAnimation(2));
    }
    public void EarthAttack()
    {
        if (CanUseElementalRangedAttack(GameManager.Instance.elementalAttacks[3])) StartCoroutine(MobileShootAnimation(3));
    }

    private IEnumerator MobileShootAnimation(int elementalAttackIndex)
    {
        anim.SetTrigger("Aim");
        yield return new WaitForSeconds(0.5f);
        anim.SetTrigger("Shoot");
        ShootProjectile(elementalAttackIndex);
    }

    private bool CanUseElementalRangedAttack(ElementalAttack currentAttack)
    {
        if (currentAttack.currentLevel == 0)
        {
#if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_STANDALONE_LINUX
            Debug.Log("This type of attack does not unlocked yet");
#endif
            return false;
        }
        else if (isOnCooldown)
        {
#if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_STANDALONE_LINUX
            Debug.Log("Ranged attack is on cooldown");
#endif
            return false;
        }
        return true;
    }

}