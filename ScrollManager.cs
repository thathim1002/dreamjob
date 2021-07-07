using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScrollManager : MonoBehaviour
{
    public ScrollRect scroll;

    public void click1()
    {
        if(Application.platform == RuntimePlatform.IPhonePlayer)
        {
            scroll.normalizedPosition = new Vector2(0, 0.25f);

        }
    }


    public void click2()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            scroll.normalizedPosition = new Vector2(0, 0.2f);
        }
    }

    private void Update()
    {
        if (!TouchScreenKeyboard.visible)
        {
            scroll.normalizedPosition = new Vector2(0, 0.6f);
        }
    }
}
