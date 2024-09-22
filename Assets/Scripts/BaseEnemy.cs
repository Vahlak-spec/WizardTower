using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseEnemy : Entity
{
    [SerializeField] private GameObject _body;
    [SerializeField] private Animator _animator;
    [SerializeField] private string _attackBool;
    [Space]
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _damageValue;
    [SerializeField] private float _attackTime;
    [Space]
    [SerializeField] private int _maxHP;
    [SerializeField] private Element[] _elements;
    [Space]
    [SerializeField] private GameObject[] _visualHP;
    [SerializeField] private ElementData[] _elementDatas;
    [Space]
    [SerializeField] private Entity _drop;
    [SerializeField] private int _value;
    [Space]
    [SerializeField] private GameObject _star;

    private int _curHP;

    private EnemyState _curState;
    private Coroutine _curStateCor;
    private bool _isActive;

    private Action<Entity> _onDeath;

    private bool _isRoll;

    public override bool IsActive => _isActive;

    public bool IsRoll
    {
        set
        {
            _star.SetActive(value);
            _isRoll = value;
        }
    }

    public override void AddDismisAction(Action<Entity> onDismis)
    {
        _onDeath += onDismis;
    }

    public override void Summon(Vector3 point)
    {
        _body.transform.position = point;

        foreach (var item in _elementDatas)
        {
            item.Clear();
        }

        for (int i = 0; i < _elements.Length; i++)
        {
            foreach (var item in _elementDatas)
            {
                item.TrySetActive(_elements[i], i);
            }
        }

        _isActive = true;
        _curHP = _maxHP;
        _body.SetActive(true);
        UpdateVisualHP(_curHP);
        SetState(EnemyState.WALK);
    }

    public override void Dismis()
    {
        _isActive = false;

        if (_curStateCor != null)
            StopCoroutine(_curStateCor);

        _body.SetActive(false);
    }

    public void Death()
    {
        _curState = EnemyState.NONE;

        _onDeath?.Invoke(this);
        _onDeath = null;

        int i = _value;

        while (i > 0)
        {
            GameSceneController.Instance.Factory.Summon<Entity>(_drop, transform.position);
            i--;
        }

        if (_isRoll)
            GameSceneController.Instance.SummonRoll();

        Dismis();
    }

    public void SetState(EnemyState state)
    {
        if (_curState == state) return;

        if (_curStateCor != null)
            StopCoroutine(_curStateCor);

        _curState = state;

        Debug.Log("SetState - " + _curState);

        switch (_curState)
        {
            case EnemyState.WALK:
                _animator.SetBool(_attackBool, false);
                _curStateCor = StartCoroutine(WalkProcces());
                break;
            case EnemyState.ATTACK:
                _animator.SetBool(_attackBool, true);
                _curStateCor = StartCoroutine(AttackProcces());
                break;
        }
    }

    private IEnumerator WalkProcces()
    {
        while (true)
        {
            _body.transform.position = _body.transform.position + (Vector3.down * _walkSpeed);
            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator AttackProcces()
    {
        while (true)
        {
            yield return new WaitForSeconds(_attackTime);
            GameSceneController.Instance.Tower.TakeDamage(_damageValue);
        }
    }

    public void TryTakeDamage(Element[] elements, int damage)
    {
        if (elements.Length != _elements.Length) return;

        for (int i = 0; i < elements.Length; i++)
        {
            if (elements[i] != _elements[i]) return;
        }

        _curHP -= damage;
        UpdateVisualHP(_curHP);

        if (_curHP <= 0)
        {
            Death();
        }
    }

    private void UpdateVisualHP(int HP)
    {
        foreach (var item in _visualHP)
        {
            item.SetActive(false);
        }

        for (int i = 0; i < HP; i++)
        {
            _visualHP[i].SetActive(true);
        }
    }

    public enum EnemyState
    {
        NONE,
        WALK,
        ATTACK
    }

    [System.Serializable]
    private class ElementData
    {
        [SerializeField] private Element _elementType;
        [SerializeField] private GameObject[] _gameObject;

        public void TrySetActive(Element elementType, int i)
        {
            if (elementType == _elementType)
                _gameObject[i].SetActive(true);
        }

        public void Clear()
        {
            foreach (var item in _gameObject)
            {
                item.SetActive(false);
            }
        }
    }
}
