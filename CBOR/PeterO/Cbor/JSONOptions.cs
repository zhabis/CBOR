using System;
using System.Collections.Generic;
using System.Text;

namespace PeterO.Cbor {
    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="T:PeterO.Cbor.JSONOptions"]/*'/>
  public sealed class JSONOptions {
    /// <summary>Initializes a new instance of the
    /// <see cref='PeterO.Cbor.JSONOptions'/> class with default
    /// options.</summary>
    public JSONOptions() : this(false) {
    }

    /// <summary>Initializes a new instance of the
    /// <see cref='PeterO.Cbor.JSONOptions'/> class with the given value
    /// for the Base64Padding option.</summary>
    /// <param name='base64Padding'>Whether padding is included when
    /// writing data in base64url or traditional base64 format to
    /// JSON.</param>
    public JSONOptions(bool base64Padding) : this(base64Padding, false) {
    }

    /// <summary>Initializes a new instance of the
    /// <see cref='PeterO.Cbor.JSONOptions'/> class with the given values
    /// for the options.</summary>
    /// <param name='base64Padding'>Whether padding is included when
    /// writing data in base64url or traditional base64 format to
    /// JSON.</param>
    /// <param name='replaceSurrogates'>Whether surrogate code points not
    /// part of a surrogate pair (which consists of two consecutive
    /// <c>char</c> s forming one Unicode code point) are each replaced
    /// with a replacement character (U + FFFD). The default is false; an
    /// exception is thrown when such code points are encountered.</param>
#pragma warning disable CS0618
    public JSONOptions(bool base64Padding, bool replaceSurrogates) {
      this.Base64Padding = base64Padding;
      this.ReplaceSurrogates = replaceSurrogates;
    }

    /// <summary>Initializes a new instance of the
    /// <see cref='PeterO.Cbor.JSONOptions'/> class.</summary>
    /// <param name='paramString'>A string setting forth the options to
    /// use. This is a semicolon-separated list of options, each of which
    /// has a key and a value separated by an equal sign ("="). Whitespace
    /// and line separators are not allowed to appear between the
    /// semicolons or between the equal signs, nor may the string begin or
    /// end with whitespace. The string can be empty, but cannot be null.
    /// The following is an example of this parameter:
    /// <c>base64padding=false;replacesurrogates=true</c>. The key can be
    /// any one of the following in any combination of case:
    /// <c>base64padding</c>, <c>replacesurrogates</c>. Other keys are
    /// ignored. If the same key appears more than once, the value given
    /// for the last such key is used. The two keys just given can have a
    /// value of <c>1</c>, <c>true</c>, <c>yes</c>, or <c>on</c> (in any
    /// combination of case), which means true, and any other value meaning
    /// false. For example, <c>base64padding=Yes</c> and
    /// <c>base64padding=1</c> both set the <c>Base64Padding</c> property
    /// to true.</param>
    /// <exception cref='ArgumentNullException'>The parameter <paramref
    /// name='paramString'/> is null.</exception>
    public JSONOptions(string paramString) {
      if (paramString == null) {
        throw new ArgumentNullException(nameof(paramString));
      }
      var parser = new OptionsParser(paramString);
      this.Base64Padding = parser.GetBoolean("base64padding", true);
      this.ReplaceSurrogates = parser.GetBoolean("replacesurrogates", true);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Cbor.JSONOptions.ToString"]/*'/>
    public override string ToString() {
      return new StringBuilder()
           .Append("base64padding=")
           .Append(this.Base64Padding ? "true" : "false")
           .Append(";replacesurrogates=")
           .Append(this.ReplaceSurrogates ? "true" : "false")
           .ToString();
    }

    /// <summary>The default options for converting CBOR objects to
    /// JSON.</summary>
    public static readonly JSONOptions Default = new JSONOptions();

    /// <include file='../../docs.xml'
    ///   path='docs/doc[@name="P:PeterO.Cbor.JSONOptions.Base64Padding"]/*'/>
    [Obsolete("This option now has no effect. This library now includes " +
         "necessary padding when writing traditional base64 to JSON and" +
         " includes no padding when writing base64url to JSON, in " +
         "accordance with the revision of the CBOR specification.")]
    public bool Base64Padding { get; private set; }

    /// <include file='../../docs.xml'
    ///   path='docs/doc[@name="P:PeterO.Cbor.JSONOptions.ReplaceSurrogates"]/*'/>
    public bool ReplaceSurrogates { get; private set; }
   }
}
