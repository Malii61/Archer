using System;
using UnityEngine;

public class PlayerAimController : MonoBehaviour
{
    [SerializeField] internal Transform aimTransform;

    private void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            aimTransform.eulerAngles = new Vector3(0, 0, MobileAimInput.AimAngle);
        }
        else
        {
            Vector3 mousePosition = Utils.GetMouseWorldPosition();
            Vector3 aimDirection = (mousePosition - transform.position).normalized;
            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            aimTransform.eulerAngles = new Vector3(0, 0, angle);      
        }
    }
}