using ProjectM;
using ProjectM.Network;
using ProjectM.Scripting;
using StarterKit.Database;
using Stunlock.Core;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using VampireCommandFramework;

namespace StarterKit.Utils
{
    internal class Helper
    {
        private static World? _serverWorld;
        public static EntityManager EntityManager => Server.EntityManager;
        public static GameDataSystem gameData => Server.GetExistingSystemManaged<GameDataSystem>();
        public static PrefabCollectionSystem PrefabCollection => Server.GetExistingSystemManaged<PrefabCollectionSystem>();
        public static ServerGameManager serverGameManager = Server.GetExistingSystemManaged<ServerScriptMapper>()._ServerGameManager;
        public static Il2CppSystem.Collections.Generic.Dictionary<string, PrefabGUID> NameToGuid => PrefabCollection.NameToPrefabGuidDictionary;
        public static void GiveStartKit(ChatCommandContext ctx, List<RecordKit> kit)
        {
            foreach (var item in kit)
            {
                var itemEntity = Helper.AddItemToInventory(ctx.Event.SenderCharacterEntity, NameToGuid[item.Name], item.Amount);
                var slot = InventoryUtilities.GetItemSlot(Helper.EntityManager, ctx.Event.SenderCharacterEntity, NameToGuid[item.Name], itemEntity);
                EquipEquipment(ctx.Event.SenderCharacterEntity, slot);
            }
        }

        public static Entity AddItemToInventory(Entity recipient, PrefabGUID guid, int amount)
        {
            try
            {
                var inventoryResponse = serverGameManager.TryAddInventoryItem(recipient, guid, amount);
                return inventoryResponse.NewEntity;
            }
            catch (System.Exception e)
            {
                Plugin.Logger.LogFatal(e);
            }
            return new Entity();
        }
        public static void EquipEquipment(Entity player, int slot)
        {
            var entity = Helper.Server.EntityManager.CreateEntity(ComponentType.ReadWrite<FromCharacter>(), ComponentType.ReadWrite<EquipItemEvent>());
            PlayerCharacter playerchar = Helper.Server.EntityManager.GetComponentData<PlayerCharacter>(player);
            Entity userEntity = playerchar.UserEntity;
            Helper.Server.EntityManager.SetComponentData<FromCharacter>(entity, new() { User = userEntity, Character = player });
            Helper.Server.EntityManager.SetComponentData<EquipItemEvent>(entity, new() { SlotIndex = slot });
        }

        public static World Server
        {
            get
            {
                if (_serverWorld != null) return _serverWorld;

                _serverWorld = GetWorld("Server")
                    ?? throw new System.Exception("There is no Server world (yet). Did you install a server mod on the client?");
                return _serverWorld;
            }
        }

        public static bool IsServer => Application.productName == "VRisingServer";

        private static World GetWorld(string name)
        {
            foreach (var world in World.s_AllWorlds)
            {
                if (world.Name == name)
                {
                    return world;
                }
            }

            return null;
        }
    }
}
