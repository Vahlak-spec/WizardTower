using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class KillAllSpell : Spell
{
    [SerializeField] private float _coolDown;

    private bool _isActive;

    public override bool TryCast()
    {
        if (_isActive) return false;

        _isActive = true;
        GameSceneController.Instance.KillAllEnemys();

        DOVirtual.DelayedCall(_coolDown, () =>
        {
            _isActive = false;
        });

        return true;
    }
}
