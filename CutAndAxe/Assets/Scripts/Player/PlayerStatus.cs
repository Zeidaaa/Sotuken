using System;
using UnityEngine;

[Serializable]
public struct PlayerStatus
{
    [Tooltip("基礎攻撃力")]
    public float basicAttack;
    [Tooltip("最終攻撃力")]
    public float finalAttack;
    [Tooltip("攻撃速度")]
    public float attackSpeed;
    [Tooltip("攻撃範囲")]
    public float attackRange;
    [Tooltip("クリティカル")]
    public float critical;
    [Tooltip("クリティカルダメージ")]
    public float criticalDamage;
    [Tooltip("移動速度")]
    public float moveSpeed;
    [Tooltip("回収範囲")]
    public float collectRange;
    [Tooltip("幸運")]
    public float luck;
    [Tooltip("所持木材")]
    public float wood;

    // コンストラクタ
    public PlayerStatus(
        float basicAttack, 
        float finalAttack, 
        float attackSpeed, 
        float attackRange, 
        float critical, 
        float criticalDamage, 
        float moveSpeed, 
        float collectRange, 
        float luck, 
        float wood
        )
    {
        this.basicAttack = basicAttack;
        this.finalAttack = finalAttack;
        this.attackSpeed = attackSpeed;
        this.attackRange = attackRange;
        this.critical = critical;
        this.criticalDamage = criticalDamage;
        this.moveSpeed = moveSpeed;
        this.collectRange = collectRange;
        this.luck = luck;
        this.wood = wood;
    }

    // アクセサ
    public float BasicAttack
    {
        get => basicAttack;
        set => basicAttack = value;
    }
    public float FinalAttack
    {
        get => finalAttack;
        set => finalAttack = value;
    }
    public float AttackSpeed
    {
        get => attackSpeed;
        set => attackSpeed = value;
    }
    public float AttackRange
    {
        get => attackRange;
        set => attackRange = value;
    }
    public float Critical
    {
        get => critical;
        set => critical = value;
    }
    public float CriticalDamage
    {
        get => criticalDamage;
        set => criticalDamage = value;
    }
    public float MoveSpeed
    {
        get => moveSpeed;
        set => moveSpeed = value;
    }
    public float CollectRange
    {
        get => collectRange;
        set => collectRange = value;
    }
    public float Luck
    {
        get => luck;
        set => luck = value;
    }
    public float Wood
    {
        get => wood;
        set => wood = value;
    }
}
