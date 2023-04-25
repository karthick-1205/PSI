// ⓅⓈⒾ  ●  Pascal Language System  ●  Academy'23
// PSIPrint.cs ~ Prints a PSI syntax tree in Pascal format
// ─────────────────────────────────────────────────────────────────────────────
namespace PSI;

public class PSIPrint : Visitor<StringBuilder> {
   public override StringBuilder Visit (NProgram p) {
      Write ($"program {p.Name}; ");
      Visit (p.Block);
      return Write (".");
   }

   public override StringBuilder Visit (NBlock b) 
      => Visit (b.Decls, b.Body);

   public override StringBuilder Visit (NDeclarations d) {
      if (d.Vars.Length > 0) {
         NWrite ("var"); N++;
         foreach (var g in d.Vars.GroupBy (a => a.Type))
            NWrite ($"{g.Select (a => a.Name).ToCSV ()} : {g.Key};");
         N--;
      }
      return S;
   }

   public override StringBuilder Visit (NVarDecl d)
      => NWrite ($"{d.Name} : {d.Type}");

   public override StringBuilder Visit (NCompoundStmt b) {
      NWrite ("begin"); N++;  Visit (b.Stmts); N--; return NWrite ("end"); 
   }

   public override StringBuilder Visit (NAssignStmt a) {
      NWrite ($"{a.Name} := "); a.Expr.Accept (this); return Write (";");
   }

   public override StringBuilder Visit (NWriteStmt w) {
      NWrite (w.NewLine ? "WriteLn (" : "Write (");
      for (int i = 0; i < w.Exprs.Length; i++) {
         if (i > 0) Write (", ");
         w.Exprs[i].Accept (this);
      }
      return Write (");");
   }

   public override StringBuilder Visit (NLiteral t)
      => Write (t.Value.ToString ());

   public override StringBuilder Visit (NIdentifier d)
      => Write (d.Name.Text);

   public override StringBuilder Visit (NUnary u) {
      Write (u.Op.Text); return u.Expr.Accept (this);
   }

   public override StringBuilder Visit (NBinary b) {
      Write ("("); b.Left.Accept (this); Write ($" {b.Op.Text} ");
      b.Right.Accept (this); return Write (")");
   }

   public override StringBuilder Visit (NFnCall f) {
      Write ($"{f.Name} (");
      for (int i = 0; i < f.Params.Length; i++) {
         if (i > 0) Write (", "); f.Params[i].Accept (this);
      }
      return Write (")");
   }

   StringBuilder Visit (params Node[] nodes) {
      nodes.ForEach (a => a.Accept (this));
      return S;
   }

   // Writes in a new line
   StringBuilder NWrite (string txt) 
      => Write ($"\n{new string (' ', N * 3)}{txt}");
   int N;   // Indent level

   // Continue writing on the same line
   StringBuilder Write (string txt) {
      Console.Write (txt);
      S.Append (txt);
      return S;
   }
   public override StringBuilder Visit (NFnDecl d) {
      throw new NotImplementedException ();
   }

   public override StringBuilder Visit (NProcFnDecl d) {
      throw new NotImplementedException ();
   }

   public override StringBuilder Visit (NReadStmt r) {
      NWrite ("read (");
      for (int i = 0; i < r.List.Length; i++) {
         if (i > 0) Write (", ");
         Write (r.List[i].Text);
      }
      return Write (");");
   }

   public override StringBuilder Visit (NCallStmt r) {
      throw new NotImplementedException ();
   }

   public override StringBuilder Visit (NWhileStmt r) {
      NWrite ("while"); N++;
      r.Expr.Accept (this);
      Write(" do"); 
      return Visit (r.Stmts);
   }

   public override StringBuilder Visit (NIfStmt f) {
      NWrite ("if"); 
      f.Expr.Accept (this);
      Write (" then");
      N++;
      if(f.Stmts.Length>=1) {
         Visit (f.Stmts[0]);
         N--;
      }
      if(f.Stmts.Length==2) { 
         NWrite ("else"); N++; 
         Visit (f.Stmts[1]); 
         N--;
      }
      return S;

   }

   public override StringBuilder Visit (NRepeatStmt r) {
      NWrite ("repeat");
      for (int i = 0; i < r.Stmts.Length; i++) {
         if (i > 0) Write (" "); N++;
         r.Stmts[i].Accept (this);
         N--;
      }
      NWrite("until");
      Visit(r.Expr);
      return Write(";");

   }

   public override StringBuilder Visit (NForStmt r) {
      NWrite ("for ");
      Write (r.Name.Text);
      Write(" := ");
      Visit (r.Expr);
      Write (r.a? " to " : " downto ");
      Visit (r.Expr1);
      Write (" do"); N++;
      Visit (r.Stmts); N--;
      return S;

   }

   readonly StringBuilder S = new ();
}