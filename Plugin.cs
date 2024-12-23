using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System.Reflection;
using JesterStompShake.Patches;

namespace JesterStompShake
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class Plugin : BaseUnityPlugin
    {
        private const string modGUID = "Piggy.JesterStompShake";
        private const string modName = "JesterStompShake";
        private const string modVersion = "1.0.9";

        private readonly Harmony harmony = new Harmony(modGUID);

        private static Plugin Instance;

        public static ManualLogSource mls;

        public static int ShakeIntensity;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            mls = BepInEx.Logging.Logger.CreateLogSource(modGUID);

            ShakeIntensity = (int)base.Config.Bind<int>("General", "Shake Intensity", 2, "Camera shake intensity (1-3)").Value;

            mls.LogInfo("Jester Stomp Shake is loaded");
            PlayerControllerBPatch.shakeIntensity = ShakeIntensity;

            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
        }
    }
}
