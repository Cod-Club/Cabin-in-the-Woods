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
        if (other.GetComponent<Player>())
        {
            Player player = other.GetComponent<Player>();
            if (
                player.interactableItem.name == "stick"
                && Input.GetKeyDown(KeyCode.E)
            )
            {
                sticks++;
                player.inventory.RemoveItem(player.slotIndex);
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
        }

        Invoke("BurnStick", 20f);
    }
}
