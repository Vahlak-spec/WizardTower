using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCusterFocusType : DamageCusterBaseType
{
    public override int Damage => 2;

    [HideInInspector] public bool CanUse;

    public static DamageCusterFocusType instance;

    private void Start()
    {
        instance = this;
    }

    public override void FixedProcces()
    {

    }

    public override void OnDeselectType()
    {
        CanUse = false;
    }

    public override void OnSelectType()
    {
        CanUse = true;
    }
}
