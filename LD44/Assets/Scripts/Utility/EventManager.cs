using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

    private Player _player;

    private void Start() {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public delegate void CompleteWave();
    public static event CompleteWave OnWaveCompleted;

    public delegate void KillEnemy();
    public static event KillEnemy OnKillEnemy;

    public delegate void Hit();
    public static event Hit OnHit;

    public delegate void Shoot();
    public static event Shoot OnShoot;

    public delegate void Collision();
    public static event Collision OnCollision;

    public delegate void YearsEvent();
    public static event YearsEvent OnYearsChanged;

    public static void CallCompleteWave() {
        if (OnWaveCompleted != null)
            OnWaveCompleted();
    }

    public static void CallKillEnemy() {
        if (OnKillEnemy != null)
            OnKillEnemy();
    }

    public static void CallYearsChanged() {
        OnYearsChanged();
    }

    public static void CallHit() {
        if (OnHit != null)
            OnHit();
    }

    public static void CallShoot() {
        if (OnShoot != null)
            OnShoot();
    }

    public static void CallCollision() {
        if (OnCollision != null)
            OnCollision();
    }
}
