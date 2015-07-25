using UnityEngine;
using System.Collections;

public class MoveAim : MonoBehaviour {

    public Transform crosshair;

    //Boolean
    bool isEndGame;

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

        if (!isEndGame)
        {
            crosshair.localPosition = new Vector3(mousePos.x,
                                                    mousePos.y,
                                                    crosshair.position.z);
        }
        else {
            crosshair.localPosition = new Vector3(crosshair.position.x,
                                                  mousePos.y,
                                                  mousePos.z);
        }
    }

    /// <summary>
    /// Cambia la posicion de la reticula en la secuencia del final
    /// </summary>
    void ChangeCrosshair()
    {
        crosshair.gameObject.SetActive(false);

        crosshair = GameObject.Find("CrosshairFinal").transform;
        isEndGame = true;
    }
}
