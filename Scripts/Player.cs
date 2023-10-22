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
    int inventorySlotAmount;
    int slotIndex = 0;
    public Transform Slots;

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
        healthBar = ui.Find("Health/health");
        healthBarEndPos = new Vector2(-healthBarEndOffset, 0);

        inventorySlotAmount = Slots.childCount;
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
        Debug.Log(inventoryKeycodes[slotIndex]);
        for (int i = 0; i < inventorySlotAmount; i++)
        {
            if (Input.GetKeyDown(inventoryKeycodes[i]))
            {
                slotIndex = i;
                break;
            }
        }

        slotIndex =
            (slotIndex + Math.Sign(Input.mouseScrollDelta.y))
            % inventorySlotAmount;

        foreach (Transform newSlot in Slots)
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
