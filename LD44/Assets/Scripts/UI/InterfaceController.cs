using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InterfaceController : MonoBehaviour {
   
    private WaveController _waveController;
    [SerializeField]
    private UpgradeController upgradeController;

    [SerializeField]
    private GameObject _upgradePanel;
    [SerializeField]
    private GameObject _gameOverScreen;

    [SerializeField]
    private TMP_Text _waveText;
    [SerializeField]
    private TMP_Text _waveHighscoreText;
    [SerializeField]
    private TMP_Text _enemiesText;
    [SerializeField]
    private TMP_Text _yearsText;

    public bool Shopping { get; private set; }

    private void OnEnable() {
        EventManager.OnWaveCompleted += UpdateWaveText;
        EventManager.OnKillEnemy += UpdateEnemiesText;
        EventManager.OnYearsChanged += UpdateYearsText;
    }

    private void OnDisable() {
        EventManager.OnWaveCompleted -= UpdateWaveText;
        EventManager.OnKillEnemy -= UpdateEnemiesText;
        EventManager.OnYearsChanged -= UpdateYearsText;
    }

    void Start () {
        _waveController = GameObject.FindGameObjectWithTag("EnemyParent").GetComponent<WaveController>();

        UpdateHighscoreText();
        UpdateEnemiesText();
        UpdateYearsText();
	}
	
	private void UpdateWaveText() {
        StartCoroutine(CountdownWaveStart());
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.U)) {
            ToggleUpgradePanel();
        }
    }

    private IEnumerator CountdownWaveStart() {
        int currentWave = WaveController.Wave;
        for(int i = (int) WaveController.TimeBetweenWaves; i > 0; i--) {
            _waveText.text = "Wave " + (currentWave + 1) + " starting in: " + i;
            yield return new WaitForSeconds(1f);
        }

        _waveText.text = "Current Wave: " + WaveController.Wave;
        _enemiesText.text = "Enemies: " + _waveController.EnemiesRemaining;
        if (Highscore.IsNewHighscore()) {
            UpdateHighscoreText();
        }
    }

    private void UpdateHighscoreText() {
        _waveHighscoreText.text = "Highscore: " + Highscore.WaveRecord;
    }

    private void UpdateEnemiesText() {
        _enemiesText.text = "Enemies: " + _waveController.EnemiesRemaining;
    }

    private void UpdateYearsText() {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        _yearsText.text = "Years: " + player.Years;
    }

    public void ToggleUpgradePanel() {
        if (_upgradePanel.activeSelf) {
            CloseUpgradePanel();
        } else {
            OpenUpgradePanel();
        }
    }

    public void OpenUpgradePanel() {
        _upgradePanel.SetActive(true);
        upgradeController.InstantiateUpgrades();
        Shopping = true;
    }

    public void CloseUpgradePanel() {
        _upgradePanel.SetActive(false);
        Shopping = false;
    }

    public void OpenGameOverPanel() {
        _gameOverScreen.SetActive(true);
    }

}
