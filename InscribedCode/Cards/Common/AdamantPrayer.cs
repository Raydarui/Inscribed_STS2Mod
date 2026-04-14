using BaseLib.Utils;
using Inscribed.InscribedCode.Cards;
using Inscribed.InscribedCode.Character;
using Inscribed.InscribedCode.Runes;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Inscribed.InscribedCode.Cards.Common;

public class AdamantPrayer() : InscribedCard(0,
    CardType.Attack, CardRarity.Common,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [];
    
    protected override HashSet<CardTag> CanonicalTags => [InscribedTags.Rune];
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromOrb<EarthRune>()
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CardPileCmd.Draw(choiceContext, this.Owner);
        await OrbCmd.Channel<EarthRune>(choiceContext, this.Owner);
    }

    protected override void OnUpgrade()
    {
        this.AddKeyword(CardKeyword.Retain);
    }
}