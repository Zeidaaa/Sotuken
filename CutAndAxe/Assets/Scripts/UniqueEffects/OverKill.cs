using UnityEngine;

public class OverKill : UniqueEffectBase<TreeDestroyed>
{
    protected override void OnEventRaised(TreeDestroyed eventData)
    {
        Debug.Log($"オーバーダメージ：{eventData.OverDamage}");
    }
}
