namespace Pharos.FGA.AspNetCore.Contracts;

public record DeleteFgaTupleCommand(string User, string Relation, string Object);