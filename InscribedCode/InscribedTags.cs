using BaseLib.Patches.Content;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Models;

namespace Inscribed.InscribedCode;

public class InscribedTags
{
    [CustomEnum] public static CardTag Rune;
    
    public static bool IsRune(CardModel card)
    {
        return card.Tags.Contains(Rune);
    }
}