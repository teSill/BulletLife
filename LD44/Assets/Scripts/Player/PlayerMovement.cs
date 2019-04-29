using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField]
    private Transform point;
    private Rigidbody2D _rb;

    private Vector3 movement;

    public float Speed;
    public static readonly float MaxSpeed = 8f;

    private PlayerShooting playerShooting;

    private void Start() {
        _rb = GetComponent<Rigidbody2D>();
        playerShooting = GetComponent<PlayerShooting>();
        Speed = 5f;
    }
    
    void FixedUpdate () {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        movement = new Vector3(moveHorizontal, moveVertical, 0);
        movement = movement.normalized * Speed * Time.deltaTime;

        if (!playerShooting.forcingRotation)
            Rotate(movement);

        _rb.MovePosition(_rb.transform.position + movement);
	}

    public void Rotate(Vector3 movement) {
        if (movement != Vector3.zero) {
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
