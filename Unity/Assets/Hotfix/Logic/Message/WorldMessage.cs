// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: WorldMessage.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using scg = global::System.Collections.Generic;
namespace ET {

  #region Messages
  /// <summary>
  ///进入地图
  /// </summary>
  public partial class C2G_EnterMap : pb::IMessage {
    private static readonly pb::MessageParser<C2G_EnterMap> _parser = new pb::MessageParser<C2G_EnterMap>(() => (C2G_EnterMap)MessagePool.Instance.Fetch(typeof(C2G_EnterMap)));
    public static pb::MessageParser<C2G_EnterMap> Parser { get { return _parser; } }

    private int rpcId_;
    public int RpcId {
      get { return rpcId_; }
      set {
        rpcId_ = value;
      }
    }

    public void WriteTo(pb::CodedOutputStream output) {
      if (RpcId != 0) {
        output.WriteRawTag(208, 5);
        output.WriteInt32(RpcId);
      }
    }

    public int CalculateSize() {
      int size = 0;
      if (RpcId != 0) {
        size += 2 + pb::CodedOutputStream.ComputeInt32Size(RpcId);
      }
      return size;
    }

    public void MergeFrom(pb::CodedInputStream input) {
      rpcId_ = 0;
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 720: {
            RpcId = input.ReadInt32();
            break;
          }
        }
      }
    }

  }

  public partial class G2C_EnterMap : pb::IMessage {
    private static readonly pb::MessageParser<G2C_EnterMap> _parser = new pb::MessageParser<G2C_EnterMap>(() => (G2C_EnterMap)MessagePool.Instance.Fetch(typeof(G2C_EnterMap)));
    public static pb::MessageParser<G2C_EnterMap> Parser { get { return _parser; } }

    private int rpcId_;
    public int RpcId {
      get { return rpcId_; }
      set {
        rpcId_ = value;
      }
    }

    private int error_;
    public int Error {
      get { return error_; }
      set {
        error_ = value;
      }
    }

    private string message_ = "";
    public string Message {
      get { return message_; }
      set {
        message_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    private long unitId_;
    /// <summary>
    /// 自己的unit id
    /// </summary>
    public long UnitId {
      get { return unitId_; }
      set {
        unitId_ = value;
      }
    }

    public void WriteTo(pb::CodedOutputStream output) {
      if (UnitId != 0L) {
        output.WriteRawTag(8);
        output.WriteInt64(UnitId);
      }
      if (RpcId != 0) {
        output.WriteRawTag(208, 5);
        output.WriteInt32(RpcId);
      }
      if (Error != 0) {
        output.WriteRawTag(216, 5);
        output.WriteInt32(Error);
      }
      if (Message.Length != 0) {
        output.WriteRawTag(226, 5);
        output.WriteString(Message);
      }
    }

    public int CalculateSize() {
      int size = 0;
      if (RpcId != 0) {
        size += 2 + pb::CodedOutputStream.ComputeInt32Size(RpcId);
      }
      if (Error != 0) {
        size += 2 + pb::CodedOutputStream.ComputeInt32Size(Error);
      }
      if (Message.Length != 0) {
        size += 2 + pb::CodedOutputStream.ComputeStringSize(Message);
      }
      if (UnitId != 0L) {
        size += 1 + pb::CodedOutputStream.ComputeInt64Size(UnitId);
      }
      return size;
    }

    public void MergeFrom(pb::CodedInputStream input) {
      unitId_ = 0;
      rpcId_ = 0;
      error_ = 0;
      message_ = "";
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 8: {
            UnitId = input.ReadInt64();
            break;
          }
          case 720: {
            RpcId = input.ReadInt32();
            break;
          }
          case 728: {
            Error = input.ReadInt32();
            break;
          }
          case 738: {
            Message = input.ReadString();
            break;
          }
        }
      }
    }

  }

  /// <summary>
  ///获得地图信息
  /// </summary>
  public partial class C2G_MapInfo : pb::IMessage {
    private static readonly pb::MessageParser<C2G_MapInfo> _parser = new pb::MessageParser<C2G_MapInfo>(() => (C2G_MapInfo)MessagePool.Instance.Fetch(typeof(C2G_MapInfo)));
    public static pb::MessageParser<C2G_MapInfo> Parser { get { return _parser; } }

    private int rpcId_;
    public int RpcId {
      get { return rpcId_; }
      set {
        rpcId_ = value;
      }
    }

    private int index_;
    public int Index {
      get { return index_; }
      set {
        index_ = value;
      }
    }

    public void WriteTo(pb::CodedOutputStream output) {
      if (Index != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(Index);
      }
      if (RpcId != 0) {
        output.WriteRawTag(208, 5);
        output.WriteInt32(RpcId);
      }
    }

    public int CalculateSize() {
      int size = 0;
      if (RpcId != 0) {
        size += 2 + pb::CodedOutputStream.ComputeInt32Size(RpcId);
      }
      if (Index != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Index);
      }
      return size;
    }

    public void MergeFrom(pb::CodedInputStream input) {
      index_ = 0;
      rpcId_ = 0;
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 8: {
            Index = input.ReadInt32();
            break;
          }
          case 720: {
            RpcId = input.ReadInt32();
            break;
          }
        }
      }
    }

  }

  public partial class G2C_MapInfo : pb::IMessage {
    private static readonly pb::MessageParser<G2C_MapInfo> _parser = new pb::MessageParser<G2C_MapInfo>(() => (G2C_MapInfo)MessagePool.Instance.Fetch(typeof(G2C_MapInfo)));
    public static pb::MessageParser<G2C_MapInfo> Parser { get { return _parser; } }

    private int rpcId_;
    public int RpcId {
      get { return rpcId_; }
      set {
        rpcId_ = value;
      }
    }

    private int error_;
    public int Error {
      get { return error_; }
      set {
        error_ = value;
      }
    }

    private string message_ = "";
    public string Message {
      get { return message_; }
      set {
        message_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    private static readonly pb::FieldCodec<global::ET.UnitInfo> _repeated_units_codec
        = pb::FieldCodec.ForMessage(10, global::ET.UnitInfo.Parser);
    private pbc::RepeatedField<global::ET.UnitInfo> units_ = new pbc::RepeatedField<global::ET.UnitInfo>();
    /// <summary>
    /// 所有的unit
    /// </summary>
    public pbc::RepeatedField<global::ET.UnitInfo> Units {
      get { return units_; }
      set { units_ = value; }
    }

    public void WriteTo(pb::CodedOutputStream output) {
      units_.WriteTo(output, _repeated_units_codec);
      if (RpcId != 0) {
        output.WriteRawTag(208, 5);
        output.WriteInt32(RpcId);
      }
      if (Error != 0) {
        output.WriteRawTag(216, 5);
        output.WriteInt32(Error);
      }
      if (Message.Length != 0) {
        output.WriteRawTag(226, 5);
        output.WriteString(Message);
      }
    }

    public int CalculateSize() {
      int size = 0;
      if (RpcId != 0) {
        size += 2 + pb::CodedOutputStream.ComputeInt32Size(RpcId);
      }
      if (Error != 0) {
        size += 2 + pb::CodedOutputStream.ComputeInt32Size(Error);
      }
      if (Message.Length != 0) {
        size += 2 + pb::CodedOutputStream.ComputeStringSize(Message);
      }
      size += units_.CalculateSize(_repeated_units_codec);
      return size;
    }

    public void MergeFrom(pb::CodedInputStream input) {
      for (int i = 0; i < units_.Count; i++) { MessagePool.Instance.Recycle(units_[i]); }
      units_.Clear();
      rpcId_ = 0;
      error_ = 0;
      message_ = "";
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 10: {
            units_.AddEntriesFrom(input, _repeated_units_codec);
            break;
          }
          case 720: {
            RpcId = input.ReadInt32();
            break;
          }
          case 728: {
            Error = input.ReadInt32();
            break;
          }
          case 738: {
            Message = input.ReadString();
            break;
          }
        }
      }
    }

  }

  /// <summary>
  ///创建unit
  /// </summary>
  public partial class M2C_CreateUnits : pb::IMessage {
    private static readonly pb::MessageParser<M2C_CreateUnits> _parser = new pb::MessageParser<M2C_CreateUnits>(() => (M2C_CreateUnits)MessagePool.Instance.Fetch(typeof(M2C_CreateUnits)));
    public static pb::MessageParser<M2C_CreateUnits> Parser { get { return _parser; } }

    private int rpcId_;
    public int RpcId {
      get { return rpcId_; }
      set {
        rpcId_ = value;
      }
    }

    private long actorId_;
    public long ActorId {
      get { return actorId_; }
      set {
        actorId_ = value;
      }
    }

    private static readonly pb::FieldCodec<global::ET.UnitInfo> _repeated_units_codec
        = pb::FieldCodec.ForMessage(10, global::ET.UnitInfo.Parser);
    private pbc::RepeatedField<global::ET.UnitInfo> units_ = new pbc::RepeatedField<global::ET.UnitInfo>();
    public pbc::RepeatedField<global::ET.UnitInfo> Units {
      get { return units_; }
      set { units_ = value; }
    }

    public void WriteTo(pb::CodedOutputStream output) {
      units_.WriteTo(output, _repeated_units_codec);
      if (RpcId != 0) {
        output.WriteRawTag(208, 5);
        output.WriteInt32(RpcId);
      }
      if (ActorId != 0L) {
        output.WriteRawTag(232, 5);
        output.WriteInt64(ActorId);
      }
    }

    public int CalculateSize() {
      int size = 0;
      if (RpcId != 0) {
        size += 2 + pb::CodedOutputStream.ComputeInt32Size(RpcId);
      }
      if (ActorId != 0L) {
        size += 2 + pb::CodedOutputStream.ComputeInt64Size(ActorId);
      }
      size += units_.CalculateSize(_repeated_units_codec);
      return size;
    }

    public void MergeFrom(pb::CodedInputStream input) {
      for (int i = 0; i < units_.Count; i++) { MessagePool.Instance.Recycle(units_[i]); }
      units_.Clear();
      rpcId_ = 0;
      actorId_ = 0;
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 10: {
            units_.AddEntriesFrom(input, _repeated_units_codec);
            break;
          }
          case 720: {
            RpcId = input.ReadInt32();
            break;
          }
          case 744: {
            ActorId = input.ReadInt64();
            break;
          }
        }
      }
    }

  }

  public partial class Frame_ClickMap : pb::IMessage {
    private static readonly pb::MessageParser<Frame_ClickMap> _parser = new pb::MessageParser<Frame_ClickMap>(() => (Frame_ClickMap)MessagePool.Instance.Fetch(typeof(Frame_ClickMap)));
    public static pb::MessageParser<Frame_ClickMap> Parser { get { return _parser; } }

    private int rpcId_;
    public int RpcId {
      get { return rpcId_; }
      set {
        rpcId_ = value;
      }
    }

    private long actorId_;
    public long ActorId {
      get { return actorId_; }
      set {
        actorId_ = value;
      }
    }

    private long id_;
    public long Id {
      get { return id_; }
      set {
        id_ = value;
      }
    }

    private float x_;
    public float X {
      get { return x_; }
      set {
        x_ = value;
      }
    }

    private float y_;
    public float Y {
      get { return y_; }
      set {
        y_ = value;
      }
    }

    private float z_;
    public float Z {
      get { return z_; }
      set {
        z_ = value;
      }
    }

    public void WriteTo(pb::CodedOutputStream output) {
      if (X != 0F) {
        output.WriteRawTag(13);
        output.WriteFloat(X);
      }
      if (Y != 0F) {
        output.WriteRawTag(21);
        output.WriteFloat(Y);
      }
      if (Z != 0F) {
        output.WriteRawTag(29);
        output.WriteFloat(Z);
      }
      if (RpcId != 0) {
        output.WriteRawTag(208, 5);
        output.WriteInt32(RpcId);
      }
      if (ActorId != 0L) {
        output.WriteRawTag(232, 5);
        output.WriteInt64(ActorId);
      }
      if (Id != 0L) {
        output.WriteRawTag(240, 5);
        output.WriteInt64(Id);
      }
    }

    public int CalculateSize() {
      int size = 0;
      if (RpcId != 0) {
        size += 2 + pb::CodedOutputStream.ComputeInt32Size(RpcId);
      }
      if (ActorId != 0L) {
        size += 2 + pb::CodedOutputStream.ComputeInt64Size(ActorId);
      }
      if (Id != 0L) {
        size += 2 + pb::CodedOutputStream.ComputeInt64Size(Id);
      }
      if (X != 0F) {
        size += 1 + 4;
      }
      if (Y != 0F) {
        size += 1 + 4;
      }
      if (Z != 0F) {
        size += 1 + 4;
      }
      return size;
    }

    public void MergeFrom(pb::CodedInputStream input) {
      x_ = 0f;
      y_ = 0f;
      z_ = 0f;
      rpcId_ = 0;
      actorId_ = 0;
      id_ = 0;
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 13: {
            X = input.ReadFloat();
            break;
          }
          case 21: {
            Y = input.ReadFloat();
            break;
          }
          case 29: {
            Z = input.ReadFloat();
            break;
          }
          case 720: {
            RpcId = input.ReadInt32();
            break;
          }
          case 744: {
            ActorId = input.ReadInt64();
            break;
          }
          case 752: {
            Id = input.ReadInt64();
            break;
          }
        }
      }
    }

  }

  public partial class M2C_PathfindingResult : pb::IMessage {
    private static readonly pb::MessageParser<M2C_PathfindingResult> _parser = new pb::MessageParser<M2C_PathfindingResult>(() => (M2C_PathfindingResult)MessagePool.Instance.Fetch(typeof(M2C_PathfindingResult)));
    public static pb::MessageParser<M2C_PathfindingResult> Parser { get { return _parser; } }

    private long actorId_;
    public long ActorId {
      get { return actorId_; }
      set {
        actorId_ = value;
      }
    }

    private long id_;
    public long Id {
      get { return id_; }
      set {
        id_ = value;
      }
    }

    private float x_;
    public float X {
      get { return x_; }
      set {
        x_ = value;
      }
    }

    private float y_;
    public float Y {
      get { return y_; }
      set {
        y_ = value;
      }
    }

    private float z_;
    public float Z {
      get { return z_; }
      set {
        z_ = value;
      }
    }

    private static readonly pb::FieldCodec<float> _repeated_xs_codec
        = pb::FieldCodec.ForFloat(42);
    private pbc::RepeatedField<float> xs_ = new pbc::RepeatedField<float>();
    public pbc::RepeatedField<float> Xs {
      get { return xs_; }
      set { xs_ = value; }
    }

    private static readonly pb::FieldCodec<float> _repeated_ys_codec
        = pb::FieldCodec.ForFloat(50);
    private pbc::RepeatedField<float> ys_ = new pbc::RepeatedField<float>();
    public pbc::RepeatedField<float> Ys {
      get { return ys_; }
      set { ys_ = value; }
    }

    private static readonly pb::FieldCodec<float> _repeated_zs_codec
        = pb::FieldCodec.ForFloat(58);
    private pbc::RepeatedField<float> zs_ = new pbc::RepeatedField<float>();
    public pbc::RepeatedField<float> Zs {
      get { return zs_; }
      set { zs_ = value; }
    }

    public void WriteTo(pb::CodedOutputStream output) {
      if (Id != 0L) {
        output.WriteRawTag(8);
        output.WriteInt64(Id);
      }
      if (X != 0F) {
        output.WriteRawTag(21);
        output.WriteFloat(X);
      }
      if (Y != 0F) {
        output.WriteRawTag(29);
        output.WriteFloat(Y);
      }
      if (Z != 0F) {
        output.WriteRawTag(37);
        output.WriteFloat(Z);
      }
      xs_.WriteTo(output, _repeated_xs_codec);
      ys_.WriteTo(output, _repeated_ys_codec);
      zs_.WriteTo(output, _repeated_zs_codec);
      if (ActorId != 0L) {
        output.WriteRawTag(232, 5);
        output.WriteInt64(ActorId);
      }
    }

    public int CalculateSize() {
      int size = 0;
      if (ActorId != 0L) {
        size += 2 + pb::CodedOutputStream.ComputeInt64Size(ActorId);
      }
      if (Id != 0L) {
        size += 1 + pb::CodedOutputStream.ComputeInt64Size(Id);
      }
      if (X != 0F) {
        size += 1 + 4;
      }
      if (Y != 0F) {
        size += 1 + 4;
      }
      if (Z != 0F) {
        size += 1 + 4;
      }
      size += xs_.CalculateSize(_repeated_xs_codec);
      size += ys_.CalculateSize(_repeated_ys_codec);
      size += zs_.CalculateSize(_repeated_zs_codec);
      return size;
    }

    public void MergeFrom(pb::CodedInputStream input) {
      id_ = 0;
      x_ = 0f;
      y_ = 0f;
      z_ = 0f;
      xs_.Clear();
      ys_.Clear();
      zs_.Clear();
      actorId_ = 0;
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 8: {
            Id = input.ReadInt64();
            break;
          }
          case 21: {
            X = input.ReadFloat();
            break;
          }
          case 29: {
            Y = input.ReadFloat();
            break;
          }
          case 37: {
            Z = input.ReadFloat();
            break;
          }
          case 42:
          case 45: {
            xs_.AddEntriesFrom(input, _repeated_xs_codec);
            break;
          }
          case 50:
          case 53: {
            ys_.AddEntriesFrom(input, _repeated_ys_codec);
            break;
          }
          case 58:
          case 61: {
            zs_.AddEntriesFrom(input, _repeated_zs_codec);
            break;
          }
          case 744: {
            ActorId = input.ReadInt64();
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code