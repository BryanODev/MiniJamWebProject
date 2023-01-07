using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public float waterSpeed;
    public float currentWaterSpeed;
    public float playerToWaterDistance;
    public Transform spider;

    void Update()
    {
        playerToWaterDistance = Vector2.Distance(transform.position, spider.position);

        if (playerToWaterDistance >= 40)
        {
            currentWaterSpeed = 5.0f;
        }
        else 
        {
            currentWaterSpeed = 1.0f;
        }

        transform.Translate(Vector2.up * currentWaterSpeed * Time.deltaTime);
    }
}
