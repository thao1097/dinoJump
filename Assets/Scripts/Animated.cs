using UnityEngine;

public class Animated : MonoBehaviour
{
    // cycle between sprites using array
    public Sprite[] sprites;

    private SpriteRenderer spriteRenderer;

    private int frame;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // OnEnable() is called before Start() 
    // -> problematic for Animate() because game speed = 0 before Start(), and division by 0 => error
    private void OnEnable()
    {
        // kicking off animation using Invoke
        // invokes the method in time seconds: by using 0f, Animate() gets called next frame = after Start()
        Invoke(nameof(Animate), 0f);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
    
    private void Animate()
    {
        frame++;
        //to cycle index through array
        if (frame >= sprites.Length)
        {
            frame = 0;
        }
        if (frame >= 0 && frame < sprites.Length)
        {
            spriteRenderer.sprite = sprites[frame];
        }

        // animte based on speed of game, more speed = faster cycle 
        Invoke(nameof(Animate), 1f / GameManager.Instance.gameSpeed);
    }
}
