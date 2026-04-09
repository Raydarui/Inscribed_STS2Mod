using Inscribed.InscribedCode.Runes;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Inscribed.InscribedCode.Cards.rare;

  
public class EarthRuneCard() : InscribedCard(1,
    CardType.Skill, CardRarity.Rare,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromOrb<EarthRune>()
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        EarthRuneCard card = this;
        await OrbCmd.Channel<Runes.EarthRune>(choiceContext, card.Owner);
    }

    protected override void OnUpgrade()
    {

    }
}