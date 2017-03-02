using UnityEngine;
using System.Collections;

public class camMouseLook : MonoBehaviour 
{
    // movement and smoothign in vectors
    Vector2 mouseLook;
    Vector2 smoothV;

    // sets sensitivity value 
    public float sensitivity = 5.0f;
    // sets smoothing value
    public float smoothing = 2.0f;
    //public bool Paused = !Paused;

    GameObject player;

    void Start()
    {
        // makes player parent of camera at start
        player = this.transform.parent.gameObject;
    }


    void Update()
    {
        var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        // equals out sens and smoothing while moving 
        md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));

        // smoothes movement left and right
        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
        mouseLook += smoothV;

        // only allows player to look up and down to 90 degrees
        mouseLook.y = Mathf.Clamp(mouseLook.y, -90f, 90f);

        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        player.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, player.transform.up);
    }
}