using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;

namespace MultiTouchActionsTest
{
	[Activity(Label = "MultiTouchActionsTest", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		ArrayAdapter     adapter;
		JavaList<string> messages = new JavaList<string>();

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			SetContentView(Resource.Layout.Main);

			adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, Android.Resource.Id.Text1, messages);

			var output = FindViewById<ListView>(Resource.Id.output);
			output.Adapter = adapter;
			output.Touch  += OnTouch;
		}

		void OnTouch(object sender, View.TouchEventArgs e)
		{
			switch (e.Event.ActionMasked)
			{
				case MotionEventActions.Down       : messages.Add("Down: "        + e.Event.PointerCount + " pointers"); break;
				case MotionEventActions.PointerDown: messages.Add("PointerDown: " + e.Event.PointerCount + " pointers"); break;
				case MotionEventActions.Move       : messages.Add("Move: "        + e.Event.PointerCount + " pointers"); break;
				case MotionEventActions.PointerUp  : messages.Add("PointerUp: "   + e.Event.PointerCount + " pointers"); break;
				case MotionEventActions.Up         : messages.Add("Up: "          + e.Event.PointerCount + " pointers"); break;
			}

			adapter.NotifyDataSetChanged();

			e.Handled = false; // enables scrolling
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			switch (item.ItemId)
			{
				case Resource.Id.refresh: Clear(); break;
			}

			return base.OnOptionsItemSelected(item);
		}

		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			MenuInflater.Inflate(Resource.Menu.actionbar, menu);
			return base.OnCreateOptionsMenu(menu);
		}

		void Clear()
		{
			messages.Clear();

			adapter.NotifyDataSetChanged();
		}
	}
}