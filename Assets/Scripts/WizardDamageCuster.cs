using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WizardDamageCuster : MonoBehaviour
{
    public int CurDamage => _curType.Damage;

    [SerializeField] private ButtonData[] _buttonDatas;
    [SerializeField] private Image _image;
    [Space]
    [SerializeField] private float _maxMana;
    [SerializeField] private float _manaPerSecond;
    [SerializeField] private Image _fill;

    private float _tempMana;
    private float _manaAddW;

    private DamageCusterBaseType _curType;

    public bool CanCast()
    {
        foreach(var item in _buttonDatas)
        {
            if(item.type == _curType)
            {
                if(_tempMana >= item.manaValue)
                {
                    _tempMana -= item.manaValue;
                    _fill.fillAmount = _tempMana / _maxMana;
                    return true;
                }
                return false;
            }
        }
        return false;
    }

    public void Init()
    {
        _tempMana = _maxMana;
        _fill.fillAmount = 1;
        _image.enabled = false;
        _manaAddW = 1;

        foreach (var item in _buttonDatas)
        {
            item.button.onClick.AddListener(() => SellectType(item));
        }
    }


    private void Update()
    {
        if (_curType != null)
            _curType.FixedProcces();

        _manaAddW -= Time.deltaTime;

        if (_manaAddW <= 0)
        {
            _manaAddW = 1;
            _tempMana = Mathf.Clamp(_tempMana + _manaPerSecond, 0, _maxMana);
            _fill.fillAmount = _tempMana / _maxMana;
        }
    }

    private void SellectType(ButtonData data)
    {
        if (_curType != null)
            _curType.OnDeselectType();

        _image.enabled = true;
        _image.sprite = data.image.sprite;

        _curType = data.type;
        _curType.OnSelectType();
    }

    public void ClearChoise()
    {
        _curType.OnDeselectType();
        _curType = null;
        _image.enabled = false;
    }

    [System.Serializable]
    private class ButtonData
    {
        public DamageCusterBaseType type;
        public Button button;
        public Image image;
        public float manaValue;
        //public VisualButton visualButton;
    }
}
