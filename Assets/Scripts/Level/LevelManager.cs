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

    public Transform spider;
    public Transform playerCamera;

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

    private void Update()
    {
        if (spider.position.y >= 75) 
        {
            Vector2 SpiderLocalPositionInSegment = segments[2].transform.InverseTransformPoint(spider.position);
            Vector3 CameraLocalPositionInSegment = segments[2].transform.InverseTransformPoint(playerCamera.position);

            segments[2].SetLevelSegmentLocation(new Vector3(0, 0, 0));
            segments[3].SetLevelSegmentLocation(new Vector3(0, 30, 0));
            segments[0].SetLevelSegmentLocation(new Vector3(0, 60, 0));
            segments[1].SetLevelSegmentLocation(new Vector3(0, 90, 0));

            spider.position = SpiderLocalPositionInSegment;

            CameraLocalPositionInSegment.z = -10;
            playerCamera.position = CameraLocalPositionInSegment;
        }
    }
}
