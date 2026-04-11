using BaseLib.Utils;
using Inscribed.InscribedCode.Cards;
using Inscribed.InscribedCode.Runes;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Orbs;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace Inscribed.InscribedCode.Cards.Common;

  
public class RiftChain() : InscribedCard(1,
    CardType.Attack, CardRarity.Common,
    TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(8, ValueProp.Move),
        new IntVar("Runes", 2)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.CardAttack(this, play.Target).Execute(choiceContext);
        OrbQueue orbQueue = this.Owner.PlayerCombatState.OrbQueue;
        int orbCount = orbQueue.Orbs.Count();
        if (orbCount >= this.DynamicVars["Runes"].IntValue)
        {
            await OrbCmd.Channel<ThunderRune>(choiceContext, this.Owner);
        }
    }

    protected override void OnUpgrade()
    {
        this.DynamicVars["Runes"].UpgradeValueBy(-1);
    }
}