using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private CubeDestroyer _cubeDestroyer;
    [SerializeField] private Platform _platform;
    [SerializeField] private float _height;
    [SerializeField] private float _spawnDelay = 1f;
    
    public event Action<Cube> Collided;
    
    private void Start()
    {
        StartCoroutine(SpawnCubes());
        _cubeDestroyer.Destroyed += Release;
    }

    private void OnDestroy() => 
        _cubeDestroyer.Destroyed -= Release;

    private IEnumerator SpawnCubes()
    {
        WaitForSeconds wait = new WaitForSeconds(_spawnDelay);

        while (true)
        {
            Spawn(out Cube freeCube);
            SetPosition(freeCube);
            freeCube.Collided += OnCollided;

            yield return wait;
        }
    }
    
    private void SetPosition(Cube cube)
    {
        Renderer renderer = _platform.GetComponent<Renderer>();
        Bounds bounds = renderer.bounds;
        float objectScale = cube.transform.localScale.x;

        float positionX = bounds.size.x / 2 - objectScale / 2;
        float positionZ = bounds.size.z / 2 - objectScale / 2;
        
        cube.transform.position = new Vector3(Random.Range(-positionX, positionX), _height, Random.Range(-positionZ, positionZ));
    }

    private void OnCollided(Cube cube)
    {
        Collided?.Invoke(cube);        
        cube.Collided -= OnCollided;
    }    
}
