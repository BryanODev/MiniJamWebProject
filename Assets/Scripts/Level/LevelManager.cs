using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  SUMMARY:
 *  Level Manager class will be in charge of all level logic. The level is divided into segments, which once the player reaches the averege of the last two, it will shift everything inside
 *  to the buttom where the first two segments are. And the other two will be pushed. This way we make sure that the player and all the other game content stays contained from a position 
 *  floating point to another. This will prevent the player from reaching a position where the floating point begins to be limited by its type.
 */

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] List<LevelSegment> segments = new List<LevelSegment>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++) 
        {
            segments.Add(transform.GetChild(i).GetComponent<LevelSegment>());
        }
    }
}
