using System;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public event Action Disabled;
    
    public void Disable() => 
        Disabled?.Invoke();
}
