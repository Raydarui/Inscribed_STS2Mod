using BaseLib.Utils;
using Inscribed.InscribedCode.Character;
using Inscribed.InscribedCode.Relics;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;

namespace Inscribed.InscribedCode.Relics;


[Pool(typeof(InscribedRelicPool))]
public class SeekerOfTruth() : InscribedRelic
{
    public override RelicRarity Rarity =>
        RelicRarity.Rare;

    public override async Task AfterObtained()
    {
        if(this.Owner.BaseOrbSlotCount <= 3)
        {
            OrbCmd.IncreaseBaseOrbCount(this.Owner, 2);
        }
    }
}