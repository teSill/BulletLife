using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {

    [SerializeField]
    private AudioClip _shotgunSound;
    [SerializeField]
    private AudioClip _bossRoar;

    [SerializeField]
    private AudioSource _audio;

    public void PlayShotgunSound() {
        _audio.PlayOneShot(_shotgunSound, 0.15f);
    }

    public void PlayBossSound() {
        _audio.PlayOneShot(_bossRoar, 0.75f);
    }
}
