using Inscribed.InscribedCode.Runes;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Inscribed.InscribedCode.Cards.Basic;

public class FireRuneCard() : InscribedCard(1,
    CardType.Skill, CardRarity.Basic,
    TargetType.Self)
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => [];
    protected override IEnumerable<DynamicVar> CanonicalVars => [];
    
    protected override HashSet<CardTag> CanonicalTags => [InscribedTags.Rune];
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromOrb<FireRune>()
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        FireRuneCard card = this;
        await OrbCmd.Channel<Runes.FireRune>(choiceContext, card.Owner);
    }

    protected override void OnUpgrade()
    {

    }
}