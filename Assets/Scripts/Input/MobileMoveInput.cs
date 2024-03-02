using UnityEngine;
using UnityEngine.EventSystems;

public class MobileMoveInput : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] RectTransform joystickBackground;
    [SerializeField] RectTransform joystickHandle;
    [Range(0, 2)] public float handleLimit = 1f;
    public float movementRetarderValue = 2f;
    public static Vector2 movementInput = Vector2.zero;
    private void Start()
    {
        gameObject.SetActive(Application.platform == RuntimePlatform.Android);
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 joyDirection = eventData.position -
                               RectTransformUtility.WorldToScreenPoint(new Camera(), joystickBackground.position);
        movementInput = (joyDirection.magnitude > joystickBackground.sizeDelta.x / 2f)
            ? joyDirection.normalized
            : joyDirection / (joystickBackground.sizeDelta.x / 2f);
        movementInput = new Vector2(movementInput.x / movementRetarderValue, movementInput.y / movementRetarderValue);
        joystickHandle.anchoredPosition = (movementInput * joystickBackground.sizeDelta.x / 2f) * handleLimit;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        movementInput = Vector2.zero;
        joystickHandle.anchoredPosition = Vector2.zero;
    }
}