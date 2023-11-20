using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public string sceneToLoad;
    FadeInOut fadeInOut;
    public Transform ui;
    TextMeshProUGUI timeText;
    TextMeshProUGUI dayText;
    public float timeOfDay = 0;
    public float dayNum = 1;

    // Start is called before the first frame update
    void Start()
    {
        fadeInOut = ui.transform.Find("FadePanel").GetComponent<FadeInOut>();
        timeText = ui.transform
            .Find("TimeInfo/Time")
            .GetComponent<TextMeshProUGUI>();
        dayText = ui.transform
            .Find("TimeInfo/Day")
            .GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        timeOfDay += Time.deltaTime;
        if (timeOfDay >= 1200)
        {
            timeOfDay = 0;
            dayText.text = (++dayNum).ToString();
        }

        timeText.text = timeOfDay.ToString();
    }

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
