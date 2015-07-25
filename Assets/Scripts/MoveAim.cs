using UnityEngine;
using System.Collections;

public class MoveAim : MonoBehaviour {

    public Transform crosshair;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        MoveCrossHair();
    }

    /// <summary>
    /// Mueve la mira del mouse
    /// </summary>
    void MoveCrossHair() {

        Vector3 mousePos;
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));

        crosshair.localPosition = new Vector3(  mousePos.x,
                                                mousePos.y,
                                                crosshair.position.z);
    }
}
