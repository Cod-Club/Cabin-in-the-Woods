using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnTouch : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("touched big scary sharp diamond thing with " + other.gameObject.name);
        if (other.transform.GetComponent<Player>() != null)
        {
            other.transform.GetComponent<Player>().TakeDamage(10f);
        }
    }
}
