using Godot;
using System;
using System.Linq.Expressions;

public partial class ParallaxImage : Parallax2D
{
	[Export] private Texture2D _scrTexture;
	[Export] private Sprite2D _sprite;
	[Export] private float _speedScale;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Autoscroll = new Vector2(-_speedScale * 102.0f, 0);
		float scaleFactor = GetViewportRect().Size.Y / _scrTexture.GetHeight();

		_sprite.Texture = _scrTexture;
		_sprite.Scale = new Vector2(scaleFactor, scaleFactor);

		RepeatSize = new Vector2(_scrTexture.GetWidth()*scaleFactor,0);
	}
}
