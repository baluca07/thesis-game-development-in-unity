using UnityEngine;

//TODO - Implement correctly rotation
public class AutoAim : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject weaponObject;

    private GameObject nearestEnemy;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        weaponObject = transform.parent.gameObject;

        // Find the nearest enemy at the start
        FindNearestEnemy();
    }

    private void Update()
    {
        // Continuously update the nearest enemy
        FindNearestEnemy();

        if (nearestEnemy != null)
        {
            // Calculate the direction to the nearest enemy
            Vector3 direction = (nearestEnemy.transform.position - weaponObject.transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Rotate the player towards the enemy (only Y axis rotation)
            Vector3 playerLookDirection = nearestEnemy.transform.position - player.transform.position;
            float playerAngle = Mathf.Atan2(playerLookDirection.y, playerLookDirection.x) * Mathf.Rad2Deg;
            player.transform.rotation = Quaternion.Euler(0, playerAngle, 0);

            // Rotate the weapon towards the enemy
            weaponObject.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    void FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0)
        {
            nearestEnemy = null;
            return;
        }

        GameObject closest = enemies[0];
        float closestDistance = Vector3.Distance(weaponObject.transform.position, enemies[0].transform.position);

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(weaponObject.transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closest = enemy;
                closestDistance = distance;
            }
        }

        nearestEnemy = closest;
    }
}
