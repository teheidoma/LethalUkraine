using UnityEngine;

namespace LethalCompanyTemplate;

public class FlagItem : GrabbableObject {
    public AudioClip AudioClip;

    public override void EquipItem() {
        LethalUkrainePlugin.logger.LogInfo("playing sound");
        var audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(AudioClip);
        FindObjectOfType<RoundManager>().PlayAudibleNoise(transform.position, 50, 0.9f);
    }

    public override void OnHitGround() {
        LethalUkrainePlugin.logger.LogInfo("stopped sound :(");
        gameObject.GetComponent<AudioSource>().Stop();
    }
}