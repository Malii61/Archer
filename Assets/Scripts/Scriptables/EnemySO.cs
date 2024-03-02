using UnityEngine;

public enum EnemyType
{
    Spike,
    Blowgunner,
}

[CreateAssetMenu()]
public class EnemySO : ScriptableObject
{
    public EnemyType enemyType;
    public float enemyRange;
    public float enemyMovementSpeed;
    public float enemyDamage;
    public float health;
    public float attackSpeed;
    public Transform enemyPrefab;
}