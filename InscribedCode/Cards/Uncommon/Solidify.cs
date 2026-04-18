using Inscribed.InscribedCode.Cards;
using Inscribed.InscribedCode.Runes;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Orbs;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace Inscribed.InscribedCode.Cards.uncommon;

  
public class Solidify() : InscribedCard(1,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromOrb<IceRune>()
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        OrbQueue orbQueue = this.Owner.PlayerCombatState.OrbQueue;
        int orbCount = orbQueue.Orbs.Count();
        for (int i = 0; i < orbCount; i++)
        {
            await OrbCmd.EvokeNext(choiceContext, this.Owner, true);
        }
        for (int i = 0; i < orbCount; i++)
        {
            await OrbCmd.Channel<IceRune>(choiceContext, this.Owner);
        }
    }

    protected override void OnUpgrade()
    {
        this.DynamicVars.Energy.UpgradeValueBy(-1);
    }
}