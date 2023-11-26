using System;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [HideInInspector]
    public GameManager gameManager;

    [HideInInspector]
    public Transform inventorySlots;

    [HideInInspector]
    Transform emptySlot;

    void Start()
    {
        gameManager = GameObject
            .Find("GameManager")
            .GetComponent<GameManager>();
        gameManager = transform.GetComponent<Player>().gameManager;
        inventorySlots = gameManager.ui.Find("Inventory");
    }

    private void getEmptySlot()
    {
        emptySlot = null;

        foreach (Transform slot in inventorySlots)
        {
            if (
                slot.GetComponent<UnityEngine.UI.Image>().sprite.name
                == "Background"
            )
            {
                emptySlot = slot;
                break;
            }
        }

        Debug.Log(emptySlot.GetComponent<UnityEngine.UI.Image>().sprite.name);
    }

    public void AddItem(Transform item)
    {
        getEmptySlot();
        if (emptySlot == null)
            return;

        // put item into hermits hand
        item.SetParent(transform.Find("Inventory"));
        item.localPosition = Vector3.zero;

        // remove physics of item
        Destroy(item.GetComponent<Rigidbody2D>());
        item.GetComponent<BoxCollider2D>().enabled = false;
        item.GetComponent<BoxCollider2D>().enabled = false;

        // set image in inventory slot to item sprite
        emptySlot.GetComponent<UnityEngine.UI.Image>().sprite = item.Find(
                "Canvas/Image"
            )
            .GetComponent<UnityEngine.UI.Image>()
            .sprite;
    }
}
