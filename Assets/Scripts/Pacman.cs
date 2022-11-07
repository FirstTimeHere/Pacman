using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Pacman : MonoBehaviour
{
    public Movement movement { get; private set; }
   // private float speed = 2f; первый вариант
    private void Awake()
    {
        this.movement = GetComponent<Movement>();
    }
    private void Update()
    {


        /*  первый вариант, но он не подхлдит так как ТЫ им двигаешь, а не он самостоятельно идет по направлению
         *  float moveH = Input.GetAxis("Horizontal");
         float moveV = Input.GetAxis("Vertical");
         Vector2 position = new Vector2(moveH * speed, moveV * speed);
         this.movement.SetDirection(position);*/

        //второй вариант
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            movement.SetDirection(Vector2.up);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            movement.SetDirection(Vector2.down);
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            movement.SetDirection(Vector2.left);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            movement.SetDirection(Vector2.right);
        }
        float angle=Mathf.Atan2(this.movement.direction.y, this.movement.direction.x);
        this.transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);


    

}
    public void ResetState()
    {
        this.movement.ResetState();
        this.gameObject.SetActive(true);
        
    }
}
