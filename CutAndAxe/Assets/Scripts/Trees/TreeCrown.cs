using System;
using UnityEngine;

public class TreeCrown : MonoBehaviour
{
    [SerializeField]
    private int m_dropNum = 2;

    [SerializeField] 
    private float m_treeCrownDensity = 0.5f;

    [SerializeField]
    private float m_fallPower = 5.0f;

    [SerializeField]
    private Timer m_dropTimer;

    [SerializeField]
    private GameObject m_woodPrefab;

    private Rigidbody m_rigidBody;

    void Awake()
    {
        // 質量をメッシュの体積と密度から計算して設定する
        m_rigidBody = GetComponent<Rigidbody>();
        MeshFilter meshFilter = GetComponent<MeshFilter>();

        if (meshFilter != null && meshFilter.sharedMesh != null && m_rigidBody != null)
        {
            Vector3 size = Vector3.Scale(meshFilter.sharedMesh.bounds.size, transform.localScale);
            float volume = size.x * size.y * size.z;

            float calculatedMass = volume * m_treeCrownDensity;
            m_rigidBody.mass = Mathf.Max(calculatedMass, 0.1f);
        }

        // 重心にスケールを掛けて調整する
        m_rigidBody.centerOfMass = new Vector3(0, m_rigidBody.centerOfMass.y * transform.localScale.y, 0);
    }
    
    public void Reset()
    {
        m_dropTimer = new Timer
            (
                counter: 0f,
                time: 20f
            );
    }

    void Update()
    {
        if (m_dropTimer.TimeCount(Time.deltaTime))
        {
            // ドロップ処理
            for (int i = 0; i < m_dropNum; i++)
            {
                var dropPos = GetRandomPositionInSphere(transform.position, 2.0f);
                Instantiate(m_woodPrefab, dropPos, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }

    public void SetDropWood(PlayerStatus playerStatus)
    {
        // 割り切れた幸運はそのままドロップ数に加算
        m_dropNum += (int)playerStatus.luck / 100;

        // 余りで+1個ドロップするか計算する
        if (Calculators.Probability((int)playerStatus.luck % 100))
            m_dropNum++;
    }

    public void SetFallDirection(Vector3 direction)
    {
        if (m_rigidBody == null) return;

        // 力の向きと量を計算する
        direction.y = 0f;
        Vector3 pushDir = direction.normalized;
        Vector3 force = pushDir * m_fallPower * transform.localScale.y;

        // 力を加える位置を計算する
        Vector3 forcePosition = transform.position + new Vector3(0, transform.localScale.y, 0);

        m_rigidBody.AddForceAtPosition(force, forcePosition, ForceMode.Impulse);
    }

    public Vector3 GetRandomPositionInSphere(Vector3 center, float radius)
    {
        // 半径1の球体内のランダムなベクトルに半径を掛ける
        Vector3 randomOffset = UnityEngine.Random.insideUnitSphere * radius;

        // 中心座標を足してワールド座標にする
        return center + randomOffset;
    }
}
