using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffectObj : Entity
{
    [SerializeField] private ParticleSystem _particleSystem;
    public override bool IsActive => _particleSystem.isPlaying;


    public override void AddDismisAction(Action<Entity> onDismis) { }

    public override void Dismis()
    {
        _particleSystem.Stop();
    }

    public override void Summon(Vector3 point)
    {
        transform.position = point;
        _particleSystem.Play();
    }
}
