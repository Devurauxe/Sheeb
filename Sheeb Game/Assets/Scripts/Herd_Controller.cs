using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Herd_Controller : MonoBehaviour
{
    public Sheep_Controller sheebPrefab;
    List<Sheep_Controller> sheebs = new List<Sheep_Controller>();

    public Herd_Behavior h_Behavior;

    const float sheeb_Density = 0.08f;

    public float drive_Factor;
    public float max_Speed;
    public float neighbor_Radius;
    public float avoidance_Rad_Mult;

    float square_Max_Speed;
    float square_Neighbor_Rad;
    float square_Avoidance_Rad;
    public float SquareAvoidRadius { get { return square_Avoidance_Rad; } }

    // Start is called before the first frame update
    void Start()
    {
        square_Max_Speed = Mathf.Pow(max_Speed, 2f);
        square_Neighbor_Rad = Mathf.Pow(neighbor_Radius, 2f);
        square_Avoidance_Rad = square_Neighbor_Rad * avoidance_Rad_Mult * avoidance_Rad_Mult;
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
        }
    }

    List<Transform> GetNearbyObjects(Sheep_Controller sheeb)
    {
        List<Transform> s_Context = new List<Transform>();
        Collider2D[] context_Cols = Physics2D.OverlapCircleAll(sheeb.transform.position, neighbor_Radius);

        foreach (Collider2D col in context_Cols)
        {
            if (col != sheeb.SheebCollider && col.gameObject.tag.Contains("Sheeb"))
            {
                s_Context.Add(col.transform);
            }
        }

        return s_Context;
    }
}
