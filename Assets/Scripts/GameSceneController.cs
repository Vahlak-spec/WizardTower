using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneController : MonoBehaviour
{
    [SerializeField] private Factory _factory;
    [SerializeField] private BaseLevelData[] _levelDatas;
    [SerializeField] private GameSceneElementsController _elementsUIController;
    [SerializeField] private WizardDamageCuster _wizardDamageCuster;
    [SerializeField] private Tower _tower;
    [SerializeField] private SpellController _spellController;
    [SerializeField] private RollController _rollController;
    [Space]
    [SerializeField] private Vector2 _spawenMin;
    [SerializeField] private Vector2 _spawenMax;
    [Space]
    [SerializeField] private UIPanel _winPanel;
    [SerializeField] private UIPanel _losePanel;
    [Space]
    [SerializeField] private ParticleEffectObj _killAllEffect;

    private Coroutine _procces;
    private int _coinsOnStart;
    private int _enemysPerLevel;
    private BaseLevelData _levelData;

    public static GameSceneController Instance => instance;
    public Factory Factory => _factory;
    public Tower Tower => _tower;
    public GameSceneElementsController ElementsUIController => _elementsUIController;
    public WizardDamageCuster WizardDamageCuster => _wizardDamageCuster;

    public int CoinsPerLevel => GameSystem.GetBalanseValue(BalansType.GOLD) - _coinsOnStart;
    public int EnemysPerLevel => _enemysPerLevel;

    private static GameSceneController instance;


    private List<BaseEnemy> _tempEnemys;
    public void KillAllEnemys()
    {
        Debug.Log(_tempEnemys.Count);

        int lenth = _tempEnemys.Count;
        for (int i = 0; i < lenth; i++)
        {
            _factory.Summon<Entity>(_killAllEffect, _tempEnemys[0].transform.position);
            _tempEnemys[0].Death();
        }

        _tempEnemys.Clear();
    }

    public void SummonRoll()
    {
        if (_rollController.IsShow) return;

        _rollController.Show();
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(Scenes.MainMenu.ToString());
    }
    public void StartLevel()
    {
        int tempId = GameSystem.OpenedLevel;

        foreach (var item in _levelDatas)
        {
            if (item.Id == tempId)
            {
                _levelData = item;
                break;
            }
        }

        _levelData.Init();

        _coinsOnStart = GameSystem.GetBalanseValue(BalansType.GOLD);

        _elementsUIController.Init(_levelData.openedElements);
        _factory.Init();
        _tower.Init(100, Lose);
        _wizardDamageCuster.Init();
        _spellController.Init();

        _tempEnemys = new List<BaseEnemy>();
        _procces = StartCoroutine(GameProcces());

        _winPanel.Hide();
        _losePanel.Hide();
        _rollController.Hide();
    }

    private void Start()
    {
        instance = this;

        StartLevel();
    }


    private IEnumerator GameProcces()
    {
        while (true)
        {
            if (!_levelData.CanSpawn)
            {
                yield return new WaitForEndOfFrame();
                continue;
            }

            Vector3 pos = new Vector3(Random.Range(_spawenMin.x, _spawenMax.x), Random.Range(_spawenMin.y, _spawenMax.y), 0);
            BaseEnemy entity = _factory.Summon<BaseEnemy>(_levelData.GetEnemyPref(), pos);
            entity.AddDismisAction(OnEnemyDeath);

            if (_levelData.IsRoll)
                entity.IsRoll = true;
            else
                entity.IsRoll = false;

            _tempEnemys.Add(entity);
            yield return new WaitForSeconds(_levelData.SpawenTime);
        }
    }
    private void OnEnemyDeath(Entity entity)
    {
        _tempEnemys.Remove(entity.GetComponent<BaseEnemy>());
        _enemysPerLevel++;
        if (!_levelData.CanSpawn && _tempEnemys.Count == 0)
        {
            Won();
        }
    }
    private void Won()
    {
        _levelData.OnComplete();

        int lenth = _tempEnemys.Count;
        for (int i = 0; i < lenth; i++)
        {
            _tempEnemys[0].Dismis();
        }

        _tempEnemys.Clear();

        StopCoroutine(_procces);

        _winPanel.Show();
    }
    private void Lose()
    {
        int lenth = _tempEnemys.Count;
        for (int i = 0; i < lenth; i++)
        {
            _tempEnemys[0].Dismis();
        }

        _tempEnemys.Clear();

        StopCoroutine(_procces);

        _losePanel.Show();
    }
}
