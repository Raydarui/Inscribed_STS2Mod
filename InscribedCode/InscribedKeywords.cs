using BaseLib.Patches.Content;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Models;

namespace Inscribed.InscribedCode;

  
  
public class InscribedKeywords
{
    [CustomEnum, KeywordProperties(AutoKeywordPosition.Before)]
    public static CardKeyword Inscription;
    
    


    public static bool IsInscription(CardModel card)
    {
        return card.Keywords.Contains(Inscription);
    }

}