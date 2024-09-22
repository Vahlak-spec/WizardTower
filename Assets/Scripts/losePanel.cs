using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class losePanel : UIPanel
{
    [SerializeField] private Button _toMainMenuButton;
    [SerializeField] private Button _restartLevel;
    [Space]
    [SerializeField] private Text _coinsCout;
    [SerializeField] private Text _enemysCout;

    public override void Show()
    {
        base.Show();
        _toMainMenuButton.onClick.RemoveAllListeners();
        _toMainMenuButton.onClick.AddListener(CompleteButtonPress);

        _restartLevel.onClick.RemoveAllListeners();
        _restartLevel.onClick.AddListener(RestartButtonPress);

        _coinsCout.text = GameSceneController.Instance.CoinsPerLevel.ToString();
        _enemysCout.text = GameSceneController.Instance.EnemysPerLevel.ToString();
    }

    private void CompleteButtonPress()
    {
        GameSceneController.Instance.GoToMainMenu();
    }

    private void RestartButtonPress()
    {
        GameSceneController.Instance.StartLevel();

    }
}
