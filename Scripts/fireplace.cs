using UnityEngine;

public class Fireplace : MonoBehaviour
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

        if (!player)
            return;

        int activeInventorySlotIndex = player.inventory.activeSlotIndex;

        if (
            Input.GetKeyDown(KeyCode.F)
            && player.inventory.GetItemName(activeInventorySlotIndex)
                == "Stick"
        )
        {
            Debug.Log("Placing stick");
            sticks++;
            player.inventory.DeleteItem(activeInventorySlotIndex);
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
