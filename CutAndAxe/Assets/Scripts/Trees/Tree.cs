using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField] 
    public TreeStatus m_treeStatus;

    [SerializeField]
    public Timer m_stumpDestroyTimer;

    [SerializeField]
    public Timer m_respawnTimer;

    [SerializeField]
    public float m_fadeSpeed = 3f;

    [SerializeField] 
    private Mesh m_treeMesh;
    [SerializeField]
    private Material[] m_treeMaterial;
    [SerializeField]
    private Vector3 m_treeColliderCenter = new Vector3(0, 3.5f, 0);
    [SerializeField]
    private float m_treeColliderRadius = 0.4f;
    [SerializeField]
    private float m_treeColliderHeight = 7f;

    [SerializeField] 
    private Mesh m_stumpMesh;
    [SerializeField]
    private Material[] m_stumpMaterial;
    [SerializeField]
    private Vector3 m_stumpColliderCenter = new Vector3(0, 0.3f, 0);
    [SerializeField]
    private float m_stumpColliderRadius = 0.25f;
    [SerializeField]
    private float m_stumpColliderHeight = 0.6f;

    [SerializeField]
    private GameObject m_treeCrownPrefab;

    private MeshFilter m_meshFilter;
    private MeshRenderer m_meshRenderer;
    private CapsuleCollider m_capsuleCollider;

    private Vector3 m_fadeScale = Vector3.one;
    private bool m_isFadeScale = false;

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

        m_stumpDestroyTimer = new Timer
            (
                counter: 0f,
                time: 5f
            );

        m_respawnTimer = new Timer
            (
                counter: 0f,
                time: 8f
            );
    }

    void Start()
    {
        m_treeStatus.currentHp = m_treeStatus.maxHp;
        m_meshFilter = GetComponent<MeshFilter>();
        m_meshRenderer = GetComponent<MeshRenderer>();
        m_capsuleCollider = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        FadeScale();
        
        if (gameObject.tag == "Stump")
        {
            if (m_stumpDestroyTimer.TimeCount(Time.deltaTime, true))
            {
                SetFadeScale(0);

                gameObject.tag = "RespawnTree";
            }
        }
        else if (gameObject.tag == "RespawnTree")
        {
            if (m_respawnTimer.TimeCount(Time.deltaTime, true))
            {
                SetFadeScale(1);

                gameObject.tag = "Tree";

                m_treeStatus.currentHp = m_treeStatus.maxHp;

                if (m_treeMesh != null && m_meshFilter != null && m_meshRenderer != null)
                {
                    m_meshFilter.mesh = m_treeMesh;

                    m_meshRenderer.materials = m_treeMaterial;

                    m_capsuleCollider.center = m_treeColliderCenter;
                    m_capsuleCollider.radius = m_treeColliderRadius;
                    m_capsuleCollider.height = m_treeColliderHeight;
                }
            }
        }
    }

    public void OnHitDamage(Player player)
    {
        m_treeStatus.currentHp -= Calculators.OnHitDamage(player.m_playerStatus, m_treeStatus);
        if (m_treeStatus.currentHp <= 0)
        {
            m_treeStatus.currentHp = 0;

            m_stumpDestroyTimer.Counter = 0f;

            gameObject.tag = "Stump";

            // 切り株にモデルを変更する
            if (m_stumpMesh != null && m_meshFilter != null && m_meshRenderer != null)
            {
                m_meshFilter.mesh = m_stumpMesh;
                
                m_meshRenderer.materials = m_stumpMaterial;

                m_capsuleCollider.center = m_stumpColliderCenter;
                m_capsuleCollider.radius = m_stumpColliderRadius;
                m_capsuleCollider.height = m_stumpColliderHeight;
            }

            // 切れた木を生成する
            InstantiateTreeCrown(player);
        }
    }

    private void InstantiateTreeCrown(Player player)
    {
        if (m_treeCrownPrefab != null)
        {
            Vector3 crownPosition = transform.position + new Vector3(0, m_stumpColliderHeight, 0);
            var treeCrown = Instantiate(m_treeCrownPrefab, crownPosition, transform.rotation);

            // 木材のドロップ数を設定する
            treeCrown.GetComponent<TreeCrown>().SetDropWood(player.m_playerStatus);

            // 切れた木の倒れる方向をプレイヤーとの位置関係から設定する
            treeCrown.GetComponent<TreeCrown>().SetFallDirection(transform.position - player.transform.position);
        }
    }

    public void SetFadeScale(float value)
    {
        m_fadeScale = new Vector3(value, value, value);
        m_isFadeScale = true;
    }

    public void FadeScale()
    {
        if (!m_isFadeScale) return;

        transform.localScale = Vector3.Lerp(transform.localScale, m_fadeScale, Time.deltaTime * m_fadeSpeed);

        if (Vector3.Distance(transform.localScale, m_fadeScale) < 0.01f)
        {
            transform.localScale = m_fadeScale;
            m_isFadeScale = false;
        }
    }
}
