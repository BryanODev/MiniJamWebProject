using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            SpiderMotor spider = collision.GetComponent<SpiderMotor>();

            if (spider) 
            {
                spider.Kill(transform.position - spider.transform.position);
            }
        }
    }
}
