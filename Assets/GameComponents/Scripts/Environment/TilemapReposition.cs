using UnityEngine;

public class TilemapReposition : Reposition
{
    private int _tilemapLength;

    protected override void Reposit()
    {
        base.Reposit();

        if (PlayerAbsoluteDirection.x > PlayerAbsoluteDirection.y)
        {
            transform.Translate(Vector2.right * PositionRelativePlayer.x * _tilemapLength);
        }
        else if (PlayerAbsoluteDirection.x < PlayerAbsoluteDirection.y)
        {
            transform.Translate(Vector2.up * PositionRelativePlayer.y * _tilemapLength);
        }
        else
        {
            transform.Translate(new Vector2(PositionRelativePlayer.x, PositionRelativePlayer.y) * _tilemapLength);
        }
    }

    public void Initialize(Player player, LayerMask areaLayer, int tilemapLength)
    {
        Initialize(player, areaLayer);

        const int TilesNumberOnOneSide = 2;

        _tilemapLength = tilemapLength * TilesNumberOnOneSide;
    }
}