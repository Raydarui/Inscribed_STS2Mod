using BaseLib.Abstracts;
using Inscribed.InscribedCode.Extensions;
using Godot;
using Inscribed.InscribedCode.Cards;
using Inscribed.InscribedCode.Cards.Basic;
using Inscribed.InscribedCode.Runes;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Relics;
using Inscribed.InscribedCode.Cards.Common;


namespace Inscribed.InscribedCode.Character;

public class Inscribed : PlaceholderCharacterModel
{
    public const string CharacterId = "Inscribed";

    public static readonly Color Color = new("ffffff");

    public override Color NameColor => Color;
    public override CharacterGender Gender => CharacterGender.Neutral;
    public override int StartingHp => 70;
    public override int StartingGold => 99;

    public override IEnumerable<CardModel> StartingDeck =>
    [
        /*ModelDb.Card<StrikeInscribed>(),
        ModelDb.Card<StrikeInscribed>(),
        ModelDb.Card<StrikeInscribed>(),
        ModelDb.Card<StrikeInscribed>(),
        ModelDb.Card<StrikeInscribed>(),
        ModelDb.Card<DefendInscribed>(),
        ModelDb.Card<DefendInscribed>(),
        ModelDb.Card<DefendInscribed>(),
        ModelDb.Card<DefendInscribed>(),
        ModelDb.Card<DefendInscribed>(),*/
        ModelDb.Card<FireRuneCard>(),
        ModelDb.Card<InscribedStrike>()
        /*ModelDb.Card<DarkRuneCard>(),
        ModelDb.Card<IceRuneCard>(),
        ModelDb.Card<LightningRuneCard>(),
        ModelDb.Card<LightRuneCard>(),
        ModelDb.Card<EarthRuneCard>()*/
    ];

    public override IReadOnlyList<RelicModel> StartingRelics =>
    [
        ModelDb.Relic<BurningBlood>()
    ];
    
    public override int BaseOrbSlotCount => 3;
    
    public override CardPoolModel CardPool => ModelDb.CardPool<InscribedCardPool>();
    public override RelicPoolModel RelicPool => ModelDb.RelicPool<InscribedRelicPool>();
    public override PotionPoolModel PotionPool => ModelDb.PotionPool<InscribedPotionPool>();

    /*  PlaceholderCharacterModel will utilize placeholder basegame assets for most of your character assets until you
        override all the other methods that define those assets.
        These are just some of the simplest assets, given some placeholders to differentiate your character with.
        You don't have to, but you're suggested to rename these images. */
    public override string CustomIconTexturePath => "character_icon_char_name.png".CharacterUiPath();
    public override string CustomCharacterSelectIconPath => "char_select_char_name.png".CharacterUiPath();
    public override string CustomCharacterSelectLockedIconPath => "char_select_char_name_locked.png".CharacterUiPath();
    public override string CustomMapMarkerPath => "map_marker_char_name.png".CharacterUiPath();
    public override string CustomVisualPath => "res://Inscribed/scenes/inscribed.tscn";
}