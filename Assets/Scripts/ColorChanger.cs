using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Color _fallColor = Color.black;
    [SerializeField] private List<Color> _colors = new List<Color>();

    private void OnEnable()
    {
        _spawner.Spawned += OnSpawned;
        _spawner.Collided += OnCollided;
    }

    private void OnDisable()
    {
        _spawner.Spawned -= OnSpawned;
        _spawner.Collided -= OnCollided;
    }

    private void OnSpawned(Cube cube) => 
        ChangeColor(cube, true);

    private void OnCollided(Cube cube) =>
        ChangeColor(cube, false);

    private void ChangeColor(Cube cube, bool isFall)
    {
        if (cube.gameObject.TryGetComponent(out Renderer renderer))
        {
            if (isFall)
            {
                renderer.material.color = _fallColor;
            }
            else
            {
                int colorNumber = Random.Range(0, _colors.Count);
                renderer.material.color = _colors[colorNumber];
            }
        }
    }
}