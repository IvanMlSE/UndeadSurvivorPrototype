using UnityEngine;

public abstract class Reposition : MonoBehaviour
{
    protected Player Player;
    protected Vector2 PlayerDirection;
    protected Vector2 PlayerAbsoluteDirection;
    protected Vector2 PositionRelativePlayer;

    private LayerMask _areaLayer;

    private void OnTriggerExit2D(Collider2D collision)
    {
        const int RegisterShift = 2;
        int collisionLayer = (int)Mathf.Pow(RegisterShift, collision.gameObject.layer);

        if (collisionLayer == _areaLayer.value)
        {
            Reposit();
        }
    }

    protected virtual void Reposit()
    {
        Vector2 currentPlayerPosition = Player.transform.position;
        Vector2 currentPosition = transform.position;

        PlayerDirection = currentPlayerPosition - currentPosition;
        PlayerAbsoluteDirection = GetAbsoluteVector(PlayerDirection);

        PositionRelativePlayer.x = PlayerDirection.x > 0 ? 1 : -1;
        PositionRelativePlayer.y = PlayerDirection.y > 0 ? 1 : -1;
    }

    private Vector2 GetAbsoluteVector(Vector2 vector)
    {
        vector = new Vector2(Mathf.Abs(vector.x), Mathf.Abs(vector.y));

        return vector;
    }

    protected void Initialize(Player player, LayerMask areaLayer)
    {
        Player = player;
        _areaLayer = areaLayer;
    }
}