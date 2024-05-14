using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubeColorChanger : MonoBehaviour
{
    [SerializeField] private CubeDestroyer _cubeDestroyer;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Color _fallColor = Color.black;
    
    [Header("ColorToChange")]
    [SerializeField] private List<Color> _colors = new List<Color>();

    private void Awake()
    {
        _cubeDestroyer.Collided += OnCollided;
        _cubeDestroyer.Destroyed += OnDestroyed;
    }

    private void OnDestroy()
    {
        _cubeDestroyer.Collided -= OnCollided;
        _cubeDestroyer.Destroyed -= OnDestroyed;
    }
    
    private void OnCollided(Cube _) =>
        ChangeColor(false);

    private void OnDestroyed(Cube _) => 
        ChangeColor(true);

    private void ChangeColor(bool isFall)
    {
        if (isFall)
        {
            _renderer.material.color = _fallColor;
        }
        else
        {
            int colorNumber = Random.Range(0, _colors.Count);
            _renderer.material.color = _colors[colorNumber];
        }
    }
}