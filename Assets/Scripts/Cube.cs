using System;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public event Action<Cube> Collided;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Platform platform))                    
            Collided?.Invoke(this);        
    }
}