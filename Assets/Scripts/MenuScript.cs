/* Programmer: Josh Gernold
 * Date: 12/20/16
 * 
 * 
 * 
*/


using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuScript : MonoBehaviour
{
    public GameObject optionsWall;
    public GameObject creditsWall;
    // Raycast
    public float distanceToSee;
    RaycastHit whatIHit;
    public Transform mainTeleport;
    public Transform optionsTeleport;
    public Transform creditsTeleport;
    public Transform Player;
    public AudioSource audio;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(this.transform.position, this.transform.forward * distanceToSee, Color.magenta);

        if (Physics.Raycast(this.transform.position, this.transform.forward, out whatIHit, distanceToSee))  //Raycast from position player to distance infront checking if it hits something
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (whatIHit.collider.gameObject.GetComponent<Interactions>().whatAmI == Interactions.Interacter.menu)
                {
                    if (whatIHit.collider.tag == "Start")
                    {
                        SceneManager.LoadScene(1);
                    }
                    else if (whatIHit.collider.tag == "Options")
                    {
                        Player.transform.position = optionsTeleport.position;
                    }
                    else if (whatIHit.collider.tag == "Credits")
                    {
                        Player.transform.position = creditsTeleport.position;
                    }
                    else if (whatIHit.collider.tag == "Back")
                    {
                        Player.transform.position = mainTeleport.position;
                    }
                    else if (whatIHit.collider.tag == "VolumeOn")
                    {
                        audio.Play();
                    }
                    else if (whatIHit.collider.tag == "VolumeOff")
                    {
                        audio.Stop();
                    }
                    else if (whatIHit.collider.tag == "Exit")
                    {
                        Application.Quit();
                    }
                }
            }
        }
    }
}
