﻿using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Test.Droid
{
	[Activity (Label = "Test.Droid", 
		Icon = "@drawable/icon", 
		MainLauncher = true, 
		Theme = "@android:style/Theme.Holo.Light",
		ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			global::Xamarin.Forms.Forms.Init (this, bundle);
			Notnet.Controls.PlatformInit.Init ();
			LoadApplication (new App ());
		}
	}
}

