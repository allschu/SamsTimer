using Admin.Shared.Contracts;
using Admin.Shared;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebUI.Services;

namespace WebUI.Endpoints.Users
{
    public class GetUsersPaged : EndpointBaseAsync
        .WithRequest<PagingParameters>
        .WithActionResult<ApiResult<PagedList<User>>>
    {
        private readonly ILogger<GetUsersPaged> _logger;
        private readonly IBackendService _backendService;

        public GetUsersPaged(ILogger<GetUsersPaged> logger, IBackendService backendService)
        {
            _logger = logger;
            _backendService = backendService;
        }

        [HttpGet("api/[namespace]")]
        [SwaggerOperation(
            Summary = "Get users paged",
            OperationId = "Users_Paged_Get",
            Tags = new[] { "Users" })]
        public override async Task<ActionResult<ApiResult<PagedList<User>>>> HandleAsync([FromQuery] PagingParameters pagingParameters, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Getting users paged");

            var result = await _backendService.SearchUsersAsync(pagingParameters.Page, pagingParameters.PageSize, cancellationToken);

            return result;
        }
    }
}
