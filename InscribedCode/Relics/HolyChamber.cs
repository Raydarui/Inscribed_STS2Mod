using BaseLib.Utils;
using Inscribed.InscribedCode.Character;
using Inscribed.InscribedCode.Relics;
using Inscribed.InscribedCode.Runes;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Inscribed.InscribedCode.Relics;


[Pool(typeof(InscribedRelicPool))]
public class HolyChamber() : InscribedRelic
{
    public override RelicRarity Rarity =>
        RelicRarity.Starter;

    public override async Task BeforePlayPhaseStart(PlayerChoiceContext choiceContext, Player player)
    {
        Random rnd = new();
        int orbNo = rnd.Next(1, 6);
        
        switch (orbNo)
        {
            case 1:
                await OrbCmd.Channel<Runes.FireRune>(choiceContext, player);
                break;
            case 2:
                await OrbCmd.Channel<Runes.IceRune>(choiceContext, player);
                break;
            case 3:
                await OrbCmd.Channel<Runes.LightRune>(choiceContext, player);
                break;
            case 4:
                await OrbCmd.Channel<Runes.ThunderRune>(choiceContext, player);
                break;
            case 5:
                await OrbCmd.Channel<Runes.EarthRune>(choiceContext, player);
                break;
            case 6:
                await OrbCmd.Channel<Runes.DarkRune>(choiceContext, player);
                break;
            default:
                return;
        }
        
        return;
        
    }

}