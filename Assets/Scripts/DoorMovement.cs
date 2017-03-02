/* Programmer: Josh Gernold 
 * Date: 1/30/17
 * 
 * 
 * 
*/
using UnityEngine;
using System.Collections;

public class DoorMovement : MonoBehaviour 
{

    private Animator animator = null;

	void Start () 
    {
        animator = GetComponent<Animator>();
	}

    void OnTriggerEnter(Collider col)
    {
        animator.SetBool("isopen", true);
    }

    void OnTriggerExit(Collider col)
    {
        animator.SetBool("isopen", false);
    }

}
