using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinPanel : UIPanel
{
    [SerializeField] private Button _completeButton;
    [SerializeField] private Text _coinsCout;
    [SerializeField] private Text _enemysCout;

    public override void Show()
    {
        base.Show();
        _completeButton.onClick.RemoveAllListeners();
        _completeButton.onClick.AddListener(CompleteButtonPress);

        _coinsCout.text = GameSceneController.Instance.CoinsPerLevel.ToString();
        _enemysCout.text = GameSceneController.Instance.EnemysPerLevel.ToString();
    }

    private void CompleteButtonPress()
    {
        GameSceneController.Instance.GoToMainMenu();
    }
}
