using UnityEngine;

public class InputManager : SingletonMonoBehaviour<InputManager>
{
    private PlayerInput inputActions;

    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        DontDestroyOnLoad(gameObject);

        inputActions = new PlayerInput();
    }

    private void OnEnable()
    {
        inputActions.Enable();

        inputActions.Player.Move.performed += context => MoveInput = context.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += context => MoveInput = Vector2.zero;

        inputActions.Player.Look.performed += context => LookInput = context.ReadValue<Vector2>();
        inputActions.Player.Look.canceled += context => LookInput = Vector2.zero;

        inputActions.UI.ToggleInventory.performed += _ => OnToggleInventory();
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    public void SwitchToPlayerState()
    {
        inputActions.Player.Enable();
        inputActions.UI.Disable();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    public void SwitchToUIState()
    {
        inputActions.Player.Disable();
        inputActions.UI.Enable();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
    private void OnToggleInventory()
    {
        if (inputActions.UI.enabled)
        {
            SwitchToPlayerState();
            Debug.Log("インベントリを閉じました（プレイヤーモード）");
        }
        else
        {
            SwitchToUIState();
            Debug.Log("インベントリを開きました（UIモード）");
        }
    }
}