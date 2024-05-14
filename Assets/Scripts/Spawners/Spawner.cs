using System;
using UnityEngine;

public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _prefab;
    
    [Header("Pool Settings")]
    [SerializeField] private int _poolCapacity = 10;
    [SerializeField] private bool _isAutoExpandPool = true;
    
    private ObjectsPool<T> _pool;
    
    public event Action<T> Spawned;
    public event Action<int> CountActivedObjectsChanged;
    
    public int CountCreatedObjects { get; private set; }
    
    private void Awake() => 
        _pool = new ObjectsPool<T>(_prefab, transform, _isAutoExpandPool, _poolCapacity);
    
    protected T Spawn()
    {
        T @object = _pool.GetFreeObject();
        CountCreatedObjects++;
        CountActivedObjectsChanged?.Invoke(_pool.CountActiveObjects);
        Spawned?.Invoke(@object);

        return @object;
    }

    protected void Release(T @object)
    {
        _pool.Release(@object);
        CountActivedObjectsChanged?.Invoke(_pool.CountActiveObjects);
    }
}