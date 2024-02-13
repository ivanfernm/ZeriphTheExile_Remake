using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LoadScreenManager : MonoBehaviour
{
    public static LoadScreenManager instance;
    
    public GameObject loadScreenObject;
    public Text loadingText;
    public Image loadingImage;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Call this method to load a new scene
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneCoroutine(sceneName));
    }

    IEnumerator LoadSceneCoroutine(string sceneName)
    {
        loadScreenObject.SetActive(true); // Show load screen

        //change the image fill as the scene loads
        loadingImage.fillAmount = 0;
        
        // Optional: fake loading time
        for (float i = 0; i < 1; i += Time.deltaTime)
        {
            loadingImage.fillAmount = i;
            yield return null;
        }


        // Optional: fake load time for demonstration
        yield return new WaitForSeconds(2);

        // Load the scene
        SceneManager.LoadScene(sceneName);

        // Optional: hide load screen after a delay
        // yield return new WaitForSeconds(1);
        loadScreenObject.SetActive(false); // Hide load screen
    }

    //todo: Add Async load method, And Add the animation of the loading screen 
    void Start()
    {
        // Optionally hide the load screen at the start
        loadScreenObject.SetActive(false);
    }
}