using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIPanel : MonoBehaviour
{
    [SerializeField] private Transform _scalePanel;
    [SerializeField] private Vector3 _showScale;
    [Space]
    [SerializeField] private GameObject _back;

    public bool IsShow => _back.activeSelf;

    public virtual void Show()
    {
        _back.SetActive(true);
        _scalePanel.DOScale(_showScale, 1);
    }
    public virtual void Hide()
    {
        _back.SetActive(false);
        _scalePanel.localScale = Vector3.zero;
    }
}
