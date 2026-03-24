using System.Text.Json;

namespace StorageProject.Application.DTOs.Messages
{
    public record MessageEnvelope
    {
        public string EventType { get; init; } = default!;
        public JsonElement Payload { get; init; }
    }
}
