using UnityEngine;
using UnityEngine.EventSystems;

public class MobileAimInput : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] RectTransform joystickBackground; 
    [SerializeField] RectTransform joystickHandle; 
    [SerializeField] private float aimRetarderValue = 2f; 
    [Range(0, 2)] public float handleLimit = 1f; 
    private Vector2 input = Vector2.zero; 
    public static float AimAngle; // Static variable to store the calculated aiming angle.
    public void OnDrag(PointerEventData eventData)
    {
        // Calculate the direction of joystick movement.
        Vector2 joyDirection = eventData.position -
                               RectTransformUtility.WorldToScreenPoint(new Camera(), joystickBackground.position);
        input = (joyDirection.magnitude > joystickBackground.sizeDelta.x / 2f)
            ? joyDirection.normalized
            : joyDirection / (joystickBackground.sizeDelta.x / 2f);

        // Apply aim sensitivity retarder.
        input = new Vector2(input.x / aimRetarderValue, input.y / aimRetarderValue);

        // Move the joystick handle within the specified limit.
        joystickHandle.anchoredPosition = (input * joystickBackground.sizeDelta.x / 2f) * handleLimit;

        // Calculate and store the aiming angle in degrees.
        AimAngle = Mathf.Atan2(input.y, input.x) * Mathf.Rad2Deg;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData); // Simulate drag behavior when the user initially presses on the screen.
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        joystickHandle.anchoredPosition = Vector2.zero; // Reset joystick handle position.
    }
}
