using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Subtitle
{
    public string text;
    public float startTime;
    public float endTime;
}
[System.Serializable]
public class SubtitleData
{
    public Subtitle[] subtitles;
}