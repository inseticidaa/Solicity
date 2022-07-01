using Solicity.Domain.DTOs;

namespace Solicity.Domain.Services
{
    public interface IIssueService
    {
        Task<IssueDTO> OpenIssueAsync(IssueCreationDTO issueCreationDTO, Guid requestBy);
        Task<IList<IssueDTO>> GetIssuesAsync(string search, int page, int pageSize, Guid requestBy);
        Task<IssueDetailDTO> GetIssueDetailAsync(Guid issueId, Guid requestBy);
        Task<IssueCommentDTO> AddCommentAsync(IssueCommentCreationDTO issueCommentCreationDTO, Guid requestBy);

    }
}
