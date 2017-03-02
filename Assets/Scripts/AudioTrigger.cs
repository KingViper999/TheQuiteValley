/* Programmer: Josh Gernold
 * Date: 1/15/17
 * 
 * 
 * 
*/

using UnityEngine.Audio;
using UnityEngine;
using System.Collections;

public class AudioTrigger : MonoBehaviour 
{
    public AudioClip audioTpPlay;
    public Transform Player;

	void OnTriggerEnter(Collider col) 
    {
        if (col.gameObject.tag == "Player" && gameObject.tag == "TV")
        {
            GetComponent<AudioSource>().PlayOneShot(audioTpPlay);
        }
	
	}
	

	public void PlayAudio() 
    {
        GetComponent<AudioSource>().PlayOneShot(audioTpPlay);
	}
}
