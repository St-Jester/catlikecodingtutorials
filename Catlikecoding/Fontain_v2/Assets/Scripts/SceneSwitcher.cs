using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour {

	public void SwitchScene()
    {
        int nextlevel = (SceneManager.GetActiveScene().buildIndex + 1) % (SceneManager.sceneCount+1);
        SceneManager.LoadScene(nextlevel);
    }
}
