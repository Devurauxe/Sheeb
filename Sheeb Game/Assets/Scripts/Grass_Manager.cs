using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass_Manager : MonoBehaviour
{
    internal List<Sheep_Controller> eating_Sheebs;

    public float grass_Left;

    public void Get_Sheebs()
    {
        Collider2D[] sheebs = Physics2D.OverlapBoxAll(transform.position, GetComponent<BoxCollider2D>().size, 0f);

        foreach (Collider2D sheeb in sheebs)
        {
            if (sheeb.GetComponent<Sheep_Controller>().commanded)
                eating_Sheebs.Add(sheeb.GetComponent<Sheep_Controller>());
        }
    }

    public void Eat_Grass()
    {
        foreach (Sheep_Controller sheeb in eating_Sheebs)
        {
            sheeb.GetComponent<Animator>().SetBool("Eating", true);
        }
    }
}
