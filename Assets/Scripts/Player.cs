using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    public event EventHandler OnPlayerHit;
    [SerializeField] private PlayerSO _playerSo;
    private void Awake()
    {
        Instance = this;
    }

    public void PlayerGetHit()
    {
        OnPlayerHit?.Invoke(this, EventArgs.Empty);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out ICollectable collectable))
            collectable.Collect();
    }

    #region getter
    public SkillSO GetPlayerBasicAttackSkill()
    {
        return _playerSo.basicAttackSO;
    }

    public SkillSO GetPlayerPassiveSkill()
    {
        return _playerSo.passiveSkillSO;
    }

    public SkillSO GetPlayerActiveSkill()
    {
        return _playerSo.activeSkillSO;
    }

    public float GetPlayerMaxHealth()
    {
        return _playerSo.health;
    }

    public float GetPlayerMoveSpeed()
    {
        return _playerSo.moveSpeed;
    }

    #endregion
}