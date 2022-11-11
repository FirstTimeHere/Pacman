using UnityEngine;

public class GhostScatter : GhostBehavior
{
    private void OnDisable()
    {
        this.ghost.chase.Enable();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //����� ������� �������� ��� ��������
        
        Node node = collision.GetComponent<Node>();
        if (node != null&& this.enabled&&!this.ghost.frighned.enabled)
        {
            
            int index=Random.Range(0,node.avaibleDirections.Count);
            if (node.avaibleDirections[index] == -this.ghost.movement.direction && node.avaibleDirections.Count > 1) //���� ������� ���� � �� �� ������ ������ ������
            {
                index++;
                if (index>=node.avaibleDirections.Count)
                {
                    index = 0;
                }
            }
            this.ghost.movement.SetDirection(node.avaibleDirections[index]);
        }    
    }
}
