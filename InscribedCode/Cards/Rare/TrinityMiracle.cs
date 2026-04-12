using Inscribed.InscribedCode.Cards;
using Inscribed.InscribedCode.Runes;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Inscribed.InscribedCode.Cards.Rare;

  
public class TrinityMiracle() : InscribedCard(0,
    CardType.Attack, CardRarity.Rare,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust, InscribedKeywords.Rune];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await OrbCmd.Channel<FireRune>(choiceContext, this.Owner);
        await OrbCmd.Channel<LightRune>(choiceContext, this.Owner);
        await OrbCmd.Channel<DarkRune>(choiceContext, this.Owner);
    }

    protected override void OnUpgrade()
    {
        this.AddKeyword(CardKeyword.Retain);
    }
}