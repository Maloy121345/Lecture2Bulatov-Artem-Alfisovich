using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private HealthComponent health;

    [Header("Тексты (встроенный UI)")]
    [SerializeField] private Text healthText;
    [SerializeField] private Text hungerText;
    [SerializeField] private Text thirstText;
    [SerializeField] private Text staminaText;
    [SerializeField] private Text statusText;

    private void OnEnable()
    {
        if (health != null)
        {
            health.OnStatsChanged += Refresh;
            health.OnDied += OnDied;
            Refresh(health);
        }
    }

    private void OnDisable()
    {
        if (health != null)
        {
            health.OnStatsChanged -= Refresh;
            health.OnDied -= OnDied;
        }
    }

    private void Refresh(HealthComponent h)
    {
        if (healthText)  healthText.text  = $"HP: {h.CurrentHealth:F1}";
        if (hungerText)  hungerText.text  = $"Hunger: {h.CurrentHunger:F1}";
        if (thirstText)  thirstText.text  = $"Thirst: {h.CurrentThirst:F1}";
        if (staminaText) staminaText.text = $"Stamina: {h.CurrentStamina:F1}";
        if (statusText)  statusText.text  = h.IsDead ? "DEAD" : "alive";
    }

    private void OnDied(HealthComponent h) => Refresh(h);

    public void OnDamageClicked()  => health?.TakeDamage(10f);
    public void OnHealClicked()    => health?.Heal(10f);
    public void OnEatClicked()     => health?.IncreaseHunger(15f);
    public void OnDrinkClicked()   => health?.IncreaseThirst(15f);
    public void OnSprintClicked()  => health?.DecreaseStamina(20f);
    public void OnRestClicked()    => health?.IncreaseStamina(20f);
}