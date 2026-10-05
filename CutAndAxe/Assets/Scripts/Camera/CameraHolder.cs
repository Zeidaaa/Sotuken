using UnityEngine;

public class CameraHolder : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float m_mouseSensitivity = 0.1f;
    [SerializeField] private float m_upperLimit = -80f;
    [SerializeField] private float m_lowerLimit = 80f;

    private float m_rotationX = 0f;
    private Transform m_playerBody;

    private void Awake()
    {
        m_playerBody = transform.parent;
    }

    private void Update()
    {
        Vector2 lookInput = InputManager.Instance.LookInput;

        float mouseX = lookInput.x * m_mouseSensitivity;
        float mouseY = lookInput.y * m_mouseSensitivity;

        m_rotationX -= mouseY;
        m_rotationX = Mathf.Clamp(m_rotationX, m_upperLimit, m_lowerLimit);
        transform.localRotation = Quaternion.Euler(m_rotationX, 0f, 0f);

        m_playerBody.Rotate(Vector3.up * mouseX);
    }
}
