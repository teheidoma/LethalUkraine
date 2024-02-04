using Unity.Netcode;
using UnityEngine;

namespace LethalUkraine;

public class SalutItem : GrabbableObject {
    public bool LookAt = true;
    
    public override void ItemActivate(bool used, bool buttonDown = true) {
        LethalUkrainePlugin.logger.LogInfo("Activate Salut");
        if (playerHeldBy != null) {
            SalutHealServerRpc();
        }
    }

    [ServerRpc]
    public void SalutHealServerRpc() {
        LethalUkrainePlugin.logger.LogInfo("Activate Salut server");
        SalutHealClientRpc();
    }

    [ClientRpc]
    private void SalutHealClientRpc() {
        LethalUkrainePlugin.logger.LogInfo("Activate client");
        playerHeldBy.health = 100;
        playerHeldBy.criticallyInjured = false;
        playerHeldBy.bleedingHeavily = false;
        playerHeldBy.DestroyItemInSlot(playerHeldBy.currentItemSlot);
    }
}