using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

[CreateAssetMenu(fileName = "NewSpiderProfile", menuName = "Spider/SpiderProfile", order = 1)]
public class SpiderProfile : ScriptableObject
{
    public string SpiderProfileName;

    [Tooltip("Sprite used for the spider preview on selection. Will be used aswell for the first sprite in the creation of the instance")]
    public Sprite SpiderPreviewSprite;
    public AnimatorController SpiderProfileAnimator;

    public Sprite GetSpriderPreviewSprite() 
    {
        return SpiderPreviewSprite;
    }

    public AnimatorController GetSpiderProfileAnimator()
    {
        return SpiderProfileAnimator;
    }
}
