using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostFrightened : GhostBehaviour
{



    public SpriteRenderer body;

    public SpriteRenderer eyes;
    
    public SpriteRenderer blue;
    
    public SpriteRenderer white;


    private void OnTriggerEnter2D(Collider2D collision)
    {

        Node node = collision.GetComponent<Node>();


        if (node != null && this.enabled)
        {
            Vector2 direction = Vector2.zero;
            float maxDistance = float.MinValue;

            foreach (Vector2 availableDirection in node.availableDirections)
            {

                Vector3 newPosition = transform.position + new Vector3(availableDirection.x, availableDirection.y);
                float distance = (ghost.target.position - newPosition).sqrMagnitude;

                if (distance > maxDistance)
                {
                    direction = availableDirection;
                    maxDistance = distance;
                }
            }

            ghost.movement.SetDirection(direction);
        }
    }



    public bool eaten { get; private set; }

    public override void Enable(float duration)
    {
        base.Enable(duration);

        body.enabled = false;
        eyes.enabled = false;
        blue.enabled = true;
        white.enabled = false;

        Invoke(nameof(Flash), duration / 2f);
    }

    public override void Disable()
    {
        base.Disable();

        body.enabled = true;
        eyes.enabled = true;
        blue.enabled = false;
        white.enabled = false;
    }

  
    private void Flash()
    {
        if (!eaten)
        {
            blue.enabled = false;
            white.enabled = true;
            white.GetComponent<AnmatedSprite>().Restart();
        }
    }

    private void Eaten()
    {
        this.eaten = true;
        Vector3 position = this.ghost.home.inside.position;
        position.z = this.ghost.transform.position.z;
        this.ghost.transform.position = position;   

        this.ghost.home.Enable(this.duration);

        body.enabled = false;
        eyes.enabled = true;
        blue.enabled = false;
        white.enabled = false;
    }


    private void OnEnable()
    {
        blue.GetComponent<AnmatedSprite>().Restart();
        this.ghost.movement.speedMultiplier = 0.5f;
        this.eaten = false;
    }

    private void OnDisable()
    {
        this.ghost.movement.speedMultiplier = 1.0f;
        this.eaten = false;
    }

   

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if (this.enabled)
            {
                Eaten();
            }
        }
    }




}
