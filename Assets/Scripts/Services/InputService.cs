using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputService : MonoBehaviour
{
    public static InputService Instance { get; private set; }
    public event Action OnInteract;
    public event Action OnPause;

    public InputAction moveAction;
    public InputAction interactAction;
    public InputAction pauseAction;

    private Action<InputAction.CallbackContext> interactCallback;
    private Action<InputAction.CallbackContext> pauseCallback;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        moveAction = new InputAction("Move", binding: "<Gamepad>/leftStick");
        moveAction.AddCompositeBinding("2DVector")
            .With("Up", "<Keyboard>/w")
            .With("Down", "<Keyboard>/s")
            .With("Left", "<Keyboard>/a")
            .With("Right", "<Keyboard>/d");

        moveAction.AddCompositeBinding("2DVector")
            .With("Up", "<Keyboard>/upArrow")
            .With("Down", "<Keyboard>/downArrow")
            .With("Left", "<Keyboard>/leftArrow")
            .With("Right", "<Keyboard>/rightArrow");
        queuedMove = Vector2.zero;

        interactAction = new InputAction("Interact", binding: "<Keyboard>/e");
        interactAction.AddBinding("<Gamepad>/buttonSouth");
        interactAction.AddBinding("<Keyboard>/space");

        pauseAction = new InputAction("Pause", binding: "<Keyboard>/escape");
    }

    private void OnEnable()
    {
        interactCallback = ctx => OnInteract?.Invoke();
        pauseCallback = ctx => OnPause?.Invoke();

        interactAction.performed += interactCallback;
        pauseAction.performed += pauseCallback;

        moveAction.Enable();
        interactAction.Enable();
        pauseAction.Enable();
    }

    private void OnDisable()
    {
        interactAction.performed -= interactCallback;
        pauseAction.performed -= pauseCallback;

        moveAction.Disable();
        interactAction.Disable();
        pauseAction.Disable();
    }

    private Vector2 queuedMove;

    void Update()
    {
        Vector2 input = moveAction.ReadValue<Vector2>();
        if (input != Vector2.zero)
            queuedMove = input;
    }

    public Vector2 ConsumeMove()
    {
        Vector2 move = queuedMove;
        queuedMove = Vector2.zero;
        return move;
    }
}
