using HarmonyLib;
using OWML.Common;
using OWML.ModHelper;
using System.Reflection;

namespace KSPUniverseRecenter
{
    public class Mod : ModBehaviour
    {
        private void Awake() =>
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());

        public static IModHelper Helper { get; private set; }

        private void Start() =>
            Helper = ModHelper;
    }
}