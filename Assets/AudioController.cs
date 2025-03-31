using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance;

    [Header("Player Audio FXs")]
    [SerializeField] private AudioClip[] meleeAttack;
    [SerializeField] private AudioClip shoot;
    [SerializeField] private AudioClip damage;
    [SerializeField] private AudioClip levelUp;

    [Header("Enemy Audio FXs")]
    [SerializeField] private AudioClip attack;
    [SerializeField] private AudioClip jump;

    [Header("UI")]
    [SerializeField] private AudioClip win;
    [SerializeField] private AudioClip lose;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void PlaySound(AudioClip clip, AudioSource audioSource)
    {
        if (clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("AudioClip is null!");
        }
    }

    public void PlayMeleeAttackSound(AudioSource audioSource)
    {
        int index = Random.Range(0,2);
        AudioClip clip = meleeAttack[index];
        PlaySound(clip, audioSource);
    }

    public void PlayShootSound(AudioSource audioSource)
    {
        PlaySound(shoot, audioSource);
    }

    public void PlayDamageSound(AudioSource audioSource)
    {
        PlaySound(damage, audioSource);
    }

    public void PlayLevelUpSound(AudioSource audioSource)
    {
        PlaySound(levelUp, audioSource);
    }

    public void PlayEnemyAttackSound(AudioSource audioSource)
    {
        PlaySound(attack, audioSource);
    }

    public void PlayEnemyJumpSound(AudioSource audioSource)
    {
        PlaySound(jump, audioSource);
    }

    public void PlayWinSound(AudioSource audioSource)
    {
        PlaySound(win, audioSource);
    }

    public void PlayLoseSound(AudioSource audioSource)
    {
        PlaySound(lose, audioSource);
    }
}
