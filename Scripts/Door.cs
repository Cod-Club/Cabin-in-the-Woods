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
        gameManager = GameObject
            .Find("GameManager")
            .GetComponent<GameManager>();
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
        SetDoorActive(other, true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        SetDoorActive(other, false);
    }

    private void SetDoorActive(Collider2D other, bool active)
    {
        if (other.GetComponent<PlayerMovement>() != null)
            door.SetActive(active);
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
