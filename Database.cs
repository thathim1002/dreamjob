using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Database : MonoBehaviour
{
    public string urlUpdateUser;
    public string urlUpdateUserLike;
    public string urlAddUser;
    public string urlChangeUser;
    public string urlLogin;

   

    private void Start()
    {
        urlUpdateUser = "http://Thathims-MacBook-Pro.local/jobgamephp/updateuser.php";
        urlUpdateUserLike = "http://Thathims-MacBook-Pro.local/jobgamephp/updateuserlike.php";
        urlAddUser = "http://Thathims-MacBook-Pro.local/jobgamephp/adduser.php";
        urlChangeUser = "http://Thathims-MacBook-Pro.local/jobgamephp/changeuser.php";
        urlLogin = "http://Thathims-MacBook-Pro.local/jobgamephp/login.php";
        BGMSetting();
    }

    public void BGMSetting()
    {
        string isOn = PlayerPrefs.GetString("BGMon");

        if(GameObject.Find("Main Camera") != null)
        {
            if(isOn == "false")
            {
                GameObject.Find("Main Camera").GetComponent<AudioSource>().clip = null;
            }
            else
            {
               // FindObjectOfType<AudioSource>().clip = null;
            }

            float volumn = PlayerPrefs.GetFloat("volumn");
           // Debug.LogError(Mathf.Log10(volumn));

            GameObject.Find("Main Camera").GetComponent<AudioSource>().volume = 0.5f+(Mathf.Log10(volumn)/2.2f);
            GameObject.Find("AudioSourceForFX").GetComponent<AudioSource>().volume = 1;// 0.5f + (Mathf.Log10(volumn)/2.2f);
        }
    }

    public void CallUpdateUser()
    {
        StartCoroutine(UpdateUser());
    }

    public void CallUpdateUserLike()
    {
        StartCoroutine(UpdateUserLike());
    }

    public void CallChangeUser(string username,string password)
    {
        StartCoroutine(ChangeUser(username,password));
    }

    public void CallAddUser(string password)
    {
        StartCoroutine(AddUser(password));
    }

    public void CallLogin(string username,string password , LoginScript.loginCallback callback)
    {
        StartCoroutine(Login(username,password,callback));
    }

    public IEnumerator UpdateUser()
    {
        // PlayerPrefs is database in unity
        int newStar = PlayerPrefs.GetInt("totalstar");
        string username = PlayerPrefs.GetString("username");
        string password = PlayerPrefs.GetString("password");

        // WWW type for web
        WWWForm form = new WWWForm();
        form.AddField("star", newStar);
        form.AddField("username", username);
        form.AddField("password", password);

        //Debug.LogError("star " + newStar + " " + username );

        WWW www = new WWW(urlUpdateUser,form);

        yield return www;
        if (www.error != null)
        {
            print("There was an error: " + www.error + " " + urlUpdateUser);
        }
        else
        {
            
        }
    }

    public IEnumerator UpdateUserLike()
    {
        // PlayerPrefs is database in unity
        string likejob = "";
        likejob += PlayerPrefs.GetString("like01","false")+",";
        likejob += PlayerPrefs.GetString("like02", "false") + ",";
        likejob += PlayerPrefs.GetString("like03", "false") + ",";
        likejob += PlayerPrefs.GetString("like04", "false") + ",";
        likejob += PlayerPrefs.GetString("like05", "false") + ",";
        likejob += PlayerPrefs.GetString("like06", "false");

        string username = PlayerPrefs.GetString("username");
        string password = PlayerPrefs.GetString("password");

        // WWW type for web
        WWWForm form = new WWWForm();
        form.AddField("likejob", likejob);
        form.AddField("username", username);
        form.AddField("password", password);

        //Debug.LogError("star " + newStar + " " + username );

        WWW www = new WWW(urlUpdateUserLike, form);

        yield return www;
        if (www.error != null)
        {
            print("There was an error: " + www.error + " " + urlUpdateUserLike);
        }
        else
        {
            print("There was a success: " + www.text + " " + urlUpdateUserLike);
        }
    }

    public IEnumerator AddUser(string password)
    {
        string username = PlayerPrefs.GetString("username");
        string birthDate = PlayerPrefs.GetString("birthdate","01/01/2500");
        string grade = PlayerPrefs.GetString("grade");
        string school = PlayerPrefs.GetString("school");

        PlayerPrefs.SetString("password", password);

        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("birthDate", birthDate);
        form.AddField("grade", grade);
        form.AddField("school", school);
        form.AddField("password", password);

        WWW www = new WWW(urlAddUser, form);

        yield return www;
        if (www.error != null)
        {
            print("There was an error: " + www.error + " " + urlAddUser);
        }
        else
        {
            Debug.LogError(www.text);
        }
    }

    public IEnumerator ChangeUser(string newUsername,string newPassword)
    {
        string username = PlayerPrefs.GetString("username");
        string password = PlayerPrefs.GetString("password");
        string birthDate = PlayerPrefs.GetString("birthdate", "01/01/2500");
        string grade = PlayerPrefs.GetString("grade");
        string school = PlayerPrefs.GetString("school");


        WWWForm form = new WWWForm();
        form.AddField("newusername", newUsername);
        form.AddField("username", username);
        form.AddField("birthDate", birthDate);
        form.AddField("grade", grade);
        form.AddField("school", school);
        form.AddField("newpassword", newPassword);
        form.AddField("password", password);

        PlayerPrefs.SetString("password", newPassword);

        WWW www = new WWW(urlChangeUser, form);

        yield return www;
        if (www.error != null)
        {
            print("There was an error: " + www.error + " " + urlChangeUser);
        }
        else
        {
            //Debug.LogError(www.text);
        }
    }

    public IEnumerator Login(string username,string password,LoginScript.loginCallback callback)
    {

        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);

        PlayerPrefs.SetString("password", password);

        WWW www = new WWW(urlLogin, form);

        yield return www;
        if (www.error != null)
        {
            print("There was an error: " + www.error + " " + urlAddUser);
        }
        else
        {
            print(www.text);

            if (www.text.Contains("success"))
            {
                PlayerPrefs.SetString("username", username);
                string star = www.text.Split(':')[2];
                string likeString = www.text.Split(':')[1];

                string[] likeArray = likeString.Split(',');
                for(int i = 1; i <= 6; i++)
                {
                    PlayerPrefs.SetString("like0" + i, likeArray[i - 1]);
                }

                PlayerPrefs.SetInt("totalstar",int.Parse(star));
                callback(true);
            }
            else if (www.text.Contains("error"))
            {
                callback(false);
            }
        }


    }

    public void logout()
    {
        PlayerPrefs.DeleteAll();
        UnityEngine.SceneManagement.SceneManager.LoadScene("UsernameScene");
    }

}
