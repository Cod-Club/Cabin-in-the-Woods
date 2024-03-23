using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public Transform ui;
    public string sceneToLoad;

    void Start()
    {
        ui = GameObject.Find("UI").transform;
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
