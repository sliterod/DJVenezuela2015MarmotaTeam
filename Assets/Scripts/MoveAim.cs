using UnityEngine;
using System.Collections;

public class MoveAim : MonoBehaviour {

    public Transform crosshair;
    Vector3 mousePos;

    //Boolean
    bool isEndGame;

	// Update is called once per frame
	void Update () {
        MoveCrossHair();
    }

    /// <summary>
    /// Mueve la mira del mouse
    /// </summary>
    void MoveCrossHair() {

        if (!isEndGame)
        {
            mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));

            crosshair.localPosition = new Vector3(mousePos.x,
                                                    mousePos.y,
                                                    crosshair.position.z);
        }

    }

    /// <summary>
    /// Cambia la posicion de la reticula en la secuencia del final
    /// </summary>
    void DeactivateCrossHair()
    {
        crosshair.gameObject.SetActive(false);
        isEndGame = true;
    }
}
