using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;
using System.Collections.Generic;
using GameNetcodeStuff;

namespace JesterStompShake.Patches
{
    [HarmonyPatch(typeof(RoundManager))]
    internal class RoundManagerPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch("UnloadSceneObjectsEarly")]
        private static void UnloadSceneObjectsEarly_Prefix()
        {
            PlayerControllerBPatch.cachedJesters.Clear();
        }
    }
}
