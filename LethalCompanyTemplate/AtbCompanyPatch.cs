using System.Linq;
using HarmonyLib;
using Unity.Netcode;
using UnityEngine;

namespace LethalCompanyTemplate;

[HarmonyPatch(typeof(RoundManager))]
public class AtbCompanyPatch {
    [HarmonyPostfix]
    [HarmonyPatch(typeof(RoundManager), "RefreshEnemiesList")]
    private static void RefreshEnemiesList(RoundManager __instance) {
        LethalUkrainePlugin.logger.LogInfo("Spawning atb logo");
        if (__instance.currentLevel.name == "CompanyBuildingLevel" && !HasAtb()) {
            LethalUkrainePlugin.logger.LogInfo("Spawning atb logo 2");
            var atb = Object.Instantiate(getAtbLogo(), new Vector3(-28.1707f, 3.4476f, -31.9498f), Quaternion.Euler(0, -90, 0));
            atb.GetComponent<NetworkObject>().Spawn();
        }
    }

    private static bool HasAtb() {
        return Object
            .FindObjectsOfType<GameObject>()
            .Count(o => o.name is "atb(Clone)" or "atb") > 0;
    }

    private static GameObject getAtbLogo() {
        return AssetBundle
            .GetAllLoadedAssetBundles()
            .First(assetBundle => assetBundle.name == "lethalukraine")
            .LoadAsset<GameObject>("assets/atb.prefab");
    }
}