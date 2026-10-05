using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    public PlayerStatus m_playerStatus;

    private CharacterController controller;

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
        Vector2 moveInput = InputManager.Instance.MoveInput;
        Vector3 moveDir = transform.right * moveInput.x + transform.forward * moveInput.y;
        controller.Move(moveDir * m_playerStatus.MoveSpeed * Time.deltaTime);
    }
}
