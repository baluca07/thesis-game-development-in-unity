using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class SessionManager : MonoBehaviour {

    public static SessionManager Instance;

    public int killedEnemiesCount = 0;
    public float sessionTime = 0;
    public int dealtDamage = 0;
    public int takenDamage = 0;

    private bool isSessionActive = false;

    [Header("Calculate Score")]
    public int maxDamageScore = 5000;
    public int damagePenalty = 5;
    public int idealTime = 180;
    public int maxTimeScore = 5000;
    public int timePenalty = 10;

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

    public void StartSession()
    {
        Time.timeScale = 1;

        killedEnemiesCount = 0;
        sessionTime = 0;
        dealtDamage = 0;
        takenDamage = 0;
        isSessionActive = true;

        StartCoroutine(CountSessionTime());
    }

    public void EndSession()
    {
        isSessionActive = false;

        Debug.Log("Session Ended:");
        Debug.Log("Killed Enemies: " + killedEnemiesCount);
        Debug.Log("Session Time: " + sessionTime);
        Debug.Log("Dealt Damage: " + dealtDamage);
        Debug.Log("Taken Damage: " + takenDamage);
    }

    private IEnumerator CountSessionTime()
    {
        while (isSessionActive)
        {
            sessionTime += Time.deltaTime;
            yield return null;
        }
    }

    public void IncrementKilledEnemies()
    {
        killedEnemiesCount++;
    }

    public void AddDealtDamage(int damage)
    {
        dealtDamage += damage;
    }

    public void AddTakenDamage(int damage)
    {
        takenDamage += damage;
    }
    public int CalculateScore()
    {
#if UNITY_ANDROID || UNITY_IOS
        maxDamageScore = GameManager.Instance.currentRoom.maxDamageScore;
        damagePenalty = GameManager.Instance.currentRoom.damagePenalty;
        idealTime = GameManager.Instance.currentRoom.idealTime;
        maxTimeScore = GameManager.Instance.currentRoom.maxTimeScore;
        timePenalty = GameManager.Instance.currentRoom.timePenalty;
#endif
        int timeScore;

        if (sessionTime <= idealTime)
        {
            timeScore = maxTimeScore;
        }
        else
        {
            timeScore = maxTimeScore - (int)((sessionTime - idealTime) * timePenalty);
            if (timeScore < 0)
            {
                timeScore = 0;
            }
        }

        int damageScore = maxDamageScore - (takenDamage * damagePenalty);
        if (damageScore < 0)
        {
            damageScore = 0;
        }

        Debug.Log($"TimeScore: {timeScore}, DamageScore: {damageScore}");
        int finalScore = timeScore + damageScore;
        return finalScore;
    }
}