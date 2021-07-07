using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JobItems : MonoBehaviour
{
    public bool isSelected;
    //Sprite is datatype for image

    public Sprite unSelectedSprite;

    //Selected Sprite use to replace when selected
    public Sprite selectedSprite;

    //Reference to tool image in game
    public Image toolImage;

    //Is this tool correct
    public bool isCorrectTool;

    //check if can click
    public bool isFinished;



    //for reset game
    public void Reset()
    {
        isSelected = false;
        toolImage.sprite = unSelectedSprite;
        toolImage.SetNativeSize();
    }

    public void userPressedOnThisTool()
    {
        if(isSelected == true)
        {
            return;
        }

        //Swap Boolean
        isSelected = true;

            //Swap image
            if (isSelected == true)
            {
                toolImage.sprite = selectedSprite;
            }
            else
            {
               // toolImage.sprite = unSelectedSprite;
            }

            toolImage.SetNativeSize();
            //isFinished = true;

        FindObjectOfType<JobsGameScript>().userAnswer(isCorrectTool);
    }


    public void userPressedOnThisJob()
    {
        if (isSelected == true)
        {
            return;
        }

        //Swap Boolean
        isSelected = true;

        //Swap image
        if (isSelected == true)
        {
            toolImage.sprite = selectedSprite;
        }
        else
        {
            // toolImage.sprite = unSelectedSprite;
        }

        toolImage.SetNativeSize();
        //isFinished = true;

        FindObjectOfType<JobsGameScript>().userAnswerJob(isCorrectTool);
    }
}
