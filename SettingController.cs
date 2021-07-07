using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SettingController : MonoBehaviour
{
    public Toggle BGMToggle;
    public Slider volumnSlider;

    private void OnEnable()
    {
        if (PlayerPrefs.GetString("BGMon") == "false")
        {
            BGMToggle.isOn = false;
        }

        float volumn = PlayerPrefs.GetFloat("volumn");
        volumnSlider.value = volumn;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void setBGM()
    {
        if (BGMToggle.isOn)
        {
            PlayerPrefs.SetString("BGMon","true");
        }
        else
        {
            PlayerPrefs.SetString("BGMon", "false");
        }

        //volumn
        float volumnSliderValue = volumnSlider.value;
        PlayerPrefs.SetFloat("volumn",volumnSliderValue);

        FindObjectOfType<Database>().BGMSetting();
        UnityEngine.SceneManagement.SceneManager.LoadScene("MenuScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
