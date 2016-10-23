using UnityEngine;
using System.Collections;

public class CamFollow : MonoBehaviour
{
    public GameObject target;       //sets target in unity to a certain game object (linked to the player in this case)

	// Use this for initialization
	void Start ()       
    {
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, -10);        //moves the position of teh camera to the the target (The player's) position
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, -10);        //same as before, just every frame, instead of once at the very start
    }
}
