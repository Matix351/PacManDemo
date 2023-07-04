using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AnmatedSprite : MonoBehaviour
{

    public SpriteRenderer spriteRenderer { get; private set; }
    public Sprite[] sprites;
    public float aniamtionTime = 0.05f;
    public int spriteIndex {  get; private set; }

    public bool loop = true;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    private void Start()
    {
        InvokeRepeating(nameof(Animation), this.aniamtionTime, this.aniamtionTime);
    }

    private void Animation()
    {
        if(!this.spriteRenderer.enabled) return;

        this.spriteIndex++;
            if(this.spriteIndex >= this.sprites.Length && this.loop)
            {
                this.spriteIndex = 0;
            }
            if(this.spriteIndex >= 0 && this.spriteIndex < this.sprites.Length)
            {
                this.spriteRenderer.sprite = this.sprites[this.spriteIndex];
            }
    }    


    public void Restart()
    {
        this.spriteIndex = -1;
        Animation();
    }

}
