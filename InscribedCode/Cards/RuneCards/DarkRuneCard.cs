using Inscribed.InscribedCode.Runes;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Inscribed.InscribedCode.Cards.Basic;

public class DarkRuneCard() : InscribedCard(1,
    CardType.Skill, CardRarity.Basic,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [];
    protected override HashSet<CardTag> CanonicalTags => [InscribedTags.Rune];

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromOrb<DarkRune>()
    ];
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        DarkRuneCard card = this;
        await OrbCmd.Channel<Runes.DarkRune>(choiceContext, card.Owner);
    }

    protected override void OnUpgrade()
    {

    }
}