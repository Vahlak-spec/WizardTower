using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BalanceCounter : MonoBehaviour
{
    [SerializeField] private Text _count;
    [SerializeField] private BalansType _balansType;

    private void Awake()
    {
        GameSystem.SetChangeBalanseAction(_balansType, OnSetBalanse);
        _count.text = GameSystem.GetBalanseValue(_balansType).ToString();
    }
    private void OnSetBalanse()
    {
        _count.text = GameSystem.GetBalanseValue(_balansType).ToString();
    }
}
