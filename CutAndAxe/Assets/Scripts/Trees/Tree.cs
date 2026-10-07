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
    private Mesh m_stumpMesh;
    [SerializeField]
    private Material[] m_stumpMaterial;

    private MeshFilter m_meshFilter;
    private MeshRenderer m_meshRenderer;

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
    }

    void Update()
    {
        FadeScale();
        
        if (gameObject.tag == "Stump")
        {
            if (m_stumpDestroyTimer.TimeCount(Time.deltaTime, true))
            {
                SetFadeScale(0);

                gameObject.tag = "Untagged";
            }
        }
        else if (gameObject.tag == "Untagged")
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
                }
            }
        }
    }

    public void OnHitDamage(PlayerStatus playerStatus)
    {
        m_treeStatus.currentHp -= Calculators.OnHitDamage(playerStatus, m_treeStatus);
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
            }
        }

        Debug.Log("木のHP: " + m_treeStatus.currentHp);
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
