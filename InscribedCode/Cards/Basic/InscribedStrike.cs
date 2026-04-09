using BaseLib.Utils;
using Godot;
using Inscribed.InscribedCode.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.DevConsole;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Orbs;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.GameInfo.Objects;

namespace Inscribed.InscribedCode.Cards.Basic;

  
public class InscribedStrike() : InscribedCard(1,
    CardType.Attack, CardRarity.Basic,
    TargetType.AnyEnemy)
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => [InscribedKeywords.Inscription];
    protected override HashSet<CardTag> CanonicalTags => [CardTag.Strike];
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(6, ValueProp.Move)];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromKeyword(InscribedKeywords.Inscription)
    ];

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
                // MainFile.Logger.Info(orb.DarkenedColor.ToString());
                
                switch (orb.DarkenedColor.ToString())
                {
                    // fire rune
                    case "(0.54509807, 0, 0, 1)":
                        strengthAdded += 5;
                        // MainFile.Logger.Info("Strenght added");
                        break;
                    // ice rune
                    case "(0, 1, 1, 1)":
                        blockAdded += 5;
                        // MainFile.Logger.Info("Block added");
                        break;
                    // Lightning Rune
                    case "(1, 1, 0, 1)":
                        hitCount++;
                        // MainFile.Logger.Info("Hit twice");
                        break;
                    // Earth rune
                    case "(0.54509807, 0.27058825, 0.07450981, 1)":
                        weakAdded++;
                        // MainFile.Logger.Info("Weak added");
                        break;
                    // Light rune
                    case "(0.827451, 0.827451, 0.827451, 1)":
                        cardAndEnergyAdded++;
                        // MainFile.Logger.Info("Card and Energy added");
                        break;
                    // Dark Rune
                    case "(0.6627451, 0.6627451, 0.6627451, 1)":
                        debilitateAdded += 2;
                        break;
                }
                    await OrbCmd.EvokeNext(choiceContext, this.Owner, true); 
            }
        }
       
        // apply block
        await CreatureCmd.GainBlock(this.Owner.Creature, new BlockVar(blockAdded, ValueProp.Move), play);
        // await Cmd.Wait(0.2f);
        
        // apply strenght
        await CommonActions.ApplySelf<StrengthPower>(this, strengthAdded);
        // await Cmd.Wait(0.25f);

        // apply debilitate
        ArgumentNullException.ThrowIfNull(play.Target, "play.Target");
        await CommonActions.Apply<DebilitatePower>(play.Target, this,  debilitateAdded);
        // await Cmd.Wait(0.25f);
        
        
        // Apply weak
        ArgumentNullException.ThrowIfNull(play.Target, "play.Target");
        await CommonActions.Apply<WeakPower>(play.Target, this,  weakAdded);
        // await Cmd.Wait(0.25f);
        
        // attack
        for (int i = 0; i < hitCount; ++i)
        {
            await CommonActions.CardAttack(this, play.Target).Execute(choiceContext);
            // await Cmd.Wait(0.25f);
        }
        
        
        // clear strength
        await CommonActions.ApplySelf<StrengthPower>(this, -strengthAdded);
        // await Cmd.Wait(0.25f);
        
        // draw card and energy
        for (int i = 0; i < cardAndEnergyAdded; i++)
        {
            await CardPileCmd.Draw(choiceContext, this.Owner);
            await PlayerCmd.GainEnergy(1, this.Owner);
        }
    }

    protected override void OnUpgrade()
    {
        this.DynamicVars.Damage.UpgradeValueBy(3M);
    }
}