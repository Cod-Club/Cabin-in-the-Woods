using UnityEngine;

public class fireplace : MonoBehaviour
{
    GameManager gameManager;
    int sticks = 0;
    bool on = false;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        BurnStick();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();

        if (player && Input.GetKeyDown(KeyCode.F))
        {
            int activeInventorySlotIndex = player.inventory.activeSlotIndex;

            if (
                player.inventory.GetItemName(activeInventorySlotIndex)
                == "Stick"
            )
            {
                Debug.Log("Placing stick");
                sticks++;
                player.inventory.DeleteItem(activeInventorySlotIndex);
            }
        }
    }

    private void Update()
    {
        if (!on && sticks == 5)
        {
            on = true;
            sticks++;
        }

        if (sticks == 0)
        {
            on = false;
        }
    }

    void BurnStick()
    {
        if (on)
        {
            sticks = Mathf.Max(sticks - 1, 0);
            Debug.Log("Burning stick");
        }

        Debug.Log(sticks);
        Invoke(nameof(BurnStick), 20f);
    }
}
