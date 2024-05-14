using System;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public event Action Enabled;

    public void Enable() => 
        Enabled?.Invoke();
}
