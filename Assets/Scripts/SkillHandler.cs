using System;
using UnityEngine;

public abstract class SkillHandler : MonoBehaviour
{
    internal float cooldown;
    private SkillSO _skill;
    private IUsable _iUsable;
    private Transform aimTransform;
    protected virtual void Start()
    {
        aimTransform = GetComponent<PlayerAimController>().aimTransform;
        _skill = GetSkill();
        cooldown = _skill.cooldown;
        PoolHandler.Instance.Create(_skill.skillPrefab,
            PoolHandler.Instance.GetSkillPoolType(_skill.skillId));
        SkillSlotsUI.Instance.InitializeImageSprite(_skill.skillType, _skill.skillId);
    }

    protected virtual void Update()
    {
        if(!GameManager.Instance.isGameStarted) return;
        
        cooldown -= Time.deltaTime;
        SkillSlotsUI.Instance.SetFillAmount(_skill.skillType, cooldown / _skill.cooldown);
    }

    protected void UseSkill()
    {
        Transform _currentSkill =
            PoolHandler.Instance.Get(PoolHandler.Instance.GetSkillPoolType(_skill.skillId));
        _currentSkill.position = (Vector2)aimTransform.position + _skill.offset;
        _currentSkill.rotation = aimTransform.rotation;
        _iUsable = _currentSkill.GetComponent<IUsable>();
        PoolHandler.Instance.Release(_currentSkill, PoolHandler.Instance.GetSkillPoolType(_skill.skillId),
            _skill.lifeTime);
        _iUsable.Use(_skill.damage);
        cooldown = _skill.cooldown;
    }    
    protected abstract SkillSO GetSkill();

}