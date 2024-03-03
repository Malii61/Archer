using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image fill;

    public void SetHealth(float currentHealth, float maxHealth)
    {
        gameObject.SetActive(!currentHealth.Equals(maxHealth));
        fill.fillAmount = currentHealth / maxHealth;
    }
}