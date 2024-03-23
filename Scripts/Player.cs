using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    GameManager gameManager;

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
    public float health;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        inventory = transform.GetComponent<Inventory>();
        healthBar = gameManager.ui.Find("Health/health");
        healthBarEndPos = new Vector2(-healthBarEndOffset, 0);

        health = startHealth;
    }

    // Update is called once per frame
    void Update()
    {
        SetHealth();
        inventory.UpdateActiveInventorySlot();

        if (interactables.Count > 0 && Input.GetKeyDown(KeyCode.E))
        {
            inventory.AddItem(interactables[0]);
        }

        if (Input.GetKeyDown(KeyCode.Q))
            inventory.DropItem();
    }

    void SetHealth()
    {
        healthBar.localPosition = Vector3.Lerp(
            healthBarEndPos,
            healthBarStartPos,
            health / startHealth
        );
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;
    }
}
