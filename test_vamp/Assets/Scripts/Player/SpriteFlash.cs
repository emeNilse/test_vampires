using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFlash : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public IEnumerator FlashCoroutine(float flashDuration, Color flashColor, int numberOfFlashes)
    {
        Color startColor = _spriteRenderer.color;
        
        float elaspedFlashTime = 0;
        float elaspedFlashPercentage = 0;

        while (elaspedFlashTime < flashDuration)
        {
            elaspedFlashTime += Time.deltaTime;
            elaspedFlashPercentage = elaspedFlashTime / flashDuration;

            if (elaspedFlashPercentage > 1)
            {
                elaspedFlashPercentage = 1;
            }

            float pingPongPercentage = Mathf.PingPong(elaspedFlashPercentage * 2 * numberOfFlashes, 1);
            _spriteRenderer.color = Color.Lerp(startColor, flashColor, pingPongPercentage);

            yield return null;
        }
    }
}
