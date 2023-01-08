using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public float waterSpeed = 2;
    public float fastWaterSpeed = 5;
    public float currentWaterSpeed;
    public float playerToWaterDistance;
    public Transform spider;

    public float allowedDistance = 35.0f;
    bool canMove = true;

    void Update()
    {
        if (canMove)
        {
            //playerToWaterDistance = Vector2.Distance(transform.position, spider.position);

            if (playerToWaterDistance >= allowedDistance)
            {
                currentWaterSpeed = fastWaterSpeed;
            }
            else
            {
                currentWaterSpeed = waterSpeed;
            }

            transform.Translate(Vector2.up * currentWaterSpeed * Time.deltaTime);

            if (spider.position.y < transform.position.y)
            {
                Debug.Log("Player is underneath water?");

                Vector3 pos = transform.position;
                pos.y = spider.position.y - Vector2.Distance(transform.position, spider.position);
                transform.position = pos;
            }
        }
    }


    public void StopWaterRise() 
    {
        canMove = false;
    }

    public void SetYPosition(float YPOs) 
    {
        Vector3 pos = transform.position;
        pos.y = YPOs;
        transform.position = pos;
    }
}
