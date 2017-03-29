using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "NewSpriteAnimation", menuName = "SpriteAnim/New Animation", order = 1)]
public class SpriteAnimation : ScriptableObject
{
    public Sprite[] Frames;
    public float FPS;
    public bool Loop = true;
}
