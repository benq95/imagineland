using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StoryData", menuName = "ScriptableObjects/StoryDataObject", order = 1)]
public class StoryData : ScriptableObject
{
    public String description;
    public Sprite photo;
    public StoryData nextStoryData;
}
