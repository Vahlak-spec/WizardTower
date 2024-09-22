using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelData")]
public class BaseLevelData : ScriptableObject
{
    public int Id => _id;
    public bool CanSpawn => _enemysLeft > 0 || _isInfinit;
    public float SpawenTime => Random.Range(_spawenTimeMin,_spawenTimeMax);

    public Element[] openedElements;

    [SerializeField] EnemyData[] _enemyDatas;
    [SerializeField] private int _enemysToWon; 
    [SerializeField] private float _spawenTimeMin;
    [SerializeField] private float _spawenTimeMax;
    [SerializeField] private float _rollChanse;
    [SerializeField] private bool _isInfinit;
    [SerializeField] private int _id;

    private int _enemysLeft;

    public bool IsRoll => Random.Range(0, 100) < _rollChanse;
    public void Init()
    {
        _enemysLeft = _enemysToWon;
    }

    public void OnComplete()
    {
        if (!_isInfinit) GameSystem.OpenedLevel++;
    }

    public Entity GetEnemyPref()
    {
        int i = Random.Range(0, 100);
        int j = 0;

        _enemysLeft--;

        Debug.Log("Enemys Left - " + _enemysLeft);

        foreach (var item in _enemyDatas)
        {
            j += item.chanse;

            if(i < j)
            {
                return item._entity;
            }
        }
        return null;
    }

    [System.Serializable]
    private class EnemyData
    {
        public Entity _entity;
        public int chanse;
    }
}
