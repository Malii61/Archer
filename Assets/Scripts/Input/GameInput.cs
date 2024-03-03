using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    private InputActions inputActions;
    public event EventHandler OnActiveSkillPerformed;

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
        PerformActiveSkill();
    }

    public void PerformActiveSkill()
    {
        OnActiveSkillPerformed?.Invoke(this, EventArgs.Empty);
    }

    private void OnDestroy()
    {
        inputActions.Dispose();
    }
    // Method to get the normalized movement vector based on the platform
    public Vector2 GetMovementVectorNormalized()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            // Normalize and scale the movement input for Android
            return MobileMoveInput.movementInput.normalized / 3.5f;
        }
        // Get the movement input from the new Input System and normalize it
        Vector2 inputVector = inputActions.Player.Movement.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }

}