using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingTextBehaviour : MonoBehaviour
{
    // variables needed for the floating text
    public Vector2 direction;
    public float speed;
    public float destroyTimer;
    public float alphaLevel = 1;
    public float aDegradation;

    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(WaitToDestroy());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        alphaLevel -= aDegradation * Time.deltaTime;
        gameObject.GetComponent<Text>().color = new Color(1, 1, 1, alphaLevel);
    }

    IEnumerator WaitToDestroy()
    {
        Debug.Log("Start Destroy Timer");
        yield return new WaitForSeconds(destroyTimer);
        Debug.Log("Destroy");
        Destroy(gameObject);
    }
}
