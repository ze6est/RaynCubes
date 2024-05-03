using System.Collections;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private float _minDisableTime = 2f;
    [SerializeField] private float _maxDisableTime = 5f;

    private void OnEnable() => 
        _spawner.Collided += OnCollided;

    private void OnDisable() => 
        _spawner.Collided -= OnCollided;

    private void OnCollided(Cube cube) => 
        StartCoroutine(Disable(cube));

    private IEnumerator Disable(Cube cube)
    {
        float time = Random.Range(_minDisableTime, _maxDisableTime);

        yield return new WaitForSeconds(time);
        cube.gameObject.SetActive(false);
    }
}