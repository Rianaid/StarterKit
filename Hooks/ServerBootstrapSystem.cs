using HarmonyLib;
using ProjectM;
using ProjectM.Network;
using StarterKit.Database;
using Stunlock.Network;

namespace StarterKit.Hooks
{
    [HarmonyPatch(typeof(ServerBootstrapSystem), nameof(ServerBootstrapSystem.OnUserConnected))]
    public static class OnUserConnected_Patch
    {
        public static void Postfix(ServerBootstrapSystem __instance, NetConnectionId netConnectionId)
        {
            try
            {
                var em = __instance.EntityManager;
                var userIndex = __instance._NetEndPointToApprovedUserIndex[netConnectionId];
                var serverClient = __instance._ApprovedUsersLookup[userIndex];
                var userEntity = serverClient.UserEntity;
                var userData = __instance.EntityManager.GetComponentData<User>(userEntity);
                if (!DB.UsedKits.ContainsKey(userData.PlatformId))
                {
                    DB.UsedKits.TryAdd(userData.PlatformId, false);
                }
            }
            catch { }
        }
    }
}
