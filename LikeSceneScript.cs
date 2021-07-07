using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LikeSceneScript : MonoBehaviour
{
    public List<Sprite> unlockSprites;
    public List<Image> jobImages;
    public List<Image> popups;

    int oldStar;
    public Text starText;

    public AudioSource fxSource;
    public AudioClip openSound;
    public AudioClip pushSound;

    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.DeleteAll();
        readData();

        oldStar = PlayerPrefs.GetInt("totalstar", 0);
        starText.text = oldStar.ToString();
    }

    void readData()
    {
        for(int i = 1; i <= unlockSprites.Count; i++)
        {
            string data = PlayerPrefs.GetString("like0"+i,"false");
            if(data == "true")
            {
                jobImages[i-1].sprite = unlockSprites[i-1];
                jobImages[i - 1].GetComponent<Button>().interactable = true;
            }
            else
            {
                jobImages[i - 1].GetComponent<Button>().interactable = false;
            }
        }
    }

    public void openPopup(int index)
    {
        popups[index].gameObject.SetActive(true);
        fxSource.PlayOneShot(openSound);
    }

    public void goBackToMenu()
    {
        fxSource.PlayOneShot(pushSound);
        SceneManager.LoadScene("MenuScene");
    }
}
