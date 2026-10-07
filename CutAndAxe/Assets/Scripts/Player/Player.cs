using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    public PlayerStatus m_playerStatus;

    private CharacterController controller;
    private float m_verticalVelocity;
    private float m_gravity = -9.81f;

    public bool IsGraunded
    {
        get => controller.isGrounded;
    }

    private void Reset()
    {
        // 一旦初期値を設定、
        // 後々セーブデータから読み込むため、
        // 初期データ生成時には初期値が設定される様にする。
        m_playerStatus = new PlayerStatus
            (
                basicAttack: 10f,
                finalAttack: 10f,
                attackSpeed: 1f,
                attackRange: 100f,
                critical: 5f,
                criticalDamage: 50f,
                moveSpeed: 1f,
                collectRange: 100f,
                luck: 10f,
                wood: 0f
            );
    }

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        // 重力の計算
        if (controller.isGrounded) m_verticalVelocity = -2.0f;
        else m_verticalVelocity += m_gravity * Time.deltaTime;

        // 入力による移動量の計算
        Vector2 moveInput = InputManager.Instance.MoveInput;
        Vector3 moveDir = transform.right * moveInput.x + transform.forward * moveInput.y;

        // 移動量と重力を合算して移動
        Vector3 totalMove = moveDir * m_playerStatus.MoveSpeed;
        totalMove.y = m_verticalVelocity;
        controller.Move(totalMove * Time.deltaTime);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wood"))
        {
            Destroy(other.gameObject);
            m_playerStatus.Wood++;

            Debug.Log($"木材を取得しました。現在の木材数: {m_playerStatus.Wood}");
        }
    }
}
