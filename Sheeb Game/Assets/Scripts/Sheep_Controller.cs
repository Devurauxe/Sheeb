using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Sheep_Controller : MonoBehaviour
{
    internal Collider2D sheeb_Collider;
    internal Rigidbody2D rb;

    public Collider2D SheebCollider { get { return sheeb_Collider; } }

    // Start is called before the first frame update
    void Start()
    {
        switch (Random.Range(0, 3))
        {
            case 0:
                tag = "Red_Sheeb";
                break;
            case 1:
                tag = "Blue_Sheeb";
                break;
            case 2:
                tag = "Green_Sheeb";
                break;
        }

        sheeb_Collider = GetComponent<Collider2D>();
    }

    public void Move(Vector2 velocity)
    {
        transform.up = velocity;
        transform.position += (Vector3)velocity * Time.deltaTime;
    }
}
