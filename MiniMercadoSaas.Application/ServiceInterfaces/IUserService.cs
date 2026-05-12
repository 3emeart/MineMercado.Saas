using MiniMercadoSaas.Application.DTO.Request;
using MiniMercadoSaas.Domain.Entities;

namespace MiniMercadoSaas.Application.ServiceInterfaces;

public interface IUserService
{
    Task <User> Create (UserCreateRequest request);
}