using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostChase : GhostBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        Node node = collision.GetComponent<Node>();


        if (node != null && enabled && !ghost.frightened.enabled)
        {
            Vector2 direction = Vector2.zero;
            float minDistance = float.MaxValue;

            foreach (Vector2 availableDirection in node.availableDirections)
            {
                
                Vector3 newPosition = transform.position + new Vector3(availableDirection.x, availableDirection.y);
                float distance = (ghost.target.position - newPosition).sqrMagnitude;

                if (distance < minDistance)
                {
                    direction = availableDirection;
                    minDistance = distance;
                }
            }

            ghost.movement.SetDirection(direction);
        }
    }

    private void OnDisable()
    {
        this.ghost.scatter.Enable();
    }
}
