using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Herd_Behavior : ScriptableObject
{
    public abstract Vector2 Calculate_Move(Sheep_Controller sheeb, List<Transform> context, Herd_Controller herd);
}
