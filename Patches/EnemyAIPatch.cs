using BepInEx.Logging;
using DunGen;
using GameNetcodeStuff;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;
using JesterStompShake.Patches;
using BepInEx;

namespace JesterStompShake.Patches
{
    [HarmonyPatch(typeof(EnemyAI))]
    internal class EnemyAIPatch
    {
        [HarmonyPostfix]
        [HarmonyPatch("OnDestroy")]
        private static void OnDestroy_Postfix(EnemyAI __instance)
        {
            if (__instance is JesterAI)
            {
                JesterAI jesterScript = __instance.GetComponent<JesterAI>();
                PlayerControllerBPatch.cachedJesters.Remove(jesterScript);
            }
        }
    }
}