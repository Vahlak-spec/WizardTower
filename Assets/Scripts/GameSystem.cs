using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public static class GameSystem
{
    private static List<OnChangeBalance> onChangeBalances = new List<OnChangeBalance>();

    private class OnChangeBalance
    {
        public OnChangeBalance(BalansType balansType, Action action)
        {
            this.balansType = balansType;
            this.action = action;
        }

        public BalansType balansType;
        public Action action;
    }

    public static void SetChangeBalanseAction(BalansType balansType, Action action)
    {
        foreach(var item in onChangeBalances)
        {
            if(item.balansType == balansType)
            {
                item.action = action;
                return;
            }
        }

        onChangeBalances.Add(new OnChangeBalance(balansType, action));
    }

    public static int GetBalanseValue(BalansType balansType)
    {
        if (!PlayerPrefs.HasKey(balansType.ToString()))
        {
            PlayerPrefs.SetInt(balansType.ToString(), 0);
            PlayerPrefs.Save();
            return 0;
        }

        return PlayerPrefs.GetInt(balansType.ToString());
    }

    public static void AddBalanseValue(BalansType balansType, int value)
    {
        int newValue = 0;

        if (PlayerPrefs.HasKey(balansType.ToString()))
            newValue = PlayerPrefs.GetInt(balansType.ToString());

        PlayerPrefs.SetInt(balansType.ToString(), newValue + value);
        PlayerPrefs.Save();

        foreach (var item in onChangeBalances)
        {
            if (item.balansType == balansType)
            {
                item.action.Invoke();
            }
        }
    }

    public static int CurLevelId
    {
        set
        {
            PlayerPrefs.SetInt("CurLevelId", value);
            PlayerPrefs.Save();
        }
        get
        {
            return PlayerPrefs.GetInt("CurLevelId");
        }
    }

    public static int OpenedLevel
    {
        set
        {
            PlayerPrefs.SetInt("OpenedLevel", value);
            PlayerPrefs.Save();
        }
        get
        {
            if(!PlayerPrefs.HasKey("OpenedLevel"))
            {
                PlayerPrefs.SetInt("OpenedLevel", 1);
                PlayerPrefs.Save();

                return 1;
            }

            return PlayerPrefs.GetInt("OpenedLevel");
        }
    }
}

public enum BalansType
{
    GOLD,
    ENERGY,

    KillAll,
    Wall,
    Heal
}

public enum Scenes
{
    GameScene,
    MainMenu
}