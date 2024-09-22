using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusDamageTaker : MonoBehaviour
{
    [SerializeField] private BaseEnemy _enemy;
    [SerializeField] private float _dieDelay;
    [SerializeField] private ParticleSystem _light;

    private bool _isAttack;

    private void Start()
    {
        _light.gameObject.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (DamageCusterFocusType.instance.CanUse && GameSceneController.Instance.WizardDamageCuster.CanCast())
        {
            StartCoroutine(Procces());
        }
    }

    private IEnumerator Procces()
    {
        _isAttack = true;
        _light.gameObject.SetActive(true);
        _light.Play();

        yield return new WaitForSeconds(_dieDelay);

        _enemy.TryTakeDamage(GameSceneController.Instance.ElementsUIController.CurElements, GameSceneController.Instance.WizardDamageCuster.CurDamage);

        GameSceneController.Instance.ElementsUIController.Clear();
        GameSceneController.Instance.WizardDamageCuster.ClearChoise();

        _light.Stop();
        _light.gameObject.SetActive(false);
        _isAttack = false;
    }
}
