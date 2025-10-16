using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class InvincibilityController : MonoBehaviour
{
    private PlayerHealth _healthController;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _healthController = GetComponent<PlayerHealth>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void StartInvincibility(float duration)
    {
        if (_healthController != null)
        {
            StopAllCoroutines(); // ensures multiple triggers don’t overlap
            StartCoroutine(InvincibilityCoroutine(duration));
        }
    }

    private IEnumerator InvincibilityCoroutine(float duration)
    {
        _healthController.SetInvincible(true);

        float timer = 0f;
        float flickerInterval = 0.1f;

        while (timer < duration)
        {
            _spriteRenderer.enabled = !_spriteRenderer.enabled;
            timer += flickerInterval;
            yield return new WaitForSeconds(flickerInterval);
        }

        _spriteRenderer.enabled = true;
        _healthController.SetInvincible(false);
    }
}


