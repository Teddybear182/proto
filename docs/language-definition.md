## Language grammar

```
program ::=
  statement*

statement ::=
  break-statement ";"
  continue-statement ";"
  return-statement ";"
  var-definition
  if-statement
  loop-statement
  block-statement
  function-statement
  expression ";"

break-statement ::=
  "break" ";"

continue-statement ::=
  "continue" ";"

return-statement ::=
  "return" expression ";"

var-definition ::=
  "var" identifier ":" type (assign_expr)? ";"
  "const" identifier ":" type assign_expr ";"

if-statement ::=
  "if" expression "then" statement ("else" statement)?

block-statement ::=
  "begin" statement* "end"

loop-statement ::=
  "while" expression statement

function-statement ::=
  "task" indetifier ":" type "(" ((identifier,)*)? ")" statement

expression ::=
  assign_expr

assign_expr ::=
  or_expr ":=" or_expr

or_expr ::=
  and_expr "or" and_expr

and_expr ::=
  comp_expr1 "and" comp_expr1

comp_expr1 ::=
  comp_expr2 "===" comp_expr2
  comp_expr2 "!==" comp_expr2

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
  "!" primary_expr

primary_expr ::=
  "(" expression ")"
  identifier
  identifier "(" ((expression,)*)? ")"
  integer
  float
  decimal
  str
  char
  boolean
  null

```
