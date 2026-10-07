using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeHolder : MonoBehaviour
{
    [SerializeField] private Animator m_animator;
    [SerializeField] private SphereCollider m_axeCollider;
    [SerializeField] private Player m_player;

    private float m_hitBoxStart = 1.083f;
    private float m_hitBoxEnd = 1.5f;
    private float m_animationTotalTime = 2.0f;

    private float m_defaultAnimatorSpeed = 1.0f;
    private float m_defaultAttackRange = 1.0f;

    private bool m_isAttacking = false;

    private HashSet<Tree> m_hitTreesThisAttack = new HashSet<Tree>();

    private void Start()
    {
        if (m_axeCollider == null) return;
        m_axeCollider.enabled = false;
    }

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

        float attackSpeedMultiplier = m_defaultAnimatorSpeed;

        // ステータスから攻撃速度に基づくアニメーション速度を計算
        if (m_player == null) Debug.LogWarning("m_playerが設定されていません。 => AxeHolder.cs");
        else attackSpeedMultiplier = m_player.m_playerStatus.AttackSpeed;
        m_animator.speed = attackSpeedMultiplier;

        m_animator.SetTrigger("TriggerAxe_Cut");

        yield return new WaitForSeconds(m_hitBoxStart / attackSpeedMultiplier);
        StartHitBox();

        yield return new WaitForSeconds((m_hitBoxEnd - m_hitBoxStart) / attackSpeedMultiplier);
        EndHitBox();
    
        yield return new WaitForSeconds((m_animationTotalTime - m_hitBoxEnd) / attackSpeedMultiplier);
        
        m_isAttacking = false;
        
        if (InputManager.Instance != null && InputManager.Instance.IsAttackingPressed)
        {
            StartCoroutine(SwingAxeRoutine());
        }
    }

    private void StartHitBox()
    {
        if (m_axeCollider == null) return; 

        m_hitTreesThisAttack.Clear();
        
        float rangeMultiplier = m_defaultAttackRange;
        if (m_player == null) Debug.LogWarning("m_playerが設定されていません。 => AxeHolder.cs");
        else rangeMultiplier = m_player.m_playerStatus.AttackRange;

        m_axeCollider.radius = rangeMultiplier;
        m_axeCollider.enabled = true;
    }

    private void EndHitBox()
    {
        if (m_axeCollider == null) return;
        m_axeCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (m_axeCollider == null || !m_axeCollider.enabled) return;

        if (other.CompareTag("Tree"))
        {
            Tree hitTree = other.gameObject.GetComponent<Tree>();

            // 二重ヒット防止
            if (m_hitTreesThisAttack.Contains(hitTree)) return;
            m_hitTreesThisAttack.Add(hitTree);

            // ダメージ計算
            hitTree.OnHitDamage(m_player.m_playerStatus);
        }
    }
}