using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace ForTheHoard
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        static ManualLogSource logger;

        private void Awake()
        {
            // Plugin startup logic
            logger = Logger;
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded");
            Logger.LogInfo($"Patching...");
            Harmony.CreateAndPatchAll(typeof(Plugin));
            Logger.LogInfo($"Patched");
        }

        [HarmonyPatch(typeof(TrunkHolder), "Start")]
        [HarmonyPostfix]
        static void Start_Postfix(TrunkHolder __instance)
        {
            __instance.minDetectDistance = 0.1f;
            __instance.minDetectDistanceAttached = 0.1f;
        }

        // Debug drawing
        /*
        static GameObject lines;

        [HarmonyPatch(typeof(TrunkHolder), "Start")]
        [HarmonyPostfix]
        static void Start_Postfix(TrunkHolder __instance)
        {
            foreach (TrunkHolder.detectPoint_t t in __instance.detectPoints)
            {
                GameObject g = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                g.transform.SetParent(t.detectPoint.transform);
                g.transform.localPosition = new Vector3(0, 0, 0);
                g.transform.localScale = new Vector3(.02f, .02f, .02f);
            }

            lines = new GameObject();
            lines.transform.SetParent(__instance.transform);
        }

        [HarmonyPatch(typeof(TrunkHolder), "GetAddIndex")]
        [HarmonyPrefix]
        static void GetAddIndex_Prefix(TrunkHolder __instance)
        {
            UnityEngine.Object.Destroy(lines);
            lines = new GameObject();
            lines.transform.SetParent(__instance.transform);
        }

        [HarmonyPatch(typeof(TrunkHolder), "GetDetectDistance")]
        [HarmonyPostfix]
        static void GetDetectDistance_Postfix(TrunkHolder __instance, Vector3 detectPos)
        {
            GameObject g = new GameObject();
            g.transform.SetParent(lines.transform);
            LineRenderer l = g.AddComponent<LineRenderer>();
            l.SetWidth(.1f, .1f);
            l.SetPositions(new Vector3[] { detectPos, detectPos + (-__instance.transform.up * __instance.maxDetectDistance) });
        }
        */
    }
}

