program Comp13;
var
  name: String;
  initial: char;
  age: integer;
  percent: real;
  y: boolean;
begin
   writeln ("Enter the Name: ");
   readln (name);
   writeln ("Enter the Initial: ");
   readln (initial);
   writeln ("Enter the Age: ");
   readln (age);
   writeln ("Enter the percentage in 10th: ");
   readln (percent);
   writeln ("Is it 100 > 56 say true or false: ");
   readln (y);
   writeln ("The Name is: ",name);
   writeln ("The Initial is: ",initial);
   writeln ("The age is: ",age);
   writeln ("The Percentage in 10th is: ",percent);
   writeln (y);
end.