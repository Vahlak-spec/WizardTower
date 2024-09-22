using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float _expTime;
    [Space]
    [SerializeField] private Collider2D _collider;
    [SerializeField] private ParticleSystem _particleSystem;

    private Coroutine _procces;

    private Element[] _curElements;
    private int _damage;

    public void Summon(Vector3 point)
    {
        transform.position = point;
        _particleSystem.gameObject.SetActive(true);
        _particleSystem.Play();

        _curElements = GameSceneController.Instance.ElementsUIController.CurElements;
        GameSceneController.Instance.ElementsUIController.Clear();

        _damage = GameSceneController.Instance.WizardDamageCuster.CurDamage;
        GameSceneController.Instance.WizardDamageCuster.ClearChoise();

        _procces = StartCoroutine(Procces());


    }

    public void Hide()
    {
        _collider.enabled = false;
        _particleSystem.gameObject.SetActive(false);

        if (_procces != null)
            StopCoroutine(_procces);
    }

    private IEnumerator Procces()
    {
        yield return new WaitForSeconds(_expTime/2);

        _collider.enabled = true;

        yield return new WaitForSeconds(_expTime / 2);

        Hide();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<BaseEnemy>(out BaseEnemy enemy))
        {
            enemy.TryTakeDamage(_curElements, _damage);
        }
    }
}
