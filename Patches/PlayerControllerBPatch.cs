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

namespace JesterStompShake.Patches
{
    [HarmonyPatch(typeof(PlayerControllerB))]
    internal class PlayerControllerBPatch
    {
        [HarmonyPostfix]
        [HarmonyPatch("Start")]
        private static void Start_Postfix()
        {
            JesterStompCheck.jesterStomped = false;
        }

        [HarmonyPostfix]
        [HarmonyPatch("Update")]
        private static void Update_Postfix(ref Transform ___thisPlayerBody)
        {
            JesterAI[] jesterComponents = GameObject.FindObjectsOfType<JesterAI>();

            foreach (JesterAI jesterComponent in jesterComponents)
            {
                GameObject jesterEnemy = jesterComponent.gameObject;
                float distance = Vector3.Distance(___thisPlayerBody.position, jesterEnemy.transform.position);
                if (distance > 0 && distance < 2 && JesterStompCheck.jesterStomped && jesterComponent.currentBehaviourStateIndex == 2)
                {
                    if (JesterStompShakeModBase.ShakeIntensity == 1) { HUDManager.Instance.ShakeCamera(ScreenShakeType.Small); }
                    else if (JesterStompShakeModBase.ShakeIntensity == 2) { HUDManager.Instance.ShakeCamera(ScreenShakeType.Big); }
                    else if (JesterStompShakeModBase.ShakeIntensity == 3) { HUDManager.Instance.ShakeCamera(ScreenShakeType.VeryStrong); }
                    JesterStompCheck.jesterStomped = false;
                }
                else if (distance > 2 && distance < 4 && JesterStompCheck.jesterStomped && jesterComponent.currentBehaviourStateIndex == 2)
                {
                    if (JesterStompShakeModBase.ShakeIntensity == 1) { HUDManager.Instance.ShakeCamera(ScreenShakeType.Small); }
                    else if (JesterStompShakeModBase.ShakeIntensity == 2) { HUDManager.Instance.ShakeCamera(ScreenShakeType.Big); }
                    else if (JesterStompShakeModBase.ShakeIntensity == 3) { HUDManager.Instance.ShakeCamera(ScreenShakeType.Big); }
                    JesterStompCheck.jesterStomped = false;
                }
                else if (distance > 4 && distance < 35 && JesterStompCheck.jesterStomped && jesterComponent.currentBehaviourStateIndex == 2)
                {
                    if (JesterStompShakeModBase.ShakeIntensity == 2) { HUDManager.Instance.ShakeCamera(ScreenShakeType.Small); }
                    else if (JesterStompShakeModBase.ShakeIntensity == 3) { HUDManager.Instance.ShakeCamera(ScreenShakeType.Big); }
                    JesterStompCheck.jesterStomped = false;
                }
                else
                {
                    JesterStompCheck.jesterStomped = false;
                }
            }
        }
    }
}
