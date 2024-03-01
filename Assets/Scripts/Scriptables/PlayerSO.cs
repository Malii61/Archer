using UnityEngine;

[CreateAssetMenu()]
public class PlayerSO : ScriptableObject
{
    public SkillSO basicAttackSO;
    public SkillSO passiveSkillSO;
    public SkillSO activeSkillSO;
    public float health;
    public float moveSpeed;
}