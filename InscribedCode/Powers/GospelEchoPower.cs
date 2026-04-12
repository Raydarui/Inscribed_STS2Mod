using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Inscribed.InscribedCode.Powers;


public class GospelEchoPower() : InscribedPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;

    public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (side == CombatSide.Enemy || this.Owner.IsMonster)
        {
            return;
        }
        
        Random rnd = new();
        int orbNo = rnd.Next(1, 6);
        
        switch (orbNo)
        {
            case 1:
                await OrbCmd.Channel<Runes.FireRune>(choiceContext, this.Owner.Player);
                break;
            case 2:
                await OrbCmd.Channel<Runes.IceRune>(choiceContext, this.Owner.Player);
                break;
            case 3:
                await OrbCmd.Channel<Runes.LightRune>(choiceContext, this.Owner.Player);
                break;
            case 4:
                await OrbCmd.Channel<Runes.ThunderRune>(choiceContext, this.Owner.Player);
                break;
            case 5:
                await OrbCmd.Channel<Runes.EarthRune>(choiceContext, this.Owner.Player);
                break;
            case 6:
                await OrbCmd.Channel<Runes.DarkRune>(choiceContext, this.Owner.Player);
                break;
            default:
                return;
        }
    }
}