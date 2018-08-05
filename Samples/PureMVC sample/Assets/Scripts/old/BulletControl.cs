using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
        // автоматическое уничтожение объекта через две секунды
		Destroy(gameObject, 2);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        // If it hits an enemy...
        if (col.tag == "Enemy")
        {
            EnemyControl script = col.gameObject.GetComponent<EnemyControl>();
            if (script != null)
            {
                // ... find the Enemy script and call the Hurt function.
                script.Hurt();
            }
            else
            {
                EnemyControl2 script2 = col.gameObject.GetComponent<EnemyControl2>();
                if (script2 != null)
                    script2.Hurt();
            }

            // Call the explosion instantiation.
            //OnExplode();

            // Destroy the rocket.
            Destroy(gameObject);
        }
   
        // Otherwise if the player manages to shoot himself...
        else if (col.gameObject.tag != "Player")
        {
            // Instantiate the explosion and destroy the rocket.
            Destroy(gameObject);
        }
    }
}
