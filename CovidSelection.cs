using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CovidSelection : ToolSelectionGameController
{
    public bool firstRowSelected;
    public bool secondRowSelected;
    public bool thirdRowSelected;

    public void userPickedTool(bool isCorrect,int row)
    {
        //save select on row
        if (row == 1)
        {
            firstRowSelected = true;
        }

        if (row == 2)
        {
            secondRowSelected = true;
        }

        if (row == 3)
        {
            thirdRowSelected = true;
        }

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
                        wrong3.SetActive(false);
                    }
                    else if (correctCount == 2)
                    {
                        wrong1.SetActive(true);
                        wrong2.SetActive(false);
                        wrong3.SetActive(false);
                    }
                    else if (correctCount == 0)
                    {
                        wrong1.SetActive(false);
                        wrong2.SetActive(false);
                        wrong3.SetActive(true);
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

    public bool checkCanSelectMoreTool(int row)
    {

        //block same row select
        if (row == 1 && firstRowSelected == true)
        {
            return false;
        }

        if (row == 2 && secondRowSelected == true)
        {
            return false;
        }

        if (row == 3 && thirdRowSelected == true)
        {
            return false;
        }

        if (currentPickedCount < maxSelectionLimit)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public void clearRow(int row)
    {
        if (row == 1)
        {
            firstRowSelected = false;
        }

        if (row == 2)
        {
            secondRowSelected = false;
        }

        if (row == 3)
        {
            thirdRowSelected = false;
        }
    }

    int playCount = 0;
    int oldStar;

    //for clothes covid
    public void calculateStarCovid()
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

}
