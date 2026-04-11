using BaseLib.Utils;
using Inscribed.InscribedCode.Character;
using Inscribed.InscribedCode.Relics;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Inscribed.InscribedCode.Relics;


[Pool(typeof(InscribedRelicPool))]
public class EchoOfTheGospel() : InscribedRelic
{
    public override RelicRarity Rarity =>
        RelicRarity.Rare;

    public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (side == CombatSide.Enemy)
        {
            return;
        }
        
        Random rnd = new();
        int orbNo = rnd.Next(1, 6);
        
        switch (orbNo)
        {
            case 1:
                await OrbCmd.Channel<Runes.FireRune>(choiceContext, this.Owner);
                break;
            case 2:
                await OrbCmd.Channel<Runes.IceRune>(choiceContext, this.Owner);
                break;
            case 3:
                await OrbCmd.Channel<Runes.LightRune>(choiceContext, this.Owner);
                break;
            case 4:
                await OrbCmd.Channel<Runes.ThunderRune>(choiceContext, this.Owner);
                break;
            case 5:
                await OrbCmd.Channel<Runes.EarthRune>(choiceContext, this.Owner);
                break;
            case 6:
                await OrbCmd.Channel<Runes.DarkRune>(choiceContext, this.Owner);
                break;
            default:
                return;
        }
    }
}