using UnityEngine;
using System.Collections;

public class GlowEffect : MonoBehaviour {

    bool glowUp;

    public float glowSpeed = 0.01f;

    public float glowMax = 1f;
    public float glowMin = 0.5f;

    SpriteRenderer spriteRenderer;


	void OnEnable () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        InvokeRepeating("Glow", 0, 0.01f);
	}
	
    
    void Glow()
    {
        if (!glowUp)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, (spriteRenderer.color.a - glowSpeed));
            if (spriteRenderer.color.a <= glowMin)
            {
                glowUp = true;
            }
        }
        if (glowUp)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, (spriteRenderer.color.a + glowSpeed));
            if (spriteRenderer.color.a >= glowMax)
            {
                glowUp = false;
            }
        }
    }

}
