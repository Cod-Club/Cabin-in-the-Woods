using System;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameManager gameManager;
    public Transform inventorySlots;
    Transform emptySlot;

    void Start()
    {
        gameManager = GameObject
            .Find("gameManager")
            .GetComponent<GameManager>();
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
            }
        }
    }

    public void AddItem(Transform item)
    {
        getEmptySlot();
        item.SetParent(emptySlot.transform);
    }
}
