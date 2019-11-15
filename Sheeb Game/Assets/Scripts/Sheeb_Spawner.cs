using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheeb_Spawner : MonoBehaviour
{
    public Herd_Controller controller;

    private void Start()
    {
        StartCoroutine(Spawn_Handler());
    }

    public IEnumerator Spawn_Handler()
    {
        while (true)
        {
            yield return new WaitForSeconds(10);
            Spawn_Sheeb();
        }
    }

    public void Spawn_Sheeb()
    {
        Vector2 spawn_Pos = new Vector2(Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x), Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y));
        controller.Spawn_Sheeb(spawn_Pos);

        Debug.Log("Sheeb spawned");
    }
}
