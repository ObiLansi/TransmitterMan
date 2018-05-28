﻿using System.Collections.Generic;



namespace TransmitterTool.Datatypes.Standard
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Collections.Generic.List{System.String}" />
    sealed public class StringList : List<string>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="StringList"/> class.
        /// </summary>
        public StringList()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="StringList"/> class.
        /// </summary>
        /// <param name="iSize">Size of the i.</param>
        public StringList(int iSize)
            : base(iSize)
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="StringList"/> class.
        /// </summary>
        /// <param name="strArray">The string array.</param>
        public StringList(string[] strArray)
            : base(strArray)
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="StringList"/> class.
        /// </summary>
        /// <param name="collection">Die Auflistung, deren Elemente in die neue Liste kopiert werden.</param>
        public StringList(IEnumerable<string> collection)
            : base(collection)
        {
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Performs an implicit conversion from <see cref="StringList"/> to <see cref="System.String[]"/>.
        /// </summary>
        /// <param name="sl">The sl.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        static public implicit operator string[] (StringList sl)
        {
            return sl.ToArray();
        }

    } // end sealed public class StringList
}
