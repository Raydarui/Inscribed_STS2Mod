using BaseLib.Utils;
using Inscribed.InscribedCode.Cards;
using Inscribed.InscribedCode.Character;
using Inscribed.InscribedCode.Runes;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace Inscribed.InscribedCode.Cards.Basic;

public class SolarInscription() : InscribedCard(1,
    CardType.Skill, CardRarity.Basic,
    TargetType.Self)
{
    protected override HashSet<CardTag> CanonicalTags => [CardTag.Defend];
    protected override IEnumerable<DynamicVar> CanonicalVars => [new BlockVar(5, ValueProp.Move)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.CardBlock(this, play);
        await OrbCmd.Channel<FireRune>(choiceContext, this.Owner);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["Block"].UpgradeValueBy(4m);
    }
}