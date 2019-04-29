using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField]
    private GameObject _player;

    private Vector3 _offset;

	void Start () {
		_offset = transform.position - _player.transform.position;
	}

    private void LateUpdate() {
        transform.position = _player.transform.position + _offset;
    }
}
