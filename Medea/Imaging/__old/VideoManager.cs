// ==========================================================================
//  Project MEDEA
//  Medical Examination Database & Endoscopy Application
//  Copyright (c) 2006 Gyro Software. All rights reserved.
//
//  Module: 
//
//      VideoManager.cs
//
//  Purpose:
//
//
// ==========================================================================
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Windows.Forms;
// DShowNET
using DShowNET;
using DShowNET.Device;

namespace Gyrosoft.Medea.Imaging
{
    public class GrabbedEventArgs : EventArgs
    {
        #region Private Fields

        private Image _image = null;

        #endregion

        #region Construction

        public GrabbedEventArgs(Image image) : base()
        {
            _image = image;
        }

        #endregion

        #region Public Properties

        public Image Image
        {
            get
            {
                return _image;
            }
        }

        #endregion
    }

    public class VideoManager : IDisposable, ISampleGrabberCB
    {
        #region Public Events

        public delegate void GrabbedEvent(object sender, GrabbedEventArgs e);
        public GrabbedEvent Grabbed = null;

        #endregion

        #region Private Fields

        // message codes (Win32)
        private const int WM_GRAPHNOTIFY = 0x00008001;

        // window styles (Win32)
        private const int WS_CHILD = 0x40000000;
        private const int WS_CLIPCHILDREN = 0x02000000;

        // filter names
        private const string CaptureFilterName = "Medea Video Capture";
        private const string GrabFilterName = "Medea Sample Grabber";

        // internals
        private bool _correctDirectXVersion = false;
        private List<DsDevice> _devices = new List<DsDevice>();
        private DsDevice _activeDevice = null;
        private Control _host = null;
        private bool _grabbed = true;
        private byte[] _grabBuffer = null;
        private int _grabSize = 0;

        // DirectShow interfaces
        private IBaseFilter _capFilter = null;
        private IGraphBuilder _graphBuilder = null;
        private ICaptureGraphBuilder2 _capGraph = null;
        private ISampleGrabber _sampleGrabber = null;
        private IMediaControl _mediaControl = null;
        private IVideoWindow _videoWindow = null;
        private IMediaEventEx _mediaEvent = null;
        private IBaseFilter _baseGrabFilter = null;
        private VideoInfoHeader _videoInfoHeader = null;
        //private IAMStreamConfig _streamConfig = null;

        private delegate void SampleGrabbed();

        #endregion

        #region Construction

        /// <summary>
        /// Constructs new VideoManager object.
        /// </summary>
        public VideoManager()
        {
            _correctDirectXVersion = DsUtils.IsCorrectDirectXVersion();

            ArrayList availableDevices = null;
            if (DsDev.GetDevicesOfCat(FilterCategory.VideoInputDevice, out availableDevices)) {
                foreach (DsDevice dev in availableDevices) {
                    if (_activeDevice == null) {
                        _activeDevice = dev;
                    }
                    _devices.Add(dev);
                }
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets a value indicating that correct DirectX version installed.
        /// </summary>
        public bool CorrectDirectXVersion
        {
            get
            {
                return _correctDirectXVersion;
            }
        }

        /// <summary>
        /// Gets a collection of available video input devices.
        /// </summary>
        public List<DsDevice> Devices
        {
            get
            {
                return _devices;
            }
        }

        /// <summary>
        /// Gets or sets an object represents active video input device.
        /// </summary>
        public DsDevice ActiveDevice
        {
            get
            {
                return _activeDevice;
            }

            set
            {
                _activeDevice = value;
            }
        }

        /// <summary>
        /// Gets or sets active device name.
        /// </summary>
        public string ActiveDeviceName
        {
            get
            {
                return (_activeDevice != null) ? 
                    _activeDevice.Name : String.Empty;
            }

            set
            {
                if (value != null && value != String.Empty) {
                    foreach (DsDevice dev in _devices) {
                        if (dev.Name == value) {
                            _activeDevice = dev;
                            return;
                        }
                    }
                    throw new Exception(String.Format(Properties.Resources.ErrorVMBadDeviceName, value));
                }
            }
        }

        /// <summary>
        /// Gets or sets video host control.
        /// </summary>
        public Control Host
        {
            get
            {
                return _host;
            }

            set
            {
                if (_host != null) {
                    _host.Resize -= new EventHandler(Host_Resize);
                    _host = null;
                }

                if (value != null) {
                    _host = value;
                    _host.Resize += new EventHandler(Host_Resize);
                }
            }
        }

        #endregion

        #region Public Methods

        public void Dispose()
        {
            foreach (DsDevice dev in this.Devices) {
                dev.Dispose();
            }
        }

        public void Start()
        {
            int hr = 0;

            if (!this.CorrectDirectXVersion) {
                throw new Exception(Properties.Resources.ErrorVMBadDirectXVersion);
            }

            if (this.ActiveDevice == null) {
                throw new Exception(Properties.Resources.ErrorVMNoVideoInputDevice);
            }

            if (this.Host == null) {
                throw new Exception(Properties.Resources.ErrorVMInvalidVideoWindow);
            }

            this.CreateCaptureDevice((IMoniker)this.ActiveDevice.Mon);
            this.GetInterfaces();
            this.SetupGraph();
            this.SetupVideoWindow();

            if ((hr = _mediaControl.Run()) < 0) {
                ThrowExceptionForHResult(hr);
            }
        }

        public void Stop()
        {
            this.ReleaseInterfaces();
        }

        public void Pause()
        {
            if (_mediaControl != null) {
                _mediaControl.Pause();
            }
        }

        public void Grab()
        {
            int hr;

            if (_grabBuffer == null) {
                int size = _videoInfoHeader.BmiHeader.ImageSize;
                if (size < 1000 || size > 16000000)
                    return;

                _grabBuffer = new byte[size + 64000];
            }

            _grabbed = false;
            hr = _sampleGrabber.SetCallback(this, 1);
        }

        public bool Configure()
        {
            if (_capFilter == null || _capGraph == null || _host == null)
                return false;

            return DsUtils.ShowCapPinDialog(_capGraph, _capFilter, _host.Handle);
        }

        #endregion

        #region Private Methods

        private void CreateCaptureDevice(IMoniker moniker)
        {
            object capObj = null;
            try {
                Guid guidBaseFilter = typeof(IBaseFilter).GUID;
                moniker.BindToObject(null, null, ref guidBaseFilter, out capObj);
                _capFilter = (IBaseFilter)capObj;
                capObj = null;
            }
            finally {
                if (capObj != null) {
                    Marshal.ReleaseComObject(capObj);
                    capObj = null;
                }
            }
        }

        private void GetInterfaces()
        {
            object comObj = null;
            Type comType;

            try {
                // IGraphBuilder
                comType = Type.GetTypeFromCLSID(Clsid.FilterGraph);
                if (comType == null) {
                    throw new NotImplementedException(Properties.Resources.ErrorVMNoFilterGraph);
                }
                comObj = Activator.CreateInstance(comType);
                _graphBuilder = (IGraphBuilder)comObj;
                comObj = null;

                // IMediaControl
                _mediaControl = (IMediaControl)_graphBuilder;

                // IVideoWindow
                _videoWindow = (IVideoWindow)_graphBuilder;

                // IMediaEventEx
                _mediaEvent = (IMediaEventEx)_graphBuilder;

                // ICaptureGraphBuilder2
                Guid clsid = Clsid.CaptureGraphBuilder2;
                Guid riid = typeof(ICaptureGraphBuilder2).GUID;
                comObj = DsBugWO.CreateDsInstance(ref clsid, ref riid);
                _capGraph = (ICaptureGraphBuilder2)comObj;
                comObj = null;

                // ISampleGrabber
                comType = Type.GetTypeFromCLSID(Clsid.SampleGrabber);
                if (comType == null) {
                    throw new NotImplementedException(Properties.Resources.ErrorVMNoSampleGrabber);
                }
                comObj = Activator.CreateInstance(comType);
                _sampleGrabber = (ISampleGrabber)comObj;
                comObj = null;

                // IBaseFilter
                _baseGrabFilter = (IBaseFilter)_sampleGrabber;

                // IAMStreamConfig
                /*
                Guid cat = PinCategory.Capture;
                Guid med = MediaType.Video;
                Guid iid = typeof(IAMStreamConfig).GUID;
                object o;
                if (_capGraph.FindInterface(ref cat, ref med, _capFilter, ref iid, out o) != 0) {
                    throw new Exception("FindInterface failed to IAMStreamConfig.");
                }

                _streamConfig = (IAMStreamConfig)o;
                */
            }
            finally {
                if (comObj != null) {
                    Marshal.ReleaseComObject(comObj);
                    comObj = null;
                }
            }
        }

        private void SetupGraph()
        {
            AMMediaType media = null;
            Guid cat, med;
            int hr = 0;

            //DsUtils.ShowCapPinDialog(_capGraph, _capFilter, IntPtr.Zero);

            if (hr == 0) hr = _capGraph.SetFiltergraph(_graphBuilder);

            if (hr == 0) hr = _graphBuilder.AddFilter(_capFilter, CaptureFilterName);

            if (hr == 0) {
                media = new AMMediaType();
                media.majorType = MediaType.Video;
                media.subType = MediaSubType.RGB24;
                media.formatType = FormatType.VideoInfo;

                hr = _sampleGrabber.SetMediaType(media);
            }

            if (hr == 0) hr = _graphBuilder.AddFilter(_baseGrabFilter, GrabFilterName);

            if (hr == 0) {
                cat = PinCategory.Preview;
                med = MediaType.Video;
                hr = _capGraph.RenderStream(ref cat, ref med, _capFilter, null, null);
            }

            if (hr == 0) {
                cat = PinCategory.Capture;
                med = MediaType.Video;
                hr = _capGraph.RenderStream(ref cat, ref med, _capFilter, null, _baseGrabFilter);
            }

            if (hr == 0) {
                media = new AMMediaType();
                if ((hr = _sampleGrabber.GetConnectedMediaType(media)) == 0) {
                    if (media.formatType != FormatType.VideoInfo || media.formatPtr == IntPtr.Zero) {
                        throw new Exception(Properties.Resources.ErrorVMBadGrabberMediaFormat);
                    }
                }
            }

            if (hr == 0) {
                _videoInfoHeader = (VideoInfoHeader)Marshal.PtrToStructure(media.formatPtr, typeof(VideoInfoHeader));
                Marshal.FreeCoTaskMem(media.formatPtr);
                media.formatPtr = IntPtr.Zero;
            }

            if (hr == 0) hr = _sampleGrabber.SetBufferSamples(false);

            if (hr == 0) hr = _sampleGrabber.SetOneShot(true);

            if (hr == 0) hr = _sampleGrabber.SetCallback(null, 0);

            if (hr != 0) {
                this.ThrowExceptionForHResult(hr);
            }
        }

        private void SetupVideoWindow()
        {
            int hr = 0;

            if (hr == 0) hr = _videoWindow.put_Owner(this.Host.Handle);

            if (hr == 0) hr = _videoWindow.put_WindowStyle(WS_CHILD | WS_CLIPCHILDREN);

            if (hr == 0) this.ResizeVideoWindow();

            if (hr == 0) hr = _videoWindow.put_Visible(DsHlp.OATRUE);

//          if (hr == 0) hr = _mediaEvent.SetNotifyWindow(this.Host.Handle, Win32.WM_GRAPHNOTIFY, IntPtr.Zero); 

            if (hr != 0) {
                this.ThrowExceptionForHResult(hr);
            }
        }

        private void ReleaseInterfaces()
        {
            if (_mediaControl != null) {
                _mediaControl.Stop();
                _mediaControl = null;
            }

            if (_mediaEvent != null) {
                //_mediaEvent.SetNotifyWindow(IntPtr.Zero, Win32.WM_GRAPHNOTIFY, IntPtr.Zero);
                _mediaEvent = null;
            }

            if (_videoWindow != null) {
                _videoWindow.put_Visible(DsHlp.OAFALSE);
                _videoWindow.put_Owner(IntPtr.Zero);
                _videoWindow = null;
            }

            //if (_streamConfig != null) {
            //    Marshal.ReleaseComObject(_streamConfig);
            //    _streamConfig = null;
            //}

            if (_baseGrabFilter != null) {
                _graphBuilder.RemoveFilter(_baseGrabFilter);
                _baseGrabFilter = null;
            }

            if (_sampleGrabber != null) {
                Marshal.ReleaseComObject(_sampleGrabber);
                _sampleGrabber = null;
            }

            if (_capGraph != null) {
                Marshal.ReleaseComObject(_capGraph);
                _capGraph = null;
            }

            if (_capFilter != null) {
                _graphBuilder.RemoveFilter(_capFilter);
                Marshal.ReleaseComObject(_capFilter);
                _capFilter = null;
            }

            if (_graphBuilder != null) {
                Marshal.ReleaseComObject(_graphBuilder);
                _graphBuilder = null;
            }
        }

        private void ResizeVideoWindow()
        {
            if (_videoWindow != null) {
                Rectangle rect = this.Host.ClientRectangle;
                _videoWindow.SetWindowPosition(0, 0, rect.Width, rect.Height);
            }
        }

        private void Host_Resize(object sender, EventArgs e)
        {
            this.ResizeVideoWindow();
        }

        private void ThrowExceptionForHResult(int hr)
        {
            string errorMsg = String.Format(Properties.Resources.ErrorHResult, hr);
            throw new Exception(errorMsg);
        }

        private void OnSampleGrabbed()
        {
            try {
                _sampleGrabber.SetCallback(null, 0);

                int w = _videoInfoHeader.BmiHeader.Width;
                int h = _videoInfoHeader.BmiHeader.Height;

                if (((w & 0x3) != 0) || (w < 32) || (w > 4096) || (h < 32) || (h > 4096))
                    return;

                int stride = w * 3;

                GCHandle handle = GCHandle.Alloc(_grabBuffer, GCHandleType.Pinned);
                int scan0 = (int)handle.AddrOfPinnedObject();
                scan0 += (h - 1) * stride;

                Bitmap bmp = new Bitmap(w, h, -stride, PixelFormat.Format24bppRgb, (IntPtr)scan0);
                handle.Free();
                _grabBuffer = null;

                if (this.Grabbed != null) {
                    this.Grabbed(this, new GrabbedEventArgs(bmp));
                }
            }
            catch (Exception exc) {
                string errorMsg = Properties.Resources.ErrorVMCantGrab + "\n" + exc.Message;
                MessageBox.Show(errorMsg, null, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        #endregion

        #region ISampleGrabberCB Members

        public int BufferCB(double SampleTime, IntPtr pBuffer, int BufferLen)
        {
            if (_grabbed && _grabBuffer == null)
                return 0;

            _grabbed = true;
            _grabSize = BufferLen;

            if (pBuffer != IntPtr.Zero && BufferLen > 1000 && BufferLen <= _grabBuffer.Length)
                Marshal.Copy(pBuffer, _grabBuffer, 0, BufferLen);

            this.Host.BeginInvoke(new SampleGrabbed(this.OnSampleGrabbed));
            return 0;
        }

        public int SampleCB(double SampleTime, IMediaSample pSample)
        {
            return 0;
        }

        #endregion
    }
}