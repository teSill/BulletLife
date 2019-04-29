using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {
    
    private Player _player;

    [SerializeField]
    private float _thrust = 5f;
    [SerializeField]
    private float _knockbackTime = 0.5f;
    [SerializeField]
    private float _bulletDamage = 1f;
    
	void Start () {
		_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        transform.rotation = _player.transform.rotation;
	}

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Enemy")) {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy == null || GetComponent<SpriteRenderer>().sprite == null || enemy.bulletNameHitBy.Equals(gameObject.name))
                return;
            
            GetComponent<SpriteRenderer>().sprite = null; // Set the sprite to null to make it appear as if the object was destroyed. It'll get destroyed after a few seconds

            Vector2 difference = enemy.transform.position - transform.position;
            difference = difference.normalized * _thrust;

            enemy.bulletNameHitBy = gameObject.name;
            enemy.TakeDamage(_bulletDamage);

            enemy.GetComponent<Rigidbody2D>().AddForce(difference, ForceMode2D.Impulse);
            StartCoroutine(enemy.Knockback(enemy, _knockbackTime));
        }
    }
}
