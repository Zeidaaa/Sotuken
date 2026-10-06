using System;
using UnityEngine;

[Serializable]
public struct TreeStatus
{
    [Tooltip("最大体力")]
    public float maxHp;
    [Tooltip("現在の体力")]
    public float currentHp;
    [Tooltip("防御力")]
    public float defense;

    // コンストラクタ
    public TreeStatus(
        float maxHp, 
        float currentHp,
        float defense
        )
    {
        this.maxHp = maxHp;
        this.currentHp = currentHp;
        this.defense = defense;
    }

    // アクセサ
    public float MaxHp
    {
        get => maxHp;
        set => maxHp = value;
    }
    public float CurrentHp
    {
        get => currentHp;
        set => currentHp = value;
    }
    public float Defense
    {
        get => defense;
        set => defense = value;
    }
}
