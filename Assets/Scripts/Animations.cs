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
        boss = boss.GetComponent<Animation>();
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
        character.Stop("run");
        character.CrossFade("hurt");
    }

    /// <summary>
    /// Animacion de disparar la pelota
    /// </summary>
    /// <param name="isEndGame">Pregunta si es el final del juego</param>
    /// <param name="shotInGoal">Pregunta si el tiro se hizo a la porteria</param>
    public void CharacterShotAnimation(bool isEndGame, bool shotInGoal)
    {
        character.Stop("run");
        character.PlayQueued("shot",QueueMode.PlayNow);

        if (isEndGame)
        {
            //Placeholder
            if (shotInGoal) {
                character.PlayQueued("jumpUp", QueueMode.PlayNow);
            }
            else if (!shotInGoal){
                character.PlayQueued("hurt", QueueMode.PlayNow);
            }
        }
        else if (!isEndGame){
            character.PlayQueued("run", QueueMode.CompleteOthers);
        }
    }


    /*
        Boss
    */
    /// <summary>
    /// Animacion idle del boss
    /// </summary>
    void BossIdleAnimation() {
        boss.CrossFade("idle");
    }

    /// <summary>
    /// Animacion first appeareance
    /// </summary>
    void BossFirstAppearAnimation() {
        boss.PlayQueued("first", QueueMode.PlayNow);
    }

    /// <summary>
    /// Animacion deflect
    /// </summary>
    void BossDeflectAnimation() {
        boss.PlayQueued("deflect", QueueMode.PlayNow);
    }

    /// <summary>
    /// Animacion deflect jump
    /// </summary>
    void BossJumpDeflectAnimation()
    {
        boss.PlayQueued("jumpDeflect", QueueMode.PlayNow);
    }

    /// <summary>
    /// Animacion defeated
    /// </summary>
    void BossJumpDefeatedAnimation()
    {
        boss.PlayQueued("defeated", QueueMode.PlayNow);
    }

    /// <summary>
    /// Animacion deflect en el aire
    /// </summary>
    void BossDeflectAirAnimation()
    {
        boss.PlayQueued("deflectAir", QueueMode.PlayNow);
    }

    /// <summary>
    /// Animacion deflect
    /// </summary>
    void BossTauntAnimation()
    {
        boss.PlayQueued("taunt", QueueMode.PlayNow);
    }
}
