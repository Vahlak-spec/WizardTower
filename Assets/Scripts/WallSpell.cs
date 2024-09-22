using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WallSpell : Spell
{
    [SerializeField] private float _wallTime;
    [SerializeField] private GameObject _wallGO;

    private bool _isActive;

    private void Start()
    {
        _wallGO.SetActive(false);
    }

    public override bool TryCast()
    {
        if (_isActive) return false;

        _isActive = true;
        _wallGO.SetActive(true);
        GameSceneController.Instance.Tower.SetActive(false);

        DOVirtual.DelayedCall(_wallTime, () =>
        {
            _wallGO.SetActive(false);
            GameSceneController.Instance.Tower.SetActive(true);
            _isActive = false;
        });

        return true;
    }
}
