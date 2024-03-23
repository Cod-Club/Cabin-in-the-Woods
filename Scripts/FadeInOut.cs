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
        if (!finished)
        {
            panelImg.color = new Color(
                0,
                0,
                0,
                fadeIn ? t / fadeDuration : 1 - t / fadeDuration
            );

            t += Time.deltaTime;
            finished = t >= fadeDuration;
        }
    }

    public void ResetFade()
    {
        t = 0;
        finished = false;
    }

    void Update()
    {
        Fade();
    }
}
