using UnityEngine;

public class Shurikens : MonoBehaviour, IUsable
{
    [SerializeField] private SingleShuriken[] _shurikens;
    [SerializeField] private float throwForce = 3f;
    public void Use(float damage = 0)
    {
        foreach (var shuriken in _shurikens)
        {
            shuriken.Throw(throwForce, damage);
        }
    }
}