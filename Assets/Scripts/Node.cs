using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public LayerMask obstacleOrWallLayer;
    public List<Vector2> avaibleDirections { get; private set; }
    private void Start()
    {
        this.avaibleDirections = new List<Vector2>();
        CheckAvailableDirection(Vector2.up);
        CheckAvailableDirection(Vector2.down);
        CheckAvailableDirection(Vector2.left);
        CheckAvailableDirection(Vector2.right);
    }
    private void CheckAvailableDirection(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, Vector2.one * 0.5f, 0f, direction, 1f, this.obstacleOrWallLayer);
        if (hit.collider==null)
        {
            this.avaibleDirections.Add(direction);
        }
    }
}
