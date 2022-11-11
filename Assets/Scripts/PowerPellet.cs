using UnityEngine;

public class PowerPellet : Pellet
{
    public float direction = 8.0f;

    protected override void Eat() //используем с большой таблеткой
    {
        FindObjectOfType<GameManager>().PowerPelletEaten(this);
    }
}
