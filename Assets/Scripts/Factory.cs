using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    [SerializeField] private FactoryData[] _factoryDatas;

    public void Init()
    {
        foreach (var item in _factoryDatas)
            item.Init();
    }

    public T Summon<T>(Entity entity, Vector3 pos)
    {
        foreach(var item in _factoryDatas)
        {
            if (item.IsEquels(entity))
            {
                return item.Summon<T>(pos);
            }
        }

        return _factoryDatas[0].Summon<T>(pos);
    }

    [System.Serializable]
    private class FactoryData
    {
        [SerializeField] private Entity _enetityPref;
        [SerializeField] private int _num;

        private Entity[] _entitys;

        public bool IsEquels(Entity entity)
        {
            return entity.name == _enetityPref.name;
        }
        public T Summon<T>(Vector3 pos)
        {
            foreach(var item in _entitys)
            {
                if (!item.IsActive)
                {
                    item.Summon(pos);
                    return item.GetComponent<T>();
                }
            }

            return _entitys[0].GetComponent<T>();
        }
        public void Init()
        {
            _entitys = new Entity[_num];

            for (int i = 0; i < _entitys.Length; i++)
            {
                _entitys[i] = Instantiate(_enetityPref);
                _entitys[i].Dismis();
            }
        }
    }
}
