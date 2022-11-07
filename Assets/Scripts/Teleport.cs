using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform connection;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector3 position=collision.transform.position;
        position.y = this.connection.position.y;
        position.x = this.connection.position.x;
        collision.transform.position = position;

    }
}
