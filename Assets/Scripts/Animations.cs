using UnityEngine;
using System.Collections;

public class Animations : MonoBehaviour {

    public Animation character;
    public Animation enemy;

	// Use this for initialization
	void Start () {
        character = character.GetComponent<Animation>();
        enemy = enemy.GetComponent<Animation>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void DashAnimation() {
        character.CrossFade("");
    }

    public void IdleAnimation() {
        character.CrossFade("");
    }

    public void JumpAnimation()
    {
        character.CrossFade("");
    }

}
