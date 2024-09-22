using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollResGiveMoney : RollRes
{
    [SerializeField] private Entity _coinPrefab;
    [SerializeField] private Transform[] _givePoints;
    [SerializeField] private int _moneyCount;

    public override void Activate()
    {
        Vector2 newPoint = Vector3.zero;
        for(int i = 0; i < _moneyCount; i++) 
        {
            newPoint = _givePoints[UnityEngine.Random.Range(0, _givePoints.Length)].position;
            newPoint.x = newPoint.x + UnityEngine.Random.Range(-2, 2);
            newPoint.y = newPoint.y + UnityEngine.Random.Range(-2, 2);

            GameSceneController.Instance.Factory.Summon<Entity>(_coinPrefab, newPoint);
        }
    }
}
