using HarmonyLib;
using ProjectM;
using StarterKit.Database;
using Unity.Collections;

namespace StarterKit.Hooks
{
    [HarmonyPatch(typeof(TriggerPersistenceSaveSystem), nameof(TriggerPersistenceSaveSystem.TriggerSave))]
    public class TriggerPersistenceSaveSystem_Patch
    {
        [HarmonyPostfix]
        public static void Postfix(SaveReason reason, FixedString128Bytes saveName, ServerRuntimeSettings saveConfig)
        {
            DB.SaveData();
        }
    }
}
