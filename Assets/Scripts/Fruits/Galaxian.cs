using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Galaxian : Pellet
{
    protected override void Eat()
    {
        FindObjectOfType<GameManager>().GalaxianEaten(this);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            Eat();
        }
    }
}
