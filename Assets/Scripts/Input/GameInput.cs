using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    private InputActions inputActions;
    public event EventHandler OnAbility1Performed;
    private void Awake()
    {
        if (!Instance)
            Instance = this;

        inputActions = new InputActions();
        inputActions.Player.Ability1.performed += Ability1_performed;
        inputActions.Player.Enable();
    }
    private void Ability1_performed(InputAction.CallbackContext obj)
    {
        PerformAbility1();
    }

    public void PerformAbility1()
    {
        OnAbility1Performed?.Invoke(this, EventArgs.Empty);
    }
    private void OnDestroy()
    {
        inputActions.Dispose();
    }
    public InputActions GetInputActions()
    {
        return inputActions;
    }
    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = inputActions.Player.Movement.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }
    public Vector2 GetLook()
    {
        Vector2 lookVector = inputActions.Player.Look.ReadValue<Vector2>();
        return lookVector;

    }
}
