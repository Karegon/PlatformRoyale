using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl2 : MonoBehaviour {
    // Use this for initialization
    void Start()
    {
        // автоматическое уничтожение объекта через две секунды
        Destroy(gameObject, 2);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // If it hits an enemy...
        if (col.tag == "Player")
        {
            PlayerControl script = col.gameObject.GetComponent<PlayerControl>();
            if (script != null)
            {
                // ... find the Enemy script and call the Hurt function.
                script.Hurt();
            }

            // Call the explosion instantiation.
            //OnExplode();

            // Destroy the rocket.
            Destroy(gameObject);
        }

        // Otherwise if the player manages to shoot himself...
        else if (col.gameObject.tag != "Enemy")
        {
            // Instantiate the explosion and destroy the rocket.
            Destroy(gameObject);
        }
    }
}
