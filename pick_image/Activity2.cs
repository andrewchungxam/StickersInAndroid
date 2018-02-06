
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SkiaSharp;

namespace PickImageFromGallery
{
    [Activity(Label = "Activity2")]
    public class Activity2 : Activity
    {
        TextView activityTwoTextView;
        ImageView _activityTwoImageView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ActivityTwo);

            // Create your application here
            string passedText = Intent.GetStringExtra("MyData") ?? "Data not available";

            activityTwoTextView = FindViewById<TextView>(Resource.Id.ActivityTwoTextView);
            activityTwoTextView.Text = passedText;

            using(Bitmap bitmap = BitmapFactory.DecodeStream(this.OpenFileInput("myImage"))) 
            {
                _activityTwoImageView = FindViewById<ImageView>(Resource.Id.ActivityTwoimageView1);
                _activityTwoImageView.SetImageBitmap(bitmap);
            }

        }
    }
}
