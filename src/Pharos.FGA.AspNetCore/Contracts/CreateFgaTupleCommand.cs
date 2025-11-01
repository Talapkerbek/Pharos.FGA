namespace Pharos.FGA.AspNetCore.Contracts;

public record CreateFgaTupleCommand(string User, string Relation, string Object);