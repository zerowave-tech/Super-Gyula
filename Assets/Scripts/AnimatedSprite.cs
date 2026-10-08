// unity connecting
using UnityEngine;

// connect the spriterender to object, if not create it 
[RequireComponent(typeof(SpriteRenderer))]
// class with inheritance from Moonbehaviour
public class AnimatedSprite : MonoBehaviour
{
//variables 
    public Sprite[] sprites;
    public float framerate = 1f / 6f;
    private SpriteRenderer spriteRenderer;
    private int frame;
    // In introducing, find and place sprite
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
// Repeating func, repeatly find method and time of frame change 
    private void OnEnable()
    {
        InvokeRepeating(nameof(Animate), framerate, framerate);
    }
// disable animation when object is mesh
    private void OnDisable()
    {
        CancelInvoke();
    }
//frame changing
    private void Animate()
    {
    //frame alternate
        frame++;
        // reset frame number for cycling animation
        if (frame >= sprites.Length) {
            frame = 0;
        }
        // that if, for when haven't sprite and don't crash for other sprite
        if (frame >= 0 && frame < sprites.Length) {
            spriteRenderer.sprite = sprites[frame];
        }
    }

}
