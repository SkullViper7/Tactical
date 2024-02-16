using System.Collections.Generic;
using UnityEngine;

public class EntitySFX : MonoBehaviour
{
    [SerializeField]
    List<AudioClip> _footsteps;

    [SerializeField]
    AudioClip _attack;

    [SerializeField]
    AudioClip _hurt;

    [SerializeField]
    AudioClip _death;

    AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Step()
    {
        int randomIndex = Random.Range(0, _footsteps.Count);

        _audioSource.PlayOneShot(_footsteps[randomIndex]);
    }

    public void Attack()
    {
        _audioSource.PlayOneShot(_attack);
    }

    public void TakeDamage()
    {
        _audioSource.PlayOneShot(_hurt);
    }

    public void Die()
    {
        _audioSource.PlayOneShot(_death);
    }
}
