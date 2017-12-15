using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Uri = Android.Net.Uri;

namespace PickImageFromGallery
{
    [Activity(Label = "PickImageFromGallery", MainLauncher = true, Icon = "@drawable/icon")]
    public class Activity1 : Activity
    {
        public static readonly int PickImageId = 1000;
        ImageView _imageView;



        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if ((requestCode == PickImageId) && (resultCode == Result.Ok) && (data != null))
            {
                Uri uri = data.Data;
                _imageView.SetImageURI(uri);
            }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            View v = new GestureRecognizerView(this);
            SetContentView(Resource.Layout.Main);

            ////METHOD 1
            FrameLayout mainFrameLayout = (FrameLayout)FindViewById(Resource.Id.mainFrameLayout);
            mainFrameLayout.AddView(v);



            ////OPTIONAL METHOD 2 - (optionally add a linear layout to wrap the image holder in Main.axml) - 
            // //and in code below, the linear layout surrounds the gesture recognizer

            //FrameLayout mainFrameLayout = (FrameLayout)FindViewById(Resource.Id.mainFrameLayout);
            ////LINEAR LAYOUT - which holds gesture recognizer
            //LinearLayout linearLayoutGestureHolder = (LinearLayout)View.Inflate(this, Resource.Layout.GestureHolder, null);
            //linearLayoutGestureHolder.AddView(v);
            //mainFrameLayout.AddView(linearLayoutGestureHolder);

            _imageView = FindViewById<ImageView>(Resource.Id.imageView1);
            Button button = FindViewById<Button>(Resource.Id.MyButton);
            button.Click += ButtonOnClick;

        }

        void ButtonOnClick(object sender, EventArgs eventArgs)
        {
            Intent = new Intent();
            Intent.SetType("image/*");
            Intent.SetAction(Intent.ActionGetContent);
            StartActivityForResult(Intent.CreateChooser(Intent, "Select Picture"), PickImageId);
        }
    }
}