using UnityEngine.Events;

public class Player : Entity
{
    public new float MaxHealth => base.MaxHealth;
    public new float CurrentHealth => base.CurrentHealth;

    public event UnityAction HealthIncreased;

    public void AddHealth(float health)
    {
        base.CurrentHealth += health;

        if (base.CurrentHealth > MaxHealth)
        {
            base.CurrentHealth = MaxHealth;
        }

        HealthIncreased?.Invoke();
    }

    public override void InitiateRevival(float maxHealth)
    {
        base.InitiateRevival(maxHealth);

        AddHealth(maxHealth);
    }
}