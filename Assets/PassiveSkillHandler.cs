public class PassiveSkillHandler : SkillHandler
{
    protected override SkillSO GetSkill()
    {
        return Player.Instance.GetPlayerPassiveSkill();
    }

    protected override void Update()
    {
        base.Update();
        if (cooldown <= 0)
        {
            UseSkill();
        }
    }
}