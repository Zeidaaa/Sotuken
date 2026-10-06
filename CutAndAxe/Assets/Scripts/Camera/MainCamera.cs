using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [Header("設定")]
    [SerializeField, Tooltip("親となるPlayerControllerの参照")]
    private Player m_player;

    [SerializeField, Tooltip("歩行時の揺れの強さ（上下）")]
    private float m_shakingIntensity = 5.0f;
    [SerializeField, Tooltip("歩行時の揺れの大きさ（上下）")]
    private float m_shakingMagnitude = 0.1f;

    [Header("オプション")]
    [SerializeField, Range(0, 1f)]
    [Tooltip("横方向の揺れの比率（0で縦のみ、1で完全な円）")]
    private float m_shakingHorizontalRatio = 0.3f;

    [SerializeField, Tooltip("プレイヤーが動いていない時に揺れを減衰させる速さ")]
    private float m_shakingDampenedSpeed = 5.0f;

    private float m_timer = 0.0f;
    private Vector3 m_startLocalPos;
    private float m_defaultPosY;

    private void Start()
    {
        // カメラの初期ローカル位置を保存
        m_startLocalPos = transform.localPosition;
        m_defaultPosY = m_startLocalPos.y;
    }

    private void Update()
    {
        if (InputManager.Instance.MoveInput.sqrMagnitude > 0.01f && m_player.IsGraunded)
        {
            float currentFrequency = m_shakingIntensity * (m_player.m_playerStatus.MoveSpeed / 1.0f);

            m_timer += Time.deltaTime * currentFrequency;

            float shakingSin = Mathf.Sin(m_timer);
            float shakingCos = Mathf.Cos(m_timer / 2.0f);
            
            float verticalOffset = shakingSin * m_shakingMagnitude;
            float horizontalOffset = shakingCos * m_shakingMagnitude * m_shakingHorizontalRatio;

            transform.localPosition = new Vector3(
                m_startLocalPos.x + horizontalOffset,
                m_defaultPosY + verticalOffset,
                m_startLocalPos.z
            );
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, m_startLocalPos, Time.deltaTime * m_shakingDampenedSpeed);

            if (Vector3.Distance(transform.localPosition, m_startLocalPos) < 0.001f)
            {
                transform.localPosition = m_startLocalPos;
                m_timer = 0;
            }
        }
    }
}
