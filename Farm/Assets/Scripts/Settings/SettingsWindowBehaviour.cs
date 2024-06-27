using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsWindowBehaviour : MonoBehaviour
{

    [SerializeField] private GameObject settingsWindow;

    public void SettingsOpen() {
        Time.timeScale = 0;
        settingsWindow.SetActive(true);
    }

    public void SettingsClose() {
        Time.timeScale = 1;
        settingsWindow.SetActive(false);
    }
}
