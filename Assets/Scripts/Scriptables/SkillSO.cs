using UnityEngine;

[CreateAssetMenu()]
public class SkillSO : ScriptableObject
{
    public enum SkillType
    {
        Basic,
        Passive,
        Active,
    }

    public enum SkillId
    {
        //Basic
        Bow,
        
        //Passive
        Shurikens,
        
        //Active
        Fear,
        
    }
    public SkillType skillType;
    public SkillId skillId;
    public float damage;
    public float cooldown;
    public Transform skillPrefab;
    public Vector2 offset;
    public float lifeTime;
}