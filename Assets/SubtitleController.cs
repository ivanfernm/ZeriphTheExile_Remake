using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

public class SubtitleController : MonoBehaviour
{
    public TextMeshProUGUI subtitleText;
    public SubtitleData subtitleData; // This will hold all your subtitles

    public string filePath = "subtitles.json"; // Adjust the path as necessary
    private int currentSubtitleIndex = 0;
    
    void Start()
    {
        LoadSubtitlesFromFile(filePath); // Adjust the path as necessary
    }

  void Update()
    {
        if (currentSubtitleIndex < subtitleData.subtitles.Length)
        {
            Subtitle currentSubtitle = subtitleData.subtitles[currentSubtitleIndex];
            
            if (Time.time >= currentSubtitle.startTime && Time.time <= currentSubtitle.endTime)
            {
                subtitleText.text = currentSubtitle.text;
            }
            else if (Time.time > currentSubtitle.endTime)
            {
                subtitleText.text = "";
                currentSubtitleIndex++;
            }
        }
    }
  
  [ContextMenu("LoadSubtitle")]
    public void LoadSubtitlesFromFile(string filePath)
    {
        string fullPath = Path.Combine(Application.dataPath, filePath);

        if (File.Exists(fullPath))
        {
            string dataAsJson = File.ReadAllText(fullPath);
            subtitleData = JsonUtility.FromJson<SubtitleData>(dataAsJson);
            currentSubtitleIndex = 0;
        }
        else
        {
            Debug.LogError("Cannot find file at " + fullPath);
        }
    }

    public void LoadSubtitles(SubtitleData data)
    {
        subtitleData = data;
        currentSubtitleIndex = 0;
    }
    
    public void DisplayNextSubtitle()
    {
        if (currentSubtitleIndex < subtitleData.subtitles.Length)
        {
            var subtitle = subtitleData.subtitles[currentSubtitleIndex++];
            subtitleText.text = subtitle.text;

            // Optionally, schedule hiding the subtitle
            StartCoroutine(HideSubtitleAfterDelay(subtitle.endTime - subtitle.startTime));
        }
    }

    private IEnumerator HideSubtitleAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        subtitleText.text = ""; // Clears the text after the specified delay
    }
}
