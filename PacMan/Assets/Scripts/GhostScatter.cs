using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScatter : GhostBehaviour
{

  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("01");
        Node node = collision.GetComponent<Node>();
        
        if (node != null && this.enabled && !this.ghost.frightened.enabled)
        {
            //Debug.Log("02");
            int index = Random.Range(0, node.availableDirections.Count);
            //Debug.Log(index.ToString());

            if (node.availableDirections[index] == -this.ghost.movement.direction && node.availableDirections.Count > 1 ) 
            {
                //Debug.Log("03");
                index++;
                //Debug.Log(index.ToString());
                if (index >= node.availableDirections.Count)
                {
                    //Debug.Log("03");
                    index = 0;
                  //  Debug.Log(index.ToString());
                }
                //Debug.Log("04");

                

            }

            this.ghost.movement.SetDirection(node.availableDirections[index]);
        }
    }


    private void OnDisable()
    {
        this.ghost.chase.Enable();
    }


}
