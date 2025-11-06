namespace Pharos.FGA.AspNetCore.Authorization.Service;

public interface IFgaService
{
    Task AddRelation(string user, string relation, string @object);
    Task DeleteRelation(string user, string relation, string @object);
    Task<bool> CheckRelation(string user, string relation, string @object);
}