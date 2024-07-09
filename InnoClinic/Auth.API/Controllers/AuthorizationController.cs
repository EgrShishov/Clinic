using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

public class AuthorizationController : ApiController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly UserManager<Account> _userManager;

    public AuthorizationController(IMediator mediator, IMapper mapper, UserManager<Account> userManager)
    {
        _mediator = mediator;
        _mapper = mapper;
        _userManager = userManager;
    }

    [HttpPost("sign-in")]
    public async Task<IActionResult> SignIn(SignInRequest request)
    {
        var command = _mapper.Map<SignInCommand>(request);
        var signInResult = await _mediator.Send(command);

        return signInResult.Match(
            response => 
            {
                Response.Cookies.Append("access", response.accessToken);
                Response.Cookies.Append("refresh", response.refreshToken);
                return Ok(_mapper.Map<AuthorizationResponse>(response));
            },
            errors => Problem(errors));
    }

    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUp(SignUpRequest request)
    {
        var command = _mapper.Map<SignUpCommand>(request);
        var signUpResult = await _mediator.Send(command);

        return signUpResult.Match(
            response => 
            {
                Response.Cookies.Append("access", response.accessToken);
                Response.Cookies.Append("refresh", response.refreshToken);
                return Ok(_mapper.Map<AuthorizationResponse>(response));
            },
            errors => Problem(errors));
    }

    [HttpPost("sign-out")]
    public async Task<IActionResult> SignOut()
    {
        Response.Cookies.Delete("refresh");
        return NoContent();
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(RefreshTokenRequest request)
    {
        int accountId = User.GetUserId();
        var account = await _userManager.FindByIdAsync(accountId.ToString());

        if(account is null)
        {
            return NotFound();
        }

        var refreshTokenResult = await _mediator.Send(new RefreshTokenCommand(request.accessToken, request.refreshToken));
        if (refreshTokenResult.IsError)
        {
            return BadRequest(refreshTokenResult.FirstError);
        }

        account.RefreshTokens.Add(request.refreshToken);
        await _userManager.UpdateAsync(account);

        return refreshTokenResult.Match(
            response =>
            {
                Response.Cookies.Delete("access");
                Response.Cookies.Append("access", response.accessToken);
                return Ok(_mapper.Map<AuthorizationResponse>(response));
            },
            errors => Problem(errors));
    }

    [HttpGet("verify")]
    public async Task<IActionResult> VerifyEmail(string link)
    {
        var verificationResult = await _mediator.Send(new VerifyEmailCommand(link));

        return verificationResult.Match(
            response => Ok(response),
            errors => Problem(errors));
    }

    [HttpGet("account/{id:int}")]
    public async Task<IActionResult> GetAccountInformation(int id)
    {
        var result = await _mediator.Send(new GetAccountByIdQuery(id));

        return result.Match(
            response => Ok(_mapper.Map<AccountResponse>(response)),
            errors => Problem(errors));
    }
}
