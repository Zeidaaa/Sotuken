using UnityEngine;

public static class Calculators
{
    public static bool Probability(float probability)
    {
        float rate = probability * 0.01f;
        return UnityEngine.Random.value < rate;
    }

    public static float OnHitDamage(PlayerStatus playerStatus, TreeStatus treeStatus)
    {
        float damage = playerStatus.finalAttack - treeStatus.defense;

        // クリティカル処理
        if (Probability(playerStatus.critical))
        {
            damage *= 1f + (playerStatus.criticalDamage * 0.01f);
        }

        return damage;
    }
}
