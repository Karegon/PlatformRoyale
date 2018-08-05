using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombControl : MonoBehaviour
{

    public float WaitTime = 2;
    public float Radius = 100;
    public float Power = 50;

    // Use this for initialization
    void Start () {
	    Invoke("Boom", WaitTime);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void Boom()
    {
        print("boom");

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, Radius);

        foreach (Collider2D hit in colliders)
        {
            if (hit.attachedRigidbody != null)
            {
                Vector3 direction = hit.transform.position - transform.position;
                direction.z = 0;

                //if (CanUse(position, hit.attachedRigidbody))
                {
                    hit.attachedRigidbody.AddForce(direction.normalized * Power);
                }
            }
        }
        DestroyObject(gameObject, 0.2f);
    }
}
