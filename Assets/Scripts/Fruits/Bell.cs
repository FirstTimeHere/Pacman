using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bell : Pellet
{
    protected override void Eat()
    {
        FindObjectOfType<GameManager>().BellEaten(this);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            Eat();
        }
    }
}
