using BaseLib.Utils;
using Godot;
using Inscribed.InscribedCode.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Orbs;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace Inscribed.InscribedCode.Cards.Basic;

  
public class InscribedStrike() : InscribedCard(1,
    CardType.Attack, CardRarity.Basic,
    TargetType.AnyEnemy)
{
    protected override HashSet<CardTag> CanonicalTags => [CardTag.Strike];
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(6, ValueProp.Move)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        int strenghtAdded = 0;
        OrbQueue orbQueue = this.Owner.PlayerCombatState.OrbQueue;
        if (orbQueue.Orbs.Count != 0)
        {
            OrbModel orb = orbQueue.Orbs.First();

            if (orb.DarkenedColor == new Color("ff0000"))
            {
                strenghtAdded = 5;
            }
        }
        
        
        await CommonActions.ApplySelf<StrengthPower>(this, strenghtAdded);
        await Cmd.Wait(0.25f);
        await CommonActions.CardAttack(this, play.Target).Execute(choiceContext);
        await Cmd.Wait(0.25f);
        await OrbCmd.EvokeNext(choiceContext, this.Owner, true);
        await Cmd.Wait(0.25f);
        await CommonActions.ApplySelf<StrengthPower>(this, -strenghtAdded);
        await Cmd.Wait(0.25f);
    }

    protected override void OnUpgrade()
    {

    }
}