using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highscore : MonoBehaviour {

	public static int WaveRecord;

    private void Awake() {
        if (PlayerPrefs.GetInt("WaveHighscore") == 0) {
            PlayerPrefs.SetInt("WaveHighscore", WaveController.Wave);
        } else {
            WaveRecord = PlayerPrefs.GetInt("WaveHighscore");
        }
    }

    public static bool IsNewHighscore() {
        if (WaveController.Wave > PlayerPrefs.GetInt("WaveHighscore")) {
            PlayerPrefs.SetInt("WaveHighscore", WaveController.Wave);
            return true;
        }
        return false;
    }

}
