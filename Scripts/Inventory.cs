using System;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental;
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
    Sprite defaultSprite;
    readonly KeyCode[] inventoryKeycodes =
    {
        KeyCode.Alpha1,
        KeyCode.Alpha2,
        KeyCode.Alpha3,
        KeyCode.Alpha4,
        KeyCode.Alpha5,
        KeyCode.Alpha6
    };
    public int activeSlotIndex = 0;

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
        int activeSlotIndex = GetEmptySlotIndex();
        if (activeSlotIndex == -1)
            return;

        // add item to player inventory
        item.SetParent(playerInventory.GetChild(activeSlotIndex));
        item.localPosition = Vector3.zero;

        // set image in UI inventory slot to item sprite
        uiInventory
            .GetChild(activeSlotIndex)
            .GetComponent<UnityEngine.UI.Image>()
            .sprite = item.Find("Canvas/Image")
            .GetComponent<UnityEngine.UI.Image>()
            .sprite;

        // remove physics of item
        item.GetComponent<Rigidbody2D>().isKinematic = true;
        foreach (
            BoxCollider2D component in item.GetComponents<BoxCollider2D>()
        )
            component.enabled = false;
    }

    public void DropItem()
    {
        Transform item = GetItem(activeSlotIndex);

        // return item to world
        item.parent = null;
        item.GetComponent<Rigidbody2D>().isKinematic = false;
        foreach (
            BoxCollider2D component in item.GetComponents<BoxCollider2D>()
        )
            component.enabled = true;

        // remove item image from UI inventory
        uiInventory
            .GetChild(activeSlotIndex)
            .GetComponent<UnityEngine.UI.Image>()
            .sprite = defaultSprite;
    }

    public void DeleteItem(int activeSlotIndex)
    {
        uiInventory
            .GetChild(activeSlotIndex)
            .GetComponent<UnityEngine.UI.Image>()
            .sprite = defaultSprite;

        Destroy(
            playerInventory.GetChild(activeSlotIndex).GetChild(0).gameObject
        );
    }

    public Transform GetItem(int activeSlotIndex)
    {
        Transform slot = playerInventory.GetChild(activeSlotIndex);

        if (slot.childCount == 0)
            return null;

        return slot.GetChild(0);
    }

    public string GetItemName(int activeSlotIndex)
    {
        Transform item = GetItem(activeSlotIndex);

        if (item == null)
            return null;

        return item.GetComponent<Interactable>().id;
    }

    public void UpdateActiveInventorySlot()
    {
        int slotAmount = playerInventory.childCount;

        // update active slot if up/down arrow key is pressed
        if (Input.GetKeyDown(KeyCode.UpArrow))
            activeSlotIndex = (activeSlotIndex + 1) % slotAmount;
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            activeSlotIndex = (slotAmount + activeSlotIndex - 1) % slotAmount;
        else
            // update active slot if corresponding number key is pressed
            for (int i = 0; i < slotAmount; i++)
            {
                if (Input.GetKeyDown(inventoryKeycodes[i]))
                {
                    activeSlotIndex = i;
                    break;
                }
            }

        // update image in UI inventory and gameobjects in player inventory
        for (int i = 0; i < slotAmount; i++)
        {
            uiInventory
                .GetChild(i)
                .GetComponent<UnityEngine.UI.Image>()
                .color = new Color(
                255f,
                255f,
                255f,
                i == activeSlotIndex ? 1f : 0.5f
            );

            playerInventory
                .GetChild(i)
                .gameObject.SetActive(i == activeSlotIndex);
        }
    }
}
