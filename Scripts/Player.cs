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
    [HideInInspector]
    public float health;
    public float startHealth = 100f;
    int inventorySlots;
    int slot = 0;
    public Transform Slots;

    Dictionary<int, KeyCode> dict;
    KeyCode _1 = KeyCode.Alpha1;
    KeyCode _2 = KeyCode.Alpha2;
    KeyCode _3 = KeyCode.Alpha3;
    KeyCode _4 = KeyCode.Alpha4;
    KeyCode _5 = KeyCode.Alpha5;
    KeyCode _6 = KeyCode.Alpha6;

    // Start is called before the first frame update
    void Start()
    {
        health = startHealth;
        ui = gameManager.ui.transform;
        healthBar = ui.Find("Health/health");
        healthBarEndPos = new Vector3(-healthBarEndOffset, 0, 0);

        dict = new Dictionary<int, KeyCode>()
        {
            { 0, _1 },
            { 1, _2 },
            { 2, _3 },
            { 3, _4 },
            { 4, _5 },
            { 5, _6 },
        };

        inventorySlots = Slots.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.localPosition = Vector3.Lerp(
            healthBarEndPos,
            healthBarStartPos,
            health / startHealth
        );

        Inventory();
    }

    void Inventory()
    {
        Debug.Log(dict[slot]);
        for (int i = 0; i < inventorySlots; i++)
        {
            if (Input.GetKeyDown(dict[i]))
                slot = i;
        }

        if (Input.mouseScrollDelta.y < 0)
        {
            slot++;
        }
        if (Input.mouseScrollDelta.y > 0)
        {
            slot--;
        }

        if (slot < 0)
            slot = inventorySlots - 1;
        if (slot >= inventorySlots)
            slot = 0;

        foreach (Transform newSlot in Slots)
        {
            newSlot.gameObject.SetActive(newSlot.gameObject.name == slot.ToString());
        }
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;
    }
}
