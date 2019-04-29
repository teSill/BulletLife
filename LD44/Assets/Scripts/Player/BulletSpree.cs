using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpree : MonoBehaviour {    

    private List<BulletScript> _bullets = new List<BulletScript>();

    public BulletSpree(List<BulletScript> bullets) {
        _bullets = bullets;
    }

    public List<BulletScript> GetBullets() {
        return _bullets;
    }
}
