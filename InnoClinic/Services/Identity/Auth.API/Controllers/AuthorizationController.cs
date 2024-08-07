using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

public class AuthorizationController : ApiController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IConfiguration _config;
    private readonly UserManager<Account> _userManager;

    public AuthorizationController(
        IMediator mediator, 
        IMapper mapper, 
        UserManager<Account> userManager, 
        IConfiguration config)
    {
        _mediator = mediator;
        _mapper = mapper;
        _userManager = userManager;
        _config = config;
    }

    [HttpPost("sign-in")]
    public async Task<IActionResult> SignIn(SignInRequest request)
    {
        var command = _mapper.Map<SignInCommand>(request);
        var signInResult = await _mediator.Send(command);

        return signInResult.Match(
            response => 
            {
                Response.Cookies.Append("access", response.AccessToken);
                Response.Cookies.Append("refresh", response.RefreshToken);
                return Ok(response);
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
                Response.Cookies.Append("access", response.AccessToken);
                Response.Cookies.Append("refresh", response.RefreshToken);
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

        var refreshTokenResult = await _mediator.Send(new RefreshTokenCommand(request.AccessToken, request.RefreshToken));
        if (refreshTokenResult.IsError)
        {
            return BadRequest(refreshTokenResult.FirstError);
        }

        account.RefreshToken = request.RefreshToken;
        await _userManager.UpdateAsync(account);

        return refreshTokenResult.Match(
            response =>
            {
                Response.Cookies.Delete("access");
                Response.Cookies.Append("access", response.accessToken);
                return Ok(response);
            },
            errors => Problem(errors));
    }

    [HttpGet("verify")]
    public async Task<IActionResult> VerifyEmail(string link)
    {
        var id = User.GetUserId();
        var verificationResult = await _mediator.Send(new VerifyEmailCommand(id.ToString(), link));

        return verificationResult.Match(
            response =>
            {
                var role = User.GetRole();
                return Redirect($"{_config["ProfilesAPI"]}/{role}/create-profile"); //redirect to create profile page
            },
            errors => Problem(errors));
    }

    [HttpGet("account/{id:int}")]
    public async Task<IActionResult> GetAccount(int id)
    {
        var result = await _mediator.Send(new GetAccountByIdQuery(id));

        return result.Match(
            response => Ok(_mapper.Map<AccountResponse>(response)),
            errors => Problem(errors));
    }
}
