INCLUDE ..\globals.ink

// Chef: State 3 : ON
Setzen!
->main

=== main ===
Was für Themen gibt es?
*[Kundenbetreuung] 
    Nach wie vor, eure Aufgabe!
    -> main
*[Gehaltsfragen] 
    ->ant0
*[Urlaub buchen] 
    ->ant1
    
=== ant0 ===
Was fordern Sie?
*[Die Erhöhung meines Überstundensatzes]
    -> ueberstunden
*[Anhebung meines Stundenlohns]
    -> stundenlohn
    
=== ueberstunden ===
Der Überstundensatz gilt für jeden und ist mehr als ausreichend! Abgelehnt!
    *[So respektlos kann nur jemand reden, der nie Überstunden macht!]
        Respektlos? Ich bin der Chef! Passen Sie auf, was Sie sagen! #rage:-1
        ->main
    *[Ich versichere Ihnen einen deutlichen Umsatzwachstum!]
        Das sind große Worte! Wenn Sie nicht nur leer quatschen gewehre ich! #rage:-2
        ->main
        
=== stundenlohn ===
Machen Sie Witze!? Das verbiete ich mir! Lieber kaufe ich mir einen Anzug, als das zu tun!
    *[Sie würden anders denken, wenn Sie an meiner Stelle wären!] #rage:1
        Das interessiert mich nicht! Ich entscheide hier!
        -> main
    
    *[Es geht nicht nur um Geld, sondern um Respekt.] #rage:2
        Respekt verdienen Sie sich durch Gehorsam und Arbeit! 
        -> main
    
=== ant1 ===  
Urlaub gibt es hier nicht, ehe der Umsatz nicht stimmt!
    *[Umsatz und Urlaub bedingen einander]
        Sie glauben, Sie kennen das Geschäft besser als ich? 
        ->ant2
    *[Das ist absurd. Jeder Mensch braucht Erholung!] #rage:1
        Absurd? Nein, was absurd ist, ist Ihre Einstellung! 
        -> end
    
=== ant2 ===
Verantwortung und Leistung bedingen den Verdienst von Urlaub!
        *[Machen Sie sich nicht lächerlich, das ist gesetzlich geregelt.] #rage:2
            Sie wagen es, mich auf Gesetze hinzuweisen? Vorsicht! 
            -> end
       *[Wenn Sie die Zahlen sehen, werden Sie anders denken.] #rage:-1
             Zahlen sind nicht alles! Doch vielleicht werde ich es in Erwägung ziehen.
            -> end
           
=== end ===
An die Arbeit!
~SwitchDialogueState("Off")
-> DONE










