using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateHangarBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject updateUI;
    [SerializeField] private GameObject newHangar;
    [SerializeField] private GameObject oldHangar;
    [SerializeField] private GameBehaviour gameBeh;
    [SerializeField] private TextMeshProUGUI updateText;

    private GameObject player;

    public int updatePrice = 250;

    private void Start()
    {
        updateText.text += " " + updatePrice.ToString();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            updateUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            updateUI.SetActive(false);
        }
    }

    public void BuyUpdateHangar()
    {
        if (gameBeh.Balance >= updatePrice)
        {
            gameBeh.Balance -= updatePrice;
            updateUI.SetActive(false);
            oldHangar.SetActive(false);
            newHangar.SetActive(true);
            player.GetComponent<PlayerController>().IncreaseWheatPrice(2);
        }
    }

}
