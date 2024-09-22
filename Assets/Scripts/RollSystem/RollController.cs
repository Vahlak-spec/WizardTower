using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RollController : UIPanel
{
    [SerializeField] private float _succChanse;
    [SerializeField] private Line[] _lines;
    [SerializeField] private Ress[] _reses;
    [SerializeField] private Sprite[] _maySprite;
    [SerializeField] private Button _fastButton;
    [SerializeField] private float _yOffset;
    [Space]
    [SerializeField] private int _w1;
    [SerializeField] private int _w2;
    [SerializeField] private float _speed;

    private bool _isSuc;
    private Ress _res;
    private int _l;
    private Coroutine _procces;

    public override void Show()
    {
        base.Show();

        _fastButton.onClick.RemoveAllListeners();
        _fastButton.onClick.AddListener(Stop);

        _isSuc = UnityEngine.Random.Range(0, 100) < _succChanse;
        _l = _lines.Length;

        if (_isSuc)
        {
            _res = _reses[UnityEngine.Random.Range(0, _reses.Length)];

            for (int i = 0; i < _lines.Length; i++)
            {
                _lines[i].Launch(_speed, _w1 + (_w2 * i), _yOffset, _maySprite, _res.sprite, OnComplete);
            }
        }
        else
        {
            for (int i = 0; i < _lines.Length; i++)
            {
                int r = _w1 + (_w2 * i);
                Debug.Log("R - " + r);
                _lines[i].Launch(_speed, r, _yOffset, _maySprite, _maySprite[UnityEngine.Random.Range(0, _maySprite.Length)], OnComplete);
            }
        }

        _procces = StartCoroutine(Procces());
    }

    private void Stop()
    {
        for (int i = 0; i < _lines.Length; i++)
        {
            _lines[i].Stop();
        }

        DOVirtual.DelayedCall(1, () =>
        {
            StopCoroutine(_procces);
            Hide();
        });
    }

    private void OnComplete()
    {
        _l--;
        if (_l <= 0)
        {
            DOVirtual.DelayedCall(1, () =>
            {
                StopCoroutine(_procces);
                Hide();
            });
        }
    }

    private IEnumerator Procces()
    {
        while (true)
        {
            foreach (var item in _lines)
            {
                item.OnFixed();
            }
            yield return new WaitForFixedUpdate();
        }
    }

    [System.Serializable]
    private class Line
    {
        [SerializeField] private RollCell[] _cells;

        private Sprite[] _sprites;
        private Sprite _resSprite;

        int _tw;
        int _bw;
        int _r;
        float _yOffset;
        float _speed;
        Action _onComplete;
        RollCell _lastCells;
        private bool _isComplete;

        public void Launch(float speed, int r, float yOffset, Sprite[] sprites, Sprite res, Action onComlete)
        {
            _speed = speed;
            _sprites = sprites;
            _resSprite = res;
            _yOffset = yOffset;
            _isComplete = false;
            int i = 1;

            _tw = 0;
            _bw = 0;
            _onComplete = onComlete;
            _r = r;

            _cells[0].transform.localPosition = Vector3.zero;

            for (int j = 1; j < _cells.Length; j++)
            {
                _cells[j].transform.localPosition = new Vector3(0, yOffset * i);

                if (i > 0)
                {
                    _tw++;
                    i *= -1;
                }
                else
                {
                    i *= -1;
                    i++;
                    _bw++;
                }
            }

            _bw++;
        }
        public void Stop()
        {
            _isComplete = true;

            _cells[0].transform.localPosition = Vector3.zero;
            _cells[0].SetSprite(_resSprite);

            int i = 1;

            for (int j = 1; j < _cells.Length; j++)
            {
                _cells[j].transform.localPosition = new Vector3(0, _yOffset * i);
                _cells[j].SetSprite(_sprites[UnityEngine.Random.Range(0, _cells.Length)]);
                if (i > 0)
                {
                    i *= -1;
                }
                else
                {
                    i *= -1;
                    i++;
                }
            }

        }
        float d1;
        public void OnFixed()
        {
            if (_isComplete) return;

            if (_r > 0)
            {
                for (int i = 0; i < _cells.Length; i++)
                {
                    _cells[i].transform.localPosition = new Vector3(0, _cells[i].transform.localPosition.y - _speed, 0);
                    if (_cells[i].transform.localPosition.y < (_bw * -_yOffset))
                    {
                        d1 = _cells[i].transform.localPosition.y + (_bw * _yOffset);
                        _cells[i].transform.localPosition = new Vector3(0, (_tw * _yOffset) + d1, 0);
                        _r--;
                        Debug.Log("R = " + _r);
                        if (_r <= 0)
                        {
                            _lastCells = _cells[i];
                            _lastCells.SetSprite(_resSprite);
                        }
                        else
                        {
                            _cells[i].SetSprite(_sprites[UnityEngine.Random.Range(0, _cells.Length)]);
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < _cells.Length; i++)
                {
                    _cells[i].transform.localPosition = new Vector3(0, _cells[i].transform.localPosition.y - _speed, 0);
                    if (_cells[i].transform.localPosition.y < (_bw * -_yOffset))
                    {
                        d1 = _cells[i].transform.localPosition.y + (_bw * _yOffset);
                        _cells[i].transform.localPosition = new Vector3(0, (_tw * _yOffset) + d1, 0);
                        _cells[i].SetSprite(_sprites[UnityEngine.Random.Range(0, _cells.Length)]);
                    }
                }
                if (_lastCells.transform.localPosition.y > -_speed && _lastCells.transform.localPosition.y < _speed)
                {
                    _isComplete = true;
                    _onComplete.Invoke();
                }
            }
        }
    }

    [System.Serializable]
    private class Ress
    {
        public float chanse;
        public RollRes rollRes;
        public Sprite sprite;
    }
}
