using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameManager gameManager;

    [HideInInspector]
    public Transform ui;

    [HideInInspector]
    public Transform healthBar;
    Vector3 healthBarStartPos;
    Vector3 healthBarEndPos;
    public float healthBarEndOffset = 144f;

    [Space]
    public int startHealth;

    [HideInInspector]
    public float health;

    [HideInInspector]
    int inventorySlotAmount;

    [HideInInspector]
    int slotIndex = 0;

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
        ui = gameManager.ui;
        healthBar = ui.Find("Health/health");
        healthBarEndPos = new Vector2(-healthBarEndOffset, 0);
        slots = ui.Find("Inventory");

        inventorySlotAmount = slots.childCount;
        health = startHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.localPosition = Vector3.Lerp(
            healthBarEndPos,
            healthBarStartPos,
            health / startHealth
        );

        UpdateActiveInventorySlot();
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

        foreach (Transform newSlot in slots)
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
