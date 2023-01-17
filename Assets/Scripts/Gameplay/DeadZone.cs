using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Disables everything it touches except it self
        if (collision.CompareTag("DeadZone")) 
        {
            return;
        }

        collision.gameObject.SetActive(false);
    }
}
