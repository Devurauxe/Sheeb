using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerController : MonoBehaviour
{
    //variables
    //Rewired variables
    Player player;
    public int playerID;

    //Movement related variables
    Rigidbody2D rb2d;
    public float moveSpeed;
    public Vector2 movement;
    float moveH;
    float moveV;

    //Variable for flipping
    bool facingRight;


    // Start is called before the first frame update
    void Start()
    {
        //Set player variable to get the rewired component
        player = ReInput.players.GetPlayer(playerID);

        //Set the rb2d to get the rigidbody2D component
        rb2d = this.GetComponent<Rigidbody2D>();

        //the character starts off facing right.
        facingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Setting the variable moveH and moveV to their respective Axis
        moveH = player.GetAxisRaw("Horizontal");
        moveV = player.GetAxisRaw("Vertical");

        //Setting movement to a vector2 with horizontal and vertical movement
        movement = new Vector2(moveH, moveV);

        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
    }

    private void FixedUpdate()
    {
        // Calling a method beneath this void
        MoveCharacter(movement);

        //If the character moves left change scale x to -1 and if moving left change scale x to 1
        if(moveH > 0 && !facingRight || moveH < 0 && facingRight)
        {
            facingRight = !facingRight;

            Vector3 playerScale = transform.localScale;

            playerScale.x *= -1;

            transform.localScale = playerScale;
        }
    }

    void MoveCharacter(Vector2 direction)
    {
        //changing the rigidbody's position in line with inputs from the player
        rb2d.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }
}
