using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Text _levelText;
    [SerializeField] private Menu[] _menus;


    private void Start()
    {
        _playButton.onClick.AddListener(OnPlayButtonPress);

        _levelText.text = "Level " + GameSystem.OpenedLevel;
    }

    private void OnPlayButtonPress()
    {
        Debug.Log("OnPlayButtonPress");

        GameSystem.CurLevelId = GameSystem.OpenedLevel;

        SceneManager.LoadScene(Scenes.GameScene.ToString());
    }

    [System.Serializable]
    private class Menu
    {
        [SerializeField] private Button _button;
        [SerializeField] private GameObject _menu;

        public void Init()
        {
            _menu.SetActive(false);

            _button.onClick.AddListener(() => { _menu.SetActive(true); });
        }
    }
}
