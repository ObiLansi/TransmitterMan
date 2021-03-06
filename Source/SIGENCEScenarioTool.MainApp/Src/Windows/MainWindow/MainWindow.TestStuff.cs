﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Linq;

using GMap.NET;
using GMap.NET.WindowsPresentation;

using Microsoft.Win32;

using NetTopologySuite.Geometries;

using SIGENCEScenarioTool.Datatypes.Geo;
using SIGENCEScenarioTool.Dialogs;
using SIGENCEScenarioTool.Extensions;
using SIGENCEScenarioTool.Markers;
using SIGENCEScenarioTool.Models;
using SIGENCEScenarioTool.Tools;



namespace SIGENCEScenarioTool.Windows.MainWindow
{
    /// <summary>
    /// 
    /// </summary>
    public partial class MainWindow
    {
        /// <summary>
        /// Handles the Click event of the MenuItem_ChartingTest control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void MenuItem_ChartingTest_Click( object sender, RoutedEventArgs e )
        {
            ChartingDialog cw = new ChartingDialog(new RFDeviceList(from device in this.RFDeviceViewModelCollection select device.RFDevice));
            cw.ShowDialog();
            cw = null;

            e.Handled = true;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        ///// <summary>
        ///// Sets the blink1.
        ///// </summary>
        //[Conditional("DEBUG")]
        //private void SetBlink1()
        //{
        //    try
        //    {
        //        if (this.ReceivedData == true)
        //        {
        //            Blink.SetColor(Colors.Green);
        //        }
        //        else
        //        {
        //            Blink.Off();
        //        }
        //    }
        //    catch (Exception)
        //    {
        //    }
        //}

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        ///// <summary>
        ///// The t UDP server
        ///// </summary>
        //private Thread tUDPServer = null;

        ///// <summary>
        ///// Initializes the UDP server.
        ///// </summary>
        //private void StartUDPServer()
        //{
        //    if (this.tUDPServer == null)
        //    {
        //        this.tUDPServer = new Thread(UDPReceiveData)
        //        {
        //            IsBackground = true,
        //            Name = "UDPServerThread"
        //        };
        //        this.tUDPServer.Start();
        //    }
        //}


        ///// <summary>
        ///// Stops the UDP server.
        ///// </summary>
        //private void StopUDPServer()
        //{
        //    if (this.tUDPServer != null)
        //    {
        //        this.tUDPServer.Abort();

        //        this.tUDPServer = null;
        //    }
        //}


        ///// <summary>
        ///// UDPs the receive data.
        ///// </summary>
        //private void UDPReceiveData()
        //{
        //    UdpClient client = null;

        //    try
        //    {
        //        client = new UdpClient(this.settings.UDPPortReceiving);
        //        {
        //            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(this.settings.UDPHost), this.settings.UDPPortReceiving);

        //            // A neverending story ...
        //            while (true)
        //            {
        //                // Obwohl der Thread Aborted wird beendet er das Receiver nicht und somit auch nicht Thread :-(
        //                // Erst wenn er was empfangen hat merkt er das er Aborted ist und die Expcetion tritt auf ...
        //                byte[] baReceived = client.Receive(ref ep);

        //                string strReceived = Encoding.Default.GetString(baReceived);

        //                this.DebugOutput += strReceived + "\n\n";
        //                this.ReceivedData = true;
        //            }
        //        }
        //    }
        //    catch (ThreadAbortException)
        //    {
        //        // Do nothing ...
        //        //Debug.WriteLine(ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        MB.Warning(ex.Message);
        //    }
        //    finally
        //    {
        //        if (client != null)
        //        {
        //            try
        //            {
        //                client.Close();
        //                client.Dispose();
        //                client = null;
        //            }
        //            catch (Exception)
        //            {

        //            }
        //        }
        //    }
        //}

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Removes the streets.
        /// </summary>
        [Conditional("DEBUG")]
        private void RemoveStreets()
        {
            List<GMapMarker> lDelete = new List<GMapMarker>();

            foreach( GMapMarker mm in this.mcMapControl.Markers )
            {
                if( mm.Tag is Highway )
                {
                    lDelete.Add(mm);
                }
            }

            this.Dispatcher.Invoke(() =>
           {
               lDelete.ForEach(mm => this.mcMapControl.Markers.Remove(mm));
           });
        }


        /// <summary>
        /// Loads the streets.
        /// </summary>
        [Conditional("DEBUG")]
        private void LoadStreets()
        {
            RemoveStreets();

            string strFilename = $"{Tool.StartupPath}\\streets_bw.sqlite";

            RectLatLng bb = this.mcMapControl.ViewArea;

            //TODO: Check if the area is not to big ... But what is big ?

            SQLiteConnectionStringBuilder csbDatabase = new SQLiteConnectionStringBuilder
            {
                DataSource = strFilename
            };

            using( SQLiteConnection dbConnection = new SQLiteConnection(csbDatabase.ConnectionString) )
            {
                dbConnection.Open();

                try
                {
                    //                                     0     1   2   3
                    //const string strSelectStatement = "select highway,ref,name,way from streets_bw where highway in ('motorway','motorway_link','trunk','trunk_link','primary','secondary','primary_link','secondary_link','residential')";
                    const string strSelectStatement = "select highway,ref,name,way from streets_bw";

                    uint iCounter = 0;

                    DateTime dtStart = DateTime.Now;

                    using( SQLiteCommand dbSelectCommand = new SQLiteCommand(strSelectStatement, dbConnection) )
                    {
                        using( SQLiteDataReader dbResult = dbSelectCommand.ExecuteReader() )
                        {
                            while( dbResult.Read() )
                            {
                                Highway type = Highway.Unknown;

                                try
                                {
                                    type = (Highway) Enum.Parse(typeof(Highway), dbResult.GetString(0), true);
                                }
                                catch( Exception ex )
                                {
                                    Debug.WriteLine(ex.Message);
                                }

                                string strRef = dbResult.GetStringOrNull(1);
                                string strName = dbResult.GetStringOrNull(2);

                                LineString way = (LineString) dbResult.GetGeometryFromWKB(3);

                                if( bb.Contains(way.Coordinate.ToPointLatLng()) )
                                {
                                    List<PointLatLng> list = new List<PointLatLng>(way.Count);

                                    list.AddRange(way.Coordinates.Select(pos => pos.ToPointLatLng()));

                                    this.Dispatcher.Invoke(() =>
                                    {
                                        PathMarker mrWay = new PathMarker(this.mcMapControl, list, type, $"{( strName.IsNotEmpty() ? strName : "Unknown" )}{( strRef.IsNotEmpty() ? $" ({strRef})" : "" )}")
                                        {
                                            Tag = type
                                        };

                                        this.mcMapControl.Markers.Add(mrWay);
                                    });

                                    iCounter++;
                                }
                            }
                        }
                    }

                    DateTime dtStop = DateTime.Now;

                    MB.Information("Load {0} Ways In {1}.", iCounter, ( dtStop - dtStart ).ToHHMMSSString());
                }
                catch( Exception ex )
                {
                    MB.Error(ex);
                }
                finally
                {
                    if( dbConnection.State == ConnectionState.Open )
                    {
                        dbConnection.Close();
                    }
                }
            }
        }


        /// <summary>
        /// Handles the Click event of the MenuItem_LoadStreets control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void MenuItem_LoadStreets_Click( object sender, RoutedEventArgs e )
        {
            Task.Run(() => { LoadStreets(); });

            e.Handled = true;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// The tm
        /// </summary>
        private TerrainModel tm = null;

        /// <summary>
        /// Loads the height data.
        /// </summary>
        private void LoadHeightData()
        {
            this.tm = new TerrainModel();

            DateTime dtStart = DateTime.Now;
            //tm.LoadXYZFile(@"D:\BigData\SRTM Tiles Germany\srtm_38_03.xyz");
            this.tm.LoadXYZFile(@"C:\Lanser\BigData\SRTM Tiles Germany\srtm_38_03.xyz");
            DateTime dtStop = DateTime.Now;

            GMapPolygon polygon = new GMapPolygon(new List<PointLatLng>
            {
                new PointLatLng(this.tm.YMin,this.tm.XMin),
                new PointLatLng(this.tm.YMax,this.tm.XMin),
                new PointLatLng(this.tm.YMax,this.tm.XMax),
                new PointLatLng(this.tm.YMin,this.tm.XMax)
            });

            this.mcMapControl.Markers.Add(polygon);

            MB.Information($"Time: {( dtStop - dtStart ).ToHHMMSSString()} / Points: {this.tm.PointCount}");

            GC.WaitForPendingFinalizers();
            GC.Collect();
        }


        /// <summary>
        /// Creates the highest points.
        /// </summary>
        private void CreateHighestPoints()
        {
            if( this.tm == null )
            {
                MB.Information("Please Load First An Terrain Model!");
                return;
            }

            DateTime dtStart = DateTime.Now;
            List<LatLonAlt> highestpoints = this.tm.GetHighestPoints(new Envelope(this.tm.XMin, this.tm.XMax, this.tm.YMin, this.tm.YMax), 10, 20);
            DateTime dtStop = DateTime.Now;


            if( highestpoints.Count > 0 )
            {
                int iCounter = 0;

                foreach( LatLonAlt lla in highestpoints )
                {
                    RFDevice dev = new RFDevice()
                    {
                        Latitude = lla.Lat,
                        Longitude = lla.Lon,
                        Altitude = lla.Alt,
                        Name = $"HighPoint #{++iCounter } @ {lla.Alt} m",
                        DeviceSource = DeviceSource.Automatic,
                        Id = iCounter
                    };

                    AddRFDevice(dev, true);
                }
            }

            MB.Information($"Time: {( dtStop - dtStart ).ToHHMMSSString()} / Points: {highestpoints.Count}");

            GC.WaitForPendingFinalizers();
            GC.Collect();
        }


        /// <summary>
        /// Handles the Click event of the MenuItem_HeightDataTest control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void MenuItem_LoadHeightDataTest_Click( object sender, RoutedEventArgs e )
        {
            //Task.Run(() => { LoadHeightData(); });
            this.Cursor = Cursors.Wait;

            LoadHeightData();

            this.Cursor = Cursors.Arrow;

            e.Handled = true;
        }


        /// <summary>
        /// Handles the Click event of the MenuItem_UseHeightDataTest control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void MenuItem_UseHeightDataTest_Click( object sender, RoutedEventArgs e )
        {
            this.Cursor = Cursors.Wait;

            CreateHighestPoints();

            this.Cursor = Cursors.Arrow;

            e.Handled = true;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Handles the MouseDoubleClick event of the DataGridGeoData control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        private void DataGridGeoData_MouseDoubleClick( object sender, MouseButtonEventArgs e )
        {
            GeoNode gn = ( sender as DataGrid ).SelectedItem as GeoNode;

            JumpToGeoNode(gn);

            e.Handled = true;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Messages the mocker.
        /// </summary>
        /// <param name="strFilename">The string filename.</param>
        private void MessageMocker( string strFilename )
        {
            const int MARGIN_LEFT_RIGHT = 10;
            const int MARGIN_TOP_BOTTOM = 5;

            try
            {
                XDocument xdoc = XDocument.Load(strFilename);

                Window w = new Window
                {
                    Title = "SIGENCE XML MQTT Message Mocker",
                    Width = 600,
                    Height = 600,
                    ResizeMode = ResizeMode.NoResize,
                    WindowStartupLocation = WindowStartupLocation.CenterScreen
                };

                Grid grid = new Grid();

                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

                int iCurrentRow = 0;

                Action<UIElement, UIElement> AddRow = ( e1, e2 ) =>
                {
                    grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });

                    grid.Children.Add(e1);
                    grid.Children.Add(e2);

                    Grid.SetColumn(e1, 0);
                    Grid.SetRow(e1, iCurrentRow);

                    Grid.SetColumn(e2, 1);
                    Grid.SetRow(e2, iCurrentRow);

                    iCurrentRow++;
                };

                //-----------------------------------------------------------------------------------------------------

                AddRow(
                    new Label
                    {
                        Content = "MQTT Topic",
                        Margin = new Thickness(MARGIN_LEFT_RIGHT, MARGIN_TOP_BOTTOM, MARGIN_LEFT_RIGHT, MARGIN_TOP_BOTTOM),
                        FontWeight = FontWeights.Bold
                    },
                    new TextBox
                    {
                        Text = "",
                        Margin = new Thickness(MARGIN_LEFT_RIGHT, MARGIN_TOP_BOTTOM, MARGIN_LEFT_RIGHT, MARGIN_TOP_BOTTOM),
                        VerticalContentAlignment = VerticalAlignment.Center
                    }
                );

                //-----------------------------------------------------------------------------------------------------

                XElement root = xdoc.Root;

                AddRow(
                    new Label
                    {
                        Content = "Name",
                        Margin = new Thickness(MARGIN_LEFT_RIGHT),
                        FontWeight = FontWeights.Bold
                    },
                    new Label
                    {
                        Content = root.Element("name").Value,
                        Margin = new Thickness(MARGIN_LEFT_RIGHT, MARGIN_TOP_BOTTOM, MARGIN_LEFT_RIGHT, MARGIN_TOP_BOTTOM),
                        BorderBrush = Brushes.Black,
                        BorderThickness = new Thickness(1)
                    }
                );

                AddRow(
                    new Label
                    {
                        Content = "Id",
                        Margin = new Thickness(MARGIN_LEFT_RIGHT, MARGIN_TOP_BOTTOM, MARGIN_LEFT_RIGHT, MARGIN_TOP_BOTTOM),
                        FontWeight = FontWeights.Bold
                    },
                    new Label
                    {
                        Content = root.Element("id").Value,
                        Margin = new Thickness(MARGIN_LEFT_RIGHT, MARGIN_TOP_BOTTOM, MARGIN_LEFT_RIGHT, MARGIN_TOP_BOTTOM),
                        BorderBrush = Brushes.Black,
                        BorderThickness = new Thickness(1)
                    }
                );

                AddRow(
                    new Label
                    {
                        Content = "Version",
                        Margin = new Thickness(MARGIN_LEFT_RIGHT, MARGIN_TOP_BOTTOM, MARGIN_LEFT_RIGHT, MARGIN_TOP_BOTTOM),
                        FontWeight = FontWeights.Bold
                    },
                    new Label
                    {
                        Content = root.Element("version").Value,
                        Margin = new Thickness(MARGIN_LEFT_RIGHT, MARGIN_TOP_BOTTOM, MARGIN_LEFT_RIGHT, MARGIN_TOP_BOTTOM),
                        BorderBrush = Brushes.Black,
                        BorderThickness = new Thickness(1)
                    }
                );

                foreach( XElement eField in root.Elements("key") )
                {
                    string strFieldName = eField.Element("name").Value;

                    AddRow(
                        new Label
                        {
                            Content = strFieldName,
                            Margin = new Thickness(MARGIN_LEFT_RIGHT, MARGIN_TOP_BOTTOM, MARGIN_LEFT_RIGHT, MARGIN_TOP_BOTTOM),
                            FontWeight = FontWeights.Bold
                        },
                        new TextBox
                        {
                            Text = "",
                            Margin = new Thickness(MARGIN_LEFT_RIGHT, MARGIN_TOP_BOTTOM, MARGIN_LEFT_RIGHT, MARGIN_TOP_BOTTOM),
                            VerticalContentAlignment = VerticalAlignment.Center,
                            Tag = eField.Element("name").Value
                        }
                    );
                }

                //-----------------------------------------------------------------------------------------------------

                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

                Button btnSend = new Button { Content = "Send", Width = 100, Height = 40, Margin = new Thickness(20) };
                btnSend.Click += ( s, a ) => MB.NotYetImplemented();

                Button btnCancel = new Button { Content = "Cancel", Width = 100, Height = 40, Margin = new Thickness(20) };
                btnCancel.Click += ( s, a ) => w.Close();

                StackPanel sp = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Center };
                sp.Children.Add(btnSend);
                sp.Children.Add(btnCancel);

                grid.Children.Add(sp);

                Grid.SetColumn(sp, 0);
                Grid.SetRow(sp, iCurrentRow);
                Grid.SetColumnSpan(sp, 2);

                //-----------------------------------------------------------------------------------------------------

                w.Content = new ScrollViewer { Content = grid, VerticalScrollBarVisibility = ScrollBarVisibility.Auto };

                w.ShowDialog();
            }
            catch( Exception ex )
            {
                MB.Error(ex);
            }
        }


        /// <summary>
        /// Handles the Click event of the MenuItem_MQTTMessageSender control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void MenuItem_MQTTMessageSender_Click( object sender, RoutedEventArgs e )
        {

            OpenFileDialog ofd = new OpenFileDialog
            {
                Title = "Load XML Message Definition",
                Filter = "SIGENCE XML Message Definition (*.xml)|*.xml",
                AddExtension = true,
                CheckPathExists = true,
                CheckFileExists = true,
                Multiselect = false
            };

            if( ofd.ShowDialog() == true )
            {
                MessageMocker(ofd.FileName);
            }

            e.Handled = true;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Creates the heatmap.
        /// </summary>
        /// <param name="pllTopLeft">The PLL top left.</param>
        /// <param name="pllBottomRight">The PLL bottom right.</param>
        /// <param name="bColor">Color of the b.</param>
        [Conditional("DEBUG")]
        private void CreateHeatmap( PointLatLng pllTopLeft, PointLatLng pllBottomRight, Brush bColor )
        {
            List<PointLatLng> points = new List<PointLatLng>
            {
                pllTopLeft,
                new PointLatLng(pllBottomRight.Lat,pllTopLeft.Lng),
                pllBottomRight,
                new PointLatLng(pllTopLeft.Lat,pllBottomRight.Lng),
            };

            GMapPolygon mp = new GMapPolygon(points);

            mp.RegenerateShape(this.mcMapControl);

            if( mp.Shape is System.Windows.Shapes.Path path )
            {
                path.Stroke = Brushes.Black;
                path.StrokeThickness = 0.1;
                path.Fill = bColor;
            }

            this.mcMapControl.Markers.Add(mp);
        }


        /// <summary>
        /// Creates the heatmap.
        /// </summary>
        [Conditional("DEBUG")]
        private void CreateHeatmap()
        {
            PointLatLng pll = this.mcMapControl.Position;

            double dWidth = 0.00025;
            double dHeight = 0.00015;

            int iKachelBreite = 16;
            int iKachelHöhe = 16;

            Random r = new Random();
            List<Brush> colors = new List<Brush> { Brushes.White, Brushes.LightYellow, Brushes.Yellow, Brushes.Orange, Brushes.OrangeRed, Brushes.Red };

            for( double x = pll.Lng - ( iKachelBreite * dWidth ) ; x < pll.Lng + ( iKachelBreite * dWidth ) ; x += dWidth )
            {
                for( double y = pll.Lat - ( iKachelHöhe * dHeight ) ; y < pll.Lat + ( iKachelHöhe * dHeight ) ; y += dHeight )
                {
                    PointLatLng pllTopLeft = new PointLatLng(y, x);
                    PointLatLng pllBottomRight = new PointLatLng(y + dHeight, x + dWidth);

                    CreateHeatmap(pllTopLeft, pllBottomRight, r.NextObject(colors));
                }
            }
        }


        /// <summary>
        /// Creates the heatmap2.
        /// </summary>
        /// <param name="pllCenter">The PLL center.</param>
        /// <param name="bColor">Color of the b.</param>
        [Conditional("DEBUG")]
        private void CreateHeatmap2( PointLatLng pllCenter, Brush bColor )
        {
            // Die Größe der Punkte sind in Pixel, dasm uss natürlich angepasst werden an die GeoKoordinaten, siehe GMap.NET.WindowsPresentation.GMapPolygon .
            GMapMarker marker = new GMapMarker(pllCenter)
            {
                Shape = new System.Windows.Shapes.Ellipse { Width = 10, Height = 10, Fill = bColor }
            };

            this.mcMapControl.Markers.Add(marker);
        }


        /// <summary>
        /// Creates the heatmap2.
        /// </summary>
        [Conditional("DEBUG")]
        private void CreateHeatmap2()
        {
            PointLatLng pll = this.mcMapControl.Position;

            const double dWidth = 0.0005;
            const double dHeight = 0.0003;

            const int iKachelBreite = 8;
            const int iKachelHöhe = 8;

            Random r = new Random();
            List<Brush> colors = new List<Brush> { Brushes.White, Brushes.LightYellow, Brushes.Yellow, Brushes.Orange, Brushes.OrangeRed, Brushes.Red };

            for( double x = pll.Lng - ( iKachelBreite * dWidth ) ; x < pll.Lng + ( iKachelBreite * dWidth ) ; x += dWidth )
            {
                for( double y = pll.Lat - ( iKachelHöhe * dHeight ) ; y < pll.Lat + ( iKachelHöhe * dHeight ) ; y += dHeight )
                {
                    PointLatLng pllPos = new PointLatLng(y, x);

                    CreateHeatmap2(pllPos, r.NextObject(colors));
                }
            }
        }


        /// <summary>
        /// Handles the Click event of the MenuItem_HeatmapTest control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void MenuItem_HeatmapTest_Click( object sender, RoutedEventArgs e )
        {
            CreateHeatmap();

            e.Handled = true;
        }

    } // end public partial class MainWindow
}
