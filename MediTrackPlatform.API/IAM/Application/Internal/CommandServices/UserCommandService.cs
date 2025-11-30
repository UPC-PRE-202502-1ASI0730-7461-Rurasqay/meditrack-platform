using MediTrackPlatform.API.IAM.Application.Internal.OutboundServices;
using MediTrackPlatform.API.IAM.Domain.Model.Aggregates;
using MediTrackPlatform.API.IAM.Domain.Model.Commands;
using MediTrackPlatform.API.IAM.Domain.Repositories;
using MediTrackPlatform.API.IAM.Domain.Services;
using MediTrackPlatform.API.Organization.Domain.Model.Commands;
using MediTrackPlatform.API.Organization.Domain.Services;
using MediTrackPlatform.API.Shared.Domain.Repositories;

namespace MediTrackPlatform.API.IAM.Application.Internal.CommandServices;

public class UserCommandService(
    IUserRepository userRepository,
    ITokenService tokenService,
    IHashingService hashingService,
    IUnitOfWork unitOfWork,
    IOrganizationCommandService organizationCommandService,
    IAdminCommandService adminCommandService
    ) : IUserCommandService
{
    public async Task<(User user, string token)> Handle(SignInCommand command)
    {
        var user = await userRepository.FindByEmailAsync(command.Email);

        if (user == null || !hashingService.VerifyPassword(command.Password, user.PasswordHash))
            throw new Exception("Invalid username or password");
        
        var token = tokenService.GenerateToken(user);
        
        return (user, token);
    }

    public async Task<User?> Handle(SignUpCommand command)
    {
        if (userRepository.ExistsByEmail(command.Email))
            throw new Exception($"Email {command.Email} is already taken");
        
        var hashedPassword = hashingService.HashPassword(command.Password);
        var user = new User(command.Email, hashedPassword, command.Role);
        
        try
        {
            await userRepository.AddAsync(user);
            await unitOfWork.CompleteAsync();
            
            // If role is admin, create organization and admin entities
            if ("admin".Equals(command.Role, StringComparison.OrdinalIgnoreCase))
            {
                if (string.IsNullOrWhiteSpace(command.FirstName)) throw new Exception("First name is required for admin sign-up");
                if (string.IsNullOrWhiteSpace(command.LastName)) throw new Exception("Last name is required for admin sign-up");
                if (string.IsNullOrWhiteSpace(command.OrganizationName)) throw new Exception("Organization name is required for admin sign-up");

                var createOrganizationCommand = new CreateOrganizationCommand(command.OrganizationName, "HealthCenter");
                var organizationId = await organizationCommandService.Handle(createOrganizationCommand);
                
                if (organizationId.HasValue)
                {
                    var createAdminCommand = new CreateAdminCommand(user.Id, organizationId.Value, command.FirstName, command.LastName);
                    await adminCommandService.Handle(createAdminCommand);
                }
            }
            
            return user;
        }
        catch (Exception e)
        {
            throw new Exception($"An error occurred while creating user: {e.Message}");
        }
    }
}