using UnityEngine;

public static class Calculators
{
    public static bool Probability(float probability)
    {
        float rate = probability * 0.01f;
        return UnityEngine.Random.value < rate;
    }

    public static float OnHitDamege(PlayerStatus playerStatus, TreeStatus treeStatus)
    {
        float damege = playerStatus.finalAttack - treeStatus.defense;

        // クリティカル処理
        if (Probability(playerStatus.critical))
        {
            damege *= 1f + (playerStatus.criticalDamage * 0.01f);
        }

        return damege;
    }
}
