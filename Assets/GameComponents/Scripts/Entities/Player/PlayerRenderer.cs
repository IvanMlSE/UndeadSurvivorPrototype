using UnityEngine;

public class PlayerRenderer : EntityRenderer
{
    [SerializeField] private Transform _handsTransform;
    [Header("=================================== Left Hand ==================================="), Space]
    [SerializeField] private SpriteRenderer _leftHandSprite;
    [Space]
    [SerializeField] private Vector3 _leftHandPosition;
    [SerializeField] private Vector3 _leftHandRotation;
    [SerializeField] private int _leftHandOrderInLayers;
    [Header("Reverse"), Space]
    [SerializeField] private Vector3 _leftHandPositionReverse;
    [SerializeField] private Vector3 _leftHandRotationReverse;
    [SerializeField] private int _leftHandOrderInLayersReverse;
    [Header("=================================== Right Hand ==================================="), Space]
    [SerializeField] private SpriteRenderer _rightHandSprite;
    [Space]
    [SerializeField] private Vector3 _rightHandPosition;
    [SerializeField] private Vector3 _rightHandRotation;
    [SerializeField] private int _rightHandOrderInLayers;
    [Header("Reverse"), Space]
    [SerializeField] private Vector3 _rightHandPositionReverse;
    [SerializeField] private Vector3 _rightHandRotationReverse;
    [SerializeField] private int _rightHandOrderInLayersReverse;

    protected override void Visualize()
    {
        base.Visualize();

        Animator.SetFloat(AnimatorParameters.Speed, Mathf.Abs(EntityMovement.Direction.magnitude));
        FlipHands();
    }

    protected override void OnRevived()
    {
        base.OnRevived();

        _handsTransform.gameObject.SetActive(true);
    }

    protected override void OnDied()
    {
        base.OnDied();

        Animator.SetTrigger(AnimatorParameters.Died);
        _handsTransform.gameObject.SetActive(false);
    }

    private void FlipHands()
    {
        if (EntityMovement.Direction.x != 0f)
        {
            if (EntityMovement.Direction.x > 0)
            {
                SetVisualize(_leftHandSprite, _leftHandPosition, _leftHandRotation, _leftHandOrderInLayers);
                SetVisualize(_rightHandSprite, _rightHandPosition, _rightHandRotation, _rightHandOrderInLayers);
            }
            else
            {
                SetVisualize(_leftHandSprite, _leftHandPositionReverse, _leftHandRotationReverse, _leftHandOrderInLayersReverse);
                SetVisualize(_rightHandSprite, _rightHandPositionReverse, _rightHandRotationReverse, _rightHandOrderInLayersReverse);
            }
        }
    }

    private void SetVisualize(SpriteRenderer handSprite, Vector3 handPosition, Vector3 handRotation, int handOrderInLayers)
    {
        handSprite.transform.localPosition = handPosition;
        handSprite.transform.localRotation = Quaternion.Euler(handRotation);
        handSprite.sortingOrder = handOrderInLayers;
    }

    public override void SetAnimatorController(RuntimeAnimatorController runtimeAnimatorController)
    {
        base.SetAnimatorController(runtimeAnimatorController);

        Animator.SetTrigger(AnimatorParameters.Revived);
    }

    private abstract class AnimatorParameters
    {
        public const string Speed = nameof(Speed);
        public const string Died = nameof(Died);
        public const string Revived = nameof(Revived);
    }
}