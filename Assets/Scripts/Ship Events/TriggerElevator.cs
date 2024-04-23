using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TriggerElevator : MonoBehaviour
{
    ElevatingPlatform elevator;
    private bool OnElevator = false;
    [SerializeField] private InputActionReference move;

    private void OnEnable()
    {
        move.action.Enable();
        move.action.performed += moveElevator;
    }

    private void Start()
    {
        elevator = GetComponentInParent<ElevatingPlatform>();
    }

    private void moveElevator(InputAction.CallbackContext context)
    {
        if (OnElevator && !elevator.canMove)
        {
            //If the player is in the trigger zone and presses the Up key, the elevator will start moving upward if it can
            if (context.ReadValue<Vector2>().y > 0)
            {
                elevator.reverse = false;
                // Making sure the elevator doesn't go higher if it's already at the highest point
                if (elevator.destinationPoint != elevator.points.Length - 1)
                {
                    elevator.destinationPoint++;
                    // Deactivating the triggers of the automatic doors so they close and the player can't leave while the elevator is moving
                    foreach (GameObject trigger in elevator.TriggerToDeactivate)
                    {
                        trigger.SetActive(false);
                    }
                    elevator.TriggerStatus = false;
                    elevator.canMove = true;

                }


            }
            //If the player is in the trigger zone and presses the Down key, the elevator will start moving downward if it can
            else if (context.ReadValue<Vector2>().y < 0)
            {
                elevator.reverse = true;
                // Making sure the elevator doesn't go lower if it's already at the lowest point
                if (elevator.destinationPoint != 0)
                {
                    elevator.destinationPoint--;
                    // Same as above
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
