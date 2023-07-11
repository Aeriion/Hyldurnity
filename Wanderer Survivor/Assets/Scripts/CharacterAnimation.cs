using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    public Sprite[] animationFrames;
    public float frameDuration = 0.1f;
    
    private SpriteRenderer spriteRenderer;
    private int currentFrame = 0;


    private void Start()
    {
        animationFrames = Resources.LoadAll<Sprite>("Import");
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (animationFrames.Length > 0)
        {
            InvokeRepeating("NextFrame", frameDuration, frameDuration);
        }
        else
        {
            Debug.LogWarning("No animation frames found.");
        }
    }

    private void NextFrame()
    {
        if (animationFrames.Length > 0)
        {
            currentFrame = (currentFrame + 1) % animationFrames.Length;
            spriteRenderer.sprite = animationFrames[currentFrame];
        }
    }
}