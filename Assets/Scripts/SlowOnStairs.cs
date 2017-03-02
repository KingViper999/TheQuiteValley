/* Programmer: Josh Gernold
 * Date: 1/20/17
 *  
 * 
 * 
*/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Audio;

public class SlowOnStairs : MonoBehaviour 
{
    public Transform Player;
    public float speed = 10;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "player")
        {
            speed -= 8;

            Debug.Log("Player is slowed");
        }
    }
}
