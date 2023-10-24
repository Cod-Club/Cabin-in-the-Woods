using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    public Transform panel;
    public float fadeDuration = 0.7f;

    [HideInInspector]
    public bool fadeIn = false;

    [HideInInspector]
    public float t = 0f;

    [HideInInspector]
    public bool finished = false;

    Image panelImg;

    private void Start()
    {
        panelImg = panel.transform.GetComponent<Image>();
    }

    public void Fade()
    {
        panelImg.color = new Color(0, 0, 0, fadeIn ? t / fadeDuration : 1 - t / fadeDuration);

        finished = t >= fadeDuration;
    }

    void Update()
    {
        Fade();
        t += Time.deltaTime;
    }
}
