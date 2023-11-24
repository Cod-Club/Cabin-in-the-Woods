using System;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

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
