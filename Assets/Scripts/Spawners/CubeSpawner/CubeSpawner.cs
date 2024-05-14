using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubeSpawner : Spawner<Cube>
{
    [Header("Settings")]
    [SerializeField] private Platform[] _platforms;
    [SerializeField] private float _height;
    [SerializeField] private float _spawnDelay = 1f;
    
    public event Action<Cube> Destroyed;
    
    private void Start() => 
        StartCoroutine(SpawnCubes());
    
    private void OnDestroyed(Cube cube)
    {
        Release(cube);
        Destroyed?.Invoke(cube);
        
        if (cube.TryGetComponent(out CubeDestroyer cubeDestroyer))
        {
            cubeDestroyer.Destroyed -= OnDestroyed;
        }
    }    

    private IEnumerator SpawnCubes()
    {
        WaitForSeconds wait = new WaitForSeconds(_spawnDelay);

        while (true)
        {
            Cube freeCube = Spawn();
            
            if (freeCube.TryGetComponent(out CubeDestroyer cubeDestroyer))
            {
                cubeDestroyer.Destroyed += OnDestroyed;
            }

            if (freeCube.TryGetComponent(out Rigidbody rigidbody))
            {
                rigidbody.velocity = Vector3.zero;
                rigidbody.angularVelocity = Vector3.zero;
            }

            freeCube.transform.rotation = Quaternion.Euler(0, 0, 0);
            
            SetPosition(freeCube);

            yield return wait;
        }
    }
    
    private void SetPosition(Cube cube)
    {
        Platform platform = _platforms[Random.Range(0, _platforms.Length)];
        
        Renderer renderer = platform.GetComponent<Renderer>();
        Bounds bounds = renderer.bounds;
        float objectScale = cube.transform.localScale.x;

        float positionX = Random.Range(bounds.max.x - objectScale / 2, bounds.min.x + objectScale / 2);
        float positionZ = Random.Range(bounds.max.z - objectScale / 2, bounds.min.z + objectScale / 2);
        
        cube.transform.position = new Vector3(positionX, _height, positionZ);
    }
}
