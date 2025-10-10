using codex_backend.Application.Authorization.Requirements;
using codex_backend.Application.Authorization.Wrappers;
using codex_backend.Application.Dtos;
using codex_backend.Application.Repositories.Interfaces;
using codex_backend.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace codex_backend.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class StorePolicyController(
    IStorePolicyService storePolicyService,
    IAuthorizationService authService,
    IBookstoreRepository bookstoreRepository
    ) : ControllerBase
{
    private readonly IStorePolicyService _storePolicyService = storePolicyService;
    private readonly IAuthorizationService _authService = authService;
    private readonly IBookstoreRepository _bookstoreRepository = bookstoreRepository;

    [HttpPost("create")]
    public async Task<IActionResult> Post([FromBody] StorePolicyCreateDto storePolicyCreateDto)
    {
        var bookstore = await _bookstoreRepository.GetBookstoreByIdAsync(storePolicyCreateDto.BookstoreId);
        if (bookstore is null)
        {
            return NotFound(new ApiResponse(false, "Bookstore not found"));
        }

        var authorizationResult = await _authService.AuthorizeAsync(
            User, bookstore, new ResourceOwnerRequirement()
        );

        if (!authorizationResult.Succeeded)
        {
            return Forbid();
        }

        var newPolicy = await _storePolicyService.CreatePolicyAsync(storePolicyCreateDto);
        if (newPolicy is null)
        {
            return BadRequest(new ApiResponse(false, "This bookstore already has an active policy."));
        }

        var response = new ApiSingleResponse<StorePolicyReadDto>(true, "Policy created successfully", newPolicy);

        return Created(string.Empty, response);

    }

    [HttpGet("by-id/{id}")]
    public async Task<IActionResult> GetById(Guid Id)
    {
        var policy = await _storePolicyService.GetPolicyByIdAsync(Id);
        var response = new ApiSingleResponse<StorePolicyReadDto>(true, "Success", policy!);

        return Ok(response);
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] StorePolicyUpdateDto storePolicyUpdateDto)
    {
        var policy = await _storePolicyService.GetPolicyByIdAsync(id);
        if (policy is null)
        {
            return NotFound(new ApiSingleResponse<object>(false, "Policy not found", null!));
        }

        var bookstore = await _bookstoreRepository.GetBookstoreByIdAsync(policy.BookstoreId);
        if (bookstore is null)
        {
            return NotFound(new ApiSingleResponse<object>(false, "Associated bookstore not found.", null!));
        }

        var authorizationResult = await _authService.AuthorizeAsync(
            User, bookstore, new ResourceOwnerRequirement());

        if (!authorizationResult.Succeeded)
        {
            return Forbid();
        }

        var updatedPolicy = await _storePolicyService.UpdatePolicyAsync(id, storePolicyUpdateDto);
        return Ok(updatedPolicy);
    }


}