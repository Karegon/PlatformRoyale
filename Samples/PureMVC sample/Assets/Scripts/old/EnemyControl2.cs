using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl2 : MonoBehaviour {


    [HideInInspector]
    public bool facingRight = false;         // For determining which way the player is currently facing.
    public Rigidbody2D bullet;				// Prefab of the bullet

    public float healthForLabel;

    public float moveSpeed = 2f; // The speed the enemy moves at.
    public int health = 2; // How many times the enemy can be hit before it dies.

    private Transform frontCheck; // Reference to the position of the gameobject used for checking if something is in front.

    private bool dead = false; // Whether or not the enemy is dead.
    private Vector3 initCoord;

    // скорость пули
    public float bulletSpeed = 20f;

    private int shotTimeout = 10;
    private int shotQty = 10;

    void Start()
    {
        
    }

    void Awake()
    {
        // Setting up the references.
        frontCheck = transform.Find("frontCheck").transform;
        initCoord = transform.position;
    }

    void FixedUpdate()
    {
        // Create an array of all the colliders in front of the enemy.
        Collider2D[] frontHits = Physics2D.OverlapPointAll(frontCheck.position, 1);

        // Check each of the colliders.
        foreach (Collider2D c in frontHits)
        {
            // If any of the colliders is an Obstacle...
            if (c.tag == "Obstacle")
            {
                // ... Flip the enemy and stop checking the other colliders.
                Flip();
                break;
            }
        }

        // Set the enemy's velocity to moveSpeed in the x direction.

        /*GetComponent<Rigidbody2D>().velocity =
            new Vector2(transform.localScale.x * moveSpeed, GetComponent<Rigidbody2D>().velocity.y);

        */

        if (findPlayer())
        {
            moveToPlayer();
            shootToPlayer();
        }
        else
        {
            moveBack();
        }

        // If the enemy has zero or fewer hit points and isn't dead yet...
        if (health <= 0 && !dead)
            // ... call the death function.
            Death();
    }

    public void Hurt()
    {
        // Reduce the number of hit points by one.
        health--;
    }

    public void updateHealthbar()
    {
        Debug.Log("Updating health");

        
    }

    void Death()
    {
        // Find all of the sprite renderers on this object and it's children.
        SpriteRenderer[] otherRenderers = GetComponentsInChildren<SpriteRenderer>();

        // Disable all of them sprite renderers.
        foreach (SpriteRenderer s in otherRenderers)
        {
            s.enabled = false;
        }

        // Set dead to true.
        dead = true;

        // Find all of the colliders on the gameobject and set them all to be triggers.
        Collider2D[] cols = GetComponents<Collider2D>();
        foreach (Collider2D c in cols)
        {
            c.isTrigger = true;
        }
    }


    public void Flip()
    {
        facingRight = !facingRight;
        // Multiply the x component of localScale by -1.
        Vector3 enemyScale = transform.localScale;
        enemyScale.x *= -1;
        transform.localScale = enemyScale;
    }

    public bool findPlayer()
    {
        bool res = false;
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.right);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider != null && hits[i].collider.tag == "Player")
            {
                print(hits[i].collider.gameObject.name);
                res = true;
            }
        }
        return res;
    }

    public void moveToPlayer()
    {
        GetComponent<Rigidbody2D>().velocity =
            new Vector2(transform.localScale.x * moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
    }

    public void shootToPlayer()
    {
        if (shotQty > 0)
        {
            shotQty--;
        }
        else
        {
            shotQty = shotTimeout;
        
            float angle;
            float speed;
            if (facingRight)
            {
                angle = 0f;
                speed = -bulletSpeed;
            }
            else
            {
                angle = 180f;
                speed = bulletSpeed;
            }
            // ... instantiate the rocket facing right and set it's velocity to the right. 
            Rigidbody2D bulletInstance = Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(0, 0, angle))) as Rigidbody2D;
            bulletInstance.velocity = new Vector2(speed, 0);
        }
    }

    /*
     *  Возвращает врага в место постоянной дислокации
     */
    public void moveBack()
    {
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, initCoord, step);
    }

}
