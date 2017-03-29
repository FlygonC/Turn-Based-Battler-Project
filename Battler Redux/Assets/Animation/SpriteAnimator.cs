using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteAnimator : MonoBehaviour
{
    //public SpriteAnimation[] animations;
    
    public SpriteAnimation spriteAnimation;
    public float speed = 1;

    public bool animationEnded = false;

    [SerializeField]
    private int currentFrame = 0;
    private int currentAnimFrameCount
    {
        get
        {
            return spriteAnimation.Frames.Length;
        }
    }
    //[SerializeField]
    private float frameTime = 0;

    private SpriteRenderer sprite;

    public void Start()
    {
        /*if (animations.Length > 0)
        {
            currentAnimation = animations[0];
        }*/
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (sprite != null)
        {
            if (spriteAnimation != null)
            {
                frameTime += (spriteAnimation.FPS) * speed * Time.deltaTime;
                while (frameTime > 1)
                {
                    frameTime -= 1;
                    if (currentFrame < currentAnimFrameCount - 1)
                    {
                        currentFrame++;
                        //frameTime -= 1;
                    }
                    else
                    {
                        if (spriteAnimation.Loop)
                        {
                            currentFrame = 0;
                        }
                        else
                        {
                            animationEnded = true;
                        }
                    }
                }

                sprite.sprite = spriteAnimation.Frames[currentFrame];
            }
        }
    }

    public void Play(SpriteAnimation _animation, bool _hard = false)
    {
        if (_hard)
        {
            currentFrame = 0;
            sprite.sprite = _animation.Frames[0];
            spriteAnimation = _animation;
            animationEnded = false;
        }
        else if (spriteAnimation != _animation)
        {
            currentFrame = 0;
            sprite.sprite = _animation.Frames[0];
            spriteAnimation = _animation;
            animationEnded = false;
        }
        
    }

    public void PlayHard(SpriteAnimation _animation)
    {
        currentFrame = 0;
        sprite.sprite = _animation.Frames[0];
        spriteAnimation = _animation;
        animationEnded = false;
    }

}
