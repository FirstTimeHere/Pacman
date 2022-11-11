using UnityEngine;

public class GhostChase : GhostBehavior
{
    private void OnDisable()
    {
        this.ghost.scatter.Enable();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //меням позицию призрака при повороте

        Node node = collision.GetComponent<Node>();
        if (node != null && this.enabled && !this.ghost.frighned.enabled)
        {
            Vector2 direction = Vector2.zero;
            float minDistance=float.MaxValue;
            foreach (Vector2 availableDirection  in node.avaibleDirections)
            {
                Vector3 newPosition = this.transform.position + new Vector3(availableDirection.x, availableDirection.y, 0f);
                float distance = (this.ghost.target.position - newPosition).sqrMagnitude;

                if (distance < minDistance)
                {
                    direction = availableDirection;
                    minDistance = distance;
                }    
            }
            this.ghost.movement.SetDirection(direction);
        }
    }
}
