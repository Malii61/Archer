using System;
using Photon.Pun;
using UnityEngine;

public class PlayerAimController : MonoBehaviour
{
    [SerializeField] internal Transform aimTransform;

    private void Update()
    {
        // Check if the game is online and if the player object belongs to the local player.
        if (GameManager.Instance.isGameOnline)
        {
            if (!GetComponent<PhotonView>().IsMine)
                return; // Do nothing if the player object is not controlled by the local player.
        }
        
        // Update aim direction based on platform input (Android or others).
        if (Application.platform == RuntimePlatform.Android)
        {
            aimTransform.eulerAngles = new Vector3(0, 0, MobileAimInput.AimAngle);
        }
        else
        {
            // Calculate aim direction based on mouse position for non-Android platforms.
            Vector3 mousePosition = Utils.GetMouseWorldPosition();
            Vector3 aimDirection = (mousePosition - transform.position).normalized;
            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            aimTransform.eulerAngles = new Vector3(0, 0, angle);      
        }
    }
}