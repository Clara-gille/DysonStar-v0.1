using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerElevator : MonoBehaviour
{
    ElevatingPlatform elevator;
    private bool OnElevator = false;

    private void Start()
    {
        elevator = GetComponentInParent<ElevatingPlatform>();
    }

    private void Update()
    {
        if (OnElevator && !elevator.canMove)
        {
            //If the player is in the trigger zone and presses the "Z" key, the elevator will start moving upward if it can
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                elevator.reverse = false;
                if (elevator.destinationPoint != elevator.points.Length - 1)
                {
                    elevator.destinationPoint++;
                    foreach (GameObject trigger in elevator.TriggerToDeactivate)
                    {
                        trigger.SetActive(false);
                    }
                    elevator.TriggerStatus = false;
                    elevator.canMove = true;

                }


            }
            //If the player is in the trigger zone and presses the "S" key, the elevator will start moving downward if it can
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                elevator.reverse = true;
                if (elevator.destinationPoint != 0)
                {
                    elevator.destinationPoint--;
                    foreach (GameObject trigger in elevator.TriggerToDeactivate)
                    { 
                        trigger.SetActive(false);
                    }
                    elevator.TriggerStatus = false;
                    elevator.canMove = true;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        OnElevator = true;
     
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        OnElevator = false;
    }

}
