using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orange : Pellet
{
    protected override void Eat()
    {
        FindObjectOfType<GameManager>().OrangeEaten(this);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            Eat();
        }
    }
}
