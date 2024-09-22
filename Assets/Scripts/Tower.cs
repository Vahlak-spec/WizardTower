using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Unity.Mathematics;

public class Tower : MonoBehaviour
{
    [SerializeField] private Image _fill;

    private float _curHP;
    private float _maxHP;

    private Action _onLose;

    private bool _isActive;

    public void SetActive(bool isActive)
    {
        _isActive = isActive;
    }

    public void AddHP(float value)
    {
        _curHP += value;
        _fill.fillAmount = _curHP / _maxHP;
    }

    public void Init(float maxHP, Action onLose)
    {
        _isActive = true;

        _curHP = maxHP;
        _maxHP = maxHP;

        _onLose = onLose;
    }

    public void TakeDamage(float damage)
    {
        if (!_isActive) return;

        _curHP -= damage;
        _fill.fillAmount = _curHP / _maxHP;

        if(_curHP <= 0)
        {
            _onLose?.Invoke();
            _onLose = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("TrySetAttack");
        if (collision.TryGetComponent<BaseEnemy>(out BaseEnemy enemy))
        {
            Debug.Log("SetAttack");
            enemy.SetState(BaseEnemy.EnemyState.ATTACK);
        }
    }


}
