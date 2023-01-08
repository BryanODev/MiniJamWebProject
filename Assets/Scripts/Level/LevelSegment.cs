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
    LevelManager levelManager;
    public ObjectPooler pooler;
    //Pick a random object from here, and place it

    //Pick fly, and place them

    public Color segmentGizmosColor = new Color(1, 1, 1, 1);

    private void Start()
    {
        pooler = ObjectPooler.Instance;
        SetUpLevelSegment();
    }

    public void SetUpLevelSegment(LevelManager manager) 
    {
        levelManager = manager;
    }

    void SetUpLevelSegment() 
    {
        float LevelSegmentFlySpawnYLocation = -15;
        float LevelSegmentPipeSpawnYLocation = -10;
        float LevelSegmentSpikesSpawnYLocation = -5;

        for (int i = 0; i < 10; i++)
        {
            Vector2 SpawnPos = transform.position;
            SpawnPos.x = Random.Range(-4.0f, 4.0f);
            SpawnPos.y = transform.position.y + LevelSegmentFlySpawnYLocation;
            SpawnFly(SpawnPos);

            LevelSegmentFlySpawnYLocation += 3;
        }

        for (int i = 0; i < 3; i++) 
        {
            Vector2 SpawnLocation = transform.position;
            float sideRandomRange = Random.Range(0,75);
            int facingDirection = 1;

            SpawnLocation.x = 4;

            if (sideRandomRange >= 50)
            {
                SpawnLocation.x *= -1;
                facingDirection = -1;
            }

            SpawnLocation.y = transform.position.y + LevelSegmentPipeSpawnYLocation;
            SpawnPipe(SpawnLocation, facingDirection);

            LevelSegmentPipeSpawnYLocation += 10;
        }

        for (int i = 0; i < 3; i++)
        {
            Vector2 SpawnLocation = transform.position;
            float sideRandomRange = Random.Range(0, 75);
            int facingDirection = 1;

            SpawnLocation.x = -5;

            if (sideRandomRange >= 50)
            {
                SpawnLocation.x *= -1;
                facingDirection = -1;
            }

            SpawnLocation.y = transform.position.y + LevelSegmentSpikesSpawnYLocation;
            SpawnSpikes(SpawnLocation, facingDirection);

            LevelSegmentSpikesSpawnYLocation += 5;
        }

        
    }

    public void SetLevelSegmentLocation(Vector2 newPosition) 
    {
        transform.position = newPosition;
    }

    void SpawnFly(Vector2 position) 
    {
        GameObject Fly = ObjectPooler.Instance.GetObjectFromPool("Fly");
        Fly.transform.position = position;
        Fly.transform.SetParent(transform);

        Fly.SetActive(true);
    }

    void SpawnPipe(Vector2 position, int facingDirection = 1) 
    {
        GameObject Pipe = pooler.GetObjectFromPool("Pipe");
        Pipe.transform.position = position;
        Pipe.transform.SetParent(transform);

        Vector2 scale = Vector3.one;
        scale.x = facingDirection;
        Pipe.transform.localScale = scale;

        Pipe.SetActive(true);
    }

    void SpawnSpikes(Vector2 position, int facingDirection = 1)
    {
        GameObject Spikes = pooler.GetObjectFromPool("Spikes");
        Spikes.transform.position = position;
        Spikes.transform.SetParent(transform);

        Vector2 scale = Vector3.one;
        scale.x = facingDirection;
        Spikes.transform.localScale = scale;

        Spikes.SetActive(true);
    }

    public void ResetLevelSegment() 
    {
        Debug.Log(transform.childCount);
        float childCount = transform.childCount;
        //Put every child back to the object pooler
        for (int i = 0; i < childCount; i++) 
        {
            if (transform.GetChild(i) != null)
            {
                Debug.Log(i);
                Transform child = transform.GetChild(i);
                Debug.Log("Moved Child: " + i + " index");
                
                child.position = pooler.transform.position;
                //Problem with parenting? Idk
                //pooler.SetObjectToPoolerParent(child); //child.SetParent(ObjectPooler.Instance.GetPoolerTransform());
                child.gameObject.SetActive(false);

            }
        }

        SetUpLevelSegment();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = segmentGizmosColor;
        Gizmos.DrawWireCube(transform.position, new Vector3(12, 30, 1));
    }
}
