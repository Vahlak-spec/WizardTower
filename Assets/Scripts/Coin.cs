using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Coin : Entity
{
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private float _radius;

    public override bool IsActive => _active;

    private bool _active = false;

    public override void Dismis()
    {
        if (!_active) return;

        _sprite.enabled = false;
        _active = false;
    }

    public override void Summon(Vector3 point)
    {
        transform.position = point;

        _sprite.enabled = true;
        _active = true;

        GameSystem.AddBalanseValue(BalansType.GOLD, 1);

        Vector3 dir = new Vector3(UnityEngine.Random.Range(-1, 1), UnityEngine.Random.Range(-1, 1), 0);
        dir.Normalize();
        _sprite.transform.DOJump(_sprite.transform.position + dir * _radius, 1, 1, 0.5f).OnComplete(Dismis);

    }

    public override void AddDismisAction(Action<Entity> onDismis)
    {
        throw new NotImplementedException();
    }
}
