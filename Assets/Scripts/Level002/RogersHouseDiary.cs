using UnityEngine;

public class RogersHouseDiary : Interactable
{
    Player player;

    public override void OnInteraction()
    {
        Debug.Log("TODO: Display diary text");
        player.readDiary = true;
    }

    protected override void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        base.Start();
    }
}
