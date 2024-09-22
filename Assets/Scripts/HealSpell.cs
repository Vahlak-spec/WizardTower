using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSpell : Spell
{
    [SerializeField] private float _coolDown;
    [SerializeField] private float _value;

    private bool _isActive;

    public override bool TryCast()
    {
        if (_isActive) return false;

        _isActive = true;
        GameSceneController.Instance.Tower.AddHP(_value);

        DOVirtual.DelayedCall(_coolDown, () =>
        {
            _isActive = false;
        });

        return true;
    }
}
