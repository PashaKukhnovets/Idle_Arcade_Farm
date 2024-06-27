using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBehaviour : MonoBehaviour
{
    public void ChangeScene(int _numberOfScene) {
        SceneManager.LoadScene(_numberOfScene);
    }

    public void ExitGame() {
        Application.Quit();
    }
}
