using BaseLib.Utils;
using Inscribed.InscribedCode.Cards.Basic;
using Inscribed.InscribedCode.Character;
using Inscribed.InscribedCode.Relics;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace Inscribed.InscribedCode.Relics;

[Pool(typeof(InscribedRelicPool))]
public class QuiverOfOrder() : InscribedRelic
{
    public override RelicRarity Rarity =>
        RelicRarity.Uncommon;
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [new IntVar("RunesPlayed", 0)];

    public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
    {
        if (!InscribedKeywords.isRune(cardPlay.Card))
        {
            return;
        }

        this.DynamicVars["RunesPlayed"].UpgradeValueBy(1);

        if (this.DynamicVars["RunesPlayed"].IntValue == 3)
        {
            MainFile.Logger.Info("Test");
            var card = this.Owner.Creature.CombatState.CreateCard<InscribedStrike>(this.Owner);
            card.SetToFreeThisTurn();
            await CardPileCmd.AddGeneratedCardToCombat(card, PileType.Hand, true);
        }
    }

    public override Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        this.DynamicVars["RunesPlayed"].ResetToBase();
        return base.AfterPlayerTurnStart(choiceContext, player);
    }
}