using Unity.Netcode;

namespace LethalCompanyTemplate;

public class RevoItem : PhysicsProp {
    public override void ItemActivate(bool used, bool buttonDown = true) {
        if (playerHeldBy != null) {
            if (playerHeldBy.GetComponent<RevoBuff>()) {
                LethalUkrainePlugin.logger.LogInfo("PLAYER ALREADY HAVE BUFF");
                return;
            }

            playerHeldBy.activatingItem = true;
            LethalUkrainePlugin.logger.LogInfo("ITEM CONSUMED");
            UseRevoServerRpc();
        }
    }

    public override void PocketItem() {
        if (IsOwner && playerHeldBy != null) {
            isPocketed = true;
            EnableItemMeshes(false);
        }
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