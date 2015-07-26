using UnityEngine;
using System.Collections;

public class Animations : MonoBehaviour {

    public Animation character;
    public Animation boss;

    //Booleans
    bool isRunning;

	// Use this for initialization
	void Start () {
        character = character.GetComponent<Animation>();
        //boss = boss.GetComponent<Animation>();
    }

    void Update() {

    }

    /*
        Character
    */
    public void CharacterDashAnimation() {
        character.PlayQueued("dash", QueueMode.PlayNow);
        character.PlayQueued("run", QueueMode.CompleteOthers);
    }

    public void CharacterIdleAnimation() {
        character.CrossFade("idle");
    }

    public void CharacterJumpUpAnimation()
    {
        StartCoroutine(JumpCoroutine());   
    }

    IEnumerator JumpCoroutine() {

        character.PlayQueued("jumpUp", QueueMode.PlayNow);

        yield return new WaitForSeconds(0.7f);

        character.PlayQueued("jumpDown", QueueMode.CompleteOthers);

        yield return new WaitForSeconds(1.0f);

        character.PlayQueued("run", QueueMode.CompleteOthers);
    }

    public void CharacterJumpDownAnimation()
    {
        character.CrossFade("jumpDown");
    }

    public void CharacterRunAnimation()
    {
        character.CrossFade("run");
    }

    public void CharacterHurtAnimation()
    {
        character.CrossFade("hurt");
    }

    public void CharacterShotAnimation(bool isEndGame)
    {
        character.PlayQueued("shot",QueueMode.PlayNow);

        if (isEndGame)
        {
            //Placeholder
        }
        else {
            character.PlayQueued("run", QueueMode.CompleteOthers);
        }
    }


    /*
        Boss
    */
}
