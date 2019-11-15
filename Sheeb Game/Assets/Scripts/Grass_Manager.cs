using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass_Manager : MonoBehaviour
{
    internal List<Sheep_Controller> eating_Sheebs = new List<Sheep_Controller>();

    public bool selected;

    public float grass_Left;

    private void Update()
    {
        Get_Sheebs();

        if (eating_Sheebs.Count > 0f)
            Eat_Grass();
    }

    public void Get_Sheebs()
    {
        Collider2D[] sheebs = Physics2D.OverlapBoxAll(transform.position, GetComponent<BoxCollider2D>().size / 2, 0f);

        foreach (Collider2D sheeb in sheebs)
        {
            if (sheeb.GetComponent<Sheep_Controller>() != null && sheeb.GetComponent<Sheep_Controller>().commanded && selected && !eating_Sheebs.Contains(sheeb.GetComponent<Sheep_Controller>()))
                eating_Sheebs.Add(sheeb.GetComponent<Sheep_Controller>());
        }
    }

    public void Eat_Grass()
    {
        foreach (Sheep_Controller sheeb in eating_Sheebs)
        {
            Destroy(sheeb.marker);

            sheeb.GetComponentInChildren<Animator>().SetBool("Eating", true);
        }
    }
}
