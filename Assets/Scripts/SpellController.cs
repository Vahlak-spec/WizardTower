using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SpellController : MonoBehaviour
{
    [SerializeField] private SpellData[] _spellDatas;

    public void Init()
    {
        foreach(var item in _spellDatas)
            item.Init();
    }

    [System.Serializable]
    private class SpellData
    {
        [SerializeField] private Spell spell;
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI count;
        [SerializeField] private BalansType balanse;

        private int _value;

        public void Init()
        {
            _value = GameSystem.GetBalanseValue(balanse);
            count.text = _value.ToString();

            button.onClick.AddListener(OnPressButton);
        }

        private void OnPressButton()
        {
            if (_value <= 0) return;

            if (spell.TryCast())
            {
                GameSystem.AddBalanseValue(balanse, -1);
                _value--;
            }

            count.text = _value.ToString();
        }
    }
}
