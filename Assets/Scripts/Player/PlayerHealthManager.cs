using CandyCoded.HapticFeedback;
using ExitGames.Client.Photon;
using Photon.Pun;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviourPunCallbacks, IDamagablePlayer
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
        if (!GameManager.Instance.isGameOnline)
        {
            ItemDropManager.Instance.OnFruitCollected += OnFruitCollected;
        }
    }

    private void OnFruitCollected(object sender, float healAmount)
    {
        UpdateHealth(healAmount);
    }

    public void GetDamage(float damage)
    {
        if (_isDied) return;
        if (GameManager.Instance.isGameOnline)
        {
            if (!GetComponent<PhotonView>().IsMine)
            {
                UpdateHealth(-damage);
                PhotonNetwork.SetPlayerCustomProperties(new Hashtable() { { "damage", damage } });
                return;
            }
        }

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

    public override void OnPlayerPropertiesUpdate(Photon.Realtime.Player targetPlayer, Hashtable changedProps)
    {
        if (changedProps.TryGetValue("damage", out var damage) && GetComponent<PhotonView>().IsMine &&
            !Equals(targetPlayer, PhotonNetwork.LocalPlayer))
        {
            GetDamage((float)damage);
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
        if (GameManager.Instance.isGameOnline)
        {
            PhotonNetwork.Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}