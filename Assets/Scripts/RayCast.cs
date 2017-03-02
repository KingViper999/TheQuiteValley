/* Programmer: Josh Gernold
 * Date: 12/8/16
 * 
 * Update 12/9/16 Did testing on destroying cubes
 * Update 12/13/16 Added in text to tell player to press e to interact 
 * 
 * 
 * 
**/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Audio;

public class RayCast : MonoBehaviour
{
    // Added 12/13/16
    // TextBoxes
    public Text dialogText;
    public Text hudText;
    public Text interact;
    public ParticleSystem partFX; // Holds reference to last interactable that RayCast hit

    // Distance of ray
    public float distanceToSee;
    RaycastHit whatIHit;

    void Start()
    {

    }

    void Update()
    {
        // Sets color of ray
        Debug.DrawRay(this.transform.position, this.transform.forward * distanceToSee, Color.red);
        // Finds distance ray is casting prom cam
        if (Physics.Raycast(this.transform.position, this.transform.forward, out whatIHit, distanceToSee))
        {
            // Added 12/13/16
            if (whatIHit.collider.gameObject.GetComponent<Interactions>() != null) // if the object is interactable
            {

                interact.enabled = true;
                interact.text = "Press 'E' to interact";
                partFX = whatIHit.collider.gameObject.GetComponent<ParticleSystem>();

            }
            else
            {
                interact.enabled = false;
                interact.text = "";

            }
            // Detects if E is pressed
            if (Input.GetKeyDown(KeyCode.E))

            // BOXES
            {   // Prints name of object ray comes in contact with when pressing E
                Debug.Log("i touched " + whatIHit.collider.gameObject.name);

                if (whatIHit.collider.tag == "Cube")// Detects if object has tag 
                {       // Detects if object has name cube1
                    if (whatIHit.collider.gameObject.GetComponent<Interactions>().whatAmI == Interactions.Interacter.Cube)
                    {   //Destroys object when E is pressed
                        Destroy(whatIHit.collider.gameObject);
                    }
                   
                }
                else if (whatIHit.collider.tag == "Book")
                {
                    if (whatIHit.collider.gameObject.GetComponent<Interactions>().whatAmI == Interactions.Interacter.Cube)
                    {
                        Collectables.colBook = true;
                    }
                }
            }

        }
    }
}
