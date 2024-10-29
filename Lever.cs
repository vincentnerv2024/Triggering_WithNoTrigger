using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField]
    private Player playerRef;


    [SerializeField]
    float checkDistance = 1.0f;


    [SerializeField]
    private WallDoor _wallDoor;

    public Vector3 initialPosition;

    public Vector3 pushedPosition;
    
    public float moveSpeed;

    private void Update()
    {

        
            Debug.DrawRay(transform.position, transform.up * checkDistance, Color.red); // Visualize the ray
            if (IsSomebodyStayingOnTheLever())
            {
                _wallDoor.OpenDoor();
            transform.position = pushedPosition;
            Debug.Log("Try to open");
            }
            else if (playerRef == null)
            {
                Debug.Log("Try to close");
                    _wallDoor.CloseDoor();
                transform.position = initialPosition;
            }



        

    }


    bool IsSomebodyStayingOnTheLever()
    {
        RaycastHit hit;
        var ray = this.transform.position;

        var rayDir = this.transform.up;
        if(Physics.Raycast(ray, rayDir,out hit, checkDistance))
        {
            if (hit.collider.CompareTag("Player"))
            {
                playerRef = hit.collider.GetComponent<Player>();
                return true;
            }

        }
        else
        {
            playerRef = null;
        }

        return false;
    }


}
