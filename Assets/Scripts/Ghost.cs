using UnityEngine;

public class Ghost : MonoBehaviour
{
   
    public Movement movement { get; private set; }
    public GhostHome home { get; private set; }
    public GhostChase chase { get; private set; }
    public GhostScatter scatter { get; private set; }
    public GhostFrighned frighned { get; private set; }
    public GhostBehavior initionalBehaviour;
    public Transform target;
    public int points = 200;

    private void Awake()
    {
        this.movement = GetComponent<Movement>();
        this.home=GetComponent<GhostHome>();
        this.chase=GetComponent<GhostChase>();
        this.scatter=GetComponent<GhostScatter>();
        this.frighned=GetComponent<GhostFrighned>();
    }
    private void Start()
    {
        ResetState();
    }
    public void ResetState()
    {
        this.gameObject.SetActive(true);
        this.movement.ResetState();

        this.frighned.Disable();
        this.chase.Disable();
        this.scatter.Enable();

        if (this.home!=this.initionalBehaviour)
        {
            this.home.Disable();
        }
        if (this.initionalBehaviour!=null)
        {
            this.initionalBehaviour.Enable();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        { 
          
            if (this.frighned.enabled)
            {
                FindObjectOfType<GameManager>().GhostEaten(this);
            }
            else
            {
                FindObjectOfType<GameManager>().PacmanEaten();
            }
        }
    }
}
