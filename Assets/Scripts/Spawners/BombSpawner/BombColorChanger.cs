using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class BombColorChanger : MonoBehaviour
{
    [SerializeField] private BombSpawner _spawner;
    [SerializeField] private Color _startColor;
    [SerializeField] private Color _endColor;
    [SerializeField] private float _minTime = 2f;
    [SerializeField] private float _maxTime = 5f;

    public event Action<Bomb> ColorChanged;
    
    private void OnEnable() => 
        _spawner.Spawned += OnSpawned;

    private void OnDisable() => 
        _spawner.Spawned -= OnSpawned;

    private void OnSpawned(Bomb bomb) => 
        StartCoroutine(ChangeColor(bomb));
    
    private IEnumerator ChangeColor(Bomb bomb)
    {
        if (bomb.TryGetComponent(out Renderer renderer))
        {
            float currentTime = 0;
            float time = Random.Range(_minTime, _maxTime);
            
            do 
            {
                renderer.material.color = Color.Lerp (_startColor, _endColor, currentTime/time);
                currentTime += Time.deltaTime;
                yield return null;
            } 
            while (currentTime <= time);
        }
        
        ColorChanged?.Invoke(bomb);
    }
}
