using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextManager : MonoBehaviour
{
    private static FloatingTextManager instance;

    public GameObject textPrefab;

    public Transform textSpawn;

    public RectTransform canvasTransform;

    public static FloatingTextManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType<FloatingTextManager>();
            }
            return instance;
        }
    }

    public void CreateText()
    {
        GameObject ftm = Instantiate(textPrefab, textSpawn.position, textSpawn.rotation);

        ftm.transform.SetParent(canvasTransform);
        ftm.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
    }

}
