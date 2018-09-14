﻿using System;

namespace SIGENCEScenarioTool.Datatypes.Physically
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="SIGENCEScenarioTool.Datatypes.DataTypeBase{System.Double}" />
    sealed public class Bandwidth : DataTypeBase<double>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Bandwidth" /> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public Bandwidth( double value ) : base( value )
        {
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.Double"/> to <see cref="Bandwidth"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        static public implicit operator Bandwidth( double value )
        {
            return new Bandwidth( value );
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format( CULTUREINFO , "{0}" , this.Value );
        }


        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <returns>
        /// <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </returns>
        public override bool? IsValid()
        {
            throw new NotImplementedException( "public override bool? IsValid()" );
        }

    } // end sealed public class Bandwith
}
