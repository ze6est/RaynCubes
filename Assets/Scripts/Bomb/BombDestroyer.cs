using System;
using UnityEngine;

public class BombDestroyer : MonoBehaviour
{
    [SerializeField] private BombColorChanger _bombColorChanger;

    public event Action<Bomb> Destroyed;
    
    private void Awake() => 
        _bombColorChanger.ColorChanged += OnColorChanged;

    private void OnDestroy() => 
        _bombColorChanger.ColorChanged -= OnColorChanged;

    private void OnColorChanged(Bomb bomb) => 
        Destroyed?.Invoke(bomb);
}
