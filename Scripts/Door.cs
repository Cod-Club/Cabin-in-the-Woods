using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public string sceneToLoad;
    [Space]
    public GameManager gameManager;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<PlayerMovement>() != null)
        {
            gameManager.sceneToLoad = sceneToLoad;
            gameManager.ui.transform.Find("OpenDoor").gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerMovement>() != null)
        {
            gameManager.sceneToLoad = null;
            gameManager.ui.transform.Find("OpenDoor").gameObject.SetActive(false);
        }
    }
}
