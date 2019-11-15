using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Sheep_Controller : MonoBehaviour
{
    public Herd_Controller herd_Con;
    internal Collider2D sheeb_Collider; // The sheeb's individual collider
    internal Rigidbody2D rb; // The rigidbody of the sheeb (not used currently)

    internal bool keep_Moving = true; // Should the sheeb keep moving?

    //Visual Scaling Variables:
    public float changeDirectionTime; //How long to wait after a sheeb has changed direction (visually) before it can change direction again (prevents jitter)
    private float sheebScale; //How beeg a sheeb is (set at start based on transform)
    private float timeSinceDChange = 0; //The time it was last time sheeb changed direction

    //variables for commanding sheebs places
    internal bool commanded;
    private Vector3 newTargetPosition;
    public GameObject command_Marker;
    internal GameObject marker;
    private GameObject selected_Tile;

    public Collider2D SheebCollider { get { return sheeb_Collider; } } // Get method for sheeb collider

    // Start is called before the first frame update
    void Awake()
    {
        // Randomly determine the sheeb's tag and color
        switch (Random.Range(0, 3))
        {
            case 0:
                tag = "Red_Sheeb";
                GetComponentInChildren<SpriteRenderer>().color = new Color32(255, 102, 102, 255);
                break;
            case 1:
                tag = "Blue_Sheeb";
                GetComponentInChildren<SpriteRenderer>().color = new Color32(102, 214, 255, 255);
                break;
            case 2:
                tag = "Green_Sheeb";
                GetComponentInChildren<SpriteRenderer>().color = new Color32(102, 255, 110, 255);
                break;
        }

        sheeb_Collider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        sheebScale = transform.localScale.x; //Grab arbitrary scale value from default sheeb

        commanded = false;
    }

    // Move the sheeb forward
    public void Move(Vector2 velocity)
    {   

        if (keep_Moving && GetComponentInChildren<Animator>().GetBool("InAir")) //Animator check added for bounces
        {
            transform.up = velocity;
            transform.position += (Vector3)velocity * Time.deltaTime;

            //Visual Positional Update:
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y); //Simulate depth
            if (changeDirectionTime <= (Time.realtimeSinceStartup - timeSinceDChange))
            {
                float prevScale = transform.localScale.x; //Initialize memory variable
                if (velocity.x > 0) { transform.localScale = Vector3.one * sheebScale; } else { transform.localScale = new Vector3(-1, 1, 1) * sheebScale; } //Change scale (if necessary)
                if (prevScale != transform.localScale.x) { timeSinceDChange = Time.realtimeSinceStartup; } //Increment time tracker if scale was changed
            }
        }

        if (Input.GetMouseButtonUp(1))
        {
            herd_Con.target_Marked = false;

            foreach(SelectorBox sheeb in SelectorBox.currentlySelected)
            {
                sheeb.transform.parent = null;

                if(transform.parent == null)
                {
                    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward, Mathf.Infinity, 1 << LayerMask.NameToLayer("Grass"));

                    if (hit)
                    {
                        selected_Tile = hit.collider.gameObject;

                        hit.collider.gameObject.GetComponent<Grass_Manager>().selected = true;
                        newTargetPosition = new Vector2(Random.Range(hit.collider.bounds.min.x, hit.collider.bounds.max.x - 2f),
                                                        Random.Range(hit.collider.bounds.min.y + 3f, hit.collider.bounds.max.y));
                        commanded = true;
                    }
                }
            }

            if (!herd_Con.target_Marked && selected_Tile != null)
            {
                Mark_Target();
                herd_Con.target_Marked = true;
            }
        }

        if (commanded && transform.parent == null && GetComponentInChildren<Animator>().GetBool("InAir"))
        {
            transform.position = Vector2.MoveTowards(transform.position, newTargetPosition, 2 * Time.deltaTime);
        }
    }

    public void Mark_Target()
    {
        marker = Instantiate(command_Marker, selected_Tile.transform.position, Quaternion.identity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Set the sheeb's to ignore the collision between other sheebs and the player
        if (collision.gameObject.tag.Contains("Sheeb"))
            Physics2D.IgnoreCollision(sheeb_Collider, collision.collider);
    }
}
