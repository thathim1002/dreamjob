using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginScript : MonoBehaviour
{
    public InputField usernameField;
    public InputField passwordField;
    public Text loginResult;
    public GameObject blockObject;

    public delegate void loginCallback(bool isSuccess);

    public loginCallback m_methodToCall;

    public AudioClip pushSound;
    public AudioSource fxSource;

    private void Start()
    {
        m_methodToCall = loginResultCallback;
    }

    public void login()
    {
        if (usernameField.text == "")
        {
            loginResult.gameObject.SetActive(true);
            loginResult.text = "กรุณาระบุชื่อเล่น";
        }
        else if (passwordField.text == "")
        {
            loginResult.gameObject.SetActive(true);
            loginResult.text = "กรุณาระบุรหัสผ่าน";
        }
        else
        {
            loginResult.gameObject.SetActive(false);
            blockObject.SetActive(true);
            FindObjectOfType<Database>().CallLogin(usernameField.text,passwordField.text,m_methodToCall);
        }

    }

    public void loginResultCallback(bool isSuccess)
    {
        //Debug.LogError("Callback call login result");
        blockObject.SetActive(false);

        if (isSuccess)
        {
            //login passed
            //Debug.LogError("Success");
            fxSource.PlayOneShot(pushSound);
            SceneManager.LoadScene("MenuScene");
            
}
        else if(!isSuccess)
        {
            //login failed
            //Debug.LogError("Failed");
            loginResult.gameObject.SetActive(true);
            loginResult.text = "ชื่อเล่นหรือรหัสผ่านผิด";
        }
    }
}
