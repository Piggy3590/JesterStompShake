using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JesterStompShake.Patches;
using UnityEngine;

namespace JesterStompShake
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class JesterStompShakeModBase : BaseUnityPlugin
    {
        private const string modGUID = "Piggy.JesterStompShake";
        private const string modName = "JesterStompShake";
        private const string modVersion = "1.0.0";

        private readonly Harmony harmony = new Harmony(modGUID);

        private static JesterStompShakeModBase Instance;

        internal ManualLogSource mls;

        public static float ShakeIntensity;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            mls = BepInEx.Logging.Logger.CreateLogSource(modGUID);

            JesterStompShakeModBase.ShakeIntensity = (float)base.Config.Bind<int>("General", "Shake Intensity", 2, "Camera shake intensity (1-3)").Value;

            mls.LogInfo("Jester Stomp Shake is loaded");

            harmony.PatchAll(typeof(JesterStompShakeModBase));
            harmony.PatchAll(typeof(PlayerControllerBPatch));
            harmony.PatchAll(typeof(PlayAudioAnimationEventPatch));
            //harmony.PatchAll(typeof(TimeOfDayPatch));
            harmony.PatchAll(typeof(JesterStompCheck));
        }
    }
}
