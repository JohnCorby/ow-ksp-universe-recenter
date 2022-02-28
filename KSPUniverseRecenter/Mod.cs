using HarmonyLib;
using OWML.Common;
using OWML.ModHelper;
using System.Reflection;

namespace KSPUniverseRecenter
{
    public class Mod : ModBehaviour
    {
        public static IModHelper Helper { get; private set; }
        public static float OffsetThreshold { get; private set; }

        private void Awake() =>
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());

        private void Start() =>
            Helper = ModHelper;

        public override void Configure(IModConfig config) =>
            OffsetThreshold = config.GetSettingsValue<float>("Offset Threshold");
    }
}