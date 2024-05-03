using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class ObjectsPool<T> where T : MonoBehaviour
{
    private T _prefab;
    private Transform _container;
    private bool _isAutoExpand;
    private List<T> _pool;

    public ObjectsPool(T prefab, Transform container, bool isAutoExpand, int capacity)
    {
        _prefab = prefab;
        _container = container;
        _isAutoExpand = isAutoExpand;

        CreatePool(capacity);
    }

    public T GetFreeObject()
    {
        if(TryFreeObject(out var obj))
            return obj;

        if (_isAutoExpand == true)
            return CreateObject();

        throw new Exception($"No free objects in pool of type {typeof(T)}");
    }

    private void CreatePool(int capacity)
    {
        _pool = new List<T>();

        for (int i = 0; i < capacity; i++)
            CreateObject();
    }

    private T CreateObject(bool isActive = false)
    {
        var createdObject = Object.Instantiate(_prefab, _container);
        createdObject.gameObject.SetActive(isActive);
        _pool.Add(createdObject);
        return createdObject;
    }

    private bool TryFreeObject(out T obj)
    {
        foreach (var element in _pool)
        {
            if(element.gameObject.activeInHierarchy == false)
            {
                obj = element;
                obj.gameObject.SetActive(true);
                return true;
            }
        }

        obj = null;
        return false;
    }
}
