using UnityEngine;
using UnityEngine.Events;

public abstract class Entity : MonoBehaviour
{
    protected float MaxHealth;
    protected float CurrentHealth;

    public bool IsHit { get; private set; }
    public bool IsLife { get; private set; }

    public event UnityAction Revived;
    public event UnityAction Hited;
    public event UnityAction Died;

    public void GetDamage(float damage)
    {
        if (IsLife)
        {
            InitiateHit(damage);

            if (CurrentHealth <= 0)
            {
                InitiateDeath();
            }
        }
    }

    private void InitiateHit(float damage)
    {
        CurrentHealth -= damage;

        IsHit = true;

        Hited?.Invoke();
    }

    protected virtual void InitiateDeath()
    {
        CurrentHealth = 0;

        IsHit = false;
        IsLife = false;

        Died?.Invoke();
    }

    public virtual void InitiateRevival(float maxHealth)
    {
        MaxHealth = maxHealth;
        CurrentHealth = maxHealth;

        IsLife = true;

        Revived?.Invoke();
    }

    public void ResetHit()
    {
        IsHit = false;
    }
}