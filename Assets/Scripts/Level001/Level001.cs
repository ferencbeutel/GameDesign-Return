using UnityEngine;

public class Level001 : MonoBehaviour
{
    public EnergyBeamDoor doorToLab;
    public GameObject boulder;

    Player player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

    }

    private void Update()
    {
        if (player.activatedEnergySwitch && !doorToLab.isActive)
        {
            doorToLab.Activate();
        }
        if (player.readDiary && !boulder.activeSelf)
        {
            boulder.SetActive(true);
        }
    }
}
