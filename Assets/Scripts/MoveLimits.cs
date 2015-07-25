using UnityEngine;
using System.Collections;

public class MoveLimits : MonoBehaviour {

    public Transform limits;

	// Update is called once per frame
	void Update () {
        ChangeLimitsPosition();
	}

    /// <summary>
    /// Mueve los limites de posicion
    /// </summary>
    void ChangeLimitsPosition()
    {
        Vector3 newPosition;
        float speed;

        speed = this.GetComponent<MoveCharacter>().speed;

        newPosition = new Vector3( limits.position.x + speed,
                                   limits.position.y,
                                   limits.position.z);

        limits.position = newPosition;
    }
}
