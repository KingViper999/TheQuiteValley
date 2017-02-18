/* Programmer: Josh Gernold
 * Purpose: To set a tag for each interactable item in game
**/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Interactions : MonoBehaviour
{
    public enum Interacter { Cube, Book, Lamp, Oven, Door, TV, Cabinet, menu };
    public Interacter whatAmI;

}
