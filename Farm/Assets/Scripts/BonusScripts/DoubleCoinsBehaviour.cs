using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleCoinsBehaviour : MonoBehaviour
{
    private GameObject player;
    private GameObject gameManager;
    public int currentWheatPrice;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        currentWheatPrice = player.GetComponent<PlayerController>()._wheatPrice;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            gameManager = GameObject.FindGameObjectWithTag("GameManager");
            gameManager.GetComponent<GameBehaviour>().SetDDTimer(true);
            player.GetComponent<PlayerController>()._wheatPrice *= 2;
            StartCoroutine(DoubleCoinsCoroutine());
            this.gameObject.transform.position = new Vector3(0.0f, 100.0f, 0.0f);
        }
    }

    private IEnumerator DoubleCoinsCoroutine()
    {
        yield return new WaitForSeconds(15.0f);
        player.GetComponent<PlayerController>()._wheatPrice = currentWheatPrice;
        Destroy(this.gameObject);
    }
}
