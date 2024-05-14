using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class BombColorChanger : MonoBehaviour
{
    [SerializeField] private Bomb _bomb;
    [SerializeField] private Renderer _renderer;

    [Header("Colors")] 
    [SerializeField] private Color _startColor;
    [SerializeField] private Color _endColor;

    [Header("TimeChanged")] 
    [SerializeField] private float _minTime = 2f;
    [SerializeField] private float _maxTime = 5f;

    public event Action<Bomb> ColorChanged;

    private void Awake() => 
        _bomb.Enabled += OnEnabled;

    private void OnDestroy() =>
        _bomb.Enabled -= OnEnabled;

    private void OnEnabled() =>
        StartCoroutine(ChangeColor(_bomb));

    private IEnumerator ChangeColor(Bomb bomb)
    {
        float currentTime = 0;
        float time = Random.Range(_minTime, _maxTime);

        do
        {
            _renderer.material.color = Color.Lerp(_startColor, _endColor, currentTime / time);
            currentTime += Time.deltaTime;
            yield return null;
        } while (currentTime <= time);

        ColorChanged?.Invoke(bomb);
    }
}