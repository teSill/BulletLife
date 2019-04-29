using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {

    public enum State {
        Idle,
        Shooting
    }

    public State CurrentState { get; private set; }

    private Player _player;
    private PlayerMovement _playerMovement;
    private PlayerUpgrades _playerUpgrades;
    private CameraShake _cameraShake;
    private SoundController _soundController;

    [SerializeField]
    private GameObject _ammoPrefab;
    [SerializeField]
    private Transform _ammoStartPos;
    [SerializeField]
    private Sprite _playerIdle;
    [SerializeField]
    private Sprite _playerShoot;

    [SerializeField]
    private float _bulletSpeed = 650;
    private float _bulletLifetime = 5f;

    private float _yearsPerShot = 1f;
    private float _timeBetweenShots = 0.05f;
    private bool _canShoot = true;

    private float _rotationDuration = 0.35f;
    private float _rotationTimer = 0f;
    public bool forcingRotation = false;


    private void Start() {

        _player = GetComponent<Player>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerUpgrades = GetComponent<PlayerUpgrades>();
        _cameraShake = Camera.main.GetComponent<CameraShake>();
        _soundController = GameObject.FindGameObjectWithTag("SoundController").GetComponent<SoundController>();;
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            if (_canShoot && !_player.interfaceController.Shopping) {
                StartCoroutine(Shoot());
            }
        }
        if (forcingRotation) {
            _rotationTimer += Time.deltaTime;
            if (_rotationTimer >= _rotationDuration) {
                forcingRotation = false;
            }
        }
    }

    private IEnumerator Shoot() {
        UpdateState(State.Shooting);

        string bulletName = Utility.GetRandomString(5);
        for(int i = 0; i < _playerUpgrades.AmmoUpgradesPurchased; i++) {
            GameObject bullet = Instantiate(_ammoPrefab, _ammoStartPos.position, Quaternion.identity);
            bullet.name = bulletName;
            transform.Rotate(new Vector3(0, 0, GetAngle(i)));

            bullet.GetComponent<Rigidbody2D>().AddForce(_ammoStartPos.right * _bulletSpeed);
            Destroy(bullet, _bulletLifetime);
        }

        _soundController.PlayShotgunSound();
        _player.Years -= _yearsPerShot;
        StartCoroutine(_cameraShake.ShakeCamera());

        yield return new WaitForSeconds(_timeBetweenShots);
        UpdateState(State.Idle);
    }

    private float GetAngle(int i) {
        switch(i) {
            case 1:
                return 7.5f;
            case 2:
                return 10f;
            case 3:
                return -7.5f;
            case 4:
                return -10f;
            default:
                return 0f;
        }
    }

    private void UpdateState(State state) {
        switch(state) {
            case State.Idle:
                GetComponent<SpriteRenderer>().sprite = _playerIdle;
                CurrentState = State.Idle;
                _canShoot = true;
                break;
             case State.Shooting:
                GetComponent<SpriteRenderer>().sprite = _playerShoot;
                _playerMovement.Rotate(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
                CurrentState = State.Shooting;
                forcingRotation = true;
                _rotationTimer = 0f;
                _canShoot = false;
                break;
            default:
                GetComponent<SpriteRenderer>().sprite = _playerIdle;
                CurrentState = State.Idle;
                break;
        }
    }
}
