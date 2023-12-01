using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [HideInInspector]
    public GameManager gameManager;

    [HideInInspector]
    public Transform ui;

    [HideInInspector]
    public Inventory inventory;

    [HideInInspector]
    public Transform healthBar;
    Vector3 healthBarStartPos;
    Vector3 healthBarEndPos;
    public float healthBarEndOffset = 144f;

    [Space]
    public int startHealth;

    [HideInInspector]
    public Transform interactableItem;

    [HideInInspector]
    public Transform item;

    [HideInInspector]
    public float health;

    [HideInInspector]
    public Transform inventorySlots;

    int inventorySlotAmount;

    [HideInInspector]
    public int slotIndex = 0;

    [HideInInspector]
    public Transform slots;

    readonly KeyCode[] inventoryKeycodes =
    {
        KeyCode.Alpha1,
        KeyCode.Alpha2,
        KeyCode.Alpha3,
        KeyCode.Alpha4,
        KeyCode.Alpha5,
        KeyCode.Alpha6
    };

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        ui = gameManager.ui;
        healthBar = ui.Find("Health/health");
        healthBarEndPos = new Vector2(-healthBarEndOffset, 0);
        inventorySlots = ui.Find("Inventory");
        inventory = transform.GetComponent<Inventory>();

        inventorySlotAmount = inventorySlots.childCount;
        health = startHealth;
    }

    // Update is called once per frame
    void Update()
    {
        SetHealth();
        UpdateActiveInventorySlot();

        if (interactableItem != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                inventory.AddItem(interactableItem.transform);
            }
        }
    }

    void SetHealth()
    {
        healthBar.localPosition = Vector3.Lerp(
            healthBarEndPos,
            healthBarStartPos,
            health / startHealth
        );
    }

    void UpdateActiveInventorySlot()
    {
        for (int i = 0; i < inventorySlotAmount; i++)
        {
            if (Input.GetKeyDown(inventoryKeycodes[i]))
            {
                slotIndex = i;
                break;
            }
        }

        slotIndex =
            (
                inventorySlotAmount
                + slotIndex
                - Math.Sign(Input.mouseScrollDelta.y)
            ) % inventorySlotAmount;

        if (
            transform.Find("Inventory/" + slotIndex.ToString()).childCount != 0
        )
        {
            item = transform.Find("Inventory").GetChild(slotIndex);
        }

        foreach (Transform slot in inventorySlots)
        {
            slot.GetComponent<UnityEngine.UI.Image>().color = new Color(
                255f,
                255f,
                255f,
                slot.name == slotIndex.ToString() ? 1f : 0.5f
            );
        }

        foreach (Transform child in transform.Find("Inventory"))
        {
            child.gameObject.SetActive(child.name == slotIndex.ToString());
        }
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;
    }
}
