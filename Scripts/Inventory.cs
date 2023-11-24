using System;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    // [HideInInspector]
    public GameManager gameManager;

    [HideInInspector]
    public Transform inventorySlots;

    [HideInInspector]
    Transform emptySlot;

    void Start()
    {
        // gameManager = GameObject
        //     .Find("gameManager")
        //     .GetComponent<GameManager>();
        gameManager = transform.GetComponent<Player>().gameManager;
        inventorySlots = gameManager.ui.Find("Inventory");
    }

    private void getEmptySlot()
    {
        emptySlot = null;

        foreach (Transform slot in inventorySlots)
        {
            if (slot.childCount == 0)
            {
                emptySlot = slot;
                break;
            }
        }
    }

    public void AddItem(Transform item)
    {
        getEmptySlot();
        item.SetParent(transform.Find("Inventory"));
        item.transform.localPosition = Vector3.zero;
        Destroy(item.GetComponent<Rigidbody2D>());
        item.GetComponent<BoxCollider2D>().enabled = false;
        item.GetComponent<BoxCollider2D>().enabled = false;

        emptySlot.GetComponent<UnityEngine.UI.Image>().sprite =
            item.GetComponent<UnityEngine.UI.Image>().sprite;
    }
}
