using Microsoft.EntityFrameworkCore;
using RMS.Models.Entity;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Interfaces.Workflow;

public interface IWorkflowRuntimeService
{
    Task SubmitAsync(
     Guid workflowInstanceId,
     string currentUserId,
     string? comment = null,
    List<string>? receiverUserId = null);

    Task ApproveAsync(
        Guid workflowInstanceId,
        string currentUserId,
        string? comment = null);

    Task RevisionAsync(
        Guid workflowInstanceId,
        string currentUserId,
        string? comment = null);

    Task RejectAsync(
        Guid workflowInstanceId,
        string currentUserId,
        string? comment = null);

    Task CancelAsync(
        Guid workflowInstanceId,
        string currentUserId,
        string? comment = null);

    Task<Guid> StartWorkflowAsync(
    Guid workflowId,
    Guid entityId,
    string entityType,
    Guid senderUserId,
    List<string>? selectedUsers = null);



}


