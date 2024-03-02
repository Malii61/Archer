using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static Vector2 GetRandomPositionAtCertainPoint(Vector2 point, float range = 2f)
    {
        return new Vector2(Random.Range(point.x - range, maxInclusive: point.x + range),
            Random.Range(point.y - range, maxInclusive: point.y + range));
    }

    // Get mouse position in world with z = 0f
    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }
    private static Vector3 GetWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
}