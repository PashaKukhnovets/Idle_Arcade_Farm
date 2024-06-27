using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarBehaviour : MonoBehaviour
{
    private Slider healthBarSlider;
    private GameObject player;

    void Start()
    {
        healthBarSlider = this.GetComponent<Slider>();
        player = GameObject.FindGameObjectWithTag("Player");

        healthBarSlider.maxValue = player.GetComponent<PlayerController>()._playerHealth;
        healthBarSlider.value = player.GetComponent<PlayerController>()._playerHealth;
    }

    void Update()
    {
        UpdateHealth();
    }

    public void UpdateHealth()
    {
        healthBarSlider.value = player.GetComponent<PlayerController>()._playerHealth;
    }
}
