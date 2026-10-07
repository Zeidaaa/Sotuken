using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField] 
    public TreeStatus m_treeStatus;

    private void Reset()
    {
        // 一旦初期値を設定、
        // 後々セーブデータから読み込むため、
        // 初期データ生成時には初期値が設定される様にする。
        m_treeStatus = new TreeStatus
            (
                maxHp: 40f,
                currentHp: 40f,
                defense: 2f
            );
    }

    void Start()
    {
        m_treeStatus.currentHp = m_treeStatus.maxHp;
    }

    void Update()
    {
        
    }

    public void OnHitDamage(PlayerStatus playerStatus)
    {
        m_treeStatus.currentHp -= Calculators.OnHitDamage(playerStatus, m_treeStatus);
        if (m_treeStatus.currentHp <= 0) m_treeStatus.currentHp = 0;

        Debug.Log("木のHP: " + m_treeStatus.currentHp);
    }
}
