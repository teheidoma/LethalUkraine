using System;
using System.Collections;
using GameNetcodeStuff;
using Unity.Netcode;
using UnityEngine;

namespace LethalCompanyTemplate;

public class RevoBuff : NetworkBehaviour {
    public float secondsLeft;
    public float originalSpeed;
    private PlayerControllerB _playerController;

    public void Start() {
        _playerController = GetComponent<PlayerControllerB>();
        StartCoroutine(SpeedUp(_playerController));
    }

    public override void OnDestroy() {
        if (Math.Abs(_playerController.movementSpeed - originalSpeed) > 0.1f) _playerController.movementSpeed = originalSpeed;
    }

    private IEnumerator SpeedUp(PlayerControllerB player) {
        originalSpeed = player.movementSpeed;
        LethalUkrainePlugin.logger.LogInfo($"PLAYER {player.playerUsername} buffed with REVASIK");
        player.movementSpeed *= 3f;
        for (var buffTime = 180f; buffTime > 0f; buffTime -= 1f) {
            secondsLeft = buffTime;
            yield return new WaitForSeconds(1f);
        }

        LethalUkrainePlugin.logger.LogInfo($"PLAYER {player.playerUsername} end buff with REVASIK");

        player.movementSpeed = originalSpeed;
        Destroy(this);
    }
}