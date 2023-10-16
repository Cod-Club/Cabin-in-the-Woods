using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameManager gameManager;
    [HideInInspector] public Transform ui;
    [HideInInspector] public Transform healthBar;
    Vector3 healthBarStartPos;
    Vector3 healthBarEndPos;
    public float healthBarEndOffset = 144f;
    [Space]

    [HideInInspector] public float health;
    public float startHealth = 100f;
    // Start is called before the first frame updateuhhhhhhhhh
    void Start()
    {
        health = startHealth;
        ui = gameManager.ui.transform;
        healthBar = ui.Find("Health/health");
        healthBarEndPos = new Vector3(-healthBarEndOffset, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.localPosition = Vector3.Lerp(healthBarEndPos, healthBarStartPos, health / startHealth);
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;
    }
}
