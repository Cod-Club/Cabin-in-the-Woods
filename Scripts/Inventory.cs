using System;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [HideInInspector]
    GameManager gameManager;

    [HideInInspector]
    public Transform uiInventory;
    public Transform playerInventory;

    [HideInInspector]
    Transform emptySlot;

    [SerializeField]
    UnityEngine.UI.Image defaultImage;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        uiInventory = gameManager.ui.Find("Inventory");
        playerInventory = transform.Find("Inventory");
    }

    private void GetEmptySlot()
    {
        emptySlot = null;

        foreach (Transform slot in uiInventory)
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

        // Debug.Log(emptySlot.GetComponent<UnityEngine.UI.Image>().sprite.name);
    }

    public void AddItem(Transform item)
    {
        GetEmptySlot();
        if (emptySlot == null)
            return;

        // put item into hermits hand
        item.SetParent(playerInventory.Find(emptySlot.name));
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

    public void RemoveItem(int slotIndex)
    {
        uiInventory
            .GetChild(slotIndex)
            .GetComponent<UnityEngine.UI.Image>()
            .sprite = defaultImage.sprite;

        Destroy(playerInventory.GetChild(slotIndex).GetChild(0).gameObject);
    }
}
