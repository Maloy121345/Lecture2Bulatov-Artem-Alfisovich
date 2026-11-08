using UnityEngine;

[CreateAssetMenu(fileName = "HealthProfile", menuName = "Survival/Health Profile")]
public class HealthProfile : ScriptableObject
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float maxHunger = 100f;   // Minecraft
    [SerializeField] private float maxThirst = 100f;   // DayZ / Unturned
    [SerializeField] private float maxStamina = 100f;  // DayZ / Minecraft
    [SerializeField] private float healthRegenPerSec = 0.5f;   // The Long Dark
    [SerializeField] private float hungerDecayPerSec = 0.4f;   // DayZ / TLD
    [SerializeField] private float thirstDecayPerSec = 0.6f;   // DayZ / TLD
    [SerializeField] private float starveDamagePerSec = 2f;    // Minecraft: голод=0 -> урон

    public float MaxHealth => maxHealth;
    public float MaxHunger => maxHunger;
    public float MaxThirst => maxThirst;
    public float MaxStamina => maxStamina;
    public float HealthRegenPerSec => healthRegenPerSec;
    public float HungerDecayPerSec => hungerDecayPerSec;
    public float ThirstDecayPerSec => thirstDecayPerSec;
    public float StarveDamagePerSec => starveDamagePerSec;
}