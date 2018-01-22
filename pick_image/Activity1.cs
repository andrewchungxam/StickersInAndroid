using System;
using System.IO;
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.IO;
using SkiaSharp;
using Uri = Android.Net.Uri;

namespace PickImageFromGallery
{
//    [Android.Runtime.Register("android/content/ContentResolver", DoNotGenerateAcw = true)]
    [Activity(Label = "PickImageFromGallery", MainLauncher = true, Icon = "@drawable/icon")]
    public class Activity1 : Activity
    {
        public static readonly int PickImageId = 1000;
        ImageView _imageView;
        // TextView that shows animation selected in SeekBar

        TextView mytextView;

        public float XLocation { get; set; } //= 1.23F;
        public float YLocation { get; set; } //= 1.23F;

        public int StickerXLength { get; set; }
        public int StickerYHeight { get; set; }

        public float StickerXLengthFloat { get; set; }
        public float StickerYLengthFloat { get; set; }

        public int BackgroundMinimumXLength { get; set; }
        public int BackgroundMinimuYHeight { get; set; }

        GestureRecognizerView _v;
        Uri _uri;

        FrameLayout mainFrameLayout; 

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if ((requestCode == PickImageId) && (resultCode == Result.Ok) && (data != null))
            {
                _uri = data.Data;

                //SET _IMAGEVIEW to IMAGE
                _imageView.SetImageURI(_uri);
                var minWidth = _imageView.Drawable.MinimumWidth;
                var minHeight = _imageView.Drawable.MinimumHeight;

                //CHECK SEE WHAT THE minWIDTH IS ON DEVICE
                //tested minWidth on device = 1344
                //tested minHeight on device = 1008

                var imageContentResolver = this.ContentResolver;
                using(Bitmap selectedImageBitmap = MediaStore.Images.Media.GetBitmap(imageContentResolver, _uri))
                {
                    //GET CURRENT SIZES AND RESIZE THAT _URI TO YOUR CHOSEN RESOLUTION

                    //ORIGINAL BITMAP SIZES
                    var sIBXWidth = selectedImageBitmap.Width;
                    var sIBYHeight = selectedImageBitmap.Height;

                    if (sIBXWidth > minWidth)
                    {

                        int finalCaculatedHeight =  minWidth * sIBYHeight / sIBXWidth;
                        var testString = "testString";

                        using(Bitmap scaledDownBitmap = Bitmap.CreateScaledBitmap(selectedImageBitmap, minWidth, finalCaculatedHeight, false))
                        {
                            //SAVE THAT TO FILE
                            using(System.IO.MemoryStream memStream = new MemoryStream())
                            {
                                scaledDownBitmap.Compress(Bitmap.CompressFormat.Png, 0, memStream);
                                string resizedPickedImageFileName = "rPIFName";
                                using (var fos = OpenFileOutput(resizedPickedImageFileName, FileCreationMode.Private))  
                                {
                                    fos.Write(memStream.ToArray(), 0, memStream.ToArray().Length);
                                    fos.Flush();
                                    fos.Close();
                                }
                            }
                        }
                    }
                }

                using (Bitmap bitmap = BitmapFactory.DecodeStream(this.OpenFileInput("rPIFName")))
                {
                    _imageView = FindViewById<ImageView>(Resource.Id.imageView1);
                    _imageView.SetImageBitmap(bitmap);

                    var testMinWidth = _imageView.Drawable.MinimumWidth;
                    var testMinHeight = _imageView.Drawable.MinimumHeight;

                    int[] screenPOSXY = new int[2];
                    _v.GetLocationOnScreen(screenPOSXY);
                    int xOfScreenForV = screenPOSXY[0];
                    int yOfScreenForV = screenPOSXY[1];
                    //yOfScreenForV = 561

                    var x2OfImageView = _imageView.GetX();
                    var y2OfImageView = _imageView.GetY();

                    int[] screenPOSXYOfImageView = new int[2];
                    _imageView.GetLocationOnScreen(screenPOSXYOfImageView);
                    int xOfScreenOfImageView = screenPOSXY[0];
                    int yOfScreenOfImageView = screenPOSXY[1];
                    //yOFScreenOfImageView = 561


                }
            }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);
            mainFrameLayout = (FrameLayout)FindViewById(Resource.Id.mainFrameLayout);

            _v = new GestureRecognizerView(this);

            ////METHOD 1
            mainFrameLayout.AddView(_v);

            var xOfV = _v.GetX();
            var yOfV = _v.GetY();

            int[] screenPOSXY = new int[2];
            _v.GetLocationOnScreen(screenPOSXY);
            int xOfScreenForV = screenPOSXY[0];
            int yOfScreenForV = screenPOSXY[1];

            ////OPTIONAL METHOD 2 - (optionally add a linear layout to wrap the image holder in Main.axml) - 
            // //and in code below, the linear layout surrounds the gesture recognizer

            //FrameLayout mainFrameLayout = (FrameLayout)FindViewById(Resource.Id.mainFrameLayout);
            ////LINEAR LAYOUT - which holds gesture recognizer
            //LinearLayout linearLayoutGestureHolder = (LinearLayout)View.Inflate(this, Resource.Layout.GestureHolder, null);
            //linearLayoutGestureHolder.AddView(v);
            //mainFrameLayout.AddView(linearLayoutGestureHolder);

            _imageView = FindViewById<ImageView>(Resource.Id.imageView1);

            var xOfImageView = _imageView.GetX();
            var yOfImageView = _imageView.GetY();

            int[] screenPOSXYImageView = new int[2];
            _v.GetLocationOnScreen(screenPOSXYImageView);
            int xOfScreenForImageView = screenPOSXY[0];
            int yOfScreenForImageView = screenPOSXY[1];


            Button button = FindViewById<Button>(Resource.Id.MyButton);
            button.Click += ButtonOnClick;

            Button secondButton = FindViewById<Button>(Resource.Id.MySecondButton);
            secondButton.Click += SecondButtonOnClick;

            mytextView = FindViewById<TextView>(Resource.Id.durationLabel);
            mytextView.Text = String.Format("Location:");

        }

        private void SecondButtonOnClick(object sender, EventArgs e)
        {
            //GET X/Y LOCATION FROM GestureVIEW

            DisplayMetrics dm = this.Resources.DisplayMetrics;

            //GET DATA FROM THE GestureRecognizerView
            XLocation = _v.StickerXLocation * 1.25F; ///dm.Density;
            YLocation = _v.StickerYLocation * 1.25F; ///dm.Density;

            //           StickerXLength = _v.StickerXLength;
            //           StickerYHeight = _v.StickerYHeight;


            StickerXLengthFloat = _v.StickerXLength * 1.25F;// *1.20F;
            StickerYLengthFloat = _v.StickerYHeight * 1.25F;// *1.20F; 

            RunOnUiThread
            (() =>
            {
                var xlocationText = string.Format("{0:N3}", XLocation);
                var ylocationText = string.Format("{0:N3}", YLocation);
                mytextView.Text = String.Format("Location: {0}, {1}", xlocationText, ylocationText);//"Location"; 
            });

            int width = 0;
            int height = 0;

            //GET THE DRAWABLE IN FILE
            System.IO.Stream inputStream = this.Resources.OpenRawResource(Resource.Drawable.ic_launcher);
            SKBitmap stickerBitmap = SKBitmap.Decode(inputStream);

            //width = stickerBitmap.Width;
            //height = stickerBitmap.Height;

            //IF YOU RAN INTO AN ERROR HERE - YOU LIKELY DIDN'T CHOSE AN IMAGE
            //PUT VARIOUS CHECKS MAKE SURE AN IMAGE WAS SELECTED  

            //CREATING BITMAP FROM THE IMAGE IN THE PHOTO LIBRARY (THIS MAKES A LARGE FULL RESOLUTION IMAGE)
            //METHOD 1
            //ContentResolver cr = this.ContentResolver;
            //var theInputStream = cr.OpenInputStream(_uri);
            //SKBitmap pickedImageBitmap = SKBitmap.Decode(theInputStream);
            //width = pickedImageBitmap.Width;
            //height = pickedImageBitmap.Height;


            //CREATE A BITMAP FROM AN IMAGE STORED IN APP PRIVATE STORAGE (THIS NORMALIZES IMAGE RESOLUTION)

            //SKBitmap privateStorageImageBitmap = SKBitmap.Decode("rPIFName");
            //This method not valid due to issue SKIA's file searching library (not a SkiaSharp issue but an underlying issue)

            SKBitmap privateStorageImageBitmap;
            //using (MemoryStream rPIStream = new MemoryStream())
            //{

            using (var fos = OpenFileInput("rPIFName")) 
            {
                privateStorageImageBitmap = SKBitmap.Decode(fos);
            }
            //}

            width = privateStorageImageBitmap.Width;
            height = privateStorageImageBitmap.Height;
 

            SKImage finalImage = null;

            using (var tempSurface = SKSurface.Create(new SKImageInfo(width, height)))
            {
                //get the drawing canvas of the surface
                var canvas = tempSurface.Canvas;

                //set background color
                canvas.Clear(SKColors.Transparent);

                //go through each image and draw it on the final image
                int offset = 0;
                int offsetTop = 0;

                //FIRST PUT THE PICKED BACKGROUND IMAGE 
                //METHOD 1 - without resizing bitmap
                //canvas.DrawBitmap(pickedImageBitmap, SKRect.Create(offset, offsetTop, pickedImageBitmap.Width, pickedImageBitmap.Height)); // 810, 810)); //pickedImageBitmap.Width, pickedImageBitmap.Height));

                //METHOD 1 - with resized bitmap

                canvas.DrawBitmap(privateStorageImageBitmap, SKRect.Create(offset, offsetTop, privateStorageImageBitmap.Width, privateStorageImageBitmap.Height)); // 810, 810)); //pickedImageBitmap.Width, pickedImageBitmap.Height));

                //810 in low res photos

                //SECOND PUT IN THE "STICKER"
                //canvas.DrawBitmap(stickerBitmap, SKRect.Create(XLocation, YLocation, stickerBitmap.Width, stickerBitmap.Height));  //5*StickerXLength, 5*StickerYHeight));
                canvas.DrawBitmap(stickerBitmap, SKRect.Create(XLocation, YLocation, StickerXLengthFloat, StickerYLengthFloat));  //5*StickerXLength, 5*StickerYHeight));


                finalImage = tempSurface.Snapshot();

                SKData data = finalImage.Encode(SKEncodedImageFormat.Png, 80); // don't dispose
                Stream stream = data?.AsStream(true);  // AsStream(true);

                String fileName = "myImage";

                using (MemoryStream memStream = new MemoryStream())
                {
                    stream.CopyTo(memStream);
                    using (var fos = OpenFileOutput(fileName, FileCreationMode.Private))  //("myImage", FileCreationMode.Private);
                    {
                        fos.Write(memStream.ToArray(), 0, memStream.ToArray().Length);
                        fos.Flush();
                        fos.Close();
                    }
                }

                //SET UP THE FINAL IMAGE TO BE DISPLAYED
                var activity2 = new Intent(this, typeof(Activity2));
                activity2.PutExtra("MyData", "Data from Activity1");
                StartActivity(activity2);
            }
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