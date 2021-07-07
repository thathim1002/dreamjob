using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolItems : MonoBehaviour
{

    public bool isSelected;

    public Sprite unSelectedSprite;

    public Sprite selectedSprite;

    //Reference to tool image in game
    public Image toolImage;

    public ToolSelectionGameController controller;

    public bool isCorrectTool;

    public int row;

    //check if can click
    //public bool isFinished;

    //for reset game
    public void Reset()
    {
        isSelected = false;
        toolImage.sprite = unSelectedSprite;
        toolImage.SetNativeSize();
    }

    public void userPressedOnThisTool()
    {
        //check if isAllow = true -> user can select more
        bool isAllow = controller.checkCanSelectMoreTool();

        if (isAllow == true)
        {
            //Debug.LogError("You can select");

            //Swap Boolean
            isSelected = !isSelected;

            //Swap image
            if (isSelected == true)
            {
                toolImage.sprite = selectedSprite;
                controller.userPickedTool(isCorrectTool);
            }
            else
            {
                toolImage.sprite = unSelectedSprite;
                controller.currentPickedCount--;

                if (isCorrectTool)
                {
                    controller.correctCount--;
                }
            }
            toolImage.SetNativeSize();
        }

    }

    public void userPressedOnThisToolCovid()
    {
        //check if isAllow = true -> user can select more
        bool isAllow = ((CovidSelection)controller).checkCanSelectMoreTool(row);

        if (isAllow == true)
        {
            //Debug.LogError("You can select");

            //Swap Boolean
            isSelected = !isSelected;

            //Swap image
            if (isSelected == true)
            {
                toolImage.sprite = selectedSprite;
                ((CovidSelection)controller).userPickedTool(isCorrectTool,row);
                //controller.userPickedTool(isCorrectTool);
            }
            else
            {
                toolImage.sprite = unSelectedSprite;
                controller.currentPickedCount--;

                if (isCorrectTool)
                {
                    controller.correctCount--;
                }
            }
            toolImage.SetNativeSize();
        }
        else if(isSelected)
        {
            isSelected = false;
            toolImage.sprite = unSelectedSprite;
            controller.currentPickedCount--;
            toolImage.SetNativeSize();
            ((CovidSelection)controller).clearRow(row);
            if (isCorrectTool)
            {
                controller.correctCount--;
            }
        }

    }

}