using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass_Manager : MonoBehaviour
{
    internal List<Sheep_Controller> eating_Sheebs = new List<Sheep_Controller>();
    internal Animator[] grass_Tufts;

    public GameObject score_Controller;

    public Timer timer;

    public bool selected;

    bool eat_Started;
    internal bool grass_Left = true;

    internal int eat_Speed;

    private void Start()
    {
        grass_Tufts = GetComponentsInChildren<Animator>();

        StartCoroutine(Grass_Eat());
    }

    private void Update()
    {
        Get_Sheebs();

        foreach (Animator grass in grass_Tufts)
        {
            if (grass.GetInteger("Health") <= 0 && grass_Left)
            {
                grass_Left = false;
                eat_Started = false;
                Stop_Eating();
            }
        }

        if (eating_Sheebs.Count > 0f && grass_Left)
        {
            Eat_Grass();
            eat_Started = true;
        }
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

            sheeb.GetComponentInChildren<Animator>().SetBool("Moving", false);
            sheeb.GetComponentInChildren<Animator>().SetBool("Eating", true);

            if (!eat_Started)
                eat_Speed += 5;
        }
    }

    public void Stop_Eating()
    {
        foreach (Sheep_Controller sheeb in eating_Sheebs)
        {
            sheeb.GetComponentInChildren<Animator>().SetBool("Moving", true);
            sheeb.GetComponentInChildren<Animator>().SetBool("Eating", false);
        }

        eat_Speed = 0;

        timer.time_Counter += 10;

        score_Controller.GetComponent<Score>().Trigger_Score();
    }

    public IEnumerator Grass_Eat()
    {
        while (true)
        {
            yield return new WaitUntil(() => eat_Started);

            yield return new WaitForSeconds(2);

            foreach (Animator grass_T in grass_Tufts)
            {
                grass_T.SetInteger("Health", grass_T.GetInteger("Health") - eat_Speed);
            }
        }
    }
}
