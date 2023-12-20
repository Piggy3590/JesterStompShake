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
    [HarmonyPatch(typeof(PlayAudioAnimationEvent))]
    internal class PlayAudioAnimationEventPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch("PlayAudio2RandomClip")]
        private static void PlayAudio2RandomClip_Patch()
        {
            JesterStompCheck.jesterStomped = true;
        }
    }
}
