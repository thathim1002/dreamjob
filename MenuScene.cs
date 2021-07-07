using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuScene : MonoBehaviour
{
    public Text usernameText;

    public AudioSource fxSource;
    public AudioClip pushSound;

    private void Start()
    {
        string username = PlayerPrefs.GetString("username");
        
        usernameText.text = username;
    }

    public void GoToToolsGame()
    {

        fxSource.PlayOneShot(pushSound);
        SceneManager.LoadScene("1_quiz01Scene");
    }

    public void GoToWordsGame()
    {
        fxSource.PlayOneShot(pushSound);
        SceneManager.LoadScene("2_quiz04Scene");
    }

    public void GoToTaiGame()
    {
        fxSource.PlayOneShot(pushSound);
        SceneManager.LoadScene("3_quiz01Scene");
    }

    public void GoToLikeScene()
    {
        fxSource.PlayOneShot(pushSound);
        SceneManager.LoadScene("LikeScene");
    }
}
