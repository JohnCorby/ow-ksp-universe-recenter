using HarmonyLib;

namespace KSPUniverseRecenter
{
    [HarmonyPatch]
    public static class Patches
    {
        #region offset applier

        [HarmonyPrefix]
        [HarmonyPatch(typeof(CenterOfTheUniverseOffsetApplier), nameof(CenterOfTheUniverseOffsetApplier.OnEnable))]
        private static bool CenterOfTheUniverseOffsetApplier_OnEnable() =>
            false;

        [HarmonyPrefix]
        [HarmonyPatch(typeof(CenterOfTheUniverseOffsetApplier), nameof(CenterOfTheUniverseOffsetApplier.OnDisable))]
        private static bool CenterOfTheUniverseOffsetApplier_OnDisable() =>
            false;

        #endregion

        #region center of universe

        private const float OffsetThreshold = 10000;

        [HarmonyPrefix]
        [HarmonyPatch(typeof(CenterOfTheUniverse), nameof(CenterOfTheUniverse.OnPlayerRepositioned))]
        private static bool CenterOfTheUniverse_OnPlayerRepositioned() =>
            false;

        [HarmonyPrefix]
        [HarmonyPatch(typeof(CenterOfTheUniverse), nameof(CenterOfTheUniverse.FixedUpdate))]
        private static void CenterOfTheUniverse_FixedUpdate(CenterOfTheUniverse __instance)
        {
            var offset = __instance._centerBody._transform.position.magnitude;
            if (offset > OffsetThreshold)
                __instance._recenterUniverseNextUpdate = true;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(CenterOfTheUniverse), nameof(CenterOfTheUniverse.RecenterUniverseAroundPlayer))]
        private static void CenterOfTheUniverse_RecenterUniverseAroundPlayer(CenterOfTheUniverse __instance)
        {
            var offset = __instance._centerBody.transform.position.magnitude;
            Mod.Helper.Console.WriteLine($"recentering (offset = {offset})");
        }

        #endregion
    }
}