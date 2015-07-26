using UnityEngine;
using System.Collections;

public class Prallax : MonoBehaviour {

    public float INITIAL_POINT;
    public float FINAL_POINT;
    public float speed;

    Vector3 newPosition;

    //Boolean
    bool stopClouds;

	// Use this for initialization
	void Start () {
        FINAL_POINT = Mathf.Abs(FINAL_POINT);
        newPosition = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
        if( Mathf.Abs(transform.localPosition.x) < FINAL_POINT && !stopClouds)
        {
            newPosition.x = transform.localPosition.x - speed * Time.deltaTime;
            transform.localPosition = newPosition;
        }       	        
	}

    /// <summary>
    /// Detiene el movimiento de las nubes
    /// </summary>
    void StopClouds() {
        stopClouds = true;
    }
}
