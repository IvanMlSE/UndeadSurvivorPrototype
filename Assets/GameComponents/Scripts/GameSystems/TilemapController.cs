using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapController : MonoBehaviour
{
    [SerializeField] private int _tilemapLength;
    [SerializeField] private Player _player;
    [SerializeField] private LayerMask _tilemapAreaLayer;
    [SerializeField] private BoxCollider2D _tilemapArea;
    [SerializeField] private Tilemap[] _tilemaps;

    private void Awake()
    {
        _tilemapArea.size = new Vector2(_tilemapLength, _tilemapLength);

        foreach (Tilemap tilemap in _tilemaps)
        {
            tilemap.gameObject.AddComponent<TilemapReposition>().Initialize(_player, _tilemapAreaLayer, _tilemapLength);
        }
    }
}