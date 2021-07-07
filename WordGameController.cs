using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WordGameController : MonoBehaviour
{
    //max count 
    public int maxLetterNeed;

    //count correct answer
    public int correctLetterCount;

    //count wrong answer
    public int incorrectCount;

    public Text starText;

    public GameObject star1;
    public GameObject star2;
    public GameObject star3;

    public Image starBG;

    public Sprite starBG1Sprite;
    public Sprite starBG2Sprite;
    public Sprite starBG3Sprite;

    public GameObject resultStarGroup;

    public AudioSource fxSource;
    public AudioClip correctSound;
    public AudioClip endSound;

    //hint section
    public Image hintImage;
    public List<Sprite> hintImagesSet;
    public List<AudioClip> hintImagesSound;
    public int hintIndex;

    int help = 0;
    public GameObject helpPanel;
    public GameObject nohelp;
    int oldStar;

    public List<GameObject> effectObjects;

    public List<int> answered;

    public List<Sprite> happyFaceSprites;
    public List<Sprite> sadFaceSprites;

    public Image doctorImage;

    public GameObject meaningGroup;

    public int score;

    public AudioClip helpSound;
    public AudioClip pushSound;


    public void Start()
    {
        //num of selected
        answered = new List<int>();

        //set text by user stars
        oldStar = PlayerPrefs.GetInt("totalstar", 0);
        starText.text = oldStar.ToString();

        StartCoroutine(runHint());
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

    public void getHelp2()
    {
        if (oldStar >= 5)
        {
            fxSource.PlayOneShot(helpSound);
            help++;
            oldStar -= 5;
            starText.text = oldStar.ToString();
            PlayerPrefs.SetInt("totalstar", oldStar);
            helpPanel.SetActive(false);

            for(int i = 0; i < maxLetterNeed; i++)
            {
                if(answered.Contains(i) == false)
                {
                    effectObjects[i].SetActive(true);
                    break; //1 hint
                }
            }

           // effectObject1.SetActive(true);
            StartCoroutine(waitHideHelp());
        }

    }

    public void getHelp3()
    {
        if (oldStar >= 10)
        {
            fxSource.PlayOneShot(helpSound);
            help++;
            oldStar -= 10;
            starText.text = oldStar.ToString();
            PlayerPrefs.SetInt("totalstar", oldStar);
            helpPanel.SetActive(false);

            int count = 0;

            for (int i = 0; i < maxLetterNeed; i++)
            {
                if (answered.Contains(i) == false)
                {
                    effectObjects[i].SetActive(true);
                    count++;

                    if(count == 2)
                    {
                        break; //2 hint
                    }
                }
            }
            StartCoroutine(waitHideHelp());
        }

    }

    IEnumerator waitHideHelp()
    {
        yield return new WaitForSeconds(2.0f);

        //stop hint
        for (int i = 0; i < maxLetterNeed; i++)
        {
            effectObjects[i].SetActive(false);
        }

    }

    public void showHappyFace()
    {
        int face = Random.Range(0, happyFaceSprites.Count);
        doctorImage.sprite = happyFaceSprites[face];
    }

    public void showSadFace()
    {
        int face = Random.Range(0, sadFaceSprites.Count);
        doctorImage.sprite = sadFaceSprites[face];
    }

    public void addScore(int index)
    {
        answered.Add(index);
        correctLetterCount++;

        //show doctor happy face
        showHappyFace();

        //play correct sound
        if(fxSource != null)
        {
            fxSource.PlayOneShot(correctSound);
        }

        //select finish 
        if(maxLetterNeed == correctLetterCount)
        {

            meaningGroup.SetActive(true);

            fxSource.PlayOneShot(endSound);

            resultStarGroup.SetActive(true);

            int star = 0;

            if (incorrectCount == 0)
            {
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(true);
                starBG.sprite = starBG3Sprite;
                star = 3;
            }

            else if (incorrectCount <= 3)
            {
                star1.SetActive(true);
                star2.SetActive(true);
                starBG.sprite = starBG2Sprite;
                star = 2;
            }
            else if (incorrectCount <= 6)
            {
                star1.SetActive(true);
                starBG.sprite = starBG1Sprite;
                star = 1;
            }
            else
            {
                star = 0;
            }

            //starText.text = star.ToString();
            //save star to database
            int oldStar = PlayerPrefs.GetInt("totalstar", 0);
            PlayerPrefs.SetInt("totalstar", oldStar + star);

            //Debug.LogError("Game Ended you got " + star);

        }
    }

    //increase wrong count 
    public void incorrect()
    {
        showSadFace();
        incorrectCount++;
    }

    //hint quiz
    IEnumerator runHint()
    {
        hintIndex = 0;

        if (hintIndex < hintImagesSound.Count)
        {
            fxSource.PlayOneShot(hintImagesSound[hintIndex]);
        }

        hintImage.sprite = hintImagesSet[hintIndex];
        hintImage.GetComponent<Animator>().Play("HintAnimation");
        hintImage.SetNativeSize();

        
        yield return new WaitForSeconds(6.0f);
        hintIndex++;

        if (hintIndex < hintImagesSound.Count)
        {
            fxSource.PlayOneShot(hintImagesSound[hintIndex]);
        }

        hintImage.sprite = hintImagesSet[hintIndex];
        hintImage.GetComponent<Animator>().Play("HintAnimation");
        hintImage.SetNativeSize();

       


        yield return new WaitForSeconds(6.0f);
        hintIndex++;

        if (hintIndex < hintImagesSound.Count)
        {
            fxSource.PlayOneShot(hintImagesSound[hintIndex]);
        }

        hintImage.sprite = hintImagesSet[hintIndex];
        hintImage.GetComponent<Animator>().Play("HintAnimation");
        hintImage.SetNativeSize();




        yield return new WaitForSeconds(6.0f);
        hintIndex++;
        if (hintIndex < hintImagesSound.Count)
        {
            fxSource.PlayOneShot(hintImagesSound[hintIndex]);
        }
        hintImage.sprite = hintImagesSet[hintIndex];
        hintImage.GetComponent<Animator>().Play("HintAnimation");
        hintImage.SetNativeSize();

        yield return new WaitForSeconds(6.0f);

        StartCoroutine(runHint());

    }

    public void goGame2_ambulance()
    {
        fxSource.PlayOneShot(pushSound);
        SceneManager.LoadScene("2_quiz01Scene");
    }

    public void goGame2_syringe()
    {
        fxSource.PlayOneShot(pushSound);
        SceneManager.LoadScene("2_quiz02Scene");
    }

    public void goGame2_medicine()
    {
        fxSource.PlayOneShot(pushSound);
        SceneManager.LoadScene("2_quiz03Scene");
    }

    public void goGame2_hospital()
    {
        fxSource.PlayOneShot(pushSound);
        SceneManager.LoadScene("2_quiz04Scene");
    }

    public void goGame2_mask()
    {
        fxSource.PlayOneShot(pushSound);
        SceneManager.LoadScene("2_quiz05Scene");
    }

    public void goGame2_nurse()
    {
        fxSource.PlayOneShot(pushSound);
        SceneManager.LoadScene("2_quiz06Scene");
    }


    public void goGame1_covid()
    {
        fxSource.PlayOneShot(pushSound);
        SceneManager.LoadScene("1_quiz03Scene");
    }

    //go to menuscene
    public void goBackToMenu()
    {
        fxSource.PlayOneShot(pushSound);
        SceneManager.LoadScene("MenuScene");
    }
    //go to game 3
    public void goGame3()
    {
        fxSource.PlayOneShot(pushSound);
        SceneManager.LoadScene("3_quiz01Scene");
    }
}