using System;
using System.Collections.Generic;
using GXPEngine.Core;
using GXPEngine.OpenGL;

namespace GXPEngine
{
	/// <summary>
	/// Implements a line with normal representation
	/// </summary>
	public class NLineSegment : LineSegment
	{
		public Arrow _normal;
		MyGame myGame;
		Ball ball;

		public NLineSegment (float pStartX, float pStartY, float pEndX, float pEndY, uint pColor = 0xffffffff, uint pLineWidth = 1)
			: this (new Vec2 (pStartX, pStartY), new Vec2 (pEndX, pEndY), pColor, pLineWidth)
		{
		}

		public NLineSegment (Vec2 pStart, Vec2 pEnd, uint pColor = 0xffffffff, uint pLineWidth = 1)
			: base (pStart, pEnd, pColor, pLineWidth)
		{
			myGame = (MyGame)game;
			_normal = new Arrow (new Vec2(0,0), new Vec2(0,0), 40, 0xffff0000, 1);
			AddChild (_normal);
			ball = new Ball(1, pStart, Vec2.Zero());
            myGame.InstantiateBall(ball);
		}

		//------------------------------------------------------------------------------------------------------------------------
		//														RenderSelf()
		//------------------------------------------------------------------------------------------------------------------------
		override protected void RenderSelf(GLContext glContext) {
			if (game != null) {
				recalculateArrowPosition ();
				Gizmos.RenderLine(start.x, start.y, end.x, end.y, color, lineWidth);
				ball.position = start;
            }
		}

		private void recalculateArrowPosition() {
			_normal.startPoint = (start + end) * 0.5f;
			_normal.vector = (end-start).Normal ();
        }

	}
}

