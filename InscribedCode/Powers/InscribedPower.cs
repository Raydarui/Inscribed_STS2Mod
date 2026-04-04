using BaseLib.Abstracts;
using BaseLib.Extensions;
using Inscribed.InscribedCode.Extensions;
using Godot;

namespace Inscribed.InscribedCode.Powers;

public abstract class InscribedPower : CustomPowerModel
{
    //Loads from Inscribed/images/powers/your_power.png
    public override string CustomPackedIconPath
    {
        get
        {
            var path = $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".PowerImagePath();
            return ResourceLoader.Exists(path) ? path : "power.png".PowerImagePath();
        }
    }

    public override string CustomBigIconPath
    {
        get
        {
            var path = $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigPowerImagePath();
            return ResourceLoader.Exists(path) ? path : "power.png".BigPowerImagePath();
        }
    }
}