using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class SkillSlotsArgs
{
    public SkillSO.SkillId skillId;
    public Sprite skillSprite;
}

public class SkillSlotsUI : MonoBehaviour
{
    public static SkillSlotsUI Instance { get; private set; }
    [SerializeField] private List<SkillSlotsArgs> _skillSlotsArgs = new();
    [SerializeField] private Image passiveSkillImage, passiveSkillFill, activeSkillImage, activeSkillFill;
    [SerializeField] private Button activeSkillBtn;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        activeSkillBtn.onClick.AddListener(() => GameInput.Instance.PerformAbility1());
    }

    #region public
    public void InitializeImageSprite(SkillSO.SkillType skillType, SkillSO.SkillId skillId)
    {
        Image skillImage = GetImageBySkillType(skillType);
        skillImage.sprite = GetSpriteById(skillId);
    }
    public void SetFillAmount(SkillSO.SkillType skillType, float fillAmount)
    {
        Image fill = GetFillBySkillType(skillType);
        fill.fillAmount = fillAmount;
    }
    #endregion

    #region private

    private Sprite GetSpriteById(SkillSO.SkillId skillId)
    {
        return _skillSlotsArgs.FirstOrDefault(x => Equals(x.skillId, skillId))?.skillSprite;
    }

    private Image GetImageBySkillType(SkillSO.SkillType skillType)
    {
        return skillType switch
        {
            SkillSO.SkillType.Active => activeSkillImage,
            _ => passiveSkillImage
        };
    }
    private Image GetFillBySkillType(SkillSO.SkillType skillType)
    {
        return skillType switch
        {
            SkillSO.SkillType.Active => activeSkillFill,
            _ => passiveSkillFill
        };
    }

    #endregion
}