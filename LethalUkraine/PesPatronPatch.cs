using System.Linq;
using HarmonyLib;
using LethalLib.Modules;
using UnityEngine;

namespace LethalUkraine;

[HarmonyPatch]
internal class PesPatronPatch {
    [HarmonyPatch(typeof(HoarderBugAI), "Start")]
    [HarmonyPostfix]
    private static void CreatePesPatron(HoarderBugAI __instance) {
        foreach (var renderer in __instance.transform.Find("HoarderBugModel").GetComponentsInChildren<Renderer>()) {
            renderer.enabled = false;
        }

        Object.Instantiate(getPesPatronPrefab(), __instance.gameObject.transform.transform);
        __instance.gameObject.transform.name = "Pes Patron";
        __instance.gameObject.transform.Find("Scan Node").GetComponent<ScanNodeProperties>().headerText = "Pes Patron";
    }

    [HarmonyPatch(typeof(RoundManager), "GenerateNewLevelClientRpc")]
    public static void GameStart(RoundManager __instance, int randomSeed, int levelID) {
        if (levelID == 3) {
            var enemy = Resources.FindObjectsOfTypeAll<EnemyType>()
                .First(type => type.enemyName == "Hoarding bug");
            __instance.SpawnEnemyGameObject(__instance.playersManager.localPlayerController.transform.position, 0, 1, enemy);
        }
    }

    private static GameObject getPesPatronPrefab() {
        return AssetBundle
            .GetAllLoadedAssetBundles()
            .First(assetBundle => assetBundle.name == "lethalukraine")
            .LoadAsset<GameObject>("assets/atb.prefab");
    }
}