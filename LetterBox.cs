using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterBox : MonoBehaviour
{
    //Hold this box correct letter
    public string correct_letter;
    public int index;

    bool canCheck;



    public WordGameController controller;

    private void Start()
    {
        controller = FindObjectOfType<WordGameController>();
    }

    //Call when some green collider enter area
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //check if the object enter have Letter tag
        canCheck = true;
        StartCoroutine(waitForCheck(collision));

    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        //check if the object enter have Letter tag
        // Debug.LogError("Stop");
        canCheck = false;

    }

    IEnumerator waitForCheck(Collider2D collision)
    {
        yield return new WaitForSeconds(0.5f);

        if (canCheck == true)
        {
            //Unpack code from letter object
            DragableIcon icon = collision.GetComponent<DragableIcon>();

            if (icon != null)
            {

                //Check if the Letter in object equal to correct one
                if (icon.letter == correct_letter)
                {
                    icon.canDrag = false;
                    //if player correct , shake the boxes
                    transform.parent.GetComponent<Animator>().Play("Shaking");
                    //end shaking boxes


                    //Debug.LogError("Correct");
                    icon.setSpriteCorrect();
                    Destroy(icon);
                    icon.transform.position = this.transform.position;
                    controller.addScore(index);

                }
                else
                {
                    icon.returnToOriginal();
                    controller.incorrect();

                    //Debug.LogError("Incorrect");
                }

            }

        }

    }
}
