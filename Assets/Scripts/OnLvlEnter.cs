/* Programmer: Josh Gernold
 * Date: 12/13/16
 * Updated 12/18/16
 * 
 * 
**/
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OnLvlEnter : MonoBehaviour
{
    // Variables for diplayed text
    public Text dialogText; // reference to DialogText
    public int textSize;
    public int oldSize;
    public Text hudText; // reference to HudText
    public string[] openingRem; // variable to set remarks for on screen display
    public int remark; // int that controlls what remark displayed
    public bool wasUp; // makes sure that the remark is only displayed once
    public string level; // name of level
    public float dialogTimer; // determines how long text will stay on screen
    public float timer; // count timer

    void Awake()
    {
        if (!wasUp)
        {
            dialogText.text = openingRem[0]; // sets remarks to first line 
        }
        // displays each time you enter game
        oldSize = hudText.fontSize; // save origional size
        hudText.fontSize = textSize; // resize to make bigger
        hudText.text = level; // level title 

    }

    void Start()
    {
        // displays title every time enter level
        hudText.enabled = true;
        if (!wasUp)
        {
            timer = dialogTimer * openingRem.Length;
            dialogText.enabled = true; // enables UI to have text appear on entering game
        }
        else
        {
            timer = dialogTimer; // gives time to display text
        }
    }


    void Update()
    {
        if (timer >= -1.5)
        {
            timer = timer - Time.deltaTime;
        }

        if (remark < openingRem.Length) // run through remakrs
        {
            // change remark after period of time 
            if (timer < dialogTimer * (openingRem.Length - remark))
            {
                dialogText.enabled = false;
                dialogText.text = openingRem[remark];
                dialogText.enabled = true;
                remark++;
            }
        }
            else
            {
                if (timer < 0.0f)
                {
                    hudText.enabled = false;
                    dialogText.enabled = false;
                    hudText.fontSize = oldSize;
                    wasUp = true;
                }
            }
        }
    }