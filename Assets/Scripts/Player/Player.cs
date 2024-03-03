using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; } 
    public event EventHandler OnPlayerHit;
    [SerializeField] private PlayerSO _playerSo; // Serialized reference to the PlayerSO scriptable object.

    private void Awake()
    {
        Instance = this; 
    }

    public void PlayerGetHit()
    {
        // Invoke the OnPlayerHit event if the game is not online.
        if (!GameManager.Instance.isGameOnline)
        {
            OnPlayerHit?.Invoke(this, EventArgs.Empty);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Collect items implementing the ICollectable interface on trigger enter.
        if (other.TryGetComponent(out ICollectable collectable))
            collectable.Collect();
    }

    #region getter

    public SkillSO GetPlayerBasicAttackSkill()
    {
        return _playerSo.basicAttackSO; // Retrieve the basic attack skill from the PlayerSO.
    }

    public SkillSO GetPlayerPassiveSkill()
    {
        return _playerSo.passiveSkillSO; // Retrieve the passive skill from the PlayerSO.
    }

    public SkillSO GetPlayerActiveSkill()
    {
        return _playerSo.activeSkillSO; // Retrieve the active skill from the PlayerSO.
    }

    public float GetPlayerMaxHealth()
    {
        return _playerSo.health; // Retrieve the maximum health from the PlayerSO.
    }

    public float GetPlayerMoveSpeed()
    {
        return _playerSo.moveSpeed; // Retrieve the movement speed from the PlayerSO.
    }

    #endregion
}