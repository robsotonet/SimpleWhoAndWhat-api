using WhoAndWhat.Domain.Enums;

namespace WhoAndWhat.Application.Features.Tasks.Dtos;

public record TaskDto(
    Guid Id,
    string Title,
    string? Description,
    TaskCategory Category,
    bool IsCompleted,
    DateTime? DueDate,
    DateTime CreatedAt,
    DateTime UpdatedAt);
