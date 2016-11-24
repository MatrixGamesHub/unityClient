/**
 * Autogenerated by Thrift Compiler (0.10.0)
 *
 * DO NOT EDIT UNLESS YOU ARE SURE THAT YOU KNOW WHAT YOU ARE DOING
 *  @generated
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Thrift;
using Thrift.Collections;
using System.Runtime.Serialization;
using Thrift.Protocol;
using Thrift.Transport;

namespace MtxRendererService
{

  #if !SILVERLIGHT
  [Serializable]
  #endif
  public partial class LevelInfo : TBase
  {
    private string _name;
    private GroundTexture _groundTexture;
    private WallTexture _wallTexture;

    public string Name
    {
      get
      {
        return _name;
      }
      set
      {
        __isset.name = true;
        this._name = value;
      }
    }

    /// <summary>
    /// 
    /// <seealso cref="GroundTexture"/>
    /// </summary>
    public GroundTexture GroundTexture
    {
      get
      {
        return _groundTexture;
      }
      set
      {
        __isset.groundTexture = true;
        this._groundTexture = value;
      }
    }

    /// <summary>
    /// 
    /// <seealso cref="WallTexture"/>
    /// </summary>
    public WallTexture WallTexture
    {
      get
      {
        return _wallTexture;
      }
      set
      {
        __isset.wallTexture = true;
        this._wallTexture = value;
      }
    }


    public Isset __isset;
    #if !SILVERLIGHT
    [Serializable]
    #endif
    public struct Isset {
      public bool name;
      public bool groundTexture;
      public bool wallTexture;
    }

    public LevelInfo() {
    }

    public void Read (TProtocol iprot)
    {
      iprot.IncrementRecursionDepth();
      try
      {
        TField field;
        iprot.ReadStructBegin();
        while (true)
        {
          field = iprot.ReadFieldBegin();
          if (field.Type == TType.Stop) { 
            break;
          }
          switch (field.ID)
          {
            case 1:
              if (field.Type == TType.String) {
                Name = iprot.ReadString();
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            case 2:
              if (field.Type == TType.I32) {
                GroundTexture = (GroundTexture)iprot.ReadI32();
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            case 3:
              if (field.Type == TType.I32) {
                WallTexture = (WallTexture)iprot.ReadI32();
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            default: 
              TProtocolUtil.Skip(iprot, field.Type);
              break;
          }
          iprot.ReadFieldEnd();
        }
        iprot.ReadStructEnd();
      }
      finally
      {
        iprot.DecrementRecursionDepth();
      }
    }

    public void Write(TProtocol oprot) {
      oprot.IncrementRecursionDepth();
      try
      {
        TStruct struc = new TStruct("LevelInfo");
        oprot.WriteStructBegin(struc);
        TField field = new TField();
        if (Name != null && __isset.name) {
          field.Name = "name";
          field.Type = TType.String;
          field.ID = 1;
          oprot.WriteFieldBegin(field);
          oprot.WriteString(Name);
          oprot.WriteFieldEnd();
        }
        if (__isset.groundTexture) {
          field.Name = "groundTexture";
          field.Type = TType.I32;
          field.ID = 2;
          oprot.WriteFieldBegin(field);
          oprot.WriteI32((int)GroundTexture);
          oprot.WriteFieldEnd();
        }
        if (__isset.wallTexture) {
          field.Name = "wallTexture";
          field.Type = TType.I32;
          field.ID = 3;
          oprot.WriteFieldBegin(field);
          oprot.WriteI32((int)WallTexture);
          oprot.WriteFieldEnd();
        }
        oprot.WriteFieldStop();
        oprot.WriteStructEnd();
      }
      finally
      {
        oprot.DecrementRecursionDepth();
      }
    }

    public override string ToString() {
      StringBuilder __sb = new StringBuilder("LevelInfo(");
      bool __first = true;
      if (Name != null && __isset.name) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("Name: ");
        __sb.Append(Name);
      }
      if (__isset.groundTexture) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("GroundTexture: ");
        __sb.Append(GroundTexture);
      }
      if (__isset.wallTexture) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("WallTexture: ");
        __sb.Append(WallTexture);
      }
      __sb.Append(")");
      return __sb.ToString();
    }

  }

}
