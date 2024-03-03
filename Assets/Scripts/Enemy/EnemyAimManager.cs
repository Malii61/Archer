using UnityEngine;

public class EnemyAimManager : MonoBehaviour
{
    private Transform _target;
    [SerializeField] private Transform aimTransform;

    private void Start()
    {
        _target = Player.Instance.transform;
    }

    private void Update()
    {
        if (!_target) return;
        Vector3 aimDirection = (_target.position - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);
    }
}