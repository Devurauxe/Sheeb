using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheebMovementTester : MonoBehaviour
{
    public float sheebSbeed; //How fast this sheeb moves when in the air

    void Update()
    {
        //Positional Update:
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);

        if (GetComponent<Animator>().GetBool("InAir") == true)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(sheebSbeed, 0);
            //GetComponent<Rigidbody2D>().velocity = new Vector2(0, sheebSbeed);
            //GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-20, 20), Random.Range(-20, 20)));
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}
