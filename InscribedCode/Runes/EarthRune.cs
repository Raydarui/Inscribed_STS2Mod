using BaseLib.Abstracts;
using Godot;
using MegaCrit.Sts2.Core.Assets;
using MegaCrit.Sts2.Core.Bindings.MegaSpine;
using MegaCrit.Sts2.Core.Helpers;

namespace Inscribed.InscribedCode.Runes;

public class EarthRune : CustomOrbModel
{
    public override decimal PassiveVal => 1;
    public override decimal EvokeVal => 0;
    public override Color DarkenedColor => new Color(Colors.SaddleBrown);
    
    public override Node2D? CreateCustomSprite()
    {
        var container = new Node2D();
        string frostPath = SceneHelper.GetScenePath("orbs/orb_visuals/frost_orb");
        Node2D frost = PreloadManager.Cache.GetScene(frostPath)
            .Instantiate<Node2D>(PackedScene.GenEditState.Disabled);
        new MegaSprite(frost.GetNode("SpineSkeleton"))
            .GetAnimationState().SetAnimation("idle_loop");
        // change the color and size
        frost.Modulate = new Color(Godot.Colors.SaddleBrown);
        frost.Scale = new Vector2(1.1f, 1.1f);
        container.AddChild(frost);
        return container;
    }
}