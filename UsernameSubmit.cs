using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UsernameSubmit : MonoBehaviour
{
    string username;
    public InputField usernameBox;
    public InputField schoolBox;
    public InputField passwordBox;

    public Text usernameText;
    public Text schoolText;

    public GameObject HelloGroup;
    public GameObject UsernameGroup;

    public Dropdown dayDropdown;
    public Dropdown monthDropdown;
    public Dropdown yearDropdown;

    public Dropdown gradeDropdown;

    public int day;
    public int month;
    public int year;

    public string grade;
    public string school;

    public GameObject passwordError;

    public AudioClip pushSound;
    public AudioSource fxSource;

    private void Start()
    {
        day = 1;
        month = 1;
        year = 2555;

        addMonth();
        addYear();
        setDayFromMonth();
    }

    public void addMonth()
    {
        monthDropdown.ClearOptions();

        List<string> m_DropOptions = new List<string>();

        for (int i = 1; i <= 12; i++)
        {
            m_DropOptions.Add(i.ToString());
        }

        monthDropdown.AddOptions(m_DropOptions);
    }

    public void addYear()
    {
        yearDropdown.ClearOptions();

        List<string> m_DropOptions = new List<string>();

        for (int i = 2555; i <= 2564; i++)
        {
            m_DropOptions.Add(i.ToString());
        }

        yearDropdown.AddOptions(m_DropOptions);
    }

    public void userSelectDay()
    {
        day = int.Parse(dayDropdown.options[dayDropdown.value].text);
        setFormattedDate();
    }

    public void userSelectMonth()
    {

        month = int.Parse(monthDropdown.options[monthDropdown.value].text);
        //month = monthDropdown.value + 1;

        setDayFromMonth();
    }

    public void userSelectYear()
    {
        year = int.Parse(yearDropdown.options[yearDropdown.value].text);
        setFormattedDate();
    }

    public void setDayFromMonth()
    {
        //Clear the old options of the Dropdown menu
        dayDropdown.ClearOptions();

        List<string> m_DropOptions = new List<string>();

        int maxDay = 31;

        if(month == 1)
        {
            maxDay = 31;
        }
        if (month == 2)
        {
            maxDay = 29;
        }
        if (month == 3)
        {
            maxDay = 31;
        }
        if (month == 4)
        {
            maxDay = 30;
        }
        if (month == 5)
        {
            maxDay = 31;
        }
        if (month == 6)
        {
            maxDay = 30;
        }
        if (month == 7)
        {
            maxDay = 31;
        }
        if (month == 8)
        {
            maxDay = 31;
        }
        if (month == 9)
        {
            maxDay = 30;
        }
        if (month == 10)
        {
            maxDay = 31;
        }
        if (month == 11)
        {
            maxDay = 30;
        }
        if (month == 12)
        {
            maxDay = 31;
        }

        for(int i = 1; i <= maxDay; i++)
        {
            m_DropOptions.Add(i.ToString());
        }

        dayDropdown.AddOptions(m_DropOptions);

        dayDropdown.value = day-1;

        setFormattedDate();
    }

    public void userSelectGrade()
    {
        grade = gradeDropdown.options[gradeDropdown.value].text;
        PlayerPrefs.SetString("grade",grade);
    }


    public void setFormattedDate()
    {
        string date = string.Format("{0:00}/{1:00}/{2:000}", day, month, year);
        PlayerPrefs.SetString("birthdate", date);
    }

    public void TellUSerName()
    {
        passwordError.SetActive(false);

        if (usernameBox.text != "" && schoolBox.text != "")
        {
            int number = 0;
            if (int.TryParse(passwordBox.text, out number))
            {
                username = usernameBox.text;
                school = schoolBox.text;

                HelloGroup.SetActive(true);
                PlayerPrefs.SetString("username", username);
                PlayerPrefs.SetString("school", school);

                FindObjectOfType<Database>().CallAddUser(passwordBox.text);
                StartCoroutine(waitAddUser());
            }
            else
            {
                passwordError.SetActive(true);
                StartCoroutine(waitPassword());

            }

        }

    }

    public void ChangeUserName()
    {
        passwordError.SetActive(false);

        if (usernameBox.text != "" && schoolBox.text != "")
        {
            int number = 0;
            if (int.TryParse(passwordBox.text, out number))
            {
                username = usernameBox.text;
                school = schoolBox.text;

               // HelloGroup.SetActive(true);
                PlayerPrefs.SetString("school", school);

                FindObjectOfType<Database>().CallChangeUser(username, PlayerPrefs.GetString("password"));
                StartCoroutine(waitAddUser());
            }
            else
            {
                passwordError.SetActive(true);
                StartCoroutine(waitPassword());

            }


        }

    }


    public void GoToMenu()
    {
        PlayerPrefs.SetString("username", username);
        FindObjectOfType<Database>().CallAddUser(passwordBox.text);
        fxSource.PlayOneShot(pushSound);
        SceneManager.LoadScene("MenuScene");
    }


    IEnumerator waitPassword()
    {
        yield return new WaitForSeconds(2.0f);
        passwordError.SetActive(false);
    }

    IEnumerator waitAddUser()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerPrefs.SetInt("totalstar",0);
        SceneManager.LoadScene("MenuScene");
    }
}
