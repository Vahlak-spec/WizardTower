using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RollCell : MonoBehaviour
{
    [SerializeField] private Image _image;

    public void SetSprite(Sprite sprite)
    {
        _image.sprite = sprite;

    }
}
