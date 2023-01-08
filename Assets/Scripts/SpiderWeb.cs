using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderWeb : MonoBehaviour
{
    public float launchForce = 800;

    public int scoreToGive = 1;
    public bool canLaunch = true;
    public bool IsIndestructable = true;

    AudioSource audioSource;
    public AudioClip[] WebbedClips;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        audioSource.PlayOneShot(GetRandomWebbedAudioClip());
    }

    AudioClip GetRandomWebbedAudioClip()
    {
        int randomJump = Random.Range(0, WebbedClips.Length);

        if (randomJump < WebbedClips.Length)
        {
            if (WebbedClips[randomJump] != null)
            {
                return WebbedClips[randomJump];
            }
        }

        return null;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        SpiderMotor spider = other.gameObject.GetComponent<SpiderMotor>();

        if (spider)
        {
            Debug.Log("Boing");

            if (canLaunch)
            {
                spider.LaunchSpider(Vector2.up, launchForce);
            }

            if (!IsIndestructable)
            {
                //Destroy gameobject for now
                gameObject.SetActive(false);

                GameMode.Instance.AddScore(scoreToGive);
            }
        }
    }
}
