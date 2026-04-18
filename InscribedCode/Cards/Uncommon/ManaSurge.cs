using Inscribed.InscribedCode.Cards;
using Inscribed.InscribedCode.Runes;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Orbs;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Inscribed.InscribedCode.Cards.uncommon;

  
public class ManaSurge() : InscribedCard(-1,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];
    protected override bool HasEnergyCostX => true;

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        int count = this.ResolveEnergyXValue();
        OrbQueue orbQueue = this.Owner.PlayerCombatState.OrbQueue;
        int orbCount = orbQueue.Orbs.Count();
        for (int i = 0; i < orbCount; i++)
        {
            await OrbCmd.EvokeNext(choiceContext, this.Owner, true);
            await PlayerCmd.GainEnergy(1, this.Owner);
        }
    }

    protected override void OnUpgrade()
    {
        this.DynamicVars.Energy.UpgradeValueBy(-1);
    }
}