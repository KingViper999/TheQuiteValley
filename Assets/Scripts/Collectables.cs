/* Programmer: Josh Gernold
 * Date: 2/1/17
 * 
 * 
*/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Collectables : MonoBehaviour 
{
    public Text gameWon;
    public string message; // message to display that player has won game
    public float timer; // timer for how long message is displayed


    public GameObject collectBook;
    public static bool colBook = false;

    void Awake()
    {
        // message to display when all collectiables are collected 
    }


	void Start() 
    {
        collectBook.GetComponent<MeshRenderer>().enabled = true;
	
	}
	
	// Update is called once per frame
	void Update() 
    {       // on update the book can be collected 
        if (colBook == true)
        {
            collectBook.GetComponent<MeshRenderer>().enabled = true;
        }
        // if book is collected then game won displays game has bee nwon
        if (colBook == true)
        {
            gameWon.enabled = true;
            timer = timer - Time.deltaTime;
            if (timer <= 0)
            {
                gameWon.enabled = false;
                SceneManager.LoadScene(0);
            }
        }
	}
}
