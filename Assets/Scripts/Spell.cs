using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    public BalansType BalansType => _balansType;

    [SerializeField] private BalansType _balansType;

    public abstract bool TryCast();
}
