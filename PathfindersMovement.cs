using UnityEngine;
using System.Collections;

public class PathfindersMovement : MonoBehaviour {

    public float Speed;
    //public NodeGrid Grid;
    public Vector3 DirectionVector = new Vector3();

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {

        //DirectionVector = new Vector3(Grid.VectorFieldMap[Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z)].x , 0, Grid.VectorFieldMap[Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z)].y);
        transform.Translate(DirectionVector * Speed * Time.deltaTime);
    }
}
