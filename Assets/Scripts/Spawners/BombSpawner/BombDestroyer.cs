using System;
using UnityEngine;

public class BombDestroyer : MonoBehaviour
{
    [SerializeField] private BombColorChanger _bombColorChanger;

    public event Action<Bomb> Destroyed;
    
    private void OnEnable() => 
        _bombColorChanger.ColorChanged += OnColorChanged;

    private void OnDisable() => 
        _bombColorChanger.ColorChanged -= OnColorChanged;

    private void OnColorChanged(Bomb bomb)
    {
        bomb.gameObject.SetActive(false);
        Destroyed?.Invoke(bomb);
        bomb.Disable();
    }
}
