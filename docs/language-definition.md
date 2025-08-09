## Language grammar

```
program ::=
  var-definition*
  function-statement*

statement ::=
  "break" ";"
  "continue" ";"
  "return" ";"
  var-definition
  if-statement
  loop-statement
  block-statement
  expression ";"

function-statement ::=
  "task" indetifier ":" type "(" ((type identifier,)*)? ")" statement

var-definition ::=
  "var" identifier ":" type (assign_expr)? ";"
  "const" identifier ":" type assign_expr ";"

if-statement ::=
  "if" expression "then" statement ("else" statement)?

block-statement ::=
  "begin" statement* "end"

loop-statement ::=
  "while" expression statement
  
 
expression ::=
  assign_expr

assign_expr ::=
  or_expr ":=" or_expr

or_expr ::=
  and_expr "or" and_expr

and_expr ::=
  comp_expr1 "and" comp_expr1

comp_expr1 ::=
  comp_expr2 "=" comp_expr2
  comp_expr2 "!=" comp_expr2

comp_expr2 ::=
  add_expr "<" add_expr
  add_expr "<=" add_expr
  add_expr ">" add_expr
  add_expr ">=" add_expr

add_expr ::=
  mult_expr "+" mult_expr
  mult_expr "-" mult_expr

mult_expr ::=
  unary_expr "*" unary_expr
  unary_expr "/" unary_expr
  unary_expr "%" unary_expr

unary_expr ::=
  "-" primary_expr
  "not" primary_expr

primary_expr ::=
  "(" expression ")"
  identifier
  identifier "(" ((expression,)*)? ")"
  numeric_literal
  string_literal
  true
  false

type ::=
    integer
    float
    decimal
    str
    char
    boolean
    null
```
