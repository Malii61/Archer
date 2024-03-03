using Photon.Pun;
using UnityEngine;

public class BasicAttackHandler : MonoBehaviour
{
    private SkillSO _basicAttackSkill;
    private float _attackTimer;
    private IUsable _iUsable;
    private Transform aimTransform;

    private void Start()
    {
        aimTransform = GetComponent<PlayerAimController>().aimTransform;
        _basicAttackSkill = Player.Instance.GetPlayerBasicAttackSkill();
        _attackTimer = _basicAttackSkill.cooldown;
        if (!GameManager.Instance.isGameOnline)
        {
            var basicAttackPrfb = Instantiate(_basicAttackSkill.skillPrefab, aimTransform);
            basicAttackPrfb.localPosition = _basicAttackSkill.offset;
            _iUsable = basicAttackPrfb.GetComponent<IUsable>();
        }
        else
        {
            _iUsable = GetComponentInChildren<IUsable>();
        }
    }

    private void Update()
    {
        if (!GameManager.Instance.isGameStarted) return;

        if (GameManager.Instance.isGameOnline)
        {
            if (!GetComponent<PhotonView>().IsMine)
                return;
        }

        _attackTimer -= Time.deltaTime;
        if (_attackTimer <= 0)
        {
            _iUsable.Use(_basicAttackSkill.damage);
            _attackTimer = _basicAttackSkill.cooldown;
        }
    }
}