using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputService : MonoBehaviour
{
    public static InputService Instance { get; private set; }
    public Action onInteract;

    public InputAction moveAction;
    public InputAction interactAction;

    private Action<InputAction.CallbackContext> interactCallback;

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
    }

    private void OnEnable()
    {
        interactCallback = ctx => onInteract?.Invoke();
        interactAction.performed += interactCallback;

        moveAction.Enable();
        interactAction.Enable();
    }

    private void OnDisable()
    {
        interactAction.performed -= interactCallback;

        moveAction.Disable();
        interactAction.Disable();
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
