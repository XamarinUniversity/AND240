﻿using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace XamPaint
{
	[Activity (Label = "XamPaint", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.Main);

			var btnClear = FindViewById<Button   > (Resource.Id.btnClear);
			var viewDraw = FindViewById<PaintView> (Resource.Id.viewDrawing);

			btnClear.Click += (sender, e) => viewDraw.Clear();
		}
	}
}