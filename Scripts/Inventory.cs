using System;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    GameManager gameManager;

    [HideInInspector]
    public Transform uiInventory;

    [HideInInspector]
    public Transform playerInventory;

    [SerializeField]
    UnityEngine.UI.Image defaultImage;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        uiInventory = gameManager.ui.Find("Inventory");
        playerInventory = transform.Find("Inventory");
    }

    private int GetEmptySlotIndex()
    {
        for (int i = 0; i < playerInventory.childCount; i++)
        {
            if (playerInventory.GetChild(i).childCount == 0)
                return i;
        }

        return -1;
    }

    public void AddItem(Transform item)
    {
        int slotIndex = GetEmptySlotIndex();
        if (slotIndex == -1)
            return;

        // add item to player inventory
        item.SetParent(playerInventory.GetChild(slotIndex));
        item.localPosition = Vector3.zero;

        // set image in UI inventory slot to item sprite
        uiInventory
            .GetChild(slotIndex)
            .GetComponent<UnityEngine.UI.Image>()
            .sprite = item.Find("Canvas/Image")
            .GetComponent<UnityEngine.UI.Image>()
            .sprite;

        // remove physics of item
        Destroy(item.GetComponent<Rigidbody2D>());
        item.GetComponent<BoxCollider2D>().enabled = false;
        item.GetComponent<BoxCollider2D>().enabled = false;
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
