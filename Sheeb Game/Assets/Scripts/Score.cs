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

    bool trigger_Score = false;

    // Start is called before the first frame update
    void Start()
    {
        currentScore = 0;
        grassValue = 100;

        StartCoroutine(GrassScore());
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + currentScore.ToString();
    }

    public void Trigger_Score()
    {
        trigger_Score = true;
    }

    IEnumerator GrassScore()
    {
        yield return new WaitUntil(() => trigger_Score = true);

        Debug.Log("Score triggered");
        FloatingTextManager.Instance.CreateText();
        yield return new WaitForSeconds(waitForTextDisappear);
        currentScore += grassValue;
        trigger_Score = false;
    }
}
