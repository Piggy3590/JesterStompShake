using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;
using System.Collections.Generic;
using GameNetcodeStuff;

namespace JesterStompShake.Patches
{
    [HarmonyPatch(typeof(PlayerControllerB))]
    internal class PlayerControllerBPatch
    {
        public static int shakeIntensity;
        public static List<JesterAI> cachedJesters = new List<JesterAI>();
        private static float updateInterval = 0.1f; // Update every 0.5 seconds
        private static float lastUpdateTime;
        private static float distance;

        [HarmonyPostfix]
        [HarmonyPatch("Update")]
        private static void Update_Postfix(ref Transform ___thisPlayerBody, ref bool ___isCameraDisabled)
        {
            if (___isCameraDisabled || cachedJesters.Count == 0) return;

            if (Time.time - lastUpdateTime > updateInterval)
            {
                foreach (JesterAI jester in cachedJesters)
                {
                    if (jester == null) continue;

                    distance = Vector3.Distance(___thisPlayerBody.position, jester.transform.position);
                    if (jester.currentBehaviourStateIndex != 2) continue;

                }
            }
            else
            {
                lastUpdateTime = Time.time;
            }
            if (JesterStompCheck.jesterStomped)
            {
                HandleShake(distance);
            }
        }

        private static void HandleShake(float distance)
        {
            if (distance < 4)
            {
                Shake(ScreenShakeType.Small, ScreenShakeType.Big, ScreenShakeType.VeryStrong);
            }
            else if (distance < 6)
            {
                Shake(ScreenShakeType.Small, ScreenShakeType.Big, ScreenShakeType.Big);
            }
            else if (distance < 35)
            {
                Shake(ScreenShakeType.Small, ScreenShakeType.Small, ScreenShakeType.Small, true);
            }
        }

        private static void Shake(ScreenShakeType small, ScreenShakeType medium, ScreenShakeType large, bool ignoreSmallOne = false)
        {
            switch (shakeIntensity)
            {
                case 1:
                    if (!ignoreSmallOne)
                    {
                        HUDManager.Instance.ShakeCamera(small);
                    }
                    break;
                case 2:
                    HUDManager.Instance.ShakeCamera(medium);
                    break;
                case 3:
                    HUDManager.Instance.ShakeCamera(large);
                    break;
            }
            JesterStompCheck.jesterStomped = false;
        }
    }
}
