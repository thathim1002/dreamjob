using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class JobsGameScript : MonoBehaviour
{
    public GameObject block;

    public GameObject wrongGroup;
    public GameObject correctGroup;

    public int wrongCount;

    public GameObject resultStarGroup;

    public AudioSource fxSource;
    public AudioClip correctSound;
    public AudioClip wrongSound;
    public AudioClip endSound;
    public AudioClip pushSound;

    public GameObject star1;
    public GameObject star2;
    public GameObject star3;

    public Image starBG;

    public Sprite starBG1Sprite;
    public Sprite starBG2Sprite;
    public Sprite starBG3Sprite;

    public Text starText;
    public Text starText2;
    public GameObject likeObject;

    int oldStar;

    public GameObject oldCanvas;
    public GameObject newCanvas;

    public string nextSceneName;

    public void Start()
    {
        oldStar = PlayerPrefs.GetInt("totalstar", 0);
        starText.text = oldStar.ToString();
        if(starText2 != null)
        {
            starText2.text = oldStar.ToString();
        }
    }

    public void userAnswer(bool isCorrect)
    {
        block.SetActive(true);
        StartCoroutine(waitShowResult(isCorrect));
    }

    public void userAnswerJob(bool isCorrect)
    {
        block.SetActive(true);
        StartCoroutine(waitShowResult(isCorrect));
    }

    IEnumerator waitShowResult(bool isCorrect)
    {
        yield return new WaitForSeconds(1.0f);

        if (isCorrect == true)
        {
            //Debug.LogError("You correct");
            fxSource.PlayOneShot(correctSound);
            correctGroup.SetActive(true);
        }
        else if (isCorrect == false)
        {
            block.SetActive(false);
            fxSource.PlayOneShot(wrongSound);
            wrongCount++;
            //Debug.LogError("You not correct");
            wrongGroup.SetActive(true);
        }
    }

    public void selectedCorrectJob()
    {
        oldCanvas.SetActive(false);
        newCanvas.SetActive(true);
    }

    public void selectedLike(string likeJob)
    {
        if (likeJob != "")
        {
            PlayerPrefs.SetString(likeJob,"true");
        }

        FindObjectOfType<Database>().CallUpdateUserLike();

        calculateStar();
    }

    public void calculateStar()
    {
        int star = 3 - wrongCount;

        resultStarGroup.SetActive(true);
        fxSource.PlayOneShot(endSound);

        if (star == 3)
        {
            star1.SetActive(true);
            star2.SetActive(true);
            star3.SetActive(true);
            starBG.sprite = starBG3Sprite;
        }

        if (star == 2)
        {
            star1.SetActive(true);
            star2.SetActive(true);
            starBG.sprite = starBG2Sprite;
        }

        if (star == 1)
        {
            star1.SetActive(true);
            starBG.sprite = starBG1Sprite;
        }

        if (star == 0)
        {
            starBG.sprite = starBG1Sprite;
        }

        //save star to database
        oldStar = PlayerPrefs.GetInt("totalstar", 0);
        PlayerPrefs.SetInt("totalstar", oldStar + star);
        if (starText2 != null)
        {
            starText2.text = (oldStar+star).ToString();
        }
    }

    public void goGame3_pharma()
    {
        fxSource.PlayOneShot(pushSound);
        SceneManager.LoadScene("3_quiz01Scene");
    }

    public void goGame3_dentist()
    {
        fxSource.PlayOneShot(pushSound);
        SceneManager.LoadScene("3_quiz02Scene");
    }

    public void goGame3_veter()
    {
        fxSource.PlayOneShot(pushSound);
        SceneManager.LoadScene("3_quiz03Scene");
    }

    public void goGame3_nurse()
    {
        fxSource.PlayOneShot(pushSound);
        SceneManager.LoadScene("3_quiz04Scene");
    }

    public void goGame3_doctor()
    {
        fxSource.PlayOneShot(pushSound);
        SceneManager.LoadScene("3_quiz05Scene");
    }

    public void goBackToMenu()
    {
        fxSource.PlayOneShot(pushSound);
        SceneManager.LoadScene("MenuScene");
    }

    public void goToNextScene()
    {
        fxSource.PlayOneShot(pushSound);
        SceneManager.LoadScene(nextSceneName);
    }
}
