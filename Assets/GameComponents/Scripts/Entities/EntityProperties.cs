using UnityEngine;

[RequireComponent(typeof(Entity))]
[RequireComponent(typeof(EntityMovement))]
[RequireComponent(typeof(EntityRenderer))]

public abstract class EntityProperties : MonoBehaviour
{
    protected Entity Entity;
    protected EntityMovement EntityMovement;
    protected EntityRenderer EntityRenderer;

    protected virtual void Awake()
    {
        Entity = GetComponent<Entity>();
        EntityMovement = GetComponent<EntityMovement>();
        EntityRenderer = GetComponent<EntityRenderer>();
    }
}