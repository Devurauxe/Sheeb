using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Herd/Behavior/Avoidance")]
public class Avoidance : Herd_Behavior
{
    public override Vector2 Calculate_Move(Sheep_Controller sheeb, List<Transform> context, Herd_Controller herd)
    {
        // If there are no neighbors, do not adjust
        if (context.Count == 0f)
            return Vector2.zero;

        // Add points together and average
        Vector2 avoid_Move = Vector2.zero;
        int n_Avoid = 0;

        foreach (Transform thing in context)
        {
            if (Vector2.SqrMagnitude(thing.position - sheeb.transform.position) < herd.SquareAvoidRadius)
            {
                n_Avoid++;
                avoid_Move += (Vector2)(sheeb.transform.position -  thing.position);
            }
        }

        if (n_Avoid > 0)
            avoid_Move /= n_Avoid;

        return avoid_Move;
    }
}
