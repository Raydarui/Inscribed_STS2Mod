using BaseLib.Abstracts;
using Inscribed.InscribedCode.Extensions;
using Godot;

namespace Inscribed.InscribedCode.Character;

public class InscribedRelicPool : CustomRelicPoolModel
{
    public override Color LabOutlineColor => Inscribed.Color;

    public override string BigEnergyIconPath => "charui/big_energy.png".ImagePath();
    public override string TextEnergyIconPath => "charui/text_energy.png".ImagePath();
}