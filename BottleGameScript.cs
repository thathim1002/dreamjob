using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BottleGameScript : MonoBehaviour
{
    public Animator bottleAnimator;
    public GameObject block;
    public GameObject pill;

    public List<Sprite> questionSprites;
    public List<AudioClip> questionSound;
    public Image questionImage;
    int questionId;
    public List<GameObject> yesNo;
    public List<AudioClip> yesnoSound;

    public List<GameObject> miniPills;

    public List<int> questionPools;
    public List<int> usedPools;

    public Text leftText;

    public Text starText;
    int oldStar;

    public GameObject bt_shake;
    public GameObject bt_tailei;

    public List<GameObject> hints;

    public string nextSceneName;

    public AudioSource fxSource;
    public AudioClip shakeSound;
    public AudioClip pushSound;

    public GameObject trick;

    public void Start()
    {
        usedPools = new List<int>();

        //set text by user stars
        oldStar = PlayerPrefs.GetInt("totalstar", 0);
        starText.text = oldStar.ToString();
    }

    //shake bottle (max is 5 time)
    public void shakeBottle()
    {
        if(usedPools.Count < 5)
        {
            if (trick != null)
            {
                trick.SetActive(false);
            }
            block.SetActive(true);
            bottleAnimator.Play("BottleShake");
            fxSource.PlayOneShot(shakeSound);
            StartCoroutine(waitBottleEndShake());
        }
    }

    //wait for bottle to stop shaking
    IEnumerator waitBottleEndShake()
    {
        yield return new WaitForSeconds(2.0f);
        bottleAnimator.Play("Normal");
        yield return new WaitForSeconds(1.0f);
        bottleAnimator.Play("BottleFade");
        yield return new WaitForSeconds(1.5f);
        pill.SetActive(true);

        //random question 
        questionId = Random.Range(0, questionPools.Count);

        while (usedPools.Contains(questionId))
        {
            questionId = Random.Range(0, questionPools.Count);
            //fxSource.PlayOneShot(questionSound[questionId]);
        }

        usedPools.Add(questionId);
        questionImage.sprite = questionSprites[questionId];
        fxSource.PlayOneShot(questionSound[questionId]);
       

    }

    //click for show yes or no.
    public void paperClick()
    {
        fxSource.PlayOneShot(yesnoSound[questionId]);
        yesNo[questionId].SetActive(true);
        StartCoroutine(waitCollectPill());
    }

    //hint mini pill
    public void hintClick(int index)
    {
        //Debug.LogError(index);
        hints[index].SetActive(true);
    }


    IEnumerator waitCollectPill()
    {
        yield return new WaitForSeconds(1.0f);
        pill.GetComponent<Animator>().Play("PillClose");
        yield return new WaitForSeconds(2.0f);
        miniPills[questionId].SetActive(true);
        pill.SetActive(false);
        bottleAnimator.Play("Normal");
        block.SetActive(false);

        for (int i = 0; i < yesNo.Count; i++)
        {
            yesNo[questionId].SetActive(false);
        }

        //set count random text = 5 4 3 2 1 0 
        leftText.text = (questionPools.Count-usedPools.Count).ToString();

        if(usedPools.Count == 5)
        {
            bt_shake.SetActive(false);
           
        }

        bt_tailei.SetActive(true);
    }

    public void goToNextScene()
    {
        fxSource.PlayOneShot(pushSound);
        SceneManager.LoadScene(nextSceneName);
    }

    public void goGame2_medicine()
    {
        fxSource.PlayOneShot(pushSound);
        SceneManager.LoadScene("2_quiz03Scene");
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

    public void GoToLike01()
    {
        fxSource.PlayOneShot(pushSound);
        SceneManager.LoadScene("3_like01Scene");
    }

    public void goBackToMenu()
    {
        fxSource.PlayOneShot(pushSound);
        SceneManager.LoadScene("MenuScene");
    }
}