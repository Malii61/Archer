using System;
public class ActiveSkillHandler : SkillHandler
{
    protected override void Start()
    {
        base.Start();
        GameInput.Instance.OnAbility1Performed += OnActiveSkillPerformed;
    }

    private void OnActiveSkillPerformed(object sender, EventArgs e)
    {
        if (cooldown <= 0)
        {
            UseSkill();
        }    
    }

    protected override SkillSO GetSkill()
    {
        return Player.Instance.GetPlayerActiveSkill();
    }
}