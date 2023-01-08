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

    public Water water;

    public Transform spider;
    public Transform playerCamera;

    public int currentSpiderAtSegmentIndex;
    public int segmentSwapIndex;

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
            LevelSegment segment = transform.GetChild(i).GetComponent<LevelSegment>();

            if (segment)
            {
                segment.SetUpLevelSegment(this);
                segments.Add(segment);
            }
        }
    }

    private void Update()
    {
        currentSpiderAtSegmentIndex = Mathf.RoundToInt(spider.transform.position.y / 37.5f);
        currentSpiderAtSegmentIndex = Mathf.Clamp(currentSpiderAtSegmentIndex,0, segments.Count);

        water.playerToWaterDistance = Vector3.Distance(water.transform.position, spider.position);

        if (spider.position.y >= 75) 
        {
            Vector2 SpiderLocalPositionInSegment = Vector2.zero;
            Vector3 CameraLocalPositionInSegment = Vector3.zero;

            
            if (segmentSwapIndex == 0)
            {
                SpiderLocalPositionInSegment = segments[2].transform.InverseTransformPoint(spider.position);
                CameraLocalPositionInSegment = segments[2].transform.InverseTransformPoint(playerCamera.position);

                segments[2].SetLevelSegmentLocation(new Vector3(0, 0, 0));
                segments[3].SetLevelSegmentLocation(new Vector3(0, 30, 0));

                segments[0].SetLevelSegmentLocation(new Vector3(0, 60, 0));
                segments[0].ResetLevelSegment();
                segments[1].SetLevelSegmentLocation(new Vector3(0, 90, 0));
                segments[1].ResetLevelSegment();

                segmentSwapIndex = 1;
            }
            else if(segmentSwapIndex == 1)
            {
                SpiderLocalPositionInSegment = segments[0].transform.InverseTransformPoint(spider.position);
                CameraLocalPositionInSegment = segments[0].transform.InverseTransformPoint(playerCamera.position);
            
                segments[0].SetLevelSegmentLocation(new Vector3(0, 0, 0));
                segments[1].SetLevelSegmentLocation(new Vector3(0, 30, 0));

                segments[2].SetLevelSegmentLocation(new Vector3(0, 60, 0));
                segments[2].ResetLevelSegment();
                segments[3].SetLevelSegmentLocation(new Vector3(0, 90, 0));
                segments[3].ResetLevelSegment();

                segmentSwapIndex = 0;
            }


            //Take Segment 2 and 3, and move them to the bottom.
            //When player reaches again 75+ Move Segments 0 and 1 back to the bottom again

            spider.position = SpiderLocalPositionInSegment;

            CameraLocalPositionInSegment.z = -10;
            playerCamera.position = CameraLocalPositionInSegment;

            water.SetYPosition(spider.position.y - water.playerToWaterDistance);
        }
    }
}
