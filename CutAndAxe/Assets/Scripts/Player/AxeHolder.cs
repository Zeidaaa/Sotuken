using System.Collections;
using UnityEngine;

public class AxeHolder : MonoBehaviour
{
    [SerializeField] private Animator m_animator;
    [SerializeField] private Player m_player;

    private float m_hitBoxStart = 1.16f;
    private float m_hitBoxEnd = 1.5f;
    private float m_animationTotalTime = 2.0f;
    private float m_animatorDefaultSpeed = 1.0f;

    private bool m_isAttacking = false;

    private void Update()
    {
        if (!m_isAttacking && InputManager.Instance != null && InputManager.Instance.IsAttackingPressed)
        {
            StartCoroutine(SwingAxeRoutine());
        }
    }

    private IEnumerator SwingAxeRoutine()
    {
        m_isAttacking = true;

        // ステータスから攻撃速度に基づくアニメーション速度を計算
        float attackSpeedMultiplier = m_player != null ? m_player.m_playerStatus.AttackSpeed : m_animatorDefaultSpeed;
        m_animator.speed = attackSpeedMultiplier;
        m_animator.SetTrigger("TriggerAxe_Cut");

        yield return new WaitForSeconds(m_hitBoxStart / attackSpeedMultiplier);
        StartHitBox();

        yield return new WaitForSeconds((m_hitBoxEnd - m_hitBoxStart) / attackSpeedMultiplier);
        EndHitBox();
    
        yield return new WaitForSeconds((m_animationTotalTime - m_hitBoxEnd) / attackSpeedMultiplier);

        m_animator.speed = m_animatorDefaultSpeed;
        m_isAttacking = false;
        
        if (InputManager.Instance != null && InputManager.Instance.IsAttackingPressed)
        {
            StartCoroutine(SwingAxeRoutine());
        }
    }

    private void StartHitBox()
    {
        Debug.Log("攻撃判定：開始");
        // 例: m_axeCollider.enabled = true;
    }

    private void EndHitBox()
    {
        Debug.Log("攻撃判定：終了");
        // 例: m_axeCollider.enabled = false;
    }
}