using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamageCusterBaseType : MonoBehaviour
{
    public abstract int Damage {  get; }
    public abstract void OnSelectType();
    public abstract void OnDeselectType();
    public abstract void FixedProcces();
}
