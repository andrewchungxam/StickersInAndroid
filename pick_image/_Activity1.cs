//using System;
//using System.IO;
//using Android.App;
//using Android.Content;
//using Android.Content.Res;
//using Android.Graphics;
//using Android.OS;
//using Android.Views;
//using Android.Widget;
//using Java.IO;
//using SkiaSharp;
//using Uri = Android.Net.Uri;

//namespace PickImageFromGallery
//{
//    [Activity(Label = "PickImageFromGallery", MainLauncher = true, Icon = "@drawable/icon")]
//    public class Activity1 : Activity
//    {
//        public static readonly int PickImageId = 1000;
//        ImageView _imageView;
//        // TextView that shows animation selected in SeekBar

//        TextView mytextView;

//        public float XLocation { get; set; } = 1.23F;
//        public float YLocation { get; set; } = 1.23F;

//        public int StickerXLength { get; set; }
//        public int StickerYHeight { get; set; }

//        //View _v;

//        GestureRecognizerView _v;
//        Uri _uri;

//        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
//        {
//            if ((requestCode == PickImageId) && (resultCode == Result.Ok) && (data != null))
//            {
//                _uri = data.Data;
//                _imageView.SetImageURI(_uri);



//            }
//        }

//        protected override void OnCreate(Bundle bundle)
//        {
//            base.OnCreate(bundle);

//            _v = new GestureRecognizerView(this);

//            SetContentView(Resource.Layout.Main);


//            //https://stackoverflow.com/questions/4884882/is-it-possible-to-load-a-drawable-from-the-assets-folder

//            //This creates it from Assets which isn't good enough - bc of the various size images
//            //// Create a new TextView and set it as our view
//            //TextView tv = new TextView(this);

//            //// Read the contents of our asset
//            //string content;
//            //AssetManager assets = this.Assets;

//            //using (StreamReader sr = new StreamReader(assets.Open("ic_launcher.png")))
//            //{
//            //    content = sr.ReadToEnd();
//            //}

//            //// Set TextView.Text to our asset content
//            //tv.Text = content;
//            //SetContentView(tv);


//            //Drawable.createFromResourceStream(resources, new TypedValue(), resources.getAssets().open(filename), null)


//            //SKBitmap backgroundBitmap = SKBitmap.Decode(assets.Open("background.png"));

//            //InputStream inputStream = Context.
//            //Context.Resources openRawResource(R.drawable.your_id);

//            //https://stackoverflow.com/questions/42802070/share-raw-resources-xamarin-android




 

//            ////METHOD 1
//            FrameLayout mainFrameLayout = (FrameLayout)FindViewById(Resource.Id.mainFrameLayout);
//            mainFrameLayout.AddView(_v);



//            ////OPTIONAL METHOD 2 - (optionally add a linear layout to wrap the image holder in Main.axml) - 
//            // //and in code below, the linear layout surrounds the gesture recognizer

//            //FrameLayout mainFrameLayout = (FrameLayout)FindViewById(Resource.Id.mainFrameLayout);
//            ////LINEAR LAYOUT - which holds gesture recognizer
//            //LinearLayout linearLayoutGestureHolder = (LinearLayout)View.Inflate(this, Resource.Layout.GestureHolder, null);
//            //linearLayoutGestureHolder.AddView(v);
//            //mainFrameLayout.AddView(linearLayoutGestureHolder);

//            _imageView = FindViewById<ImageView>(Resource.Id.imageView1);
//            Button button = FindViewById<Button>(Resource.Id.MyButton);
//            button.Click += ButtonOnClick;

//            Button secondButton = FindViewById<Button>(Resource.Id.MySecondButton);
//            secondButton.Click += SecondButtonOnClick;

//            mytextView = FindViewById<TextView>(Resource.Id.durationLabel);
//            mytextView.Text = String.Format("Location:");

//            //           var xlocation = string.Format("{0:N3}", XLocation);
//            //           var ylocation = string.Format("{0:N3}", YLocation);
//            //mytextView.Text = String.Format("Location:");// {0}, {1}", xlocation, ylocation);//"Location";
//        }

//        private void SecondButtonOnClick(object sender, EventArgs e)
//        {
//            //throw new NotImplementedException();
//            //GET X/Y LOCATION FROM GestureVIEW
//            // (need to make the View class accessible  + and properties public 

//            //GET DATA FROM THE GestureRecognizerView
//            XLocation = _v.StickerXLocation;
//            YLocation = _v.StickerYLocation;

//            StickerXLength = _v.LastStickerXLength;
//            StickerYHeight = _v.LastStickerYHeight;

//            RunOnUiThread
//            (() =>
//            {
//                var xlocation = string.Format("{0:N3}", XLocation);
//                var ylocation = string.Format("{0:N3}", YLocation);
//                mytextView.Text = String.Format("Location: {0}, {1}", xlocation, ylocation);//"Location"; 
//            });


//            int width = 0;
//            int height = 0;

//            //GET THE DRAWABLE IN FILE
//            //https://stackoverflow.com/questions/42802070/share-raw-resources-xamarin-android
//            System.IO.Stream inputStream = this.Resources.OpenRawResource(Resource.Drawable.ic_launcher);
//            SKBitmap stickerBitmap = SKBitmap.Decode(inputStream);

//            width = stickerBitmap.Width;
//            height = stickerBitmap.Height;



//            //GET DRAWABLE THAT HAS BEEN PICKED FROM PHOTOS
//            //https://stackoverflow.com/questions/43753989/xamarin-android-image-uri-to-byte-array

//            ContentResolver cr = this.ContentResolver;

//            //if(_uri!=null) 
//            //{
//            //IF YOU RAN INTO AN ERROR HERE - YOU LIKELY DIDN'T CHOSE AN IMAGE
//            //PUT CHECKS - MAKE SURE AN IMAGE WAS SELECTED  

//            //CREATING BITMAP FROM THE IMAGE IN THE PHOTO LIBRARY (THIS MAKES A LARGE FULL RESOLUTION IMAGE)
//            //METHOD 1
//            //    var theInputStream = cr.OpenInputStream(_uri);
//            //    SKBitmap pickedImageBitmap = SKBitmap.Decode(theInputStream);

//            //width = pickedImageBitmap.Width;
//            //height = pickedImageBitmap.Height;





//            //METHOD 2

//            //https://falsinsoft.blogspot.com/2015/03/android-take-screenshot-of-view.html
//            //https://stackoverflow.com/questions/4954413/what-is-the-use-of-getdrawingcache-method
//            //https://forums.xamarin.com/discussion/14939/is-there-a-way-to-get-bitmap-from-imageview

//            //_imageView.SetWillNotCacheDrawing(false);
//            _imageView.BuildDrawingCache();

//            _imageView.BuildDrawingCache(true);
//            Bitmap bitmapOfImage = _imageView.GetDrawingCache(true);
//            _imageView.SetImageBitmap(bitmapOfImage);
//            Bitmap bitPicked = Bitmap.CreateBitmap(_imageView.GetDrawingCache(true));

//            SKBitmap bitPickedtoSKBitmap;

//            //Bitmap -> Stream or ByteArray -> SKBitmap 
//            using (MemoryStream memStream = new MemoryStream())
//            {
//                bitPicked.Compress(Bitmap.CompressFormat.Png, 0, memStream);


////                bitPicked.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
//                bitPickedtoSKBitmap = SKBitmap.Decode(memStream);
//            }

//            width = bitPickedtoSKBitmap.Width;
//            height = bitPickedtoSKBitmap.Height;

//            //var bitmapOfImage = Bitmap.CreateBitmap(_imageView.GetDrawingCache(autoScale:true)); //cr.OpenInputStream(_uri);
//            //_imageView.SetWillNotCacheDrawing(true);



//                //SKBitmap pickedImageBitmap = SKBitmap.Decode(theInputStream);


//                //IF THERE EXISTS A BACKGROUND IMAGE - THEN USE IT'S SIZE
    
//            //}

//            SKImage finalImage = null;

//            using (var tempSurface = SKSurface.Create(new SKImageInfo(width, height)))
//            {
//                //get the drawing canvas of the surface
//                var canvas = tempSurface.Canvas;

//                //set background color
//                canvas.Clear(SKColors.Transparent);

//                //go through each image and draw it on the final image
//                int offset = 0;
//                int offsetTop = 0;

//                //FIRST PUT THE PICKED BACKGROUND IMAGE 
//                //canvas.DrawBitmap(pickedImageBitmap, SKRect.Create(offset, offsetTop, pickedImageBitmap.Width, pickedImageBitmap.Height));
//                //1
//                //canvas.DrawBitmap(bitPickedtoSKBitmap, SKRect.Create(offset, offsetTop, bitPicked.Width, bitPicked.Height));
//                //2
//                canvas.DrawBitmap(bitPickedtoSKBitmap, SKRect.Create(offset, offsetTop, width, height));

//                //SECOND PUT IN THE "STICKER"
// //             canvas.DrawBitmap(stickerBitmap, SKRect.Create(XLocation, YLocation, pickedImageBitmap.Width, pickedImageBitmap.Height));
//                canvas.DrawBitmap(stickerBitmap, SKRect.Create(XLocation, YLocation, StickerXLength, StickerYHeight));

//                finalImage = tempSurface.Snapshot();

//                //https://github.com/mono/SkiaSharp/issues/194
//                SKData data = finalImage.Encode(SKEncodedImageFormat.Png, 80); // don't dispose
//                Stream stream = data?.AsStream(true);  // AsStream(true);

//                //https://stackoverflow.com/questions/221925/creating-a-byte-array-from-a-stream

//                String fileName = "myImage";

//                using (MemoryStream memStream = new MemoryStream())
//                {
//                    stream.CopyTo(memStream);
////                    FileOutputStream fos = new FileOutputStream("myImage", false);  //("myImage", FileCreationMode.Private);
//                    //https://stackoverflow.com/questions/43684151/android-what-is-xamarin-c-sharp-equivalent-of-context-mode-private
//                    using (var fos = OpenFileOutput(fileName, FileCreationMode.Private))  //("myImage", FileCreationMode.Private);
//                    {
//                        //https://stackoverflow.com/questions/24966061/android-write-png-bytearray-to-file    
//                        fos.Write(memStream.ToArray(), 0, memStream.ToArray().Length);
//                        fos.Flush();
//                        fos.Close();
//                    }
//                }

//                //using (var bitmap = new SKBitmap((int)sizeX, (int)sizeY))
//                //{
//                    //using (var canvas = new SKCanvas(bitmap))
//                    //using (var paint = new SKPaint())
//                    //{
//                    //    // draw on canvas
//                    //}
//                    //using (var image = SKImage.FromBitmap(bitmap))
//                    //{
                        


//                //return stream; // this will dispose 'data' in Stream.Dispose()
//                //    //}
//                //    //// 'bitmap' can be disposed as the data is a copy of the encoded bitmap
//                //}



//                //SAVE THE IMAGES LOCALLY
//                //public String createImageFromBitmap(Bitmap bitmap)
//                //{


//                //SKImage ----> File

//                //BASICALLY ENCODE IT --> JPEG 
//                //JPEG --> byteArrayOutPutStream

//                ////    FileOutputStream fo = OpenFileOutput(fileName, Context.MODE_PRIVATE);
//                //    fo.write(bytes.toByteArray());
//                //    // remember close file output
//                //    fo.close();

//                //https://forums.xamarin.com/discussion/75958/using-skiasharp-how-to-save-a-skbitmap
//                // create an image and then get the PNG (or any other) encoded data

//                // create an image and then get the PNG (or any other) encoded data


//               // String fileName = "myImage";

//               // var sKData = finalImage.Encode(SKEncodedImageFormat.Png, 100);

//               // //sk_sp<SkData> png{ image->encode()};


//               //     ByteArrayOutputStream bytes = new ByteArrayOutputStream();
//               // //CAN WE GET BTYE ARRAY from either SKIMAGE or SKDATA? and put into bytes
//               // //SKData --> BYTE ARRAY[]

//               // var fo = new FileOutputStream(OpenFileOutput(fileName, FileCreationMode.Private));
//               //     fo.Write(bytes.toByteArray());
//               //     fo.Close();

//               //     bitmap.compress(Bitmap.CompressFormat.Jpeg, 100, bytes);
//               //     // remember close file output
                    



//               // ByteArrayOutputStream bytes = new ByteArrayOutputStream();
//               // //    bitmap.compress(Bitmap.CompressFormat.Jpeg, 100, bytes);


//               // //using (var o = new FileOutputStream(OpenFileOutput(fileName, FileCreationMode.Private)))
//               // ////o.Write(numeroCartao);
//               // //o.Write(sKData.AsStream());

//               //// FileOutputStream fo = OpenFileOutput(fileName, Private);


        

              
    




  

//                //    ByteArrayOutputStream bytes = new ByteArrayOutputStream();
//                //    bitmap.compress(Bitmap.CompressFormat.Jpeg, 100, bytes);
//                //    FileOutputStream fo = OpenFileOutput(fileName, Context.MODE_PRIVATE);
//                //    fo.write(bytes.toByteArray());
//                //    // remember close file output
//                //    fo.close();



//                //FileOutputStream fo = OpenFileOutput(fileName, Context.MODE_PRIVATE);
//                //fo.Write(bytes.toByteArray());
//                // remember close file output
//                //fo.Close();
          

//                //using (var image = SKImage.FromBitmap(bitmap))
//                //using (var data = image.Encode(SKImageEncodeFormat.Png, 80))
//                //{
//                //    // save the data to a stream
//                //    using (var stream = File.OpenWrite("path/to/image.png"))
//                //    {
//                //        data.SaveTo(stream);
//                //    }
//                //}

//                //--> Then save it to private context


//                //SKImage ----> regular old Bitmap
//                //finalImage ---> 

//                //Bitmap bitmap;
//                //https://stackoverflow.com/Questions/4352172/how-do-you-pass-images-bitmaps-between-android-activities-using-bundles

//                //String fileName = "myImage";//no .png or .jpg needed    
//                //try
//                //    {
//                //        ByteArrayOutputStream abytes = new ByteArrayOutputStream();
//                //    finalImage.ReadPixels().;   bitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, bytes);
//                //        FileOutputStream fo = OpenFileOutput(fileName, Context.MODE_PRIVATE);
//                //        fo.write(abytes.toByteArray());
//                //        // remember close file output
//                //        fo.close();
//                //    }
//                //    catch (Exception e)
//                //    {
//                //        e.printStackTrace();
//                //        fileName = null;
//                //    }


//                //    return fileName;
//                //}




//                //
//                //SUBMIT THOSE IMAGES TO STATIC PROGRAM 
//                //ADJUST THE STATIC METHOD TO RECEIVE THOSE INPUTS
//                //COMBINE THEM

//                //CREATE NEW PAGE

//                //OPEN NEW PAGE

//                //SET UP THE FINAL IMAGE TO BE DISPLAYED
//                var activity2 = new Intent(this, typeof(Activity2));
//                activity2.PutExtra("MyData", "Data from Activity1");
//                StartActivity(activity2);

//            }








//            ////submit those X/Y coordiates to SKiaSharp program
//            ////GOING TO HAVE TO TWEAK THE COORDINATES OF THE FINAL CANVAS BASED ON Picked Image

//            //string[] files = Directory.GetFiles("Resources/Drawable");  //images");

//            ////combine them into one image
//            //SKImage stitchedImage = SKIImageConversion.Combine(files);

//            ////make sure the output folder exists
//            //if (!Directory.Exists("output"))
//            //{
//            //    Directory.CreateDirectory("output");
//            //}

//            ////save the new image
//            //using (SKData encoded = stitchedImage.Encode(SKEncodedImageFormat.Png, 100))
//            //using (Stream outFile = File.OpenWrite("output/stitchedImage.png"))
//            //{
//            //    encoded.SaveTo(outFile);
//            //}

//            ////Have skiaSharp access both: 1) the Image 2) and the locally chosen background image

//            ////then have skiasharp save the images

//            ////???place the image???
        
        
//        }

//        void ButtonOnClick(object sender, EventArgs eventArgs)
//        {
//            Intent = new Intent();
//            Intent.SetType("image/*");
//            Intent.SetAction(Intent.ActionGetContent);
//            StartActivityForResult(Intent.CreateChooser(Intent, "Select Picture"), PickImageId);
//        }
//    }
//}