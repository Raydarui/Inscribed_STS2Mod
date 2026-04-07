using BaseLib.Abstracts;
using Godot;
using MegaCrit.Sts2.Core.Assets;
using MegaCrit.Sts2.Core.Bindings.MegaSpine;
using MegaCrit.Sts2.Core.Helpers;

namespace Inscribed.InscribedCode.Runes;

public class EarthRune : CustomOrbModel
{
    public override decimal PassiveVal => 0;
    public override decimal EvokeVal => 0;
    public override Color DarkenedColor => new Color(Colors.SaddleBrown);
    
    public override Node2D? CreateCustomSprite()
    {
        var container = new Node2D();
        string lightningPath = SceneHelper.GetScenePath("orbs/orb_visuals/lightning_orb");
        Node2D lightning = PreloadManager.Cache.GetScene(lightningPath)
            .Instantiate<Node2D>(PackedScene.GenEditState.Disabled);
        new MegaSprite(lightning.GetNode("SpineSkeleton"))
            .GetAnimationState().SetAnimation("idle_loop");
        // change the color and size
        lightning.Modulate = new Color(0.64f, 0.16f, 0.16f, 1.0f);
        lightning.Scale = new Vector2(1.1f, 1.1f);
        container.AddChild(lightning);
        return container;
    }
}