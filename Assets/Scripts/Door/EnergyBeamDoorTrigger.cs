using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBeamDoorTrigger : DoorTrigger
{

    public EnergyBeamDoor energyBeamDoor;

    public override void OpenDoor()
    {
        energyBeamDoor.Open();
    }
    public override void CloseDoor()
    {
        energyBeamDoor.Close();
    }
}
