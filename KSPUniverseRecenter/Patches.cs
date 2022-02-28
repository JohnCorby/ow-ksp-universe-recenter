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

        private const float OffsetThreshold = 5000;

        [HarmonyPostfix]
        [HarmonyPatch(typeof(CenterOfTheUniverse), nameof(CenterOfTheUniverse.Start))]
        private static void CenterOfTheUniverse_Start(CenterOfTheUniverse __instance) =>
            GlobalMessenger.RemoveListener("PlayerRepositioned", __instance.OnPlayerRepositioned);

        [HarmonyPostfix]
        [HarmonyPatch(typeof(CenterOfTheUniverse), nameof(CenterOfTheUniverse.FixedUpdate))]
        private static void CenterOfTheUniverse_FixedUpdate(CenterOfTheUniverse __instance)
        {
            var offset = __instance._centerBody._transform.position.magnitude;
            Mod.Helper.Console.WriteLine($"offset = {offset}");
            if (offset > OffsetThreshold)
                __instance._recenterUniverseNextUpdate = true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(CenterOfTheUniverse), nameof(CenterOfTheUniverse.RecenterUniverseAroundPlayer))]
        private static void CenterOfTheUniverse_RecenterUniverseAroundPlayer(CenterOfTheUniverse __instance) =>
            Mod.Helper.Console.WriteLine("recenter");

        #endregion
    }
}