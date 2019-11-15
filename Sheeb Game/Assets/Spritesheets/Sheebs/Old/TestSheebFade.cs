using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSheebFade : MonoBehaviour
{
    private SpriteRenderer sr;
    public float fadeSpeed;
    private float fadeRef;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Color newColor = new Color(sr.color.r, sr.color.g, sr.color.b, Mathf.SmoothDamp(sr.color.a, 0, ref fadeRef, fadeSpeed));
        sr.color = newColor;
    }
}
