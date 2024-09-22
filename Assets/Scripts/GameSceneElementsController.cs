using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneElementsController : MonoBehaviour
{
    public Element[] CurElements => _curElements.ToArray();

    [SerializeField] private ElementButtons[] _elementUIs;
    [SerializeField] private ElementVisual[] _elementVisuals;
    [Space]
    [SerializeField] private int _maxElements;

    private List<Element> _curElements;

    public void Init(Element[] openElents)
    {
        _curElements = new List<Element>();

        foreach(var item in _elementUIs)
        {
            item.DeActive();
        }

        foreach(var item in _elementVisuals)
        {
            item.Clear();
        }

        foreach (var elemetType in openElents)
        {
            foreach(var item in _elementUIs)
            {
                if(item.element == elemetType)
                {
                    item.Active(AddElementToCombo);
                }
            }
        }
    }

    private void AddElementToCombo(Element element)
    {
        if (_curElements.Count >= _maxElements) return;

        _curElements.Add(element);

        foreach (var item in _elementVisuals)
        {
            item.TryActive(element, _curElements.Count - 1);
        }
    }

    public void Clear()
    { 
        _curElements.Clear();

        foreach (var item in _elementVisuals)
        {
            item.Clear();
        }
    }

    [System.Serializable]
    private class ElementVisual
    {
        [SerializeField] private Element _element;
        [SerializeField] private GameObject[] gameObjects;

        public void TryActive(Element element, int i)
        {
            if (element == _element)
                gameObjects[i].SetActive(true);
        }

        public void Clear()
        {
            foreach (var item in gameObjects)
                item.SetActive(false);
        }
    }

    [System.Serializable]
    private class ElementButtons 
    {
        public Element element;
        [SerializeField] private Button button;

        private Action<Element> _onClick;

        public void Active(Action<Element> onClick)
        {
            _onClick = onClick;

            button.onClick.RemoveAllListeners();
            button.interactable = true;

            button.onClick.AddListener(OnClick);
        }

        public void DeActive()
        {
            button.onClick.RemoveAllListeners();
            button.interactable = false;
        }

        public void OnClick()
        {
            _onClick?.Invoke(element);
        }
    }
}
