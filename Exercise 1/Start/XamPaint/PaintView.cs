using System;
using Android.Views;
using Android.Graphics;
using Android.Content;
using Android.Util;

namespace XamPaint
{
	public class PaintView : View
	{
		public PaintView(Context context) 									 : base(context, null, 0)         { Init(); }
		public PaintView(Context context, IAttributeSet attrs)               : base(context, attrs)           { Init(); }
		public PaintView(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle) { Init(); }

		void Init()
		{

		}

		public void Clear()
		{
			
		}
	}
}