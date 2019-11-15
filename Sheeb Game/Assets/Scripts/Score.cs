using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    //Variables needed for scoretracking
    float currentScore;
    private float grassValue;

    //variables needed for the UI
    public Text scoreText;
    public float waitForTextDisappear;

    // Start is called before the first frame update
    void Start()
    {
        currentScore = 0;
        grassValue = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Return) /*Replace this if statement with the condition for the grass being eaten*/)
        {
            StartCoroutine(GrassScore());
        }

        scoreText.text = "Score: " + currentScore.ToString();
    }

    IEnumerator GrassScore()
    {
        FloatingTextManager.Instance.CreateText();
        yield return new WaitForSeconds(waitForTextDisappear);
        currentScore += grassValue;
    }
}
