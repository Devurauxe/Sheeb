using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Herd/Behavior/Alignment")]
public class Alignment : Herd_Behavior
{
    public override Vector2 Calculate_Move(Sheep_Controller sheeb, List<Transform> context, Herd_Controller herd)
    {
        // If there are no neighbors, do not adjust
        if (context.Count == 0f)
            return sheeb.transform.up;

        // Add points together and average
        Vector2 align_Move = Vector2.zero;

        foreach (Transform thing in context)
        {
            align_Move += (Vector2)thing.transform.up;
        }

        align_Move /= context.Count;

        return align_Move;
    }
}
