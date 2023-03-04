// ==========================================================================
//  Project MEDEA
//  Medical Examination Database & Endoscopy Application
//  Copyright (c) 2006 Gyro Software. All rights reserved.
//
//  Module: 
//
//      ThumbnailUpdateWorker.cs
//
//  Purpose:
//
//
// ==========================================================================
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;

namespace Gyrosoft.Medea.Imaging
{
    class ThumbnailUpdateWorker : BackgroundWorker
    {
        #region Public Events

        public delegate void BeforeRunWorkerEvent(object sender);
        public event BeforeRunWorkerEvent BeforeRunWorker;

        #endregion

        #region Private Fields

        private Queue<Thumbnail> _queue = new Queue<Thumbnail>();
        private int _jobCount = 0;

        #endregion

        #region Construction

        /// <summary>
        /// Constructs new ThumbnailUpdateWorker object.
        /// </summary>
        public ThumbnailUpdateWorker() : base()
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds a Thumbnail object to internal queue.
        /// </summary>
        /// <param name="thumb">Thumbnail object to enqueue.</param>
        public void Enqueue(Thumbnail thumb)
        {
            this.Enqueue(thumb, true);
        }

        /// <summary>
        /// Adds a Thumbnail object to internal queue.
        /// </summary>
        /// <param name="thumb">Thumbnail object to enqueue.</param>
        /// <param name="autoStart">Indicates whether worker should start.</param>
        public void Enqueue(Thumbnail thumb, bool autoStart)
        {
            if (thumb == null) {
                throw new ArgumentNullException("thumb");
            }

            // enqueue thumbnail
            lock (_queue) {
                _queue.Enqueue(thumb);
                _jobCount++;
            }

            // automatically start worker when queue is not empty
            if (autoStart) {
                this.RunWorkerAsyncIfNecessary();
            }
        }

        /// <summary>
        /// Starts worker if queue is not empty.
        /// </summary>
        public void RunWorkerAsyncIfNecessary()
        {
            if (!this.IsBusy && _queue.Count > 0) {
                if (this.BeforeRunWorker != null) {
                    this.BeforeRunWorker(this);
                }
                this.RunWorkerAsync();
            }
        }

        #endregion

        #region Protected Methods

        protected override void OnDoWork(DoWorkEventArgs e)
        {
            int total = 0, count = 0;

            do {
                if (this.CancellationPending) {
                    e.Cancel = true;
                    break;
                }
                else {
                    Thumbnail thumb = null;
                    lock (_queue) {
                        if (_queue.Count > 0) {
                            thumb = _queue.Dequeue();
                        }
                    }

                    if (thumb != null) {
                        thumb.CreateFromSource();
                        count++;

                        total = _jobCount;
                        int percentComplete = (int)((float)count / total * 100);
                        this.ReportProgress(percentComplete, thumb);
                    }

                    //Thread.Sleep(1000);
                }
            } while (_queue.Count > 0);

            _jobCount = _queue.Count;
            e.Result = 0;
        }

        #endregion
    }
}