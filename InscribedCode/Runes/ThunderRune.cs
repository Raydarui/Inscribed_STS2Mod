using BaseLib.Abstracts;
using Godot;
using MegaCrit.Sts2.Core.Assets;
using MegaCrit.Sts2.Core.Bindings.MegaSpine;
using MegaCrit.Sts2.Core.Helpers;

namespace Inscribed.InscribedCode.Runes;

public class ThunderRune : CustomOrbModel
{
    public override decimal PassiveVal => 2;
    public override decimal EvokeVal => 0;
    public override Color DarkenedColor => new Color(Colors.Yellow);
    
    public override Node2D? CreateCustomSprite()
    {
        var container = new Node2D();
        string plasmaPath = SceneHelper.GetScenePath("orbs/orb_visuals/plasma_orb");
        Node2D plasma = PreloadManager.Cache.GetScene(plasmaPath)
            .Instantiate<Node2D>(PackedScene.GenEditState.Disabled);
        new MegaSprite(plasma.GetNode("SpineSkeleton"))
            .GetAnimationState().SetAnimation("idle_loop");
        // change the color and size
        // plasma.Modulate = new Color(1.0f, 1.0f, 0.0f, 1.0f);
        plasma.Scale = new Vector2(1.1f, 1.1f);
        container.AddChild(plasma);
        return container;
    }
}