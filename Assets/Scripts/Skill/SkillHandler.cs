using System;
using UnityEngine;

public abstract class SkillHandler : MonoBehaviour
{
    internal float cooldown; 
    private SkillSO _skill; // Reference to the SkillSO associated with the skill handler.
    private IUsable _iUsable; // Reference to the IUsable interface for skill usage.
    private Transform aimTransform; // Reference to the aim transform for skill direction.

    protected virtual void Start()
    {
        aimTransform = GetComponent<PlayerAimController>().aimTransform;
        _skill = GetSkill(); 
        cooldown = _skill.cooldown;
        // Create the skill prefab pool and initialize the UI image sprite.
        PoolHandler.Instance.Create(_skill.skillPrefab, PoolHandler.Instance.GetSkillPoolType(_skill.skillId));
        SkillSlotsUI.Instance.InitializeImageSprite(_skill.skillType, _skill.skillId);
    }

    protected virtual void Update()
    {
        if (!GameManager.Instance.isGameStarted) return; // Do not update if the game is not started.

        cooldown -= Time.deltaTime; 
        SkillSlotsUI.Instance.SetFillAmount(_skill.skillType, cooldown / _skill.cooldown); 
    }

    protected void UseSkill()
    {
        Transform _currentSkill = PoolHandler.Instance.Get(PoolHandler.Instance.GetSkillPoolType(_skill.skillId));
        _currentSkill.position = (Vector2)aimTransform.position + _skill.offset;
        _currentSkill.rotation = aimTransform.rotation; 
        _iUsable = _currentSkill.GetComponent<IUsable>(); // Get the IUsable interface from the skill object.
        // Release the skill object back to the pool after a certain lifetime.
        PoolHandler.Instance.Release(_currentSkill, PoolHandler.Instance.GetSkillPoolType(_skill.skillId), _skill.lifeTime);
        _iUsable.Use(_skill.damage); // Use the skill with specified damage.
        cooldown = _skill.cooldown; // Reset the cooldown after skill usage.
    }    

    // Abstract method to be implemented by subclasses to get the specific SkillSO for the skill handler.
    protected abstract SkillSO GetSkill();
}
