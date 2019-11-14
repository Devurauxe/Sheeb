using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Sheep_Controller : MonoBehaviour
{
    internal Collider2D sheeb_Collider;
    internal Rigidbody2D rb;

    public Transform target;

    internal bool keep_Moving = true;
    internal bool in_Area;

    public Collider2D SheebCollider { get { return sheeb_Collider; } }

    // Start is called before the first frame update
    void Start()
    {
        switch (Random.Range(0, 3))
        {
            case 0:
                tag = "Red_Sheeb";
                GetComponentInChildren<SpriteRenderer>().color = Color.red;
                break;
            case 1:
                tag = "Blue_Sheeb";
                GetComponentInChildren<SpriteRenderer>().color = Color.blue;
                break;
            case 2:
                tag = "Green_Sheeb";
                GetComponentInChildren<SpriteRenderer>().color = Color.green;
                break;
        }

        sheeb_Collider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 velocity)
    {
        if (keep_Moving)
        {
            transform.up = velocity;
            transform.position += (Vector3)velocity * Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Contains("Sheeb"))
            Physics2D.IgnoreCollision(sheeb_Collider, collision.collider);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Contains("Sheeb_Point"))
            keep_Moving = true;
    }
}
