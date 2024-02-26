namespace LethalUkraine;

public class KislinkaItem : GrabbableObject {
    public int Damage;

    public override void ItemActivate(bool used, bool buttonDown = true) {
        playerHeldBy.DamagePlayer(Damage, causeOfDeath: CauseOfDeath.Electrocution);
    }
}
