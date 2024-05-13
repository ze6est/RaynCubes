using System;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    
    public event Action<Cube> Collided;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Platform _))
        {
            _rigidbody.velocity = Vector3.zero;
            Collided?.Invoke(this);
        }
    }
}