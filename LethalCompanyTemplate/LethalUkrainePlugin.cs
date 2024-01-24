using System.IO;
using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using LethalLib;
using LethalLib.Modules;
using UnityEngine;

namespace LethalCompanyTemplate;

[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
[BepInDependency(Plugin.ModGUID)]
public class LethalUkrainePlugin : BaseUnityPlugin {
    public static LethalUkrainePlugin Instance;
    public static ManualLogSource logger;
    public static GameObject atb;
    private readonly Harmony harmony = new("Teheidoma.LethalUkraine");


    private void Awake() {
        if (Instance == null) {
            Instance = this;
            logger = Logger;
        }

        Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");

        var location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var bundle = AssetBundle.LoadFromFile(Path.Combine(location, "lethalukraine"));

        if (bundle == null) {
            Logger.LogError("failed to load assets");

            return;
        }

        var flagItem = bundle.LoadAsset<Item>("assets/flag.asset");
        NetworkPrefabs.RegisterNetworkPrefab(flagItem.spawnPrefab);
        Items.RegisterScrap(flagItem, 60, Levels.LevelTypes.All);
        Items.RegisterShopItem(flagItem, 10);

        atb = bundle.LoadAsset<GameObject>("assets/atb.asset");

        var revo = bundle.LoadAsset<Item>("assets/revo.asset");
        NetworkPrefabs.RegisterNetworkPrefab(revo.spawnPrefab);
        Items.RegisterShopItem(revo, 30);

        harmony.PatchAll(typeof(AtbCompanyPatch));
        NetcodePatcher();
    }

    private static void NetcodePatcher() {
        var types = Assembly.GetExecutingAssembly().GetTypes();
        foreach (var type in types) {
            var methods = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            foreach (var method in methods) {
                var attributes = method.GetCustomAttributes(typeof(RuntimeInitializeOnLoadMethodAttribute), false);
                if (attributes.Length > 0) method.Invoke(null, null);
            }
        }
    }
}