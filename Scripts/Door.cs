using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public string sceneToLoad;

    // [HideInInspector]
    public GameManager gameManager;

    void Start()
    {
        // gameManager = GameObject
        //     .Find("gameManager")
        //     .GetComponent<GameManager>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<PlayerMovement>() != null)
        {
            gameManager.sceneToLoad = sceneToLoad;
            gameManager.ui.transform
                .Find("OpenDoor")
                .gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerMovement>() != null)
        {
            gameManager.ui.transform
                .Find("OpenDoor")
                .gameObject.SetActive(false);
        }
    }
}
