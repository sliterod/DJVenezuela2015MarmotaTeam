using UnityEngine;
using System.Collections;

public class BallRotation : MonoBehaviour {

    float rotationsPerMinute = -40.0f;
    bool isPlaying = true;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (isPlaying)
        {
            transform.Rotate(0, 0, 6.0f * rotationsPerMinute * Time.deltaTime);
        }
        
    }

    public void changeDirection()
    {
        rotationsPerMinute *= -1;
    }

    public void stop()
    {
        isPlaying = false;
    }

    public void resume()
    {
        isPlaying = true;
    }
}
