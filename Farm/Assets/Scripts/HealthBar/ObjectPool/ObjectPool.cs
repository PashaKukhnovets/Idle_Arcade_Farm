//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ObjectPool<T> where T : MonoBehaviour
//{
//    public T _prefab { get; }
//    public bool _autoExpand { get; set; }
//    public Transform _container { get; }

//    private List<T> _pool;

//    public ObjectPool(T _prefab, int _count) {
//        this._prefab = _prefab;
//        this._container = null;

//        this.CreatePool(_count);
//    }

//    public ObjectPool(T _prefab, int _count, Transform _container) {
//        this._prefab = _prefab;
//        this._container = _container;

//        this.CreatePool(_count);
//    }

//    private void CreatePool(int _count) {
//        this._pool = new List<T>();

//        for (int i = 0; i < _count; i++) {
//            this.CreateObject();
//        }
//    }

//    private T CreateObject(bool _isActiveByDefault = false) {
//        var _createdObject = Object.Instantiate(this._prefab, this._container);
//        _createdObject.gameObject.SetActive(_isActiveByDefault);
//        this._pool.Add(_createdObject);
//        return _createdObject;
//    }

//    public bool HasFreeElement(out T _element) {
//        foreach (var _object in _pool) {
//            if (!_object.gameObject.activeInHierarchy) {
//                _element = _object;
//                _object.gameObject.SetActive(true);
//                return true;
//            }
//        }
//        _element = null;
//        return false;
//    }

//    public bool HasFreeElement()
//    {
//        foreach (var _object in _pool)
//        {
//            if (!_object.gameObject.activeInHierarchy)
//            {
//                return true;
//            }
//        }
//        return false;
//    }

//    public T GetFreeElement() {
//        if (this.HasFreeElement(out var _element)) {
//            return _element;
//        }

//        if (this._autoExpand) {
//            return this.CreateObject(true);
//        }

//        throw new System.Exception($"There is no free elements in pool of type {typeof(T)}");
//    }
//}
