using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public string sceneToLoad;

    [HideInInspector]
    GameManager gameManager;

    [HideInInspector]
    FadeInOut fadeInOut;

    [HideInInspector]
    GameObject door;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        fadeInOut = gameManager.GetComponent<FadeInOut>();
        door = gameManager.ui.Find("OpenDoor").gameObject;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && door.activeSelf)
            OpenDoor();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<PlayerMovement>() != null)
            door.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerMovement>() != null)
            door.SetActive(false);
    }

    public void OpenDoor()
    {
        if (sceneToLoad == null)
        {
            Debug.LogError("No scene to load on door");
        }

        fadeInOut.fadeIn = true;
        fadeInOut.ResetFade();
        fadeInOut.Fade();

        gameManager.sceneToLoad = sceneToLoad;
        gameManager.Invoke("LoadScene", fadeInOut.fadeDuration);
    }
}
