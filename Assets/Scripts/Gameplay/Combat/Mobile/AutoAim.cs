using UnityEngine;

public class AutoAim : MonoBehaviour
{
    private GameObject player;
    private GameObject weaponObject;

    private GameObject nearestEnemy;

    [SerializeField] GameObject targetSelectorPrefab;
    private GameObject targetSelector;

    private SpriteRenderer aim;
    private Color originalAimColor;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        weaponObject = transform.parent.gameObject;

        aim = GetComponent<SpriteRenderer>();
        originalAimColor = aim.color;
        FindNearestEnemy();
        if(nearestEnemy != null)
        {
            targetSelector = Instantiate(targetSelectorPrefab,nearestEnemy.transform,nearestEnemy);
        }
        else
        {
            aim.color = Color.clear;
        }
    }

    private void Update()
    {
        FindNearestEnemy();

        if (nearestEnemy != null)
        {
            aim.color = originalAimColor;
            if(targetSelector == null)
            {
                targetSelector = Instantiate(targetSelectorPrefab, nearestEnemy.transform, nearestEnemy);
            }
            targetSelector.transform.position = nearestEnemy.transform.position;

            // Calculate the direction to the nearest enemy
            Vector3 direction = (nearestEnemy.transform.position - weaponObject.transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Rotate the weapon towards the enemy
            weaponObject.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {
            aim.color = Color.clear;
        }
    }

    private void FindNearestEnemy()
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
