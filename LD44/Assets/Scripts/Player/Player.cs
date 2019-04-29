using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [HideInInspector]
    public PlayerUpgrades playerUpgrades;
    private WaveController _waveController;

    public List<Upgrade> unlockedUpgrades = new List<Upgrade>();

    [HideInInspector]
    public InterfaceController interfaceController;

    private float _yearsPerHit = 5f;
    private float _yearsPerBossHit = 10f;
    private float _yearsPerHeart = 3f;
    private float _yearsPerBossHeart = 25f;

    [SerializeField]
    private float _years = 25f;
    public float Years {
        get {
            return _years;
        }
        set {
            if (_years == value)
                return;
                
            _years = value;
            EventManager.CallYearsChanged();
            if (_years <= 0) {
                Die();
            }
        }
    }

    private void Start() {
        interfaceController = GameObject.FindGameObjectWithTag("UIController").GetComponent<InterfaceController>();
        _waveController = GameObject.FindGameObjectWithTag("EnemyParent").GetComponent<WaveController>();
        playerUpgrades = GetComponent<PlayerUpgrades>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Enemy")) {
            _waveController.EnemiesRemaining--;
            if (!playerUpgrades.BlockAttack()) {
                if (other.gameObject.name.Contains("Boss"))
                    Years -= _yearsPerBossHit;
                else
                    Years -= _yearsPerHit;
            }
            Destroy(other.gameObject);
        } else if (other.gameObject.CompareTag("Heart")) {
            Years += _yearsPerHeart;
            Destroy(other.gameObject);
        } else if (other.gameObject.CompareTag("BossHeart")) {
            Years += _yearsPerBossHeart;
            Destroy(other.gameObject);
        }
    }

    public void Die() {
        Time.timeScale = 0;
        _years = 0;
        interfaceController.OpenGameOverPanel();
        interfaceController.CloseUpgradePanel();
    }
}
