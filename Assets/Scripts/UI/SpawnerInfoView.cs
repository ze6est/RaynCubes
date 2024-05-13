using TMPro;
using UnityEngine;

public class SpawnerInfoView<TSpawner, T> : MonoBehaviour where TSpawner : Spawner<T> where T : MonoBehaviour
{
    [SerializeField] private TSpawner _spawner;
    [SerializeField] private TextMeshProUGUI _countCreatedObjects;
    [SerializeField] private TextMeshProUGUI _counActiveObjects;

    private void Start()
    {
        _spawner.Spawned += OnSpawned;
        _spawner.CountActivedObjectsChanged += OnCountActivedObjectsChanged;
    }

    private void OnDestroy()
    {
        _spawner.Spawned -= OnSpawned;
        _spawner.CountActivedObjectsChanged -= OnCountActivedObjectsChanged;
    }

    private void OnSpawned(MonoBehaviour obj) => 
        _countCreatedObjects.text = _spawner.CountCreatedObjects.ToString();
    
    private void OnCountActivedObjectsChanged(int count) => 
        _counActiveObjects.text = count.ToString();
}
