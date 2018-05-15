using UnityEngine;

public class Level001 : MonoBehaviour
{
    public EnergyBeamDoor doorToLab;

    Player player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

    }

    private void Update()
    {
        if (player.hasFinishedTutorial && !doorToLab.isActive)
        {
            doorToLab.Activate();
        }
    }
}
