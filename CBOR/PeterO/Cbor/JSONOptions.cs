using System;
using System.Collections.Generic;
using System.Text;

namespace PeterO.Cbor {
  /// <include file='../../docs.xml'
  /// path='docs/doc[@name="T:PeterO.Cbor.JSONOptions"]/*'/>
  public sealed class JSONOptions {
    /// <summary>Initializes a new instance of the <see cref='JSONOptions'/> class with default options.</summary>
    public JSONOptions() : this(false) {
    }

    /// <summary>Initializes a new instance of the <see cref='JSONOptions'/> class with the given value for the Base64Padding option.</summary><param name='base64Padding'>Whether padding is included when writing data in base64url or traditional
    /// base64 format to JSON.
    /// </param>
    public JSONOptions(bool base64Padding) : this(base64Padding, false) {
    }

    /// <summary>Initializes a new instance of the <see cref='JSONOptions'/> class with the given values for the options.</summary><param name='base64Padding'>Whether padding is included when writing data in base64url or traditional
    /// base64 format to JSON.
    /// </param><param name='replaceSurrogates'>Whether surrogate code points not part of a surrogate pair (which consists
    /// of two consecutive
    /// <c>char</c> s forming one Unicode code point) are each replaced with a replacement
    /// character (U+FFFD). The default is false; an exception is thrown when such
    /// code points are encountered.
    /// </param>
#pragma warning disable CS0618
    public JSONOptions(bool base64Padding, bool replaceSurrogates) {
      this.Base64Padding = base64Padding;
      this.ReplaceSurrogates = replaceSurrogates;
    }
#pragma warning restore CS0618

    /// <summary>The default options for converting CBOR objects to JSON.</summary>
    public static readonly JSONOptions Default = new JSONOptions();

   /// <include file='../../docs.xml'
   /// path='docs/doc[@name="P:PeterO.Cbor.JSONOptions.Base64Padding"]/*'/>
    [Obsolete("This option now has no effect. This library now includes necessary padding when writing traditional base64 to JSON and includes no padding when writing base64url to JSON, in accordance with the revision of the CBOR specification.")]
    public bool Base64Padding { get; private set; }

   /// <include file='../../docs.xml'
   /// path='docs/doc[@name="P:PeterO.Cbor.JSONOptions.ReplaceSurrogates"]/*'/>
    public bool ReplaceSurrogates { get; private set; }
   }
}
