using BaseLib.Patches.Content;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Models;

namespace Inscribed.InscribedCode;

  
  
public class InscribedKeywords
{
    [CustomEnum, KeywordProperties(AutoKeywordPosition.Before)]
    public static CardKeyword Inscription;

    [CustomEnum, KeywordProperties(AutoKeywordPosition.Before)]
    public static CardKeyword Rune;
    


    public static bool IsInscription(CardModel card)
    {
        return card.Keywords.Contains(Inscription);
    }
    
    public static bool IsRune(CardModel card)
    {
        return card.Keywords.Contains(Rune);
    }


}