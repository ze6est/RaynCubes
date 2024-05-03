using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private Platform _platform;
    [SerializeField] private float _height = 5;
    [SerializeField] private int _poolCapacity = 10;
    [SerializeField] private bool _isAutoExpandPool = true;
    [SerializeField] private float _spawnDelay = 1f;        

    private ObjectsPool<Cube> _poolCubes;

    public event Action<Cube> Spawned;
    public event Action<Cube> Collided;

    private void Awake() => 
        _poolCubes = new ObjectsPool<Cube>(_prefab, transform, _isAutoExpandPool, _poolCapacity);

    private void Start() => 
        StartCoroutine(SpawnCubes());

    private IEnumerator SpawnCubes()
    {
        WaitForSeconds wait = new WaitForSeconds(_spawnDelay);

        while (true)
        {
            Cube freeCube = _poolCubes.GetFreeObject();
            Spawned?.Invoke(freeCube);
            freeCube.Collided += OnCollided;
            SetPosition(freeCube);

            yield return wait;
        }
    }

    private void OnCollided(Cube cube)
    {
        Collided?.Invoke(cube);        
        cube.Collided -= OnCollided;
    }    

    private void SetPosition(Cube cube)
    {
        Renderer renderer = _platform.GetComponent<Renderer>();
        float cubeSize = cube.transform.localScale.x;

        float positionX = renderer.bounds.size.x / 2 - cubeSize / 2;
        float positionZ = renderer.bounds.size.z / 2 - cubeSize / 2;

        cube.transform.position = new Vector3(Random.Range(-positionX, positionX), _height, Random.Range(-positionZ, positionZ));
    }    
}