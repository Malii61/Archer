using UnityEngine;

public class Shurikens : MonoBehaviour, IUsable
{
    [SerializeField] private SingleShuriken[] _shurikens;
    public void Use(float damage = 0)
    {
        foreach (var shuriken in _shurikens)
        {
            shuriken.Use(damage);
        }
    }
}