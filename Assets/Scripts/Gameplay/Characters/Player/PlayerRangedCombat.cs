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
    private Quaternion originalPlayerRotation; // Store the original rotation

    private bool facingRight = true;

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
    }

    private void Update()
    {
        if (isAiming)
        {
            Aim();
        }
    }

    public void StartAim()
    {
        if (isOnCooldown)
        {
            Debug.Log("Ranged attack is on cooldown");
            return;
        }

        // Store the original rotation before aiming
        originalPlayerRotation = transform.rotation;

        if(originalPlayerRotation.y == 0) facingRight = true ;
        else facingRight = false ;

        Debug.Log("Facing Right: " + facingRight);

        anim.SetTrigger("Aim");
        isAiming = true;
        aim.gameObject.SetActive(true);
        PlayerController.Instance.SetMovement(false);
    }

    public void StopAimAndAttack()
    {
        if (!isAiming) return;

        isAiming = false;
        aim.gameObject.SetActive(false);
        anim.SetTrigger("Shoot");
        ShootProjectile();
        StartCoroutine(CooldownTimer());
    }

    private void Aim()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        // Get direction to aim
        Vector3 aimDirection = (mousePosition - aim.transform.position).normalized;

        // Calculate rotation angle
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        // Determine if we need to flip
        bool shouldFlip;
        if (facingRight) shouldFlip = mousePosition.x < transform.position.x;
        else shouldFlip = mousePosition.x > transform.position.x;

        // Apply correct rotation while handling flipping
        aim.transform.rotation = Quaternion.Euler(0, 0, shouldFlip ? angle + 180 : angle);

        // Flip player (but not the aim object)
        transform.localScale = new Vector3(shouldFlip ? -1 : 1, 1, 1);
    }

    private void ShootProjectile()
    {
        Vector3 aimEulerAngles = aim.rotation.eulerAngles;

        Quaternion normalizedRotation = aim.localToWorldMatrix.rotation.normalized;

        GameObject projectile = Instantiate(projectilePrefabs[PlayerStats.Instance.currentElementalAttackIndex], firePoint.position, normalizedRotation);
        Projectile projectileScript = projectile.GetComponent<Projectile>();

        if (projectileScript != null)
        {
            projectileScript.damage = PlayerStats.Instance.currentElementalAttack.GetDamage();
            projectileScript.attackRange = projectileRange;
            projectileScript.speed = projectileSpeed;
            projectileScript.owner = Projectile.ProjectileOwner.Player; // Corrected to Player
        }
    }

    private IEnumerator CooldownTimer()
    {
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
        // Reset the player's rotation to the original state
        transform.rotation = originalPlayerRotation;
        transform.localScale = new Vector3(1, 1, 1); // Reset scale to default
    }
}