using System.Text;

namespace RMS.Models.Common;

public class StringToFormula
{
    private string[] _operators = { "-", "+", "/", "*", "^" };
    private Func<double, double, double>[] _operations = {
    (a1, a2) => a1 - a2,
    (a1, a2) => a1 + a2,
    (a1, a2) => a1 / a2,
    (a1, a2) => a1 * a2,
    (a1, a2) => Math.Pow(a1, a2)
};

    public double Eval(string expression)
    {
        List<string> tokens = getTokens(expression);
        Stack<double> operandStack = new Stack<double>();
        Stack<string> operatorStack = new Stack<string>();
        int tokenIndex = 0;

        while (tokenIndex < tokens.Count)
        {
            string token = tokens[tokenIndex];
            if (token == "(")
            {
                string subExpr = getSubExpression(tokens, ref tokenIndex);
                operandStack.Push(Eval(subExpr));
                continue;
            }
            if (token == ")")
            {
                throw new ArgumentException("Mis-matched parentheses in expression");
            }
            //If this is an operator  
            if (Array.IndexOf(_operators, token) >= 0)
            {
                while (operatorStack.Count > 0 && Array.IndexOf(_operators, token) < Array.IndexOf(_operators, operatorStack.Peek()))
                {
                    string op = operatorStack.Pop();
                    double arg2 = operandStack.Pop();
                    double arg1 = operandStack.Pop();
                    operandStack.Push(_operations[Array.IndexOf(_operators, op)](arg1, arg2));
                }
                operatorStack.Push(token);
            }
            else
            {
                operandStack.Push(double.Parse(token));
            }
            tokenIndex += 1;
        }

        while (operatorStack.Count > 0)
        {
            string op = operatorStack.Pop();
            double arg2 = operandStack.Pop();
            double arg1 = operandStack.Pop();
            operandStack.Push(_operations[Array.IndexOf(_operators, op)](arg1, arg2));
        }
        return operandStack.Pop();
    }

    private string getSubExpression(List<string> tokens, ref int index)
    {
        StringBuilder subExpr = new StringBuilder();
        int parenlevels = 1;
        index += 1;
        while (index < tokens.Count && parenlevels > 0)
        {
            string token = tokens[index];
            if (tokens[index] == "(")
            {
                parenlevels += 1;
            }

            if (tokens[index] == ")")
            {
                parenlevels -= 1;
            }

            if (parenlevels > 0)
            {
                subExpr.Append(token);
            }

            index += 1;
        }

        if ((parenlevels > 0))
        {
            throw new ArgumentException("Mis-matched parentheses in expression");
        }
        return subExpr.ToString();
    }

    private List<string> getTokens(string expression)
    {
        string operators = "()^*/+-";
        List<string> tokens = new List<string>();
        StringBuilder sb = new StringBuilder();

        foreach (char c in expression.Replace(" ", string.Empty))
        {
            if (operators.IndexOf(c) >= 0)
            {
                if ((sb.Length > 0))
                {
                    tokens.Add(sb.ToString());
                    sb.Length = 0;
                }
                tokens.Add(c.ToString());
            }
            else
            {
                sb.Append(c);
            }
        }

        if ((sb.Length > 0))
        {
            tokens.Add(sb.ToString());
        }
        return tokens;
    }


    public bool RelationalExpression(string strExpression)
    {
        int OperatorCount = 0;
        int OperationCount = 0;
        int intLen = strExpression.Length;
        float a;
        List<string> stackOperation = new List<string>();
        List<string> stackOperator = new List<string>();
        stackOperation.Add("");
        stackOperator.Add("");
        for (int i = 0; i < intLen; i++)
        {
            bool isNumber = float.TryParse(strExpression[i].ToString(), out a);
            if (isNumber || strExpression[i].ToString() == ".")
            {
                stackOperation[OperationCount] += strExpression[i].ToString();
                if ((i + 1) < intLen)
                {
                    bool isNumber2 = float.TryParse(strExpression[i + 1].ToString(), out a);
                    if (!isNumber2)
                    {
                        if (stackOperator[OperatorCount] != "")
                        {
                            OperatorCount++;
                            stackOperator.Add("");
                        }
                    }
                }
            }
            else
            {
                stackOperator[OperatorCount] += strExpression[i].ToString();
                if ((i + 1) < intLen)
                {
                    bool isNumber2 = float.TryParse(strExpression[i + 1].ToString(), out a);
                    if (isNumber2)
                    {
                        if (stackOperation[OperationCount] != "")
                        {
                            OperationCount++;
                            stackOperation.Add("");
                        }
                    }
                }
            }
        }

        bool Check = true;
        for (int i = 0; i < stackOperator.Count; i++)
        {
            if (stackOperator[i] != "")
            {
                float op1, op2;
                op1 = float.Parse(stackOperation[i].ToString());
                op2 = float.Parse(stackOperation[i + 1].ToString());
                switch (stackOperator[i])
                {
                    case "<":
                        if (op1 < op2)
                            Check = Check && true;
                        else
                        {
                            Check = false;
                            break;
                        }
                        break;
                    case ">":
                        if (op1 > op2)
                            Check = Check && true;
                        else
                        {
                            Check = false;
                            break;
                        }
                        break;
                    case "<=":
                        if (op1 <= op2)
                            Check = Check && true;
                        else
                        {
                            Check = false;
                            break;
                        }
                        break;
                    case ">=":
                        if (op1 >= op2)
                            Check = Check && true;
                        else
                        {
                            Check = false;
                            break;
                        }
                        break;
                    case "!=":
                        if (op1 != op2)
                            Check = Check && true;
                        else
                        {
                            Check = false;
                            break;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        return Check;
    }

    public bool RelationalExpression2(string strExpression)
    {
        // تعریف عملگرهای پشتیبانی‌شده به ترتیب طول (برای جلوگیری از اشتباه در تشخیص '<' به‌جای '<=')
        string[] comparisonOperators = { "<=", ">=", "!=", "==", "<", ">" };

        List<string> parts = new List<string>();
        List<string> operators = new List<string>();

        int index = 0;
        while (index < strExpression.Length)
        {
            bool matched = false;

            // جستجوی عملگرهای مقایسه در موقعیت فعلی
            foreach (var op in comparisonOperators)
            {
                if (strExpression.Substring(index).StartsWith(op))
                {
                    operators.Add(op);
                    index += op.Length;
                    matched = true;
                    break;
                }
            }

            if (!matched)
            {
                StringBuilder expr = new StringBuilder();
                while (index < strExpression.Length)
                {
                    bool isOp = false;
                    foreach (var op in comparisonOperators)
                    {
                        if (strExpression.Substring(index).StartsWith(op))
                        {
                            isOp = true;
                            break;
                        }
                    }
                    if (isOp)
                        break;

                    expr.Append(strExpression[index]);
                    index++;
                }
                parts.Add(expr.ToString());
            }
        }

        // بررسی تعداد بخش‌ها (مثلاً 20 <= 15.3 < 50 ⇒ 3 بخش و 2 عملگر)
        if (parts.Count != operators.Count + 1)
            throw new ArgumentException("Invalid relational expression.");

        var eval = new StringToFormula();

        // مقایسه هر دو بخش مجاور با عملگر مربوطه
        for (int i = 0; i < operators.Count; i++)
        {
            double left = eval.Eval(parts[i]);
            double right = eval.Eval(parts[i + 1]);
            string op = operators[i];

            bool result = op switch
            {
                "<" => left < right,
                ">" => left > right,
                "<=" => left <= right,
                ">=" => left >= right,
                "!=" => left != right,
                "==" => left == right,
                _ => throw new InvalidOperationException("Unsupported operator.")
            };

            if (!result)
                return false; // اگر یکی از شروط برقرار نباشد، خروجی false
        }

        return true; // همه شروط برقرار بودند
    }

}




