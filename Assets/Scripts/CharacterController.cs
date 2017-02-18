using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour 
{
    // sets speed of player
    public float speed = 10.0f;

	void Start () 
    {
     
	}
	

	void Update () 
    {
        // adds the speed to teh movement of horiz and vert
        float translation = Input.GetAxis("Vertical") * speed;
        float straffe = Input.GetAxis("Horizontal") * speed;

        // finds translation and straffe during movement 
        translation *= Time.deltaTime;
        straffe *= Time.deltaTime;

        
        transform.Translate(straffe, 0, translation);

	}
}
