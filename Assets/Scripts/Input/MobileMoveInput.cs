using UnityEngine;
using UnityEngine.EventSystems;

public class MobileMoveInput : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] RectTransform joystickBackground;
    [SerializeField] RectTransform joystickHandle;
    [Range(0, 2)] public float handleLimit = 1f;
    public static Vector2 movementInput = Vector2.zero;

    // Calculates the joystick input based on drag position and updates the UI accordingly.
    public void OnDrag(PointerEventData eventData)
    {
        // Calculate the direction vector from the joystick center to the drag position.
        Vector2 joyDirection = eventData.position -
                               RectTransformUtility.WorldToScreenPoint(new Camera(), joystickBackground.position);

        // Limit the input magnitude based on the joystick's size.
        movementInput = (joyDirection.magnitude > joystickBackground.sizeDelta.x / 2f)
            ? joyDirection.normalized
            : joyDirection / (joystickBackground.sizeDelta.x / 2f);

        // Normalize the input vector and adjust the handle position within the limit.
        movementInput = new Vector2(movementInput.x, movementInput.y);
        joystickHandle.anchoredPosition = (movementInput * joystickBackground.sizeDelta.x / 2f) * handleLimit;
    }

    // Initiates drag input when the joystick is pressed.
    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    // Resets the movement input and joystick handle position when the joystick is released.
    public void OnPointerUp(PointerEventData eventData)
    {
        movementInput = Vector2.zero;
        joystickHandle.anchoredPosition = Vector2.zero;
    }
}