using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCusterExplType : DamageCusterBaseType
{
    [SerializeField] private Explosion _explosion;

    public override int Damage => 1;

    private void Start()
    {
        _explosion.Hide();
    }

    public override void FixedProcces()
    {
        if (Input.GetMouseButtonDown(0) && GameSceneController.Instance.WizardDamageCuster.CanCast())
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            _explosion.Summon(pos);
        }
    }

    public override void OnDeselectType()
    {

    }

    public override void OnSelectType()
    {

    }
}
