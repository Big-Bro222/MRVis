using MessagePack;
using Synchro.Test;

namespace Synchro
{
    [MessagePack.Union(0, typeof(TestCommand))]
    [MessagePack.Union(1, typeof(SpatialStatus))]
    [MessagePack.Union(2, typeof(TransformsStatusUpdate))]
    [MessagePack.Union(3, typeof(Register))]
    [MessagePack.Union(4, typeof(UpdatePresence))]  
    [MessagePack.Union(5, typeof(SpawnObject))]
    [MessagePack.Union(6, typeof(DeleteObject))]
    [MessagePack.Union(7, typeof(ChangePermission))]
    [MessagePack.Union(8, typeof(ReCalibrate))]
    [MessagePack.Union(9, typeof(Ping))]

    public interface ISynchroCommand
    {
        void Apply();
    }
}