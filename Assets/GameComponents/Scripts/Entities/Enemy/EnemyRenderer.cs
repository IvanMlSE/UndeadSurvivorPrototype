using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class EnemyRenderer : EntityRenderer
{
    [SerializeField] private float _visibilityTimeAfterDeath;
    [SerializeField] private int _orderInLayerAfterDeath;

    private int _orderInLayer;
    private Collider2D _collider2D;

    private Coroutine _coroutine;

    protected override void Awake()
    {
        base.Awake();

        _collider2D = GetComponent<Collider2D>();
        _orderInLayer = SpriteRenderer.sortingOrder;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        Entity.Hited += () =>
        {
            Animator.SetTrigger(AnimatorParameters.Hited);
        };
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        Entity.Hited -= () =>
        {
            Animator.SetTrigger(AnimatorParameters.Hited);
        };
    }

    protected override void OnRevived()
    {
        base.OnRevived();

        _collider2D.isTrigger = false;
        Animator.SetBool(AnimatorParameters.IsDeath, false);
        SpriteRenderer.sortingOrder = _orderInLayer;
    }

    protected override void OnDied()
    {
        base.OnDied();

        _collider2D.isTrigger = true;
        Animator.SetBool(AnimatorParameters.IsDeath, true);
        SpriteRenderer.sortingOrder = _orderInLayerAfterDeath;

        HideBody();
    }

    private void HideBody()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(HideBodyCoroutine());
    }

    private IEnumerator HideBodyCoroutine()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_visibilityTimeAfterDeath);

        yield return waitForSeconds;

        gameObject.SetActive(Entity.IsLife);
    }

    private abstract class AnimatorParameters
    {
        public const string Hited = nameof(Hited);
        public const string IsDeath = nameof(IsDeath);
    }
}