using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WheatBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _gatheringWheatPrefab;
    [SerializeField] private GameObject _gatheringWheatEffect;

    private GameObject _click;
    private bool _isGathering = false;

    private float _timeOfAlive = 3.0f;

    private void Start()
    {
        _click = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        CheckIsGathering();
    }

    private void CheckIsGathering() {
        if (_isGathering && _click.GetComponent<PlayerController>().CheckGatheringClick())
        {
            _timeOfAlive -= Time.deltaTime;
            if (Mathf.Round(_timeOfAlive) <= 0)
            {
                this.gameObject.SetActive(false);
                Instantiate(_gatheringWheatEffect, new Vector3(this.transform.position.x, 0.5f, this.transform.position.z), Quaternion.identity);
                GameObject _gatheredWheat = Instantiate(_gatheringWheatPrefab, new Vector3(transform.position.x, transform.position.y + 0.25f, transform.position.z),
                   Quaternion.Euler(new Vector3(0, 90, 0)));
                _gatheredWheat.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(0f, 3.0f), 7.0f, 
                    Random.Range(-3.0f, 3.0f)), ForceMode.Impulse);
                _timeOfAlive = 3.0f;
                GameBehaviour.wheatCreateCount--;
                Destroy(this.gameObject);
            }
        }

        if (!_isGathering)
            _timeOfAlive = 3.0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>()) {
            this._isGathering = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            this._isGathering = false;
        }
    }

}
