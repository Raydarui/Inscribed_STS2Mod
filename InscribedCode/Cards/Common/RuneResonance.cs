using BaseLib.Utils;
using Inscribed.InscribedCode.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Orbs;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace Inscribed.InscribedCode.Cards.Common;

  
public class RuneResonance() : InscribedCard(1,
    CardType.Attack, CardRarity.Common,
    TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(7, ValueProp.Move), new IntVar("BonusDamage", 7)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.CardAttack(this, play.Target).Execute(choiceContext);
        
        OrbQueue orbQueue = this.Owner.PlayerCombatState.OrbQueue;
        int orbCount = orbQueue.Orbs.Count();
        int orbSlots = this.Owner.PlayerCombatState.OrbQueue.Capacity;

        if (orbCount == orbSlots)
        {
            await DamageCmd.Attack(this.DynamicVars["BonusDamage"].IntValue).FromCard(this).Targeting(play.Target).Execute(choiceContext);
        }
    }

    protected override void OnUpgrade()
    {
        this.DynamicVars["BonusDamage"].UpgradeValueBy(2);
    }
}