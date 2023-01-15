using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderWebProjectile : ProjectileMovementComponent
{
    public GameObject SpiderWebPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == instigator) 
        {
            return;
        }



        if (collision.CompareTag("Insect")) 
        {
            Fly insect = collision.GetComponent<Fly>();

            if (insect)
            {
                if (insect.isAlive)
                {
                    //Spawn a Web in top of the insect
                    SpiderWeb spiderWeb = Instantiate(SpiderWebPrefab, collision.transform.position, Quaternion.identity).GetComponent<SpiderWeb>();

                    insect.transform.SetParent(spiderWeb.transform);
                    insect.isAlive = false;
                    insect.DisableMovement();
                }
            }
        }

        Destroy(gameObject);
    }
}
