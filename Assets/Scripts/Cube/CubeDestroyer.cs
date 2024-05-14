using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;


public class CubeDestroyer : MonoBehaviour
{
    [SerializeField] private Cube _cube;
    
    [Header("DisableTime")]
    [SerializeField] private float _minDisableTime = 2f;
    [SerializeField] private float _maxDisableTime = 5f;

    private bool _isFirstCollision = true;

    public event Action<Cube> Collided;
    public event Action<Cube> Destroyed;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Platform _) && _isFirstCollision)
        {
            _isFirstCollision = false;
            Collided?.Invoke(_cube);
            StartCoroutine(Disable(_cube));
        }
    }

    private IEnumerator Disable(Cube cube)
    {
        float time = Random.Range(_minDisableTime, _maxDisableTime);

        yield return new WaitForSeconds(time);
        Destroyed?.Invoke(cube);
        _isFirstCollision = true;
    }
}