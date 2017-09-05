using Android.Views;
using Android.Graphics;
using Android.Content;
using Android.Util;
using System.Collections.Generic;
using System;

namespace XamPaintMultiTouch
{
	public class PaintView : View
	{
		//
		// Lines the user is currently drawing
		// Used to add new points to the lines during Move events
		//
		Dictionary<int, Path> currentLines = new Dictionary<int, Path>();

		//
		// All Paths and the corresponding Paints used to draw each one
		// Includes both current Paths the user is still drawing and old Paths they have completed
		// Used to redraw the screen
		//
		List<Path > allLines  = new List<Path >();
		List<Paint> allPaints = new List<Paint>();

		public PaintView(Context context)                                    : base(context, null, 0)         { }
		public PaintView(Context context, IAttributeSet attrs)               : base(context, attrs)           { }
		public PaintView(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle) { }

		public void Clear()
		{
			currentLines.Clear();
			allLines    .Clear();
			allPaints   .Clear();

			base.Invalidate(); // Force screen to be redrawn (i.e. Android will call OnDraw)
		}

		protected override void OnDraw(Canvas canvas)
		{
			for (int i = 0; i < allLines.Count; i++)
			{
				canvas.DrawPath(allLines[i], allPaints[i]); // Draw all current and previous lines
			}
		}

		public override bool OnTouchEvent(MotionEvent e)
		{
			switch (e.ActionMasked)
			{
				case MotionEventActions.Down: // First finger down, use 0 for the index
					{
						float x  = e.GetX(0);
						float y  = e.GetY(0);
						int   id = e.GetPointerId(0);

						var path = new Path();

						path.MoveTo(x, y);

						currentLines.Add(id, path);
						allLines    .Add(path);
						allPaints   .Add(GenerateRandomColorPaint());

						return true;
					}

				case MotionEventActions.PointerDown: // Other finger down, use ActionIndex for the index
					{
						float x  = e.GetX(e.ActionIndex);
						float y  = e.GetY(e.ActionIndex);
						int   id = e.GetPointerId(e.ActionIndex);

						var path = new Path();

						path.MoveTo(x, y);

						currentLines.Add(id, path);
						allLines    .Add(path);
						allPaints   .Add(GenerateRandomColorPaint());

						return true;
					}

				case MotionEventActions.Move: // Move contains data for all current fingers
					{
						for (int i = 0; i < e.PointerCount; i++)
						{
							float x  = e.GetX(i);
							float y  = e.GetY(i);
							int   id = e.GetPointerId(i);

							currentLines[id].LineTo(x, y);
						}

						base.Invalidate();

						return true;
					}

				case MotionEventActions.PointerUp: // Other finger up, use ActionIndex for the index
					{
						currentLines.Remove(e.GetPointerId(e.ActionIndex));
						return true;
					}

				case MotionEventActions.Up: // Last finger up, use 0 for the index
					{
						currentLines.Remove(e.GetPointerId(0));
						return true;
					}

				default:
					return false;
			}
		}

		//Helper method to generate randomly colored paint objects which will be used to render the Paths
		Paint GenerateRandomColorPaint()
		{
			var random = new Random((int)DateTime.Now.Ticks);

			var color = Color.Argb(255, random.Next(256), random.Next(256), random.Next(256));

			var paint = new Paint() { Color = color, StrokeWidth = 5f, AntiAlias = true };
			paint.SetStyle(Paint.Style.Stroke);

			return paint;
		}
	}
}