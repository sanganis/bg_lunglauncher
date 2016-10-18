using UnityEngine;
using System.Collections;

public class LungCharacterController : MonoBehaviour
{
    // the most lives a player can have
    public int maxLives = 5;
    // current number of lives the player has
    public int currentLives = 5;

    public bool invincible;


    // materials for making the sprite flash when damaged
    Material normalMaterial;
    // flash material is changed according to what the player encounteres
    Material flashMaterial;
    public Material enemyFlashMaterial;
    public Material powerUpFlashMaterial;
    [HideInInspector]
    public SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        normalMaterial = spriteRenderer.material;
    }

    public void LoseLives(int lose)
    {
        if (!invincible)
        {
            currentLives -= lose;
            GameController.mainUIController.SetLivesNumber();
            flashMaterial = enemyFlashMaterial;
            CallFlashOverDuration(1f);
            if (currentLives == 0)
            {
                GameController.gameController.GameOverOutOfLives();
            }
        }
    }

    public void GainLives(int gain)
    {
        currentLives += gain;        
        if (currentLives > maxLives)
        {
            currentLives = maxLives;
        }
        GameController.mainUIController.SetLivesNumber();
    }

    public void SetInvincible(float duration)
    {
        InvincibleOn();
        flashMaterial = powerUpFlashMaterial;
        CallFlashOverDuration(duration);
        Invoke("InvincibleOff",duration);
    }
    void InvincibleOn()
    {
        invincible = true;
    }
    void InvincibleOff()
    {
        invincible = false;
    }


    // for calling various sprite flashes from other scripts, like powerups
    public void CallFlashOverDuration(float duration)
    {
        StartCoroutine(CallFlashCoroutine(duration));
    }
    // invokes the flashing, cancels after duration
    IEnumerator CallFlashCoroutine(float duration)
    {
        CancelInvoke("InvokeFlash");
        InvokeRepeating("InvokeFlash", 0, 0.01f);
        yield return new WaitForSeconds(duration);
        CancelInvoke("InvokeFlash");
    }
    // determins how many flashes to run
    void InvokeFlash()
    {
        StartCoroutine("Flash");
    }
    IEnumerator Flash()
    {
        spriteRenderer.material = flashMaterial;
        yield return new WaitForSeconds(0.0002f);
        spriteRenderer.material = normalMaterial;
    }


}
