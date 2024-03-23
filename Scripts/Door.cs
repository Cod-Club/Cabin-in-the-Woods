using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public string sceneToLoad;

    GameManager gameManager;

    FadeInOut fadeInOut;

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
        if (other.GetComponent<PlayerMovement>())
            door.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerMovement>() && door)
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
        gameManager.Invoke(
            nameof(gameManager.LoadScene),
            fadeInOut.fadeDuration
        );
    }
}
