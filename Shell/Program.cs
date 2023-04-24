﻿using PSI;

static class Start {
   static void Main () {
      NProgram? node;
      try {
         var text = File.ReadAllText (@"D:\Academy23\PSI\Shell\Demo\Hello.pas");//P:/Shell/Demo/Complex.pas
         node = new Parser (new Tokenizer (text)).Parse ();
         node.Accept (new PSIPrint ());
      } catch (ParseException pe) {
         Console.WriteLine ();
         pe.Print ();
      } catch (Exception e) {
         Console.WriteLine ();
         Console.WriteLine (e);
      }
   }
}