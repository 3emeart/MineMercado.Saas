using MiniMercadoSaas.Application.DTO.Request;
using MiniMercadoSaas.Application.ServiceInterfaces;
using MiniMercadoSaas.Domain.Entities;
using MiniMercadoSaas.Domain.Interfaces;

namespace MiniMercadoSaas.Application.Services;

public class UserService : IUserService
{
    private readonly IUsuarioRepository _repository;

    public UserService(IUsuarioRepository repository)
    {
        _repository = repository;
    }

    public async Task<User> Create(UserCreateRequest request)
    {
        var existingUser = await _repository.GetByEmailAsync(request.Email);
        if (existingUser != null) throw new Exception("Esse email ja foi cadastrado");

        string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        var newUser = new User
        {
            Name = request.Name,
            Email = request.Email,
            SenhaHash = passwordHash,
            Role = request.Role,
            Active = true
        };

        await _repository.AddAsync(newUser);
        return newUser;




    }

}