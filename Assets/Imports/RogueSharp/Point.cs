//using System;

//namespace RogueSharp
//{
//   /// <summary>
//   ///   A struct that defines a point in 2D space
//   /// </summary>
//   public struct Point : IEquatable<Point>
//   {
//      private static readonly Point _zeroPoint = new Point();

//      /// <summary>
//      ///   The x coordinate of this <see cref="Point" />.
//      /// </summary>
//      public int X { get; set; }


//      /// <summary>
//      ///   The y coordinate of this <see cref="Point" />.
//      /// </summary>
//      public int Y { get; set; }

//      #region Constructors

//      /// <summary>
//      ///   Constructs a point with X and Y from two values.
//      /// </summary>
//      /// <param name="x">The x coordinate in 2d-space.</param>
//      /// <param name="y">The y coordinate in 2d-space.</param>
//      public Point( int x, int y )
//         : this()
//      {
//         X = x;
//         Y = y;
//      }

//      /// <summary>
//      ///   Constructs a point with X and Y set to the same value.
//      /// </summary>
//      /// <param name="value">The x and y coordinates in 2d-space.</param>
//      public Point( int value )
//         : this()
//      {
//         X = value;
//         Y = value;
//      }

//      #endregion

//      /// <summary>
//      ///   Returns the point (0,0)
//      /// </summary>
//      public static Point Zero
//      {
//         get { return _zeroPoint; }
//      }

//      #region Operators

//      /// <summary>
//      ///   Adds two points.
//      /// </summary>
//      /// <param name="value1">Source <see cref="Point" /> on the left of the add sign.</param>
//      /// <param name="value2">Source <see cref="Point" /> on the right of the add sign.</param>
//      /// <returns>Sum of the points.</returns>
//      public static Point operator +( Point value1, Point value2 )
//      {
//         return new Point( value1.X + value2.X, value1.Y + value2.Y );
//      }

//      /// <summary>
//      ///   Subtracts a <see cref="Point" /> from a <see cref="Point" />.
//      /// </summary>
//      /// <param name="value1">Source <see cref="Point" /> on the left of the sub sign.</param>
//      /// <param name="value2">Source <see cref="Point" /> on the right of the sub sign.</param>
//      /// <returns>Result of the subtraction.</returns>
//      public static Point operator -( Point value1, Point value2 )
//      {
//         return new Point( value1.X - value2.X, value1.Y - value2.Y );
//      }

//      /// <summary>
//      ///   Multiplies the components of two points by each other.
//      /// </summary>
//      /// <param name="value1">Source <see cref="Point" /> on the left of the mul sign.</param>
//      /// <param name="value2">Source <see cref="Point" /> on the right of the mul sign.</param>
//      /// <returns>Result of the multiplication.</returns>
//      public static Point operator *( Point value1, Point value2 )
//      {
//         return new Point( value1.X * value2.X, value1.Y * value2.Y );
//      }

//      /// <summary>
//      ///   Divides the components of a <see cref="Point" /> by the components of another <see cref="Point" />.
//      /// </summary>
//      /// <param name="source">Source <see cref="Point" /> on the left of the div sign.</param>
//      /// <param name="divisor">Divisor <see cref="Point" /> on the right of the div sign.</param>
//      /// <returns>The result of dividing the points.</returns>
//      public static Point operator /( Point source, Point divisor )
//      {
//         return new Point( source.X / divisor.X, source.Y / divisor.Y );
//      }

//      /// <summary>
//      ///   Compares whether two <see cref="Point" /> instances are equal.
//      /// </summary>
//      /// <param name="a"><see cref="Point" /> instance on the left of the equal sign.</param>
//      /// <param name="b"><see cref="Point" /> instance on the right of the equal sign.</param>
//      /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
//      public static bool operator ==( Point a, Point b )
//      {
//         return a.Equals( b );
//      }

//      /// <summary>
//      ///   Compares whether two <see cref="Point" /> instances are not equal.
//      /// </summary>
//      /// <param name="a"><see cref="Point" /> instance on the left of the not equal sign.</param>
//      /// <param name="b"><see cref="Point" /> instance on the right of the not equal sign.</param>
//      /// <returns><c>true</c> if the instances are not equal; <c>false</c> otherwise.</returns>
//      public static bool operator !=( Point a, Point b )
//      {
//         return !a.Equals( b );
//      }

//      #endregion

//      #region Public methods

//      /// <summary>
//      ///   Adds two points.
//      /// </summary>
//      /// <param name="value1">Source <see cref="Point" /> on the left of the add sign.</param>
//      /// <param name="value2">Source <see cref="Point" /> on the right of the add sign.</param>
//      /// <returns>Sum of the points.</returns>
//      public static Point Add( Point value1, Point value2 )
//      {
//         return value1 + value2;
//      }

//      /// <summary>
//      ///   Subtracts a <see cref="Point" /> from a <see cref="Point" />.
//      /// </summary>
//      /// <param name="value1">Source <see cref="Point" /> on the left of the sub sign.</param>
//      /// <param name="value2">Source <see cref="Point" /> on the right of the sub sign.</param>
//      /// <returns>Result of the subtraction.</returns>
//      public static Point Subtract( Point value1, Point value2 )
//      {
//         return value1 - value2;
//      }

//      /// <summary>
//      ///   Multiplies the components of two points by each other.
//      /// </summary>
//      /// <param name="value1">Source <see cref="Point" /> on the left of the mul sign.</param>
//      /// <param name="value2">Source <see cref="Point" /> on the right of the mul sign.</param>
//      /// <returns>Result of the multiplication.</returns>
//      public static Point Multiply( Point value1, Point value2 )
//      {
//         return value1 * value2;
//      }

//      /// <summary>
//      ///   Divides the components of a <see cref="Point" /> by the components of another <see cref="Point" />.
//      /// </summary>
//      /// <param name="source">Source <see cref="Point" /> on the left of the div sign.</param>
//      /// <param name="divisor">Divisor <see cref="Point" /> on the right of the div sign.</param>
//      /// <returns>The result of dividing the points.</returns>
//      public static Point Divide( Point source, Point divisor )
//      {
//         return new Point( source.X / divisor.X, source.Y / divisor.Y );
//      }

//      /// <summary>
//      ///   Compares whether current instance is equal to specified <see cref="Object" />.
//      /// </summary>
//      /// <param name="obj">The <see cref="Object" /> to compare.</param>
//      /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
//      public override bool Equals( object obj )
//      {
//         return obj is Point && Equals( (Point) obj );
//      }

//      /// <summary>
//      ///   Compares whether current instance is equal to specified <see cref="Point" />.
//      /// </summary>
//      /// <param name="other">The <see cref="Point" /> to compare.</param>
//      /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
//      public bool Equals( Point other )
//      {
//         return ( X == other.X ) && ( Y == other.Y );
//      }

//      /// <summary>
//      ///   Gets the hash code of this <see cref="Point" />.
//      /// </summary>
//      /// <returns>Hash code of this <see cref="Point" />.</returns>
//      public override int GetHashCode()
//      {
//         return X ^ Y;
//      }

//      /// <summary>
//      ///   Returns a <see cref="String" /> representation of this <see cref="Point" /> in the format:
//      ///   {X:[<see cref="X" />] Y:[<see cref="Y" />]}
//      /// </summary>
//      /// <returns><see cref="String" /> representation of this <see cref="Point" />.</returns>
//      public override string ToString()
//      {
//         return "{X:" + X + " Y:" + Y + "}";
//      }

//      /// <summary>
//      ///   Calculates the distance between two points.
//      /// </summary>
//      /// <param name="value1">Source vector.</param>
//      /// <param name="value2">Source vector.</param>
//      /// <returns>The distance between two points.</returns>
//      public static float Distance( Point value1, Point value2 )
//      {
//         float v1 = value1.X - value2.X, v2 = value1.Y - value2.Y;
//         return (float) Math.Sqrt( v1 * v1 + v2 * v2 );
//      }

//      /// <summary>
//      ///   Creates a new <see cref="Point" /> that contains the specified vector inversion.
//      /// </summary>
//      /// <param name="value">Source <see cref="Point" />.</param>
//      /// <returns>The result of the vector inversion.</returns>
//      public static Point Negate( Point value )
//      {
//         value.X = -value.X;
//         value.Y = -value.Y;
//         return value;
//      }

//      #endregion
//   }
//}
using System;

namespace RogueSharp
{
    /// <summary>
    /// A class that defines a point in 2D space
    /// </summary>
    public class Point : IEquatable<Point>
    {
        private static readonly Point _zeroPoint = new Point();

        /// <summary>
        /// Specifies the x-coordinate of the Point
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Specifies the y-coordinate of the Point
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Initializes a new instance of Point
        /// </summary>
        public Point()
        {
        }

        /// <summary>
        /// Initializes a new instance of Point
        /// </summary>
        /// <param name="x">The x-coordinate of the Point</param>
        /// <param name="y">The y-coordinate of the Point</param>
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Returns the point (0,0)
        /// </summary>
        public static Point Zero
        {
            get
            {
                return _zeroPoint;
            }
        }

        /// <summary>
        /// Determines whether two Point instances are equal
        /// </summary>
        /// <param name="other">The Point to compare this instance to</param>
        /// <returns>True if the instances are equal; False otherwise</returns>
        /// <exception cref="NullReferenceException">Thrown if .Equals is invoked on null Point</exception>
        public bool Equals(Point other)
        {
            if (other == null)
            {
                return false;
            }

            return ((X == other.X) && (Y == other.Y));
        }

        /// <summary>
        /// Determines whether two Point instances are equal
        /// </summary>
        /// <param name="a">Point on the left side of the equal sign</param>
        /// <param name="b">Point on the right side of the equal sign</param>
        /// <returns>True if a and b are equal; False otherwise</returns>
        public static bool operator ==(Point a, Point b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
            {
                return true;
            }
            if (ReferenceEquals(a, null))
            {
                return false;
            }

            return a.Equals(b);
        }

        /// <summary>
        /// Determines whether two Point instances are not equal
        /// </summary>
        /// <param name="a">Point on the left side of the equal sign</param>
        /// <param name="b">Point on the right side of the equal sign</param>
        /// <returns>True if a and b are not equal; False otherwise</returns>
        public static bool operator !=(Point a, Point b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Determines whether two Point instances are equal
        /// </summary>
        /// <param name="obj">The Object to compare this instance to</param>
        /// <returns>True if the instances are equal; False otherwise</returns>
        /// <exception cref="NullReferenceException">Thrown if .Equals is invoked on null Point</exception>
        public override bool Equals(object obj)
        {
            Point point = obj as Point;
            if (point == null)
            {
                return false;
            }

            return Equals(point);
        }

        /// <summary>
        /// Gets the hash code for this object which can help for quick checks of equality
        /// or when inserting this Point into a hash-based collection such as a Dictionary or Hashtable 
        /// </summary>
        /// <returns>An integer hash used to identify this Point</returns>
        public override int GetHashCode()
        {
            return X ^ Y;
        }

        /// <summary>
        /// Returns a string that represents the current Point
        /// </summary>
        /// <returns>A string that represents the current Point</returns>
        public override string ToString()
        {
            return string.Format("{{X:{0} Y:{1}}}", X, Y);
        }
    }
}