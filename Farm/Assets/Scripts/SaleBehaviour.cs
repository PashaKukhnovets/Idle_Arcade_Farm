using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaleBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _saleButton;

    private void Start()
    {
        _saleButton.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>()) 
        {
            _saleButton.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            _saleButton.SetActive(false);
        }
    }
}
