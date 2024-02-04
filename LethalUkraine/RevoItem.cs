using Unity.Netcode;

namespace LethalUkraine;

public class RevoItem : GrabbableObject {
    public override void ItemActivate(bool used, bool buttonDown = true) {
        if (playerHeldBy.GetComponent<RevoBuff>()) {
            LethalUkrainePlugin.logger.LogInfo("PLAYER ALREADY HAVE BUFF");
            return;
        }

        playerHeldBy.activatingItem = true;
        LethalUkrainePlugin.logger.LogInfo("ITEM CONSUMED");
        UseRevoServerRpc();
    }

    [ServerRpc(RequireOwnership = false)]
    public void UseRevoServerRpc() {
        LethalUkrainePlugin.logger.LogInfo("ServerRPC revo");
        UseRevoClientRpc();
    }

    [ClientRpc]
    private void UseRevoClientRpc() {
        LethalUkrainePlugin.logger.LogInfo("ClientRPC revo");
        if (playerHeldBy != null) {
            playerHeldBy.activatingItem = false;
            playerHeldBy.gameObject.AddComponent<RevoBuff>();
            playerHeldBy.DestroyItemInSlotAndSync(playerHeldBy.currentItemSlot);
        }
    }
}