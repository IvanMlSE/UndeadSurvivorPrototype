using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Entity))]
[RequireComponent(typeof(Rigidbody2D))]

public abstract class EntityMovement : MonoBehaviour
{
    protected float MovementSpeed;
    protected Vector2 MovementVector;
    protected Entity Entity;
    protected Rigidbody2D Rigidbody2D;

    public Vector2 Direction => MovementVector;

    protected virtual void Awake()
    {
        Entity = GetComponent<Entity>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    protected virtual void OnEnable()
    {
        Entity.Revived += OnRevived;
        Entity.Died += OnDied;
    }

    protected virtual void OnDisable()
    {
        Entity.Revived -= OnRevived;
        Entity.Died -= OnDied;
    }

    private void Update()
    {
        if (Entity.IsLife)
        {
            DetermineMovementVector();
        }
    }

    private void FixedUpdate()
    {
        if (Entity.IsLife)
        {
            Run();
        }
    }

    private void OnRevived()
    {
        StopCoroutine(StopSlowlyCoroutine());
    }

    private void OnDied()
    {
        StartCoroutine(StopSlowlyCoroutine());
    }

    private IEnumerator StopSlowlyCoroutine()
    {
        float velocityDuringLife = Rigidbody2D.velocity.magnitude;

        while (Rigidbody2D.velocity.magnitude > 0)
        {
            Rigidbody2D.velocity = Vector2.MoveTowards(Rigidbody2D.velocity, Vector2.zero, velocityDuringLife * Time.deltaTime);

            yield return null;
        }
    }

    protected abstract void DetermineMovementVector();

    protected virtual void Run()
    {
        Rigidbody2D.velocity = MovementVector * MovementSpeed;
    }

    public virtual void SetMovementSpeed(float movementSpeed)
    {
        MovementSpeed = movementSpeed;
    }
}