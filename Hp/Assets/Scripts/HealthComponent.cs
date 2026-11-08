using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [Header("Конфиг (ScriptableObject)")]
    [SerializeField] private HealthProfile profile;

    [Header("Текущее состояние (изменяемое)")]
    [SerializeField] private float currentHealth;
    [SerializeField] private float currentHunger;
    [SerializeField] private float currentThirst;
    [SerializeField] private float currentStamina;

    public event Action<HealthComponent> OnStatsChanged;
    public event Action<HealthComponent> OnDied;

    public bool IsDead => currentHealth <= 0f;
    public float CurrentHealth => currentHealth;
    public float CurrentHunger => currentHunger;
    public float CurrentThirst => currentThirst;
    public float CurrentStamina => currentStamina;

    private void Awake()
    {
        if (profile == null)
            profile = ScriptableObject.CreateInstance<HealthProfile>();

        currentHealth  = profile.MaxHealth;
        currentHunger  = profile.MaxHunger;
        currentThirst  = profile.MaxThirst;
        currentStamina = profile.MaxStamina;
    }

    public void IncreaseHealth(float amount)  => ApplyDelta(ref currentHealth, amount, 0f, profile.MaxHealth);
    public void DecreaseHealth(float amount)  => ApplyDelta(ref currentHealth, -amount, 0f, profile.MaxHealth);
    public void IncreaseHunger(float amount)  => ApplyDelta(ref currentHunger, amount, 0f, profile.MaxHunger);
    public void DecreaseHunger(float amount)  => ApplyDelta(ref currentHunger, -amount, 0f, profile.MaxHunger);
    public void IncreaseThirst(float amount)  => ApplyDelta(ref currentThirst, amount, 0f, profile.MaxThirst);
    public void DecreaseThirst(float amount)  => ApplyDelta(ref currentThirst, -amount, 0f, profile.MaxThirst);
    public void IncreaseStamina(float amount) => ApplyDelta(ref currentStamina, amount, 0f, profile.MaxStamina);
    public void DecreaseStamina(float amount) => ApplyDelta(ref currentStamina, -amount, 0f, profile.MaxStamina);

    public void TakeDamage(float dmg) => DecreaseHealth(dmg);
    public void Heal(float hp)        => IncreaseHealth(hp);

    private void ApplyDelta(ref float field, float delta, float min, float max)
    {
        if (IsDead && delta < 0f) return;
        field = Mathf.Clamp(field + delta, min, max);
        OnStatsChanged?.Invoke(this);
        if (field <= min && min == 0f && field == currentHealth)
            Die();
    }

    private void Die()
    {
        if (IsDead) OnDied?.Invoke(this);
    }

    private void Update()
    {
        if (IsDead || profile == null) return;
        float dt = Time.deltaTime;

        ApplyDelta(ref currentHunger, -profile.HungerDecayPerSec * dt, 0f, profile.MaxHunger);
        ApplyDelta(ref currentThirst, -profile.ThirstDecayPerSec * dt, 0f, profile.MaxThirst);
        ApplyDelta(ref currentStamina, profile.HealthRegenPerSec * 2f * dt, 0f, profile.MaxStamina);

        if (currentHunger > 0f && currentThirst > 0f)
            ApplyDelta(ref currentHealth, profile.HealthRegenPerSec * dt, 0f, profile.MaxHealth);
        else
            ApplyDelta(ref currentHealth, -profile.StarveDamagePerSec * dt, 0f, profile.MaxHealth);
    }
}