using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneController : MonoBehaviour {

	public void ReloadScene() {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
        
    }
}
