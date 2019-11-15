using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public int time;
    internal int time_Counter;

    private TextMeshProUGUI text;

    // Start is called before the first frame update
    void Awake()
    {
        time_Counter = time;
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        StartCoroutine(Decrease_Timer());
    }

    private void Update()
    {
        if (time_Counter < 100)
            text.text = time_Counter.ToString("00");
        else
            text.text = time_Counter.ToString("000");

    }

    public IEnumerator Decrease_Timer()
    {
        while (time_Counter > 0f)
        {
            yield return new WaitForSeconds(1);
            time_Counter--;
        }
    }
}
