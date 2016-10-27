// <copyright file="ScaleImage.cs" company="FT Software">
// Copyright (c) 2016 Florian Thurnwald. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace ProjektKatastrophenschutz.Controls
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    public class ScaleImage : Image
    {
        private static readonly string[] SupportedFileFormats = { ".png", ".jpg", ".jpeg" };

        private static readonly string[] SupportedScales =
        {
            ".scale-80", ".scale-100", ".scale-125", ".scale-140", ".scale-150", ".scale-175",
            ".scale-200", ".scale-225", ".scale-300", ".scale-400"
        };

        public new static readonly DependencyProperty SourceProperty = DependencyProperty.Register(
            "Source",
            typeof(ImageSource),
            typeof(ScaleImage),
            new FrameworkPropertyMetadata(
                null,
                FrameworkPropertyMetadataOptions.AffectsRender |
                FrameworkPropertyMetadataOptions.AffectsMeasure,
                OnSourceChanged));

        private double dpiFactor = 1.0;

        public ScaleImage()
        {
            this.Loaded += (sender, args) => { this.EnsureScaling(); };
        }

        public new ImageSource Source
        {
            get { return (ImageSource)this.GetValue(SourceProperty); }
            set { this.SetValue(SourceProperty, value); }
        }

        private static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var scaleImage = (ScaleImage)d;
            scaleImage.SetSourceInternal();
        }

        private void SetSourceInternal()
        {
            if (this.Source != null)
            {
                Uri uri;
                Uri.TryCreate(this.Source.ToString(), UriKind.RelativeOrAbsolute, out uri);

                try
                {
                    // Ensure an application resource
                    if (uri != null && uri.Scheme.Equals("pack") && uri.Host.Equals("application:,,,"))
                    {
                        var lastSegment = uri.Segments.Last();
                        var fileExtension = Path.GetExtension(lastSegment)?.ToLower();
                        var fileName = Path.GetFileNameWithoutExtension(lastSegment);

                        // Check for a supported file format
                        if (SupportedFileFormats.Contains(fileExtension))
                        {
                            var currentScaleEnding =
                                SupportedScales.SingleOrDefault(
                                    scaleEnding => fileName != null && fileName.ToLower().EndsWith(scaleEnding));
                            var hasSupportedEnding = currentScaleEnding?.Equals(string.Empty) == false;

                            // Break if image is already well scaled
                            if (hasSupportedEnding &&
                                currentScaleEnding.Equals(GetFileNameEndingForScaleFactor(this.dpiFactor)))
                            {
                                base.Source = this.Source;
                                return;
                            }
                            if (Math.Abs(this.dpiFactor - 1.0) < 0.000001)
                            {
                                // DPI factor 1.0
                                if (hasSupportedEnding == false)
                                {
                                    base.Source = this.Source;
                                    return;
                                }
                            }

                            // Try to find a proper scaled image
                            var fileNameWithoutScaleEnding = fileName;
                            if (currentScaleEnding != null)
                            {
                                fileNameWithoutScaleEnding = fileName?.Substring(0,
                                    fileName.Length - currentScaleEnding.Length);
                            }

                            var scaleFactor = this.dpiFactor;
                            var findNextImage = true;
                            while (findNextImage)
                            {
                                try
                                {
                                    base.Source =
                                        new BitmapImage(BuildUri(uri, fileNameWithoutScaleEnding,
                                            GetFileNameEndingForScaleFactor(scaleFactor), fileExtension));
                                    return;
                                }
                                catch (IOException)
                                {
                                    if (Math.Abs(scaleFactor - 1) < 0.000001)
                                    {
                                        try
                                        {
                                            base.Source =
                                                new BitmapImage(BuildUri(uri, fileNameWithoutScaleEnding, string.Empty,
                                                    fileExtension));
                                            return;
                                        }
                                        catch (IOException)
                                        {
                                            findNextImage = false;
                                        }
                                    }

                                    scaleFactor = GetLowerScaleFactor(scaleFactor);
                                }
                            }
                        }
                    }
                }
                catch (InvalidOperationException)
                {
                }
            }
            
            base.Source = this.Source;
        }

        private static Uri BuildUri(Uri uri, string fileNameWithoutScaleEnding, string fileNameScaleEnding,
            string fileExtension)
        {
            var uriBuilder = new UriBuilder(uri);
            var pathBuilder = new StringBuilder();
            for (var i = 0; i < uri.Segments.Length - 1; i++)
            {
                pathBuilder.Append(uri.Segments[i]);
            }

            pathBuilder.Append(fileNameWithoutScaleEnding);
            pathBuilder.Append(fileNameScaleEnding);
            pathBuilder.Append(fileExtension);
            uriBuilder.Path = pathBuilder.ToString();
            return uriBuilder.Uri;
        }

        private static string GetFileNameEndingForScaleFactor(double scaleFactor)
        {
            return $".scale-{Math.Round(scaleFactor * 100, 0)}";
        }

        private static double GetLowerScaleFactor(double scaleFactor)
        {
            if (Math.Abs(scaleFactor - 4) < 0.000001)
            {
                return 3;
            }
            if (Math.Abs(scaleFactor - 3) < 0.000001)
            {
                return 2.25;
            }
            if (Math.Abs(scaleFactor - 2.25) < 0.000001)
            {
                return 2;
            }
            if (Math.Abs(scaleFactor - 2) < 0.000001)
            {
                return 1.75;
            }
            if (Math.Abs(scaleFactor - 1.75) < 0.000001)
            {
                return 1.50;
            }
            if (Math.Abs(scaleFactor - 1.50) < 0.000001)
            {
                return 1.40;
            }
            if (Math.Abs(scaleFactor - 1.40) < 0.000001)
            {
                return 1.25;
            }
            if (Math.Abs(scaleFactor - 1.25) < 0.000001)
            {
                return 1;
            }
            if (Math.Abs(scaleFactor - 1) < 0.000001)
            {
                return 0.8;
            }

            return 1;
        }

        private void EnsureScaling()
        {
            var presentationSource = PresentationSource.FromVisual(this);
            if (presentationSource?.CompositionTarget != null)
            {
                var activeDpiFactor = presentationSource.CompositionTarget.TransformToDevice.M11;
                if (Math.Abs(activeDpiFactor - this.dpiFactor) > 0.000001)
                {
                    this.dpiFactor = activeDpiFactor;
                    this.SetSourceInternal();
                }
            }
        }
    }
}