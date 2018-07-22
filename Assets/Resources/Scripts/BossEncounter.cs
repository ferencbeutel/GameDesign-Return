using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEncounter : MonoBehaviour
{

    public Boss4711 boss4711;

    Camera2DFollow camera2DFollow;
    Player player;
    Player2D player2D;
    DialogueManager dialogueManager;
    CameraEffects cameraEffects;

    void Start()
    {
        GameObject playerGO = GameObject.FindGameObjectWithTag("Player");

        camera2DFollow = Camera.main.GetComponent<Camera2DFollow>();
        player = playerGO.GetComponent<Player>();
        player2D = playerGO.GetComponent<Player2D>();
        dialogueManager = FindObjectOfType<DialogueManager>();
        cameraEffects = FindObjectOfType<CameraEffects>(); ;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameObject.FindGameObjectWithTag("Player") == collision.gameObject)
        {
            if (!player.encounteredBoss_006)
            {
                StartCoroutine(InitBossFight());
            }
            else
            {
                InitBoss();
            }
        }
    }

    private IEnumerator InitBossFight()
    {
        player2D.LockMovement();
        camera2DFollow.AttachTarget(boss4711.transform);

        yield return new WaitForSeconds(1);

        boss4711.PlayBreakFreeAnimation();
        cameraEffects.ShakeFor(5, 1f);

        yield return new WaitForSeconds(5);

        camera2DFollow.AttachTarget(player.transform);

        yield return new WaitForSeconds(1);

        player2D.UnlockMovement();
        InitBoss();
    }

    private void InitBoss()
    {
        boss4711.Init();
    }
}
