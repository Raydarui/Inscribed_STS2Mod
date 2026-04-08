using BaseLib.Abstracts;
using Godot;
using MegaCrit.Sts2.Core.Assets;
using MegaCrit.Sts2.Core.Bindings.MegaSpine;
using MegaCrit.Sts2.Core.Helpers;

namespace Inscribed.InscribedCode.Runes;

public class LightRune : CustomOrbModel
{
    public override decimal PassiveVal => 1;
    public override decimal EvokeVal => 0;
    public override Color DarkenedColor => new Color(Colors.LightGray);
    
    public override Node2D? CreateCustomSprite()
    {
        var container = new Node2D();
        string glassPath = SceneHelper.GetScenePath("orbs/orb_visuals/glass_orb");
        Node2D glass = PreloadManager.Cache.GetScene(glassPath)
            .Instantiate<Node2D>(PackedScene.GenEditState.Disabled);
        new MegaSprite(glass.GetNode("SpineSkeleton"))
            .GetAnimationState().SetAnimation("idle_loop");
        // change the color and size
        // glass.Modulate = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        glass.Scale = new Vector2(1.1f, 1.1f);
        container.AddChild(glass);
        return container;
    }
}