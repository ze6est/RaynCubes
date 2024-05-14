using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    [Header("Settings")]
    [SerializeField] private CubeSpawner _cubeSpawner;
    
    private void OnEnable() => 
        _cubeSpawner.Destroyed += OnCubeDestroyed;

    private void OnDisable() => 
        _cubeSpawner.Destroyed -= OnCubeDestroyed;

    private void OnCubeDestroyed(Cube cube)
    {
        Bomb bomb = Spawn();

        if (bomb.TryGetComponent(out BombDestroyer bombDestroyer)) 
            bombDestroyer.Destroyed += OnDestroyed;

        bomb.Enable();
        SetPosition(bomb, cube.transform.position);
    }
    
    private void OnDestroyed(Bomb bomb)
    {
        Release(bomb);
        
        if (bomb.TryGetComponent(out BombDestroyer bombDestroyer)) 
            bombDestroyer.Destroyed -= OnDestroyed;
    }

    private void SetPosition(Bomb bomb, Vector3 position) => 
        bomb.transform.position = position;
}
