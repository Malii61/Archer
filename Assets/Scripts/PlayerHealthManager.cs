using CandyCoded.HapticFeedback;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour, IDamagablePlayer
{
    private float _maxHealth;
    private float _health;
    private HealthBar _healthBar;
    private bool _isDied;

    private void Start()
    {
        _maxHealth = Player.Instance.GetPlayerMaxHealth();
        _health = _maxHealth;
        _healthBar = GetComponentInChildren<HealthBar>();
        _healthBar.SetHealth(_health, _maxHealth);
        ItemDropManager.Instance.OnFruitCollected += OnFruitCollected;
    }

    private void OnFruitCollected(object sender, float healAmount)
    {
        UpdateHealth(healAmount);
    }

    public void GetDamage(float damage)
    {
        if (_isDied) return;
        UpdateHealth(-damage);
        Player.Instance.PlayerGetHit();
        SoundManager.Instance.Play(Sound.PlayerHit);
        CinemachineShake.Instance.ShakeCamera(.5f, .4f);
        HapticFeedback.LightFeedback();
        if (_health <= 0)
        {
            Die();
        }
    }

    private void UpdateHealth(float amount)
    {
        _health += amount;
        _health = Mathf.Clamp(_health, 0, _maxHealth);
        _healthBar.SetHealth(_health, _maxHealth);
    }

    private void Die()
    {
        GameManager.Instance.UpdateState(GameManager.GameState.GameOver);
        Destroy(gameObject);
    }
}