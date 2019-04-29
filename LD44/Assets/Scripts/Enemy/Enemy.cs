using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    private Player _player;
    private WaveController _waveController;

    [SerializeField]
    private GameObject _heartPrefab;
    [SerializeField]
    private GameObject _bossHeartPrefab;
    private float _heartLifeTime = 7.5f;

    private float _movementSpeed;
    private float _health;

    public string bulletNameHitBy;
    
	private void Start () {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _waveController = GameObject.FindGameObjectWithTag("EnemyParent").GetComponent<WaveController>();
        transform.SetParent(_waveController.transform);

        if (IsBoss()) {
            _movementSpeed = _waveController.EnemyMovementSpeed * 2f;
            _health = _waveController.EnemyHealth * 5f;
        } else {
            _movementSpeed = _waveController.EnemyMovementSpeed;
            _health = _waveController.EnemyHealth;
        }
        StartCoroutine(AttackPlayer());
	}

    private IEnumerator AttackPlayer() {
        int randomNumber = Random.Range(1, 11);
        if (randomNumber == 1 && !IsBoss()) {
            _movementSpeed = _movementSpeed * 2.25f;
        }
        
        while(Vector2.Distance(transform.position, _player.transform.position) > 0.25f) {
            Vector3 pos = Vector2.MoveTowards(transform.position, _player.transform.position, _movementSpeed * Time.deltaTime);
            Rotate();
            pos.z = 0.95f;
            transform.position = pos;
            yield return null;
        }
    }

    public void Rotate() {
        Vector3 moveDirection = _player.transform.position - transform.position; 
        if (moveDirection != Vector3.zero) {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    public void TakeDamage(float damage) {
        _health--;
        if (_health == 1)
            GetComponent<SpriteRenderer>().color = Color.red;
        if (_health == 0 && this != null) {
            DropHeart(transform.position);
            _waveController.EnemiesRemaining--;
            Destroy(gameObject);
        }
    }

    private void DropHeart(Vector3 position) {
        GameObject go = IsBoss() ? _bossHeartPrefab : _heartPrefab;
        GameObject heart = Instantiate(go, position, Quaternion.identity);
        Destroy(heart, _heartLifeTime);
    }

    public IEnumerator Knockback(Enemy enemy, float knockbackTime) {
        yield return new WaitForSeconds(knockbackTime);
        if (enemy != null) {
            enemy.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    private bool IsBoss() {
        return gameObject.name.Contains("Boss");
    }
}
