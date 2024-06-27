//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class WheatPool : MonoBehaviour
//{
//    [SerializeField] private bool _autoExpand = false;
//    [SerializeField] private WheatBehaviour _wheatPrefab;

//    private int _wheatCount = 15;
//    private int _beginCount = 0;
//    private float _prevZ = 0f;
//    private ObjectPool<WheatBehaviour> _pool;

//    private float _minX = -9.44f;
//    private float _maxX = -6.44f;
//    private float _minZ = 3.72f;
//    private float _maxZ = 18.89f;
//    private float _rY = 0.044f;
//    private float _minRangeZ = 0.7f;

//    void Start()
//    {
//        this._pool = new ObjectPool<WheatBehaviour>(_wheatPrefab, _wheatCount, this.transform);
//        this._pool._autoExpand = this._autoExpand;
//    }

//    void Update()
//    {
//        if(_pool.HasFreeElement())
//            CreateWheat();
//    }

//    private void CreateWheat() {
//        if (_beginCount <= _wheatCount)
//            _beginCount++;
//        var _rX = Random.Range(_maxX, _minX);
//        var _rZ = Random.Range(_minZ, _maxZ);
//        while (Mathf.Abs(_rZ - _prevZ) < _minRangeZ)
//            _rZ = Random.Range(_minZ, _maxZ);
//        _prevZ = _rZ;

//        var _position = new Vector3(_rX, _rY, _rZ);
//        var _wheat = _pool.GetFreeElement();
//        _wheat.transform.position = _position;
//        if (_beginCount > _wheatCount) 
//            _wheat.StartReincornation();
//    }
//}
