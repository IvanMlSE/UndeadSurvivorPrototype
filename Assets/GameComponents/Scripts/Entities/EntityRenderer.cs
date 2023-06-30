using UnityEngine;

[RequireComponent(typeof(Entity))]
[RequireComponent(typeof(EntityMovement))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]

public abstract class EntityRenderer : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer Shadow;

    protected Entity Entity;
    protected EntityMovement EntityMovement;
    protected SpriteRenderer SpriteRenderer;
    protected Animator Animator;

    protected virtual void Awake()
    {
        Entity = GetComponent<Entity>();
        EntityMovement = GetComponent<EntityMovement>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        Animator = GetComponent<Animator>();
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

    private void LateUpdate()
    {
        if (Entity.IsLife)
        {
            Visualize();
        }
    }

    protected virtual void Visualize()
    {
        ReflectScale();
    }

    protected virtual void OnRevived()
    {
        Shadow.gameObject.SetActive(true);
    }

    protected virtual void OnDied()
    {
        Shadow.gameObject.SetActive(false);
    }

    private void ReflectScale()
    {
        if (EntityMovement.Direction.x != 0f)
        {
            SpriteRenderer.flipX = EntityMovement.Direction.x < 0;
        }
    }

    public virtual void SetAnimatorController(RuntimeAnimatorController runtimeAnimatorController)
    {
        Animator.runtimeAnimatorController = runtimeAnimatorController;
    }
}