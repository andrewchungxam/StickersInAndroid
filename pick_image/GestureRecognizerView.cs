namespace PickImageFromGallery
{
	using Android.Content;
	using Android.Graphics;
	using Android.Graphics.Drawables;
	using Android.Util;
	using Android.Views;
    using Android.Widget;

    /// <summary>
    ///   This class will show how to respond to touch events using a custom subclass
    ///   of View.
    /// </summary>
    public class GestureRecognizerView : View
    {

        public float StickerXLocation { get; set; }
        public float StickerYLocation { get; set; }

        public int LastStickerXLength { get; set; }
        public int LastStickerYHeight { get; set; }

        public TextView durationLabel;
 
		private static readonly int InvalidPointerId = -1;
		private readonly Drawable _icon;
		private readonly ScaleGestureDetector _scaleDetector;
		private int _activePointerId = InvalidPointerId;
		private float _lastTouchX;
		private float _lastTouchY;
		private float _posX;
		private float _posY;
		private float _scaleFactor = 1.0f;


//        public Context localContext;

		public GestureRecognizerView (Context context)
            : base(context, null, 0)
		{
//            localContext = context;

			_icon = context.Resources.GetDrawable (Resource.Drawable.ic_launcher);
            _icon.SetBounds (0, 0, _icon.IntrinsicWidth, _icon.IntrinsicHeight);
            LastStickerXLength = _icon.IntrinsicWidth;
            LastStickerYHeight = _icon.IntrinsicHeight;
            //LastStickerXLength = _icon.MinimumWidth;
            //LastStickerYHeight = _icon.MinimumHeight;
			_scaleDetector = new ScaleGestureDetector (context, new MyScaleListener (this));



            //            durationLabel = 


            //_imageView = FindViewById<ImageView>(Resource.Id.imageView1);
            ////button.Click += ButtonOnClick;

            ////.Click += ButtonOnClick;
            /// 
            /// 
             

            //TextView mytextView = FindViewById<TextView>(Resource.Id.durationLabel2);
            //if(mytextView!=null)
                //{ 
                //mytextView.Text = "View";
                //}
            //textView.Text = "Hello";

            //WORKS BUT DOESNT DISPLAY
            //TextView theTextView = new TextView(context);
            //theTextView.Text = "Hello";





		}

		public override bool OnTouchEvent (MotionEvent ev)
		{
			_scaleDetector.OnTouchEvent (ev);

			MotionEventActions action = ev.Action & MotionEventActions.Mask;
			int pointerIndex;

			switch (action) {
			case MotionEventActions.Down:
				_lastTouchX = ev.GetX ();
				_lastTouchY = ev.GetY ();
				_activePointerId = ev.GetPointerId (0);
				break;

			case MotionEventActions.Move:
				pointerIndex = ev.FindPointerIndex (_activePointerId);
				float x = ev.GetX (pointerIndex);
				float y = ev.GetY (pointerIndex);
				if (!_scaleDetector.IsInProgress) {
					// Only move the ScaleGestureDetector isn't already processing a gesture.
					float deltaX = x - _lastTouchX;
					float deltaY = y - _lastTouchY;
					_posX += deltaX;
					_posY += deltaY;
					Invalidate ();
				}

				_lastTouchX = x;
				_lastTouchY = y;

                StickerXLocation = _lastTouchX;
                StickerYLocation = _lastTouchY;

                    //Can you access the property in the activity FROM CONTEXT get the local Activity
                    //


                //durationSeekbar.ProgressChanged += delegate {
                //    durationLabel.SetText(Resources.GetString(Resource.String.animation_duration, durationSeekbar.Progress), TextView.BufferType.Normal);
                //};

                //durationLabel = v.FindViewById<TextView>(Resource.Id.durationLabel);

                //durationLabel.SetText(Resources.GetString(Resource.String.animation_duration, durationSeekbar.Progress), TextView.BufferType.Normal);


                //LastTouchX = _lastTouchX;
                //LastTouchY = _lastTouchY;

				break;

			case MotionEventActions.Up:
			case MotionEventActions.Cancel:
                    // This events occur when something cancels the gesture (for example the
                    // activity going in the background) or when the pointer has been lifted up.
                    // We no longer need to keep track of the active pointer.
				_activePointerId = InvalidPointerId;
				break;

			case MotionEventActions.PointerUp:
                    // We only want to update the last touch position if the the appropriate pointer
                    // has been lifted off the screen.
				pointerIndex = (int)(ev.Action & MotionEventActions.PointerIndexMask) >> (int)MotionEventActions.PointerIndexShift;
				int pointerId = ev.GetPointerId (pointerIndex);
				if (pointerId == _activePointerId) {
					// This was our active pointer going up. Choose a new
					// action pointer and adjust accordingly
					int newPointerIndex = pointerIndex == 0 ? 1 : 0;
					_lastTouchX = ev.GetX (newPointerIndex);
					_lastTouchY = ev.GetY (newPointerIndex);
					_activePointerId = ev.GetPointerId (newPointerIndex);
				}
				break;
			}
			return true;
		}

		protected override void OnDraw (Canvas canvas)
		{
			base.OnDraw (canvas);
			canvas.Save ();
			canvas.Translate (_posX, _posY);
			canvas.Scale (_scaleFactor, _scaleFactor);
			_icon.Draw (canvas);
			canvas.Restore ();
		}


		private class MyScaleListener : ScaleGestureDetector.SimpleOnScaleGestureListener
		{
			private readonly GestureRecognizerView _view;

			public MyScaleListener (GestureRecognizerView view)
			{
				_view = view;
			}

			public override bool OnScale (ScaleGestureDetector detector)
			{
				_view._scaleFactor *= detector.ScaleFactor;

				// put a limit on how small or big the image can get.
				if (_view._scaleFactor > 5.0f) {
					_view._scaleFactor = 5.0f;
				}
				if (_view._scaleFactor < 0.1f) {
					_view._scaleFactor = 0.1f;
				}

				_view.Invalidate ();
				return true;
			}
		}
	}
}
