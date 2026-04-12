using BaseLib.Utils;
using Inscribed.InscribedCode.Cards;
using Inscribed.InscribedCode.Character;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Orbs;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace Inscribed.InscribedCode.Cards.Rare;

public class AstralFall() : InscribedCard(3,
    CardType.Attack, CardRarity.Basic,
    TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(15, ValueProp.Move)];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [InscribedKeywords.Inscription, CardKeyword.Exhaust];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        int strengthAdded = 0;
        int weakAdded = 0;
        int hitCount = 1;
        int blockAdded = 0;
        int cardAndEnergyAdded = 0;
        int debilitateAdded = 0;
        
        OrbQueue orbQueue = this.Owner.PlayerCombatState.OrbQueue;
        int orbCount = orbQueue.Orbs.Count();
        MainFile.Logger.Info(orbCount.ToString());
        // calculate power ups
        if (orbQueue.Orbs.Count != 0)
        {
            for (int i = 0; i < orbCount; i++)
            {
                MainFile.Logger.Info("For loop");
                OrbModel orb = orbQueue.Orbs.First();
                
                switch (orb.DarkenedColor.ToString())
                {
                    // fire rune
                    case "(0.54509807, 0, 0, 1)":
                        strengthAdded += 10;
                        break;
                    // ice rune
                    case "(0, 1, 1, 1)":
                        blockAdded += 10;
                        break;
                    // Lightning Rune
                    case "(1, 1, 0, 1)":
                        hitCount++;
                        hitCount++;
                        break;
                    // Earth rune
                    case "(0.54509807, 0.27058825, 0.07450981, 1)":
                        weakAdded++;
                        weakAdded++;
                        break;
                    // Light rune
                    case "(0.827451, 0.827451, 0.827451, 1)":
                        cardAndEnergyAdded++;
                        cardAndEnergyAdded++;
                        break;
                    // Dark Rune
                    case "(0.6627451, 0.6627451, 0.6627451, 1)":
                        debilitateAdded += 4;
                        break;
                }
                    await OrbCmd.EvokeNext(choiceContext, this.Owner, true); 
            }
        }
       
        // apply block
        await CreatureCmd.GainBlock(this.Owner.Creature, new BlockVar(blockAdded, ValueProp.Move), play);
        
        // apply strenght
        await CommonActions.ApplySelf<StrengthPower>(this, strengthAdded);

        // apply debilitate
        ArgumentNullException.ThrowIfNull(play.Target, "play.Target");
        await CommonActions.Apply<DebilitatePower>(play.Target, this,  debilitateAdded);
        
        // Apply weak
        ArgumentNullException.ThrowIfNull(play.Target, "play.Target");
        await CommonActions.Apply<WeakPower>(play.Target, this,  weakAdded);
        
        // attack
        for (int i = 0; i < hitCount; ++i)
        {
            await CommonActions.CardAttack(this, play.Target).Execute(choiceContext);
        }
        
        
        // clear strength
        await CommonActions.ApplySelf<StrengthPower>(this, -strengthAdded);
        
        // draw card and energy
        for (int i = 0; i < cardAndEnergyAdded; i++)
        {
            await CardPileCmd.Draw(choiceContext, this.Owner);
            await PlayerCmd.GainEnergy(1, this.Owner);
        }
    }

    protected override void OnUpgrade()
    {
        this.RemoveKeyword(CardKeyword.Exhaust);
        this.DynamicVars.Energy.UpgradeValueBy(-1);
    }
}