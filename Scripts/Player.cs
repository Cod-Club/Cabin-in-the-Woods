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
    public List<Transform> interactables = new();

    [HideInInspector]
    public Transform activeInventorySlot;

    [HideInInspector]
    public int activeInventorySlotIndex;

    [HideInInspector]
    public float health;

    int inventorySlotAmount;

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
        inventory = transform.GetComponent<Inventory>();
        healthBar = ui.Find("Health/health");
        healthBarEndPos = new Vector2(-healthBarEndOffset, 0);

        inventorySlotAmount = inventory.uiInventory.childCount;
        health = startHealth;
    }

    // Update is called once per frame
    void Update()
    {
        SetHealth();
        UpdateActiveInventorySlot();

        if (interactables.Count > 0 && Input.GetKeyDown(KeyCode.E))
        {
            inventory.AddItem(interactables[0]);
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
        int slotIndex = 0;
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

        activeInventorySlot = transform.Find("Inventory").GetChild(slotIndex);

        for (int i = 0; i < inventorySlotAmount; i++)
        {
            inventory.uiInventory
                .GetChild(i)
                .GetComponent<UnityEngine.UI.Image>()
                .color = new Color(
                255f,
                255f,
                255f,
                i == slotIndex ? 1f : 0.5f
            );

            inventory.playerInventory
                .GetChild(i)
                .gameObject.SetActive(i == slotIndex);
        }
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;
    }
}
