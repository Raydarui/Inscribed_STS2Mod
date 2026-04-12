using BaseLib.Utils;
using Inscribed.InscribedCode.Relics;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Rooms;

namespace Inscribed.InscribedCode.Powers;

  
public class SearchForTruthPower() : InscribedPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;
    protected override IEnumerable<DynamicVar> CanonicalVars => [new IntVar("SlotsApplied", 0)];

    public override async Task AfterApplied(Creature? applier, CardModel? cardSource)
    {
        if (this.Owner.IsPlayer)
        {
            MainFile.Logger.Info(this.Owner.Player.BaseOrbSlotCount.ToString());
            if(this.Owner.Player.BaseOrbSlotCount <= 3)
            {
                await OrbCmd.AddSlots(this.Owner.Player, 2);
                this.DynamicVars["SlotsApplied"].UpgradeValueBy(1);
            }
        }
    }

    public override async Task AfterCombatEnd(CombatRoom room)
    {
        if (this.DynamicVars["SlotsApplied"].IntValue == 1 && this.Owner.IsPlayer)
        {
            OrbCmd.RemoveSlots(this.Owner.Player, 2);
        }
    }
}