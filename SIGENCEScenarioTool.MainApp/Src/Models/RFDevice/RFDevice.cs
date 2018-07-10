﻿
/**
 * !!! GENERATED STUFF - DO NOT MODIFY MANUALLY !!!
 */

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

using SIGENCEScenarioTool.Extensions;
using SIGENCEScenarioTool.Interfaces;



namespace SIGENCEScenarioTool.Models
{
    ///<summary>
    /// Represent A Device Based On A Radio Frequency.
    ///</summary>
    sealed public class RFDevice : IEquatable<RFDevice>, INotifyPropertyChanged, ICloneable, IXmlExport
    {

        #region Instance Properties

        #region PrimaryKey

        ///<summary>
        /// The PropertyName As ReadOnly String For PrimaryKey.
        ///</summary>
        public const String PRIMARYKEY = "PrimaryKey";

        ///<summary>
        /// The DefaultValue For PrimaryKey.
        ///</summary>
        static public readonly Guid DEFAULT_PRIMARYKEY = Guid.NewGuid();
        
        ///<summary>
        /// The Internal Field For PrimaryKey.
        ///</summary>
        private Guid _PrimaryKey = Guid.NewGuid();

        ///<summary>
        /// The Unique PrimarKey For This RF Device.
        ///</summary>
        public Guid PrimaryKey 
        {
            get { return _PrimaryKey; }
            set
            {
                _PrimaryKey = value;
                FirePropertyChanged();
            }
        }

        #endregion        

        //---------------------------------------------------------------------

        #region Id

        ///<summary>
        /// The PropertyName As ReadOnly String For Id.
        ///</summary>
        public const String ID = "Id";

        ///<summary>
        /// The DefaultValue For Id.
        ///</summary>
        static public readonly int DEFAULT_ID = 0;
        
        ///<summary>
        /// The Internal Field For Id.
        ///</summary>
        private int _Id = 0;

        ///<summary>
        /// Every scenario element (i.e. transmitter, receiver) must be assigned an unique ID. Negative ID’s are reserved for receivers while all other ID’s are transmitters by default. Some applications (i.e. TDoA Emitter Localization) require a reference transmitter. For these applications ID=0 is the reference transmitter. Receivers must be assigned first in the table, followed be transmitters (with ID=0 being the first). After the Static Scenario, update of ID’s requires no specific order. Note that definition of new transmitters/receivers after the Static Scenario is prohibited.
        ///</summary>
        public int Id 
        {
            get { return _Id; }
            set
            {
                _Id = value;
                FirePropertyChanged();
            }
        }

        #endregion        

        //---------------------------------------------------------------------

        #region Name

        ///<summary>
        /// The PropertyName As ReadOnly String For Name.
        ///</summary>
        public const String NAME = "Name";

        ///<summary>
        /// The DefaultValue For Name.
        ///</summary>
        static public readonly string DEFAULT_NAME = "RFDevice";
        
        ///<summary>
        /// The Internal Field For Name.
        ///</summary>
        private string _Name = "RFDevice";

        ///<summary>
        /// A Short Describing Display Name For The RF Device.
        ///</summary>
        public string Name 
        {
            get { return _Name; }
            set
            {
                _Name = value;
                FirePropertyChanged();
            }
        }

        #endregion        

        //---------------------------------------------------------------------

        #region Latitude

        ///<summary>
        /// The PropertyName As ReadOnly String For Latitude.
        ///</summary>
        public const String LATITUDE = "Latitude";

        ///<summary>
        /// The DefaultValue For Latitude.
        ///</summary>
        static public readonly double DEFAULT_LATITUDE = double.NaN;
        
        ///<summary>
        /// The Internal Field For Latitude.
        ///</summary>
        private double _Latitude = double.NaN;

        ///<summary>
        /// The Latitude Of The RF Device (WGS84).
        ///</summary>
        public double Latitude 
        {
            get { return _Latitude; }
            set
            {
                _Latitude = value;
                FirePropertyChanged();
            }
        }

        #endregion        

        //---------------------------------------------------------------------

        #region Longitude

        ///<summary>
        /// The PropertyName As ReadOnly String For Longitude.
        ///</summary>
        public const String LONGITUDE = "Longitude";

        ///<summary>
        /// The DefaultValue For Longitude.
        ///</summary>
        static public readonly double DEFAULT_LONGITUDE = double.NaN;
        
        ///<summary>
        /// The Internal Field For Longitude.
        ///</summary>
        private double _Longitude = double.NaN;

        ///<summary>
        /// The Longitude Of The RF Device (WGS84).
        ///</summary>
        public double Longitude 
        {
            get { return _Longitude; }
            set
            {
                _Longitude = value;
                FirePropertyChanged();
            }
        }

        #endregion        

        //---------------------------------------------------------------------

        #region Altitude

        ///<summary>
        /// The PropertyName As ReadOnly String For Altitude.
        ///</summary>
        public const String ALTITUDE = "Altitude";

        ///<summary>
        /// The DefaultValue For Altitude.
        ///</summary>
        static public readonly uint DEFAULT_ALTITUDE = 0;
        
        ///<summary>
        /// The Internal Field For Altitude.
        ///</summary>
        private uint _Altitude = 0;

        ///<summary>
        /// The Elevation Of The RF Device Above The Sea Level (Meter).
        ///</summary>
        public uint Altitude 
        {
            get { return _Altitude; }
            set
            {
                _Altitude = value;
                FirePropertyChanged();
            }
        }

        #endregion        

        //---------------------------------------------------------------------

        #region Roll

        ///<summary>
        /// The PropertyName As ReadOnly String For Roll.
        ///</summary>
        public const String ROLL = "Roll";

        ///<summary>
        /// The DefaultValue For Roll.
        ///</summary>
        static public readonly double DEFAULT_ROLL = 0;
        
        ///<summary>
        /// The Internal Field For Roll.
        ///</summary>
        private double _Roll = 0;

        ///<summary>
        /// These parameters set the orientation of transmitter / receiver antennas. The respective antenna type is defined by AntennaType. The RF simulation uses the antenna orientation to compute the resulting signal power at the receivers.
        ///</summary>
        public double Roll 
        {
            get { return _Roll; }
            set
            {
                _Roll = value;
                FirePropertyChanged();
            }
        }

        #endregion        

        //---------------------------------------------------------------------

        #region Pitch

        ///<summary>
        /// The PropertyName As ReadOnly String For Pitch.
        ///</summary>
        public const String PITCH = "Pitch";

        ///<summary>
        /// The DefaultValue For Pitch.
        ///</summary>
        static public readonly double DEFAULT_PITCH = 0;
        
        ///<summary>
        /// The Internal Field For Pitch.
        ///</summary>
        private double _Pitch = 0;

        ///<summary>
        /// These parameters set the orientation of transmitter / receiver antennas. The respective antenna type is defined by AntennaType. The RF simulation uses the antenna orientation to compute the resulting signal power at the receivers.
        ///</summary>
        public double Pitch 
        {
            get { return _Pitch; }
            set
            {
                _Pitch = value;
                FirePropertyChanged();
            }
        }

        #endregion        

        //---------------------------------------------------------------------

        #region Yaw

        ///<summary>
        /// The PropertyName As ReadOnly String For Yaw.
        ///</summary>
        public const String YAW = "Yaw";

        ///<summary>
        /// The DefaultValue For Yaw.
        ///</summary>
        static public readonly double DEFAULT_YAW = 0;
        
        ///<summary>
        /// The Internal Field For Yaw.
        ///</summary>
        private double _Yaw = 0;

        ///<summary>
        /// These parameters set the orientation of transmitter / receiver antennas. The respective antenna type is defined by AntennaType. The RF simulation uses the antenna orientation to compute the resulting signal power at the receivers.
        ///</summary>
        public double Yaw 
        {
            get { return _Yaw; }
            set
            {
                _Yaw = value;
                FirePropertyChanged();
            }
        }

        #endregion        

        //---------------------------------------------------------------------

        #region RxTxType

        ///<summary>
        /// The PropertyName As ReadOnly String For RxTxType.
        ///</summary>
        public const String RXTXTYPE = "RxTxType";

        ///<summary>
        /// The DefaultValue For RxTxType.
        ///</summary>
        static public readonly RxTxType DEFAULT_RXTXTYPE = RxTxType.Unknown;
        
        ///<summary>
        /// The Internal Field For RxTxType.
        ///</summary>
        private RxTxType _RxTxType = RxTxType.Unknown;

        ///<summary>
        /// For All Receivers (i.e. ID’s < 0) This Parameter Defines The Radio Being Used.
        ///</summary>
        public RxTxType RxTxType 
        {
            get { return _RxTxType; }
            set
            {
                _RxTxType = value;
                FirePropertyChanged();
            }
        }

        #endregion        

        //---------------------------------------------------------------------

        #region AntennaType

        ///<summary>
        /// The PropertyName As ReadOnly String For AntennaType.
        ///</summary>
        public const String ANTENNATYPE = "AntennaType";

        ///<summary>
        /// The DefaultValue For AntennaType.
        ///</summary>
        static public readonly AntennaType DEFAULT_ANTENNATYPE = AntennaType.Unknown;
        
        ///<summary>
        /// The Internal Field For AntennaType.
        ///</summary>
        private AntennaType _AntennaType = AntennaType.Unknown;

        ///<summary>
        /// AntennaType Defines The Antenna Type Used For Transmitter And Receiver Respectively. Note: Currently, Only Omnidirectional Antenna Type Is Available / Supported.
        ///</summary>
        public AntennaType AntennaType 
        {
            get { return _AntennaType; }
            set
            {
                _AntennaType = value;
                FirePropertyChanged();
            }
        }

        #endregion        

        //---------------------------------------------------------------------

        #region CenterFrequency_Hz

        ///<summary>
        /// The PropertyName As ReadOnly String For CenterFrequency_Hz.
        ///</summary>
        public const String CENTERFREQUENCY_HZ = "CenterFrequency_Hz";

        ///<summary>
        /// The DefaultValue For CenterFrequency_Hz.
        ///</summary>
        static public readonly ulong DEFAULT_CENTERFREQUENCY_HZ = 0;
        
        ///<summary>
        /// The Internal Field For CenterFrequency_Hz.
        ///</summary>
        private ulong _CenterFrequency_Hz = 0;

        ///<summary>
        /// For transmitters (i.e. ID’s >= 0) this parameter defines transmitter signal center frequency [Hz]. For receivers (i.e. ID’s < 0) this parameter is currently unused.
        ///</summary>
        public ulong CenterFrequency_Hz 
        {
            get { return _CenterFrequency_Hz; }
            set
            {
                _CenterFrequency_Hz = value;
                FirePropertyChanged();
            }
        }

        #endregion        

        //---------------------------------------------------------------------

        #region Bandwith_Hz

        ///<summary>
        /// The PropertyName As ReadOnly String For Bandwith_Hz.
        ///</summary>
        public const String BANDWITH_HZ = "Bandwith_Hz";

        ///<summary>
        /// The DefaultValue For Bandwith_Hz.
        ///</summary>
        static public readonly uint DEFAULT_BANDWITH_HZ = 0;
        
        ///<summary>
        /// The Internal Field For Bandwith_Hz.
        ///</summary>
        private uint _Bandwith_Hz = 0;

        ///<summary>
        /// Bandwith_Hz as uint.
        ///</summary>
        public uint Bandwith_Hz 
        {
            get { return _Bandwith_Hz; }
            set
            {
                _Bandwith_Hz = value;
                FirePropertyChanged();
            }
        }

        #endregion        

        //---------------------------------------------------------------------

        #region Gain_dB

        ///<summary>
        /// The PropertyName As ReadOnly String For Gain_dB.
        ///</summary>
        public const String GAIN_DB = "Gain_dB";

        ///<summary>
        /// The DefaultValue For Gain_dB.
        ///</summary>
        static public readonly int DEFAULT_GAIN_DB = 0;
        
        ///<summary>
        /// The Internal Field For Gain_dB.
        ///</summary>
        private int _Gain_dB = 0;

        ///<summary>
        /// For transmitters (i.e. ID’s >= 0) this parameter defines transmitter signal power [dBm]. For receivers (i.e. ID’s < 0) this parameter is currently unused.
        ///</summary>
        public int Gain_dB 
        {
            get { return _Gain_dB; }
            set
            {
                _Gain_dB = value;
                FirePropertyChanged();
            }
        }

        #endregion        

        //---------------------------------------------------------------------

        #region SignalToNoiseRatio_dB

        ///<summary>
        /// The PropertyName As ReadOnly String For SignalToNoiseRatio_dB.
        ///</summary>
        public const String SIGNALTONOISERATIO_DB = "SignalToNoiseRatio_dB";

        ///<summary>
        /// The DefaultValue For SignalToNoiseRatio_dB.
        ///</summary>
        static public readonly uint DEFAULT_SIGNALTONOISERATIO_DB = 0;
        
        ///<summary>
        /// The Internal Field For SignalToNoiseRatio_dB.
        ///</summary>
        private uint _SignalToNoiseRatio_dB = 0;

        ///<summary>
        /// For receivers (i.e. ID’s < 0) this parameter is imposes Gaussian White Noise to the respective receiver signal. For transmitters (i.e. ID’s >= 0) this parameter is unused.
        ///</summary>
        public uint SignalToNoiseRatio_dB 
        {
            get { return _SignalToNoiseRatio_dB; }
            set
            {
                _SignalToNoiseRatio_dB = value;
                FirePropertyChanged();
            }
        }

        #endregion        

        //---------------------------------------------------------------------

        #region XPos

        ///<summary>
        /// The PropertyName As ReadOnly String For XPos.
        ///</summary>
        public const String XPOS = "XPos";

        ///<summary>
        /// The DefaultValue For XPos.
        ///</summary>
        static public readonly int DEFAULT_XPOS = 0;
        
        ///<summary>
        /// The Internal Field For XPos.
        ///</summary>
        private int _XPos = 0;

        ///<summary>
        /// XPos,YPos,ZPos Define The Transmitter / Receiver Positions In A Local Coordinate System With The Transmitter (ID=0) Being The Center Position.
        ///</summary>
        public int XPos 
        {
            get { return _XPos; }
            set
            {
                _XPos = value;
                FirePropertyChanged();
            }
        }

        #endregion        

        //---------------------------------------------------------------------

        #region YPos

        ///<summary>
        /// The PropertyName As ReadOnly String For YPos.
        ///</summary>
        public const String YPOS = "YPos";

        ///<summary>
        /// The DefaultValue For YPos.
        ///</summary>
        static public readonly int DEFAULT_YPOS = 0;
        
        ///<summary>
        /// The Internal Field For YPos.
        ///</summary>
        private int _YPos = 0;

        ///<summary>
        /// XPos,YPos,ZPos Define The Transmitter / Receiver Positions In A Local Coordinate System With The Transmitter (ID=0) Being The Center Position.
        ///</summary>
        public int YPos 
        {
            get { return _YPos; }
            set
            {
                _YPos = value;
                FirePropertyChanged();
            }
        }

        #endregion        

        //---------------------------------------------------------------------

        #region ZPos

        ///<summary>
        /// The PropertyName As ReadOnly String For ZPos.
        ///</summary>
        public const String ZPOS = "ZPos";

        ///<summary>
        /// The DefaultValue For ZPos.
        ///</summary>
        static public readonly int DEFAULT_ZPOS = 0;
        
        ///<summary>
        /// The Internal Field For ZPos.
        ///</summary>
        private int _ZPos = 0;

        ///<summary>
        /// XPos,YPos,ZPos Define The Transmitter / Receiver Positions In A Local Coordinate System With The Transmitter (ID=0) Being The Center Position.
        ///</summary>
        public int ZPos 
        {
            get { return _ZPos; }
            set
            {
                _ZPos = value;
                FirePropertyChanged();
            }
        }

        #endregion        

        //---------------------------------------------------------------------

        #region StartTime

        ///<summary>
        /// The PropertyName As ReadOnly String For StartTime.
        ///</summary>
        public const String STARTTIME = "StartTime";

        ///<summary>
        /// The DefaultValue For StartTime.
        ///</summary>
        static public readonly double DEFAULT_STARTTIME = 0;
        
        ///<summary>
        /// The Internal Field For StartTime.
        ///</summary>
        private double _StartTime = 0;

        ///<summary>
        /// This Is The Simulation Time At Which The Parameters (Following The Time Parameter In The Same Line) Are Set. All Transmitters And Receivers Used In The Simulation Must Be Set At Start Of The Simulation, I.E. At Time=0. For Static Scenarios, Where Positions Or Characteristics Settings Never Change Throughout The Simulation, The Time Column Only Contains Zero’s.
        ///</summary>
        public double StartTime 
        {
            get { return _StartTime; }
            set
            {
                _StartTime = value;
                FirePropertyChanged();
            }
        }

        #endregion        

        //---------------------------------------------------------------------

        #region Remark

        ///<summary>
        /// The PropertyName As ReadOnly String For Remark.
        ///</summary>
        public const String REMARK = "Remark";

        ///<summary>
        /// The DefaultValue For Remark.
        ///</summary>
        static public readonly string DEFAULT_REMARK = "";
        
        ///<summary>
        /// The Internal Field For Remark.
        ///</summary>
        private string _Remark = "";

        ///<summary>
        /// A Comment Or Remark For The RF Device.
        ///</summary>
        public string Remark 
        {
            get { return _Remark; }
            set
            {
                _Remark = value;
                FirePropertyChanged();
            }
        }

        #endregion        

        #endregion

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        public XElement ToXml()
        {
            return new XElement("RFDevice",

                XElementExtension.GetXElement("PrimaryKey", PrimaryKey),
                XElementExtension.GetXElement("Id", Id),
                XElementExtension.GetXElement("Name", Name),
                XElementExtension.GetXElement("Latitude", Latitude),
                XElementExtension.GetXElement("Longitude", Longitude),
                XElementExtension.GetXElement("Altitude", Altitude),
                XElementExtension.GetXElement("Roll", Roll),
                XElementExtension.GetXElement("Pitch", Pitch),
                XElementExtension.GetXElement("Yaw", Yaw),
                XElementExtension.GetXElement("RxTxType", RxTxType),
                XElementExtension.GetXElement("AntennaType", AntennaType),
                XElementExtension.GetXElement("CenterFrequency_Hz", CenterFrequency_Hz),
                XElementExtension.GetXElement("Bandwith_Hz", Bandwith_Hz),
                XElementExtension.GetXElement("Gain_dB", Gain_dB),
                XElementExtension.GetXElement("SignalToNoiseRatio_dB", SignalToNoiseRatio_dB),
                XElementExtension.GetXElement("XPos", XPos),
                XElementExtension.GetXElement("YPos", YPos),
                XElementExtension.GetXElement("ZPos", ZPos),
                XElementExtension.GetXElement("StartTime", StartTime),
                XElementExtension.GetXElement("Remark", Remark)  
            );
        }


        static public RFDevice FromXml(XElement eRoot)
        {
            XElement eChild = null;

            if (eRoot.Name.LocalName.Equals("RFDevice"))
            {
                eChild = eRoot;
            }
            else
            {
                eChild = eRoot.Element("RFDevice");
            }

            return new RFDevice
            {
                PrimaryKey = eChild.GetProperty<Guid>("PrimaryKey",Guid.NewGuid()),
                Id = eChild.GetProperty<int>("Id",0),
                Name = eChild.GetProperty<string>("Name","RFDevice"),
                Latitude = eChild.GetProperty<double>("Latitude",double.NaN),
                Longitude = eChild.GetProperty<double>("Longitude",double.NaN),
                Altitude = eChild.GetProperty<uint>("Altitude",0),
                Roll = eChild.GetProperty<double>("Roll",0),
                Pitch = eChild.GetProperty<double>("Pitch",0),
                Yaw = eChild.GetProperty<double>("Yaw",0),
                RxTxType = eChild.GetProperty<RxTxType>("RxTxType",RxTxType.Unknown),
                AntennaType = eChild.GetProperty<AntennaType>("AntennaType",AntennaType.Unknown),
                CenterFrequency_Hz = eChild.GetProperty<ulong>("CenterFrequency_Hz",0),
                Bandwith_Hz = eChild.GetProperty<uint>("Bandwith_Hz",0),
                Gain_dB = eChild.GetProperty<int>("Gain_dB",0),
                SignalToNoiseRatio_dB = eChild.GetProperty<uint>("SignalToNoiseRatio_dB",0),
                XPos = eChild.GetProperty<int>("XPos",0),
                YPos = eChild.GetProperty<int>("YPos",0),
                ZPos = eChild.GetProperty<int>("ZPos",0),
                StartTime = eChild.GetProperty<double>("StartTime",0),
                Remark = eChild.GetProperty<string>("Remark","")            
            };
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        public bool Equals(RFDevice other)
        {
            if (other == null)
            {
                return false;
            }

            if (PrimaryKey != other.PrimaryKey )
            {
                return false;
            }

            if (Id != other.Id )
            {
                return false;
            }

            if (Name != other.Name )
            {
                return false;
            }

            if (Latitude != other.Latitude )
            {
                return false;
            }

            if (Longitude != other.Longitude )
            {
                return false;
            }

            if (Altitude != other.Altitude )
            {
                return false;
            }

            if (Roll != other.Roll )
            {
                return false;
            }

            if (Pitch != other.Pitch )
            {
                return false;
            }

            if (Yaw != other.Yaw )
            {
                return false;
            }

            if (RxTxType != other.RxTxType )
            {
                return false;
            }

            if (AntennaType != other.AntennaType )
            {
                return false;
            }

            if (CenterFrequency_Hz != other.CenterFrequency_Hz )
            {
                return false;
            }

            if (Bandwith_Hz != other.Bandwith_Hz )
            {
                return false;
            }

            if (Gain_dB != other.Gain_dB )
            {
                return false;
            }

            if (SignalToNoiseRatio_dB != other.SignalToNoiseRatio_dB )
            {
                return false;
            }

            if (XPos != other.XPos )
            {
                return false;
            }

            if (YPos != other.YPos )
            {
                return false;
            }

            if (ZPos != other.ZPos )
            {
                return false;
            }

            if (StartTime != other.StartTime )
            {
                return false;
            }

            if (Remark != other.Remark )
            {
                return false;
            }

            return true;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.IsNullOrEmpty(Name) ? "Unknown" : Name;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public RFDevice Clone()
        {
            return (RFDevice)this.MemberwiseClone();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        object ICloneable.Clone()
        {
            return this.MemberwiseClone();
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="strPropertyName"></param>
        private void FirePropertyChanged([CallerMemberName]string strPropertyName = null)
        {
            // Wir cachen das Event lokal da es während der Abfrage in der if Anweisung und
            // dem eigentlichen Ausführen zurückgesetzt werden könnte und somit eine Exception
            // hervorgerufen werden könnte obwohl wir es ja überprüft haben.
            var temp = PropertyChanged;

            if (temp != null)
            {
                temp(this, new PropertyChangedEventArgs(strPropertyName));
            }
        }

    } // end sealed public class RFDevice
}