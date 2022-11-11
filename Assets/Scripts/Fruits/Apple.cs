using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : Pellet
{
    protected override void Eat()
    {
        FindObjectOfType<GameManager>().AppleEaten(this);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            Eat();
        }
    }
}
