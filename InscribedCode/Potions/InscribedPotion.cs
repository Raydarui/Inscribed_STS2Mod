using BaseLib.Abstracts;
using BaseLib.Utils;
using Inscribed.InscribedCode.Character;

namespace Inscribed.InscribedCode.Potions;

[Pool(typeof(InscribedPotionPool))]
public abstract class InscribedPotion : CustomPotionModel;