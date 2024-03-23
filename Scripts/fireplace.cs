using UnityEngine;

public class Fireplace : MonoBehaviour
{
    GameManager gameManager;
    Player player;
    int sticks = 0;
    bool isTouchingPlayer = false;
    bool on = false;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        player = FindObjectOfType<Player>();

        BurnStick();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>())
            isTouchingPlayer = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Player>())
            isTouchingPlayer = false;
    }

    private void Update()
    {
        // place stick
        if (isTouchingPlayer)
        {
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

        // burn stick
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
