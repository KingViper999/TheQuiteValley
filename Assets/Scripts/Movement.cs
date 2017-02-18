using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    public int moveSpeed = 10;
	// Use this for initialization
    void Start()
    {

    }
	// Update is called once per frame
	void Update () 
    {
        // Moves character left
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
        // Moves character right
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
        // Moves character forward
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
        // Moves character backward
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
        }


	}

}