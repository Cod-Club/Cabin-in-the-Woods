using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    public Transform panel;
    public float fadeTime = 0.7f;

    [HideInInspector]
    public bool fadeIn = false;

    [HideInInspector]
    public float timer = 0f;

    [HideInInspector]
    public bool finished = false;

    Image panelImg;

    private void Start()
    {
        panelImg = panel.transform.GetComponent<Image>();
    }

    public void Fade()
    {
        panelImg.color = new Color(
            0f,
            0f,
            0f,
            Mathf.Lerp(fadeIn ? 0 : 1, fadeIn ? 1 : 0, timer / fadeTime)
        );
        if (timer >= fadeTime)
        {
            finished = true;
        }
        else
        {
            finished = false;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        Fade();
    }
}
