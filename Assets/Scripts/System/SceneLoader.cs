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
    [SerializeField] private bool skip;
    [SerializeField] private int amount;
    //[SerializeField] private GameObject img;

    public void LoadLevel()
    {
        //SceneManager.LoadScene(sceneId);
        //settings?.Unpause();
        StartCoroutine(ClipTimer());
    }

    IEnumerator ClipTimer()
    {
        settings?.Unpause();
        if (videoPlayer != null)
        {
            videoPlayer.Play();
            for (int i = 0; i < amount; i++)
            {
                yield return new WaitForSeconds(1f);
                if (skip)
                {
                    break;
                }
            }

        }
        SceneManager.LoadScene(sceneId);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            skip = true;
        }
    }
}