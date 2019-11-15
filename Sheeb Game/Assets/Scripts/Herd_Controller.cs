using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Herd_Controller : MonoBehaviour
{
    public Sheep_Controller sheebPrefab;
    List<Sheep_Controller> sheebs = new List<Sheep_Controller>();

    public Herd_Behavior h_Behavior;

    public int spawn_Count;
    const float sheeb_Density = 0.02f;

    public float drive_Factor;
    public float max_Speed;
    public float neighbor_Radius;
    public float avoidance_Rad_Mult;
    public float standing_Threshold; //Once a sheeb's velocity goes below this point, they switch to their standing animation

    float square_Max_Speed;
    float square_Neighbor_Rad;
    float square_Avoidance_Rad;
    public float SquareAvoidRadius { get { return square_Avoidance_Rad; } }

    [Header("Sheeb Counter Text:")]
    public TextMeshProUGUI red_Sheeb_T;
    public TextMeshProUGUI blue_Sheeb_T;
    public TextMeshProUGUI green_Sheeb_T;

    private int red_Sheeb_Count;
    private int blue_Sheeb_Count;
    private int green_Sheeb_Count;

    // Start is called before the first frame update
    void Start()
    {
        square_Max_Speed = Mathf.Pow(max_Speed, 2f);
        square_Neighbor_Rad = Mathf.Pow(neighbor_Radius, 2f);
        square_Avoidance_Rad = square_Neighbor_Rad * avoidance_Rad_Mult * avoidance_Rad_Mult;

        for (int i = 0; i < spawn_Count; i++)
        {
            Sheep_Controller sheeb = Instantiate(sheebPrefab, Random.insideUnitCircle * spawn_Count * sheeb_Density, Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)), transform);
            sheeb.name = "Sheeb " + i;
            sheebs.Add(sheeb);

            switch (sheeb.tag)
            {
                case "Red_Sheeb":
                    red_Sheeb_Count++;
                    break;
                case "Blue_Sheeb":
                    blue_Sheeb_Count++;
                    break;
                case "Green_Sheeb":
                    green_Sheeb_Count++;
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Sheep_Controller sheeb in sheebs)
        {
            List<Transform> context = GetNearbyObjects(sheeb);

            Vector2 move = h_Behavior.Calculate_Move(sheeb, context, this);

            move *= drive_Factor;

            if (move.sqrMagnitude > square_Max_Speed)
                move = move.normalized * max_Speed;

            sheeb.Move(move);

            if (move.x >= standing_Threshold || move.y >= standing_Threshold)
                 { sheeb.gameObject.GetComponentInChildren<Animator>().SetBool("Moving", true);  } //Tell sheeb to bounce when moving (fast enough)
            else { sheeb.gameObject.GetComponentInChildren<Animator>().SetBool("Moving", false); } //Otherwise put sheeb in standing animation
        }

        red_Sheeb_T.text = red_Sheeb_Count.ToString("00");
        blue_Sheeb_T.text = blue_Sheeb_Count.ToString("00");
        green_Sheeb_T.text = green_Sheeb_Count.ToString("00");
    }

    List<Transform> GetNearbyObjects(Sheep_Controller sheeb)
    {
        List<Transform> s_Context = new List<Transform>();
        Collider2D[] context_Cols = Physics2D.OverlapCircleAll(sheeb.transform.position, neighbor_Radius);

        foreach (Collider2D col in context_Cols)
        {
            if (col != sheeb.SheebCollider && col.gameObject.tag.Contains("Sheeb") && col.transform.parent == transform)
            {
                if (sheeb.gameObject.CompareTag("Red_Sheeb") && col.gameObject.tag.Contains("Red"))
                    s_Context.Add(col.transform);
                else if (sheeb.gameObject.CompareTag("Blue_Sheeb") && col.gameObject.tag.Contains("Blue"))
                    s_Context.Add(col.transform);
                else if (sheeb.gameObject.CompareTag("Green_Sheeb") && col.gameObject.tag.Contains("Green"))
                    s_Context.Add(col.transform);
            }
        }

        return s_Context;
    }

    void Check_Herd()
    {
        foreach (Sheep_Controller sheeb in sheebs)
        {
            List<Transform> context = GetNearbyObjects(sheeb);

            foreach (Transform sheeb_T in context)
            {
                if (sheeb_T.GetComponent<Sheep_Controller>() != null && sheeb_T.gameObject.CompareTag(sheeb.tag))
                {
                    return;
                }
            }

            sheeb.keep_Moving = false;
        }
    }
}
