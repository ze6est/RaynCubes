using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private BombColorChanger _bombColorChanger;
    [SerializeField] private float _radius;
    [SerializeField] private float _force;

    private void Awake() => 
        _bombColorChanger.ColorChanged += OnColorChanged;

    private void OnDestroy() => 
        _bombColorChanger.ColorChanged -= OnColorChanged;

    private void OnColorChanged(Bomb bomb) => 
        Explode();

    private void Explode()
    {
        foreach (Rigidbody explodableObject in GetExplodableObjects())
            explodableObject.AddExplosionForce(_force, transform.position, _radius);
    }
    
    private List<Rigidbody> GetExplodableObjects()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _radius);

        List<Rigidbody> objects = new();

        foreach (Collider hit in hits)
        {
            if (hit.attachedRigidbody != null)
                objects.Add(hit.attachedRigidbody);
        }

        return objects;
    }
}
