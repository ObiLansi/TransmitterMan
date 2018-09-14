﻿using System;
using System.IO;
using System.Xml.Linq;

using GMap.NET;

using log4net;

using NUnit.Framework;

using SIGENCEScenarioTool.Extensions;
using SIGENCEScenarioTool.Models;
using SIGENCEScenarioTool.UnitTest.Attributes;



namespace SIGENCEScenarioTool.UnitTests
{
    /// <summary>
    /// 
    /// </summary>
    [TestFixture]
    [SIGENCEScenarioToolTestAction]
    internal sealed class SIGENCEScenarioToolExportImportTest
    {
        /// <summary>
        /// Logger zum Ausgeben der Protokollierung.
        /// </summary>
        static private readonly ILog Log = LogManager.GetLogger( typeof( SIGENCEScenarioToolExportImportTest ) );

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Test the export and import of XML.
        /// </summary>
        [Test, Category( "ExportImport" ), SIGENCEScenarioToolTestCase( "28321d28-352c-462a-9d87-49b1b3ad7224" ), Description( "Tests if the RFDevices are properly exported and imported as XML." )]
        public void Test000_ExportAndImportRFDeviceXml()
        {
            SIGENCEScenarioToolTestCaseHelper.ShowTestCaseInformation();

            //-----------------------------------------------------------------

            RFDevice source = new RFDevice
            {
                Id = -42 ,
                RxTxType = RxTxType.IdealSDR ,
                AntennaType = AntennaType.Unknown ,
                StartTime = 42 ,
                Latitude = 15 ,
                Longitude = 10 ,
                Altitude = 1974 ,
                CenterFrequency_Hz = 90000 ,
                Bandwith_Hz = 10000 ,
                Gain_dB = 5 ,
                SignalToNoiseRatio_dB = 263 ,
                Roll = -42 ,
                Pitch = -42 ,
                Yaw = -42 ,
                XPos = -1 ,
                YPos = -2 ,
                ZPos = -3 ,
                Name = "Han Solo" ,
                Remark = "A Star Wars Story." ,
            };

            XElement e = source.ToXml();

            Assert.NotNull( e );

            string strFilename = string.Format( "{0}nunit_rfdevice.{1}.xml" , Path.GetTempPath() , DateTime.Now.ToString( "yyyyMMdd_HHmmssfff" ) );

            e.SaveDefault( strFilename );

            Assert.True( File.Exists( strFilename ) );

            //-----------------------------------------------------------------

            XDocument xdoc = XDocument.Load( strFilename );

            Assert.NotNull( xdoc );

            RFDevice destination = RFDevice.FromXml( xdoc.Root );

            Assert.NotNull( destination );

            Assert.True( destination.Equals( source ) );
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// 
        /// </summary>
        public enum FileFormat : byte
        {
            Csv, Xml//, Json

        } // end public enum FileFormat


        /// <summary>
        /// All known formats
        /// </summary>
        static private readonly Array aFormats = Enum.GetValues( typeof( FileFormat ) );


        /// <summary>
        /// Test the export of RFDevices in different fileformats.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="iCount">The i count.</param>
        [Test, Category( "ExportImport" ), SIGENCEScenarioToolTestCase( "9ABC401E-9790-4FD4-9339-371914053AD8" ), Description( "Tests if the RFDevices are properly exported in different formats." )]
        public void Test001_ExportRFDevices( [ValueSource( "aFormats" )] FileFormat format , [Values( 10 , 50 , 200 , 1000 )] int iCount )
        {
            SIGENCEScenarioToolTestCaseHelper.ShowTestCaseInformation();

            //-----------------------------------------------------------------

            RFDeviceList dl = RFDeviceList.CreateRandomizedRFDeviceList( iCount , new PointLatLng( 0 , 0 ) );

            string strFilename = string.Format( "{0}nunit_rfdevice.{1}.{2}" , Path.GetTempPath() , DateTime.Now.ToString( "yyyyMMdd_HHmmssfff" ) , format );

            switch( format )
            {
                case FileFormat.Xml:
                    dl.SaveAsXml( strFilename );
                    break;

                case FileFormat.Csv:
                    dl.SaveAsCsv( strFilename );
                    break;

                    //case FileFormat.Json:
                    //    dl.SaveAsJson(strFilename);
                    //    break;
            }

            //-----------------------------------------------------------------

            Assert.True( File.Exists( strFilename ) );
        }

    } // end sealed class SIGENCEScenarioToolExportImportTest
}
