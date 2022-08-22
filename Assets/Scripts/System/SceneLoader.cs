using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private int sceneId;
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private Settings settings;
    //[SerializeField] private GameObject img;

    public void LoadLevel()
    {
        //SceneManager.LoadScene(sceneId);
        //settings?.Unpause();
        StartCoroutine(ClipTimer());
    }

    IEnumerator ClipTimer()
    {
        if (videoPlayer != null)
        {
            videoPlayer.Play();
            yield return new WaitForSeconds(5f);
        }
        SceneManager.LoadScene(sceneId);

        settings?.Unpause();
    }
}