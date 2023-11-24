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
    public float health;
    int inventorySlotAmount;
    int slotIndex = 0;

    [HideInInspector]
    public Transform inventorySlots;

    KeyCode[] inventoryKeycodes =
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
        gameManager = GameObject
            .Find("gameManager")
            .GetComponent<GameManager>();
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
        if (interactableItem != null)
        {
            Debug.Log("touching item");
            if (Input.GetKeyDown(KeyCode.E))
            {
                inventory.AddItem(interactableItem);
            }
        }

        SetHealth();
        UpdateActiveInventorySlot();
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

        foreach (Transform newSlot in inventorySlots)
        {
            newSlot.gameObject.SetActive(
                newSlot.gameObject.name == slotIndex.ToString()
            );
        }
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;
    }
}
