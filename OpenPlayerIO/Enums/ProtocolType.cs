namespace PlayerIOClient.Enums
{
    internal enum ProtocolType : byte
    {
        Binary,
        Http = 71,
        Auto = 255,
        WebSocketRfc6455Binary = 11,
        WebSocketV76Base64
    }
}