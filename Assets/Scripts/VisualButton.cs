using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualButton : MonoBehaviour
{
    [SerializeField] private Image _image;
    [Space]
    [SerializeField] private Color _activeColor;
    [SerializeField] private Color _inactiveColor;

    public void SetActive(bool value)
    {
        if (value)
        {
            _image.color = _activeColor;
        }
        else
        {
            _image.color = _inactiveColor;
        }
    }
}
