using System.Reflection;
using Mafi;
using Mafi.Base;
using Mafi.Core;
using Mafi.Core.Buildings.Storages;
using Mafi.Core.Mods;

namespace MultiplyStorageCapacity;

public sealed class MultiplyStorageCapacity : DataOnlyMod {
    public override string Name => nameof(MultiplyStorageCapacity);
    public override int Version => 1;

    public MultiplyStorageCapacity(CoreMod coreMod, BaseMod baseMod) {}

    public override void RegisterPrototypes(ProtoRegistrator registrator) {
        const int multiplier = 4;

        FieldInfo? capacityFieldInfo = typeof(StorageProto).GetField(
            nameof(StorageProto.Capacity),
            BindingFlags.Public | BindingFlags.Instance
        );
        if (capacityFieldInfo is null) {
            Log.Warning($"MultiplyStorageCapacityMod: not find {nameof(StorageProto.Capacity)} field");
        } else {
            foreach (StorageProto storageProto in registrator.PrototypesDb.All<StorageProto>()) {
                capacityFieldInfo.SetValue(storageProto, multiplier * storageProto.Capacity);
            }
        }
    }
}