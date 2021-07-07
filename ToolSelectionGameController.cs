using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ToolSelectionGameController : MonoBehaviour
{
    public int maxSelectionLimit;

    public int currentPickedCount;

    public GameObject correctGroup;
    public GameObject wrongGroup;

    public GameObject resultStarGroup;

    public int star;

    public Text starText;

    public int correctCount;

    public int wrongCount;

    public GameObject star1;
    public GameObject star2;
    public GameObject star3;

    public Image starBG;

    public Sprite starBG1Sprite;
    public Sprite starBG2Sprite;
    public Sprite starBG3Sprite;

    public GameObject[] correctObjects;

    int oldStar;

    public GameObject effectObject;

    public GameObject helpPanel;

    int help = 0;

    public bool isAllowMultipleSelect;

    int playCount = 0;

    public GameObject nohelp;

    public GameObject wrong1;
    public GameObject wrong2;
    public GameObject wrong3;

    public AudioSource fxSource;
    public AudioClip correctSound;
    public AudioClip wrongSound;
    public AudioClip endSound;
    public AudioClip helpSound;
    public AudioClip pushSound;

    public string nextSceneName;

    //public int calculateStarMethod;

    public void Start()
    {
        //set text by user stars
        oldStar = PlayerPrefs.GetInt("totalstar", 0);
        starText.text = oldStar.ToString();
    }

    public void showHelpPanel()
    {
        if (help == 0)
        {
            helpPanel.SetActive(true);
        }
        else
        {
            nohelp.SetActive(true);
        }
    }

    public void getHelp()
    {
        if (oldStar >= 5)
        {
            fxSource.PlayOneShot(helpSound);
            help++;
            oldStar -= 5;
            starText.text = oldStar.ToString();
            PlayerPrefs.SetInt("totalstar", oldStar);
            helpPanel.SetActive(false);

            effectObject.SetActive(true);

            //Random correct choice for help 
            int random = Random.Range(0, correctObjects.Length);
            effectObject.transform.position = correctObjects[random].transform.position;

            //Random no choice is no select
            while (correctObjects[random].GetComponent<ToolItems>().isSelected)
            {
                random = Random.Range(0, correctObjects.Length);
                effectObject.transform.position = correctObjects[random].transform.position;
            }
            StartCoroutine(waitHideHelp());
        }

    }

    IEnumerator waitHideHelp()
    {
        yield return new WaitForSeconds(2.0f);
        effectObject.SetActive(false); //hint -> can select now
    }

    public bool checkCanSelectMoreTool()
    {
        if (currentPickedCount < maxSelectionLimit)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public void userPickedTool(bool isCorrect)
    {
        currentPickedCount++;

        //Allow multiple select mean clothes game
        if (isAllowMultipleSelect)
        {
            if (isCorrect == true)
            {
                correctCount++;
            }
            else
            {
                wrongCount++;
            }

            if (checkCanSelectMoreTool() == false || correctCount == correctObjects.Length)
            {
                if (correctCount >= correctObjects.Length)
                {
                    correctGroup.SetActive(true);
                    fxSource.PlayOneShot(correctSound);
                }
                else
                {
                    fxSource.PlayOneShot(wrongSound);
                    wrongGroup.SetActive(true);

                    if (correctCount == 1)
                    {
                        wrong2.SetActive(true);
                        wrong1.SetActive(false);
                    }
                    else if (correctCount == 2)
                    {
                        wrong1.SetActive(true);
                        wrong2.SetActive(false);
                    }
                }
            }
        }
        else
        {
            if (isCorrect == true)
            {
                fxSource.PlayOneShot(correctSound);
                correctGroup.SetActive(true);
                correctCount++;
            }
            else
            {
                wrongGroup.SetActive(true);
                fxSource.PlayOneShot(wrongSound);
                wrongCount++;
            }
            if (checkCanSelectMoreTool() == false || correctCount == correctObjects.Length)
            {
                calculateStar();
            }
        }

    }

    //for phatad
    public void calculateStar()
    {
        star = 3 - wrongCount;

        //Debug.LogError("You got " + star + " Stars");
      
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

        FindObjectOfType<Database>().CallUpdateUser();
    }

    public void resetGame()
    {
        // reset the game
        currentPickedCount = 0;
        wrongCount = 0;
        correctCount = 0;

        //reset the tools (no limit for reset)
        foreach (ToolItems toolItems in FindObjectsOfType<ToolItems>())
        {
            toolItems.Reset();
        }
        playCount++;
    }

    //for clothes virus
    public void calculateStar2()
    {
        if (playCount == 0)
        {
            star = 3;
        }
        else if (playCount == 1)
        {
            star = 2;
        }
        else if (playCount == 2)
        {
            star = 1;
        }
        else
        {
            star = 0;
        }

        //Debug.LogError("You got " + star + " Stars " + wrongCount);

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

        //use to update star
        FindObjectOfType<Database>().CallUpdateUser();

    }

    public void userUnPickedTool()
    {
        currentPickedCount--;
    }

    //for nurse
    public void calculateStar3()
    {
        star = 4 - wrongCount;

        Debug.LogError("You got " + star + " Stars");
        //starText.text = star.ToString();

        resultStarGroup.SetActive(true);
        fxSource.PlayOneShot(endSound);

        if (star == 4)
        {
            star1.SetActive(true);
            star2.SetActive(true);
            star3.SetActive(true);
            starBG.sprite = starBG3Sprite;
        }

        if (star == 2 || star == 3)
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

        FindObjectOfType<Database>().CallUpdateUser();
    }

    public void goGame1_phatad()
    {
        fxSource.PlayOneShot(pushSound);
        SceneManager.LoadScene("1_quiz01Scene");
    }

    public void goGame1_clothes()
    {
        fxSource.PlayOneShot(pushSound);
        SceneManager.LoadScene("1_quiz02Scene");
    }

    public void goGame1_covid()
    {
        fxSource.PlayOneShot(pushSound);
        SceneManager.LoadScene("1_quiz03Scene");
    }

    public void goGame1_nurse()
    {
        fxSource.PlayOneShot(pushSound);
        SceneManager.LoadScene("1_quiz04Scene");
    }

    public void goBackToMenu()
    {
        fxSource.PlayOneShot(pushSound);
        SceneManager.LoadScene("MenuScene");
    }

    public void goGame2()
    {
        fxSource.PlayOneShot(pushSound);
        SceneManager.LoadScene("2_quiz04Scene");
    }
}
