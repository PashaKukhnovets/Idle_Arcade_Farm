using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject shopUI;
    [SerializeField] private GameObject player;
    [SerializeField] private GameBehaviour gameBeh;
    [SerializeField] private TextMeshProUGUI healthBonus;
    [SerializeField] private TextMeshProUGUI extraTime;
    [SerializeField] private GameObject healthPrefab;

    public int healthPrice = 100;
    public int extraTimePrice = 150;
    public float extraTimeCount = 25.0f;

    private void Start()
    {
        healthBonus.text = "Health Bonus " + healthPrice.ToString();
        extraTime.text = extraTimeCount.ToString() + " Sec Extra Time " + extraTimePrice.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>()) {
            shopUI.SetActive(true);        
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>()) {
            shopUI.SetActive(false);
        }
    }

    public void BuyHealthBonus() {
        if (gameBeh.Balance >= healthPrice)
        {
            gameBeh.Balance -= healthPrice;
            Instantiate(healthPrefab, new Vector3(7.0f, 0.5f, -2.0f), Quaternion.identity);
        }
    }

    public void BuyExtraTime() {
        if (gameBeh.Balance >= extraTimePrice) {
            gameBeh.Balance -= extraTimePrice;
            gameBeh._timeStart += 25.0f;
        }        
    }
}
