using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

[CreateAssetMenu(fileName = "NewSpiderHatProfile", menuName = "Spider/SpiderHatProfile", order = 2)]
public class SpiderHatProfile : ScriptableObject
{
    public string SpiderHatProfileName;

    [Tooltip("Sprite used for hat preview on selection. Will be used aswell for the first sprite in the creation of the instance")]
    public Sprite SpiderHatPreviewSprite;

    [Tooltip("Optional. Only needed if the hat is animated.")]
    public AnimatorController SpiderHatProfileAnimator;

    public Sprite GetSpriderPreviewSprite() 
    {
        return SpiderHatPreviewSprite;
    }

    public AnimatorController GetSpiderProfileAnimator()
    {
        return SpiderHatProfileAnimator;
    }
}
