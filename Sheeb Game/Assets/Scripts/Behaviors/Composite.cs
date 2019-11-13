using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Herd/Behavior/Composite")]
public class Composite : Herd_Behavior
{
    public Herd_Behavior[] behaviors;
    public float[] weights;

    public override Vector2 Calculate_Move(Sheep_Controller sheeb, List<Transform> context, Herd_Controller herd)
    {
        // Handle mismatched data
        if (weights.Length != behaviors.Length)
        {
            Debug.LogError("Data mismatch");
            return Vector2.zero;
        }

        // Set up move
        Vector2 move = Vector2.zero;

        for (int i = 0; i < behaviors.Length; i++)
        {
            Vector2 partial_Move = behaviors[i].Calculate_Move(sheeb, context, herd) * weights[i];

            if (partial_Move != Vector2.zero)
            {
                if (partial_Move.sqrMagnitude > weights[i] * weights[i])
                {
                    partial_Move.Normalize();
                    partial_Move *= weights[i];
                }

                move += partial_Move;
            }
        }

        return move;
    }
}
