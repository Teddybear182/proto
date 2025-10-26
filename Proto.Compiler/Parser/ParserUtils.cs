namespace Proto.Compiler.Parser;

using Proto.Compiler.AST.Expressions;
using Proto.Compiler.AST.Statements;

public static class ParserUtils {

  #region Variables for binary operators
  public static readonly Dictionary<string, OperatorType> Operators = new Dictionary<string, OperatorType> {
    { "or", OperatorType.Or },
    { "and", OperatorType.And },
    { "!=", OperatorType.NotEqual },
    { "=", OperatorType.Equal },
    { ">", OperatorType.GreaterThan },
    { ">=", OperatorType.GreaterThanOrEqual },
    { "<", OperatorType.LessThan },
    { "<=", OperatorType.LessThanOrEqual },
    { "+", OperatorType.Plus},
    { "-", OperatorType.Minus},
    { "*", OperatorType.Multiply},
    { "/", OperatorType.Divide},
    { "mod", OperatorType.Modulo},
    { "not", OperatorType.Not },
  };

  public static readonly string[] Or = ["or"];
  public static readonly string[] And = ["and"];
  public static readonly string[] ComparisonOperators1 = ["=", "!="];
  public static readonly string[] ComparisonOperators2 = ["<", "<=", ">", ">="];
  public static readonly string[] AdditiveOperators = ["+", "-"];
  public static readonly string[] MultiplicativeOperators = ["*", "/", "mod"];
  public static readonly string[] UnaryOperators = ["-", "not"];
  #endregion
}
