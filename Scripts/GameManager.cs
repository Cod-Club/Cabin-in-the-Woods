using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public string sceneToLoad;
    FadeInOut fadeInOut;
    public Transform ui;

    // Start is called before the first frame update
    void Start()
    {
        fadeInOut = ui.transform.Find("FadePanel").GetComponent<FadeInOut>();
    }

    // Update is called once per frame
    void Update() { }

    public void OpenDoor()
    {
        Debug.Log("clicked button");
        fadeInOut.fadeIn = true;
        fadeInOut.t = 0f;
        if (sceneToLoad == null)
        {
            Debug.LogError("No scene to load on door");
        }
        fadeInOut.Fade();

        Invoke("LoadScene", fadeInOut.fadeDuration);
    }

    void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
