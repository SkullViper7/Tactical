using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    [SerializeField]
    List<AudioClip> _footstepSounds;

    AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Step()
    {
        int randomIndex = Random.Range(0, _footstepSounds.Count);

        _audioSource.PlayOneShot(_footstepSounds[randomIndex]);
    }
}
