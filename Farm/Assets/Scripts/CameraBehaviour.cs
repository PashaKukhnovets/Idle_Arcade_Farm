using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private Transform _playerTransform;
    private Vector3 offset;

    private void Start()
    {
        offset = _cameraTransform.position - _playerTransform.position;
    }

    void Update()
    {
        _cameraTransform.position = _playerTransform.position + offset; 
    }
}
