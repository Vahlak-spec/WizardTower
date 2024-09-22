using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Entity : MonoBehaviour
{
    public abstract bool IsActive { get; }
    public abstract void AddDismisAction(Action<Entity> onDismis);
    public abstract void Summon(Vector3 point);
    public abstract void Dismis();
}
