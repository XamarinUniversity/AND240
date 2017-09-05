using System;
using Android.Views;
using Android.Graphics;
using Android.Content;
using Android.Util;

namespace XamPaint
{
	public class PaintView : View
	{
		Path  drawPath;
		Paint drawPaint;

		public PaintView(Context context) 								     : base(context, null, 0)         { Init(); }
		public PaintView(Context context, IAttributeSet attrs)               : base(context, attrs)           { Init(); }
		public PaintView(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle) { Init(); }

		void Init ()
		{
			drawPath = new Path();

			drawPaint = new Paint() 
			{
				Color = Color.Aqua,
				StrokeWidth = 5f,
			};
			
			drawPaint.SetStyle(Paint.Style.Stroke);
		}

		public override bool OnTouchEvent(MotionEvent e)
		{
			var x = e.GetX();
			var y = e.GetY();

			switch (e.ActionMasked) 
			{
			case MotionEventActions.Down:
				drawPath.MoveTo(x, y); //move the path to the current location
				break;

			case MotionEventActions.Move:
				drawPath.LineTo(x, y); //draw a line to the new location
				Invalidate();          //update the screen - this will create an OnDraw call
				break;

			default:
				return false;
			}

			return true;
		}

		protected override void OnDraw(Canvas canvas)
		{
			canvas.DrawPath(drawPath, drawPaint); // draw the Path onto the canvas using the Paint object (which controls the visualization)
		} 

		public void Clear()
		{
			drawPath.Reset();
			Invalidate();
		}
	}
}