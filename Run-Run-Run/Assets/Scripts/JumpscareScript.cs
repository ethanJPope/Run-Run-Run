using UnityEngine;
using System.Collections;
public class JumpscareScript : MonoBehaviour
{
    [SerializeField] private SpriteRenderer jumpscareSpriteRenderer;
    [SerializeField] private float minJumpscareDelay = 10f;
    [SerializeField] private float maxJumpscareDelay = 30f;
    [SerializeField] private float jumpscareFadeDuration = 1.5f;
    [SerializeField] private AudioClip jumpscareSound;
    private Transform cameraTransform;
    private Transform currentTransform;
    void Start()
    {
        cameraTransform = Camera.main.transform;
        currentTransform = GetComponent<Transform>();
        if (jumpscareSpriteRenderer != null)
        {
            Color color = jumpscareSpriteRenderer.color;
            color.a = 0f;
            jumpscareSpriteRenderer.color = color;
            StartCoroutine(JumpscareRoutine());
        }
    }

    void Update()
    {
        currentTransform.position = new Vector3(cameraTransform.position.x, cameraTransform.position.y, -1);
    }

    private IEnumerator JumpscareRoutine()
    {
        while (true)
        {
            float waitTime = Random.Range(minJumpscareDelay, maxJumpscareDelay);
            yield return new WaitForSeconds(waitTime);

            yield return StartCoroutine(FadeSprite(jumpscareSpriteRenderer, 0f, 1f, jumpscareFadeDuration));
            yield return new WaitForSeconds(0.5f);
            yield return StartCoroutine(FadeSprite(jumpscareSpriteRenderer, 1f, 0f, jumpscareFadeDuration));
        }
    }

    private IEnumerator FadeSprite(SpriteRenderer sr, float from, float to, float duration)
    {
        float elapsed = 0f;
        Color c = sr.color;
        SoundEffectsManager.instance.PlaySound(jumpscareSound, currentTransform, 1f);
        
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            c.a = Mathf.Lerp(from, to, elapsed / duration);
            sr.color = c;
            yield return null;
        }

        c.a = to;
        sr.color = c;
    }
}
