using System;
using System.Collections.Generic;

namespace PeterO.Cbor {
    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="T:PeterO.Cbor.CBORTypeMapper"]/*'/>
  public sealed class CBORTypeMapper {
    private readonly IList<string> typePrefixes;
    private readonly IList<string> typeNames;
    private readonly IDictionary<Object, ConverterInfo>
      converters;

    /// <summary>Initializes a new instance of the
    /// <see cref='CBORTypeMapper'/> class.</summary>
    public CBORTypeMapper() {
      this.typePrefixes = new List<string>();
      this.typeNames = new List<string>();
      this.converters = new Dictionary<Object, ConverterInfo>();
    }

    /// <include file='../../docs.xml'
    ///   path='docs/doc[@name="M:PeterO.Cbor.CBORTypeMapper.AddConverter``1(System.Type,PeterO.Cbor.ICBORConverter{``0})"]/*'/>
    public CBORTypeMapper AddConverter<T>(
      Type type,
      ICBORConverter<T> converter) {
      if (type == null) {
        throw new ArgumentNullException(nameof(type));
      }
      if (converter == null) {
        throw new ArgumentNullException(nameof(converter));
      }
      var ci = new ConverterInfo();
      ci.Converter = converter;
      ci.ToObject = PropertyMap.FindOneArgumentMethod(
        converter,
        "ToCBORObject",
        type);
      if (ci.ToObject == null) {
        throw new ArgumentException(
          "Converter doesn't contain a proper ToCBORObject method");
      }
      ci.FromObject = PropertyMap.FindOneArgumentMethod(
        converter,
        "FromCBORObject",
        typeof(CBORObject));
      this.converters[type] = ci;
      return this;
    }

    internal object ConvertBackWithConverter(
      CBORObject cbor,
      Type type) {
      ConverterInfo convinfo = null;
      if (this.converters.ContainsKey(type)) {
        convinfo = this.converters[type];
      } else {
        return null;
      }
      if (convinfo == null) {
        return null;
      }
      if (convinfo.FromObject == null) {
        return null;
      }
      return PropertyMap.InvokeOneArgumentMethod(
        convinfo.FromObject,
        convinfo.Converter,
        cbor);
    }

    internal CBORObject ConvertWithConverter(object obj) {
      Object type = obj.GetType();
      ConverterInfo convinfo = null;
      if (this.converters.ContainsKey(type)) {
        convinfo = this.converters[type];
      } else {
        return null;
      }
      if (convinfo == null) {
        return null;
      }
      return (CBORObject)PropertyMap.InvokeOneArgumentMethod(
        convinfo.ToObject,
        convinfo.Converter,
        obj);
    }

    /// <include file='../../docs.xml'
    ///   path='docs/doc[@name="M:PeterO.Cbor.CBORTypeMapper.FilterTypeName(System.String)"]/*'/>
    public bool FilterTypeName(string typeName) {
      if (String.IsNullOrEmpty(typeName)) {
        return false;
      }
      foreach (string prefix in this.typePrefixes) {
        if (typeName.Length >= prefix.Length &&
          typeName.Substring(0, prefix.Length).Equals(prefix,
  StringComparison.Ordinal)) {
          return true;
        }
      }
      foreach (string name in this.typeNames) {
        if (typeName.Equals(name, StringComparison.Ordinal)) {
          return true;
        }
      }
      return false;
    }

    /// <include file='../../docs.xml'
    ///   path='docs/doc[@name="M:PeterO.Cbor.CBORTypeMapper.AddTypePrefix(System.String)"]/*'/>
    public CBORTypeMapper AddTypePrefix(string prefix) {
      if (prefix == null) {
        throw new ArgumentNullException(nameof(prefix));
      }
      if (prefix.Length == 0) {
        throw new ArgumentException("prefix" + " is empty.");
      }
      this.typePrefixes.Add(prefix);
      return this;
    }

    /// <include file='../../docs.xml'
    ///   path='docs/doc[@name="M:PeterO.Cbor.CBORTypeMapper.AddTypeName(System.String)"]/*'/>
    public CBORTypeMapper AddTypeName(string name) {
      if (name == null) {
        throw new ArgumentNullException(nameof(name));
      }
      if (name.Length == 0) {
        throw new ArgumentException("name" + " is empty.");
      }
      this.typeNames.Add(name);
      return this;
    }

    internal sealed class ConverterInfo {
      public object ToObject { get; set; }

      public object FromObject { get; set; }

      public object Converter { get; set; }
    }
  }
}
