using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSliderBehaviour : MonoBehaviour
{
    [SerializeField] private AudioSource audioSrc;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private Slider slider;

    private void Update()
    {
        ChangeVolume();
    }

    private void ChangeVolume() {
        if(settingsPanel.active)
            audioSrc.volume = slider.value;
    }
}
