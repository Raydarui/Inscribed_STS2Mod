using Inscribed.InscribedCode.Cards;
using Inscribed.InscribedCode.Runes;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Orbs;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Inscribed.InscribedCode.Cards.Common;

  
public class Refraction() : InscribedCard(1,
    CardType.Skill, CardRarity.Common,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new IntVar("DrawCount", 1)];

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
            await OrbCmd.Channel<LightRune>(choiceContext, this.Owner);
        }

        for (int i = 0; i < this.DynamicVars["DrawCount"].IntValue; i++)
        {
            await CardPileCmd.Draw(choiceContext, this.Owner);
        }
    }

    protected override void OnUpgrade()
    {
        this.DynamicVars["DrawCount"].UpgradeValueBy(1);
    }
}