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
    [HarmonyPatch(typeof(TimeOfDay))]
    internal class TimeOfDayPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch("Update")]
        private static void Update_Patch(PlayAudioAnimationEvent __instance)
        {
            //JesterAI
            if (__instance.transform.parent.parent.GetComponent<JesterAI>() != null)
            {
                JesterStompCheck.jesterStomped = true;
            }
        }
    }
}