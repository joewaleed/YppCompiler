using System.Windows;

namespace Ypp_Compiler {
    internal class yppParser {
        private List<Token> tokens;
        private int index = 0;
        private Token currentToken;

        public yppParser(List<Token> tokens) {
            this.tokens = tokens;
            if (tokens.Count > 0) currentToken = tokens[index];
        }
        public void ParseStart() {
            ParseStatements();
        }

        //Throws YPP001 if the expected token is not found
        private void Match(string expected) {
            if (index < tokens.Count && (currentToken.Value == expected || currentToken.Type == expected)) {
                index++;
                if (index < tokens.Count) currentToken = tokens[index];
            } else {
                throw new Exception($"Syntax Error YPP001 : Expected '{expected}' but found '{currentToken.Value}'");
            }
        }

        private void ParseStatements() {
            bool isBlock = ParseStatement();
            if (!isBlock) Match(";");
            if (index < tokens.Count && currentToken.Value != "}") ParseStatements();
        }


        //Throws YPP002 if the token does not match any valid statement type
        private bool ParseStatement() {
            if (index >= tokens.Count) return false;

            if (currentToken.Value == "int" || currentToken.Value == "text") {
                ParseDeclaration(); return false;
            } else if (currentToken.Type == "Identifier") {
                ParseAssignment(); return false;
            } else if (currentToken.Value == "incase") {
                ParseCheck(); return true;
            } else if (currentToken.Value == "repeat") {
                ParseRepeat(); return true;
            } else if (currentToken.Value == "{") {
                ParseBlock(); return true;
            } else throw new Exception($"Syntax Error YPP002 : Unexpected token '{currentToken.Value}'");
        }

        private void ParseDeclaration() {
            if (currentToken.Value == "int") Match("int");
            else Match("text");
            ParseAssignment();
        }

        //Throws YPP003 if the left-hand side of the assignment is not an identifier
        private void ParseAssignment() {
            if (currentToken.Type != "Identifier") {
                throw new Exception($" Syntax Error YPP003 : Cannot assign value to a {currentToken.Type}. It must be an Identifier.");
            }
            Match("Identifier");
            Match("=");
            ParseExpression();
        }
        private void ParseCheck() {
            Match("incase");
            Match("(");
            ParseCondition();
            Match(")");
            ParseBlock();
            if (index < tokens.Count && currentToken.Value == "else") {
                Match("else");
                ParseBlock();
            }
        }
        private void ParseRepeat() {
            Match("repeat");
            ParseBlock();
            Match("until");
            Match("(");
            ParseCondition();
            Match(")");
        }

        private void ParseBlock() {
            if (currentToken.Value == "{") {
                Match("{");
                ParseStatements();
                Match("}");
            }
            else ParseStatement();
        }

        //Throws YPP004 if the condition does not contain a valid relational operator
        private void ParseCondition() {
                    ParseExpression();
                    if (currentToken.Value == "<" || currentToken.Value == ">" || currentToken.Value == "==" || currentToken.Value == "!=") Match(currentToken.Value);
                    else throw new Exception($"Syntax Error YPP004 : Expected Relational Operator(<, >, ==, !=) but found '{currentToken.Value}'");
                    ParseExpression();
        }

        private void ParseExpression() {
        ParseTerm();
         while (index < tokens.Count && (currentToken.Value == "+" || currentToken.Value == "-")) {
                Match(currentToken.Value);
                ParseTerm();
            }
        }

        private void ParseTerm() {
            ParseFactor();
            while (index < tokens.Count && (currentToken.Value == "*" || currentToken.Value == "/")) {
                Match(currentToken.Value);
                ParseFactor();
            }
        }

        //Throws YPP005 if the factor is not an identifier, number, or parenthesized expression
        private void ParseFactor() { 

        if (currentToken.Type == "Identifier") Match("Identifier");
        else if (currentToken.Type == "String") Match("String");
            else if (currentToken.Type == "Number") Match("Number");
        else if (currentToken.Value == "(") { 
                Match("("); 
                ParseExpression();
                Match(")");
            }
        else throw new Exception($"Syntax Error YPP005 :Expected Identifier or Number but found {currentToken.Value}");
        
        }
       }
}