// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: command.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace V2Ray.Core.App.Stats.Command {

  /// <summary>Holder for reflection information generated from command.proto</summary>
  public static partial class CommandReflection {

    #region Descriptor
    /// <summary>File descriptor for command.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static CommandReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Cg1jb21tYW5kLnByb3RvEhx2MnJheS5jb3JlLmFwcC5zdGF0cy5jb21tYW5k",
            "Ii4KD0dldFN0YXRzUmVxdWVzdBIMCgRuYW1lGAEgASgJEg0KBXJlc2V0GAIg",
            "ASgIIiMKBFN0YXQSDAoEbmFtZRgBIAEoCRINCgV2YWx1ZRgCIAEoAyJEChBH",
            "ZXRTdGF0c1Jlc3BvbnNlEjAKBHN0YXQYASABKAsyIi52MnJheS5jb3JlLmFw",
            "cC5zdGF0cy5jb21tYW5kLlN0YXQiCAoGQ29uZmlnMnsKDFN0YXRzU2Vydmlj",
            "ZRJrCghHZXRTdGF0cxItLnYycmF5LmNvcmUuYXBwLnN0YXRzLmNvbW1hbmQu",
            "R2V0U3RhdHNSZXF1ZXN0Gi4udjJyYXkuY29yZS5hcHAuc3RhdHMuY29tbWFu",
            "ZC5HZXRTdGF0c1Jlc3BvbnNlIgBCTAogY29tLnYycmF5LmNvcmUuYXBwLnN0",
            "YXRzLmNvbW1hbmRQAVoHY29tbWFuZKoCHFYyUmF5LkNvcmUuQXBwLlN0YXRz",
            "LkNvbW1hbmRiBnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::V2Ray.Core.App.Stats.Command.GetStatsRequest), global::V2Ray.Core.App.Stats.Command.GetStatsRequest.Parser, new[]{ "Name", "Reset" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::V2Ray.Core.App.Stats.Command.Stat), global::V2Ray.Core.App.Stats.Command.Stat.Parser, new[]{ "Name", "Value" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::V2Ray.Core.App.Stats.Command.GetStatsResponse), global::V2Ray.Core.App.Stats.Command.GetStatsResponse.Parser, new[]{ "Stat" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::V2Ray.Core.App.Stats.Command.Config), global::V2Ray.Core.App.Stats.Command.Config.Parser, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class GetStatsRequest : pb::IMessage<GetStatsRequest> {
    private static readonly pb::MessageParser<GetStatsRequest> _parser = new pb::MessageParser<GetStatsRequest>(() => new GetStatsRequest());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<GetStatsRequest> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::V2Ray.Core.App.Stats.Command.CommandReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public GetStatsRequest() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public GetStatsRequest(GetStatsRequest other) : this() {
      name_ = other.name_;
      reset_ = other.reset_;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public GetStatsRequest Clone() {
      return new GetStatsRequest(this);
    }

    /// <summary>Field number for the "name" field.</summary>
    public const int NameFieldNumber = 1;
    private string name_ = "";
    /// <summary>
    /// Name of the stat counter.
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Name {
      get { return name_; }
      set {
        name_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "reset" field.</summary>
    public const int ResetFieldNumber = 2;
    private bool reset_;
    /// <summary>
    /// Whether or not to reset the counter to fetching its value.
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Reset {
      get { return reset_; }
      set {
        reset_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as GetStatsRequest);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(GetStatsRequest other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Name != other.Name) return false;
      if (Reset != other.Reset) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Name.Length != 0) hash ^= Name.GetHashCode();
      if (Reset != false) hash ^= Reset.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Name.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Name);
      }
      if (Reset != false) {
        output.WriteRawTag(16);
        output.WriteBool(Reset);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Name.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Name);
      }
      if (Reset != false) {
        size += 1 + 1;
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(GetStatsRequest other) {
      if (other == null) {
        return;
      }
      if (other.Name.Length != 0) {
        Name = other.Name;
      }
      if (other.Reset != false) {
        Reset = other.Reset;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 10: {
            Name = input.ReadString();
            break;
          }
          case 16: {
            Reset = input.ReadBool();
            break;
          }
        }
      }
    }

  }

  public sealed partial class Stat : pb::IMessage<Stat> {
    private static readonly pb::MessageParser<Stat> _parser = new pb::MessageParser<Stat>(() => new Stat());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<Stat> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::V2Ray.Core.App.Stats.Command.CommandReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Stat() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Stat(Stat other) : this() {
      name_ = other.name_;
      value_ = other.value_;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Stat Clone() {
      return new Stat(this);
    }

    /// <summary>Field number for the "name" field.</summary>
    public const int NameFieldNumber = 1;
    private string name_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Name {
      get { return name_; }
      set {
        name_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "value" field.</summary>
    public const int ValueFieldNumber = 2;
    private long value_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public long Value {
      get { return value_; }
      set {
        value_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as Stat);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(Stat other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Name != other.Name) return false;
      if (Value != other.Value) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Name.Length != 0) hash ^= Name.GetHashCode();
      if (Value != 0L) hash ^= Value.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Name.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Name);
      }
      if (Value != 0L) {
        output.WriteRawTag(16);
        output.WriteInt64(Value);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Name.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Name);
      }
      if (Value != 0L) {
        size += 1 + pb::CodedOutputStream.ComputeInt64Size(Value);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(Stat other) {
      if (other == null) {
        return;
      }
      if (other.Name.Length != 0) {
        Name = other.Name;
      }
      if (other.Value != 0L) {
        Value = other.Value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 10: {
            Name = input.ReadString();
            break;
          }
          case 16: {
            Value = input.ReadInt64();
            break;
          }
        }
      }
    }

  }

  public sealed partial class GetStatsResponse : pb::IMessage<GetStatsResponse> {
    private static readonly pb::MessageParser<GetStatsResponse> _parser = new pb::MessageParser<GetStatsResponse>(() => new GetStatsResponse());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<GetStatsResponse> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::V2Ray.Core.App.Stats.Command.CommandReflection.Descriptor.MessageTypes[2]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public GetStatsResponse() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public GetStatsResponse(GetStatsResponse other) : this() {
      Stat = other.stat_ != null ? other.Stat.Clone() : null;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public GetStatsResponse Clone() {
      return new GetStatsResponse(this);
    }

    /// <summary>Field number for the "stat" field.</summary>
    public const int StatFieldNumber = 1;
    private global::V2Ray.Core.App.Stats.Command.Stat stat_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::V2Ray.Core.App.Stats.Command.Stat Stat {
      get { return stat_; }
      set {
        stat_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as GetStatsResponse);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(GetStatsResponse other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!object.Equals(Stat, other.Stat)) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (stat_ != null) hash ^= Stat.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (stat_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(Stat);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (stat_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Stat);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(GetStatsResponse other) {
      if (other == null) {
        return;
      }
      if (other.stat_ != null) {
        if (stat_ == null) {
          stat_ = new global::V2Ray.Core.App.Stats.Command.Stat();
        }
        Stat.MergeFrom(other.Stat);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 10: {
            if (stat_ == null) {
              stat_ = new global::V2Ray.Core.App.Stats.Command.Stat();
            }
            input.ReadMessage(stat_);
            break;
          }
        }
      }
    }

  }

  public sealed partial class Config : pb::IMessage<Config> {
    private static readonly pb::MessageParser<Config> _parser = new pb::MessageParser<Config>(() => new Config());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<Config> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::V2Ray.Core.App.Stats.Command.CommandReflection.Descriptor.MessageTypes[3]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Config() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Config(Config other) : this() {
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Config Clone() {
      return new Config(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as Config);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(Config other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(Config other) {
      if (other == null) {
        return;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
