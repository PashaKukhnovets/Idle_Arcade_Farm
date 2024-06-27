using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBonusBehaviour : MonoBehaviour
{
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            player.GetComponent<PlayerController>()._playerHealth += 20.0f;
            Destroy(this.gameObject);
        }
    }
}
