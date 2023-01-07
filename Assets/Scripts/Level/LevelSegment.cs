using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  SUMMARY:
 *  LevelSegment class will be incharge of spawning every obstacle, fly, etc on its place. 
 * 
 * 
 */
public class LevelSegment : MonoBehaviour
{
    //Pick a random object from here, and place it

    //Pick fly, and place them

    private void Start()
    {
        SpawnFly();
    }

    public void SetLevelSegmentLocation(Vector2 newPosition) 
    {
        transform.position = newPosition;
    }

    void SpawnFly() 
    {
        GameObject Fly = ObjectPooler.Instance.GetObjectFromPool("Fly");
        Fly.transform.position = transform.position;
        Fly.transform.SetParent(transform);
    }
}
