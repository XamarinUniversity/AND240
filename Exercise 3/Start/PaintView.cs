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