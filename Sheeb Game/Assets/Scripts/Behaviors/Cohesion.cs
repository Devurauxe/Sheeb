using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Herd/Behavior/Cohesion")]
public class Cohesion : Herd_Behavior
{
    public override Vector2 Calculate_Move(Sheep_Controller sheeb, List<Transform> context, Herd_Controller herd)
    {
        // If there are no neighbors, do not adjust
        if (context.Count == 0f)
            return Vector2.zero;

        // Add points together and average
        Vector2 coh_Move = Vector2.zero;

        foreach (Transform thing in context)
        {
            coh_Move += (Vector2)thing.position;
        }

        coh_Move /= context.Count;

        // Offset
        coh_Move -= (Vector2)sheeb.transform.position;

        return coh_Move;
    }
}
