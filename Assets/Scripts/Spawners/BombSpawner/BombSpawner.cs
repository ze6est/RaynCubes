using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    [SerializeField] private CubeDestroyer _cubeDestroyer;
    [SerializeField] private BombDestroyer _bombDestroyer;
    
    private void OnEnable()
    {
        _cubeDestroyer.Destroyed += OnDestroyed;
        _bombDestroyer.Destroyed += Release;
    }

    private void OnDisable()
    {
        _cubeDestroyer.Destroyed -= OnDestroyed;
        _bombDestroyer.Destroyed -= Release;
    }

    private void OnDestroyed(Cube cube)
    {
        Spawn(out Bomb bomb);
        SetPosition(bomb, cube.transform.position);
    }

    private void SetPosition(Bomb bomb, Vector3 position) => 
        bomb.transform.position = position;
}
