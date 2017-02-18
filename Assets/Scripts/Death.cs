/* Programmer: Josh Gernold
 * Date: 12/18/16
 * 
 * 
 * 
 * 
*/
using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour 
{
    public Transform RespawnPoint;
    public Transform Player;
    void OnTriggerObject(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            characterDeath();
        }
    }

    public void characterDeath()
    {
        Player.transform.position = RespawnPoint.position;
    }
}
