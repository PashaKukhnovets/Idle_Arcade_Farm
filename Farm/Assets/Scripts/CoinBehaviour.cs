using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    private float _speed = 1.7f;
    private Vector3 _position;

    void Start()
    {
        _position = GameObject.FindGameObjectWithTag("CoinImage").transform.position;
    }

    private void Update()
    {
        MoveToPosition();
    }

    private void MoveToPosition() 
    {
        transform.position = Vector3.MoveTowards(transform.position, _position, _speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }
}
